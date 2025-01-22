using System;

namespace Caveworks
{
    [Serializable]
    public class BaseWall
    {
        public Tile Tile { get; set; }

        public BaseWall(Tile tile)
        {
            this.Tile = tile;
        }

        public virtual void Draw(Camera camera) { }
    }
}
