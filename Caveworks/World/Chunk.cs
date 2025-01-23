using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Caveworks
{
    [Serializable]
    public class Chunk
    {
        public const int chunkSize = 32;
        public World World;
        public MyVector2 Coordinates;
        public Tile[,] Tiles;


        public Chunk(World world, MyVector2 coordinates)
        {
            World = world;
            Coordinates = coordinates;
            Tiles = new Tile[chunkSize, chunkSize];

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    Tiles[x, y] = new Tile(this, new MyVector2(x, y));
                }
            }
        }

        public void Update()
        {

        }


        public void Draw(Camera camera)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(camera);
            }
        }
    }
}
