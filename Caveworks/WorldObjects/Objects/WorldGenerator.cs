using SharpDX;
using System;
namespace Caveworks
{
    public static class WorldGenerator
    {
        public static int[,] CaveMap;

        public static Chunk[,] GenerateWorld(World world, int worldSize, int worldDiameter)
        {
            CaveMap = GenerateRandomNumberMap(worldDiameter, 0.48f);

            // makes spawn empty
            for (int x = worldDiameter/2 - 1; x < worldDiameter/2 + 2; x++)
            {
                for (int y = worldDiameter/2 - 1; y < worldDiameter/2 + 2; y++)
                {
                    CaveMap[x, y] = -8;
                }
            }

            // outer walls
            for (int x = 0; x < worldDiameter; x++)
            {
                for (int y = 0; y < worldDiameter; y++)
                {
                    if (x == 0 || x == worldDiameter - 1 || y == 0 || y == worldDiameter - 1)
                    {
                        CaveMap[x, y] = 8;
                    }
                }
            }


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
                            neighbours += map[x, y];
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
                        newMap[x, y] = 1;
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

                            if (map[chunk_x * Chunk.chunkSize + tile_x, chunk_y * Chunk.chunkSize + tile_y] == 1)
                            {
                                new StoneWall(tile);
                            }
                        }
                    }
                }
            }

            return ChunkList;
        }
    }
}