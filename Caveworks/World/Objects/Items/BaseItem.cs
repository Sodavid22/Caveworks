using System;

namespace Caveworks
{
    [Serializable]
    public class BaseItem
    {
        public Tile Tile;
        public MyVector2 Coordinates;
        public int count;
        public int StackSize;

        public BaseItem(Tile tile, MyVector2 position, int count)
        {
            this.Tile = tile;
            this.Coordinates = new MyVector2(tile.Position.X + position.X, tile.Position.Y + position.Y);
            this.count = count;
        }

        public virtual void Draw(Tile tile, Camera camera) { }
    }
}