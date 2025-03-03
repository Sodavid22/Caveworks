using System;

namespace Caveworks
{
    [Serializable]
    public class BaseWall
    {
        public virtual void Draw(Tile tile, Camera camera) { }

        public virtual int GetHardness() { return 0; }

        public virtual bool IsDestructible() { return false; }
    }
}
