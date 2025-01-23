using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Caveworks
{
    [Serializable]
    public class Chunk
    {
        public const int chunkSize = 32;
        public World World;
        public MyVector2Int GlobalCoords;
        public Tile[,] Tiles;
        public List<BaseCreature> Creatures;


        public Chunk(World world, MyVector2Int coordinates)
        {
            World = world;
            GlobalCoords = coordinates;
            Tiles = new Tile[chunkSize, chunkSize];
            Creatures = new List<BaseCreature>();

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    Tiles[x, y] = new Tile(this, new MyVector2Int(GlobalCoords.X * chunkSize + x, GlobalCoords.Y * chunkSize + y));
                }
            }
        }


        public void Update(float deltaTime)
        {
            foreach (BaseCreature creature in Creatures.ToList()) 
            {
                creature.Update(deltaTime);
            }
        }


        public void Draw(Camera camera)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(camera);
            }

            foreach (BaseCreature creature in Creatures)
            {
                creature.Draw(camera);
            }
        }
    }
}
