﻿using System;
using System.Linq;

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

            Tile.Items.Add(this);
        }

        public virtual void Draw(Tile tile, Camera camera) { }


        public void Move(MyVector2 movement)
        {
            Coordinates.X += movement.X;
            Coordinates.Y += movement.Y;

            if (CheckForNewTile())
            {
                UpdateTile();
            }
        }


        public bool CheckForNewTile()
        {
            if (Coordinates.X < Tile.Position.X || Coordinates.Y < Tile.Position.Y || Coordinates.X > Tile.Position.X + 1 || Coordinates.Y > Tile.Position.Y + 1)
            {
                return true;
            }
            return false;
        }


        public void UpdateTile()
        {
            Tile.Items.Remove(this);
            Tile = Tile.Chunk.World.GlobalCordsToTile(Coordinates.ToMyVector2Int());
            Tile.Items.Add(this);
            Tile.Items = Tile.Items.OrderBy(item => item.Coordinates.X + item.Coordinates.Y).ToList();
        }
    }
}