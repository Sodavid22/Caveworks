using SharpDX;
using System;
using System.Diagnostics;
namespace Caveworks
{
    public static class WorldGenerator
    {
        public static int[,] CaveMap;

        enum Walls
        {
            None = 0,
            Stone = 1,
            Iron = 10,
        }

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

            AddOreVein(CaveMap, worldDiameter, 20, 10, 2);

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
                        if (x > 0 && y > 0 && x < mapDiameter && y < mapDiameter)
                        {
                            if (map[x, y] > 0)
                            {
                                neighbours += 1;
                            }
                        }
                        else
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
                    if (neighbours > split)
                    {
                        if (map[x, y] == 0)
                        {
                            newMap[x, y] = 1;
                        }
                        else
                        {
                            newMap[x, y] = map[x, y];
                        }
                    }
                    else if (neighbours < split)
                    {
                        newMap[x, y] = 0;
                    }
                    else
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

                            if (tileValue == 1)
                            {
                                new StoneWall(tile);
                            }
                            else if (tileValue == 2)
                            {
                                new RawIronOreWall(tile);
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

            for (int x = -size/2; x <= size/2; x++)
            {
                for (int y = -size/2; y <= size/2; y++)
                {
                    if (x*x+y*y < size*size/4)
                    map[veinX + x, veinY + y] = wallType;
                }
            }
        }
    }
}