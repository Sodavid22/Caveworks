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
        public MyVector2Int Index;
        public Tile[,] TileList;
        public List<BaseBuilding> Buildings;


        public Chunk(World world, MyVector2Int index)
        {
            World = world;
            Index = index;
            TileList = new Tile[chunkSize, chunkSize];
            Buildings = new List<BaseBuilding>();


            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    TileList[x, y] = new Tile(this, new MyVector2Int(this.Index.X * chunkSize + x, this.Index.Y * chunkSize + y));
                }
            }
        }


        public void Update(float deltaTime)
        {
            foreach (BaseBuilding building in Buildings)
            {
                building.Update(World.DeltaTime);
            }
        }


        public void Draw(Camera camera)
        {
            foreach (Tile tile in TileList)
            {
                tile.Draw(camera);
            }

            foreach (BaseBuilding building in Buildings)
            {
                building.Draw(camera, World.DeltaTime);
            }
        }
    }
}
