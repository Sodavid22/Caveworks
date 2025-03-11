using System;

namespace Caveworks
{
    [Serializable]
    public class BaseWall
    {
        public virtual bool IsDestructible() { return false; }

        public virtual int GetHardness() { return 0; }

        public virtual void WhenMined(Player player, Tile wallTile) { }

        public virtual void Draw(Tile tile, Camera camera) { }

    }
}
