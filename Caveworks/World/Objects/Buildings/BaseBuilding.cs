using System;

namespace Caveworks
{
    [Serializable]
    public class BaseBuilding
    {

        public Tile Tile;
        public MyVector2Int Position;
        public MyVector2Int Rotation;
        public static bool Collisions;


        public BaseBuilding(Tile tile) 
        {
            this.Tile = tile;
            this.Position = Tile.Position;
        }


        public virtual void Update(float deltaTime) { }


        public virtual void Draw(Camera camera, float deltaTime) { }
    }
}
