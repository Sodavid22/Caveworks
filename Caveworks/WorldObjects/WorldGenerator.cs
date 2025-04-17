using SharpDX;
using System;
using System.Collections.Generic;
namespace Caveworks
{
    public static class WorldGenerator // 0 = empty, 1 = wall, 1 < special tiles
    {
        public static int[,] CaveMap;

        /*
        0 = nothing
        1 = wall
        10 = iron ore
        11 = copper ore
        12 = coal
        */

        public static Chunk[,] GenerateWorld(World world, int worldSize, int worldDiameter)
        {
            CaveMap = GenerateRandomNumberMap(worldDiameter, 0.5f);

            // makes spawn empty
            for (int x = worldDiameter/2 - 1; x < worldDiameter/2 + 2; x++)
            {
                for (int y = worldDiameter/2 - 1; y < worldDiameter/2 + 2; y++)
                {
                    CaveMap[x, y] = -8;
                }
            }

            AddOreVein(CaveMap, worldDiameter, 8, 5, 10);
            AddOreVein(CaveMap, worldDiameter, 16, 5, 12);
            AddOreVein(CaveMap, worldDiameter, 24, 5, 11);

            for (int i = 0; i < 8; i++)
            {
                CaveMap = Smoothen(CaveMap, worldDiameter);
            }

            Chunk[,] ChunkList = ConvertToTiles(world, CaveMap, worldSize);

            return ChunkList;
        }

        private static int[,] GenerateRandomNumberMap(int diameter, float fill)
        {
            int[,] numberMap = new int[diameter,diameter];
            Random random = new Random();

            for (int x = 0; x < diameter; x++)
            {
                for (int y = 0; y < diameter; y++)
                {
                    if (random.NextFloat(0f,1f) < fill)
                    {
                        numberMap[x, y] = 1;
                    }
                    else
                    {
                        numberMap[x, y] = 0;
                    }
                }
            }
            return numberMap;
        }

        private static int CountNeigbours(int[,] map, int mapDiameter, int tileX, int tileY)
        {
            int neighbours = 0;

            for (int x = tileX - 1; x <= tileX + 1; x++)
            {
                for (int y = tileY -1; y <= tileY + 1; y++)
                {
                    if (!(x == tileX && y == tileY))
                    {
                        if (x >= 0 && y >= 0 && x < mapDiameter && y < mapDiameter)
                        {
                            if (map[x, y] > 0) // if wall add neighbour
                            {
                                neighbours += 1;
                            }
                        }
                        else // if map border
                        {
                            neighbours += 8;
                        }
                    }
                }
            }

            return neighbours;
        }


        private static int[,] Smoothen(int[,] map,int mapDiameter)
        {
            int split = 4;
            int[,] newMap = new int[mapDiameter, mapDiameter];
            int neighbours;

            for (int x = 0; x < mapDiameter; x++)
            {
                for (int y = 0; y < mapDiameter; y++)
                {
                    neighbours = CountNeigbours(map, mapDiameter, x, y);
                    if (neighbours > split) // more walls
                    {
                        if (map[x, y] == 0) // empty tile to wall
                        {
                            newMap[x, y] = 1;
                        }
                        else // do nothing otherwise
                        {
                            newMap[x, y] = map[x, y];
                        }
                    }
                    else if (neighbours < split) // if more empty tiles make empty
                    {
                        newMap[x, y] = 0;
                    }
                    else // if equal neighbours do nothing
                    {
                        newMap[x, y] = map[x, y];
                    }
                }
            }
            return newMap;
        }

        public static Chunk[,] ConvertToTiles(World world, int[,] map, int worldSize)
        {
            Tile tile;
            int tileValue;
            Chunk chunk;
            Chunk[,] ChunkList = new Chunk[worldSize, worldSize];

            for (int chunk_x = 0; chunk_x < worldSize; chunk_x++)
            {
                for (int chunk_y = 0; chunk_y < worldSize; chunk_y++)
                {
                    chunk = new Chunk(world, new MyVector2Int(chunk_x, chunk_y));
                    ChunkList[chunk_x, chunk_y] = chunk;

                    for (int tile_x = 0; tile_x < Chunk.chunkSize; tile_x++)
                    {
                        for (int tile_y = 0; tile_y < Chunk.chunkSize; tile_y++)
                        {
                            tile = chunk.TileList[tile_x, tile_y];
                            new StoneFloor(tile);
                            tileValue = map[chunk_x * Chunk.chunkSize + tile_x, chunk_y * Chunk.chunkSize + tile_y];

                            if (chunk_x * Chunk.chunkSize + tile_x < 3 || chunk_y * Chunk.chunkSize + tile_y < 3 || chunk_x * Chunk.chunkSize + tile_x > worldSize * Chunk.chunkSize - 4 || chunk_y * Chunk.chunkSize + tile_y > worldSize * Chunk.chunkSize - 4)
                            {
                                new StoneWallPermanent(tile);
                            }
                            else
                            {
                                if (tileValue == 1) // walls
                                {
                                    if (MathF.Pow(world.WorldDiameter / 2 - tile.Position.X, 2) + MathF.Pow(world.WorldDiameter / 2 - tile.Position.Y, 2) > MathF.Pow(128, 2))
                                    {
                                        new StoneWallStrongest(tile);
                                    }
                                    else if (MathF.Pow(world.WorldDiameter / 2 - tile.Position.X, 2) + MathF.Pow(world.WorldDiameter / 2 - tile.Position.Y, 2) > MathF.Pow(64, 2))
                                    {
                                        new StoneWallStronger(tile);
                                    }
                                    else
                                    {
                                        new StoneWall(tile);
                                    }
                                }
                                // ores
                                else if (tileValue == 10)
                                {
                                    new RawIronOreWall(tile);
                                }
                                else if (tileValue == 11)
                                {
                                    new RawCopperOreWall(tile);
                                }
                                else if (tileValue == 12)
                                {
                                    new RawCoalWall(tile);
                                }
                            }
                        }
                    }
                }
            }

            return ChunkList;
        }


        public static void AddOreVein(int[,] map, int mapDiameter, int centerDistance, int size, int wallType) //TODO
        {
            Random random = new Random();
            int veinX = random.Next(centerDistance - 1);
            int veinY = (int)Math.Sqrt((centerDistance * centerDistance) - (veinX * veinX));

            if (random.Next(100) > 50)
            {
                veinX = -veinX;
            }
            if (random.Next(100) > 50)
            {
                veinY = -veinY;
            }
            veinX = mapDiameter / 2 + veinX;
            veinY = mapDiameter / 2 + veinY;

            for (int i=0; i<5; i++)
            {
                int offsetX = random.Next(-size / 2, size / 2);
                int offsetY = random.Next(-size / 2, size / 2);

                for (int x = -size / 2; x <= size / 2; x++)
                {
                    for (int y = -size / 2; y <= size / 2; y++)
                    {
                        if (x * x + y * y < size * size / 4)
                        {
                            map[veinX + x + offsetX, veinY + y + offsetY] = wallType;
                        }
                    }
                }
            }
        }
    }
}