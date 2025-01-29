using System;
using System.Collections.Generic;
using System.Linq;

namespace Caveworks
{
    [Serializable]
    public class Chunk
    {
        public const int chunkSize = 32;
        public World World;
        public MyVector2Int Position;
        public Tile[,] TileList;
        public List<BaseCreature> Creatures;


        public Chunk(World world, MyVector2Int position)
        {
            World = world;
            Position = position;
            TileList = new Tile[chunkSize, chunkSize];
            Creatures = new List<BaseCreature>();

            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    TileList[x, y] = new Tile(this, new MyVector2Int(Position.X * chunkSize + x, Position.Y * chunkSize + y));
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
            foreach (Tile tile in TileList)
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
