using System;

namespace Caveworks
{
    [Serializable]
    public class BaseFloor
    {
        public virtual void Draw(Tile tile, Camera camera) { }
    }
}
