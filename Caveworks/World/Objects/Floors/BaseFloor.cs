using System;

namespace Caveworks
{
    [Serializable]
    public class BaseFloor
    {
        public Tile Tile;

        public BaseFloor(Tile tile) 
        {
            this.Tile = tile;
        }

        public virtual void Draw(Camera camera) { }
    }
}
