using System;

namespace Caveworks
{
    [Serializable]
    public class BaseWall
    {
        public virtual bool IsMineable() { return true; }

        public virtual bool IsDestructible() { return true; }

        public virtual int GetHardness() { return 0; }

        public virtual int GetDrillTime() { return 0; }

        public virtual BaseItem GetItem(Tile wallTile) { return null; }

        public virtual void Draw(Tile tile, Camera camera) { }

    }
}
