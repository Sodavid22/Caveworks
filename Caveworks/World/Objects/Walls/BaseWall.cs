using System;

namespace Caveworks
{
    [Serializable]
    public class BaseWall
    {
        public virtual void Draw(Tile tile, Camera camera) { }
    }
}
