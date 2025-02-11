using System;

namespace Caveworks
{
    [Serializable]
    public class BaseFloor
    {
        public BaseFloor(Tile tile)
        {
            tile.Floor = this;
        }

        public virtual void Draw(Tile tile, Camera camera) { }
    }
}
