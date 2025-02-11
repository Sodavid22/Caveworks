using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class BaseBuilding
    {

        public Tile Tile;
        public MyVector2Int Position;
        public MyVector2Int Rotation;
        public static int Size;
        public static bool Collisions;


        public BaseBuilding(Tile tile, int size)
        {
            Tile = tile;
            this.Position = tile.Position;


            Tile.Chunk.Buildings.Add(this);
            for (int x = 0; x <= size - 1; x++) 
            {
                for (int y = 0; y <= size - 1; y++)
                {
                    tile.Chunk.World.GlobalCordsToTile(new MyVector2Int(Position.X + x, Position.Y + y)).Building = this;
                }
            }
        }


        public virtual void Update(float deltaTime) { }


        public virtual void Draw(Camera camera, float deltaTime) { }
    }
}
