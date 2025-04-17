using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Diagnostics;
using System.Linq;

namespace Caveworks
{
    [Serializable]
    public class BaseItem
    {
        public MyVector2 Coordinates;
        public int Count;
        public const int StackSize = 100;

        public BaseItem(Tile tile, MyVector2 position, int count)
        {
            this.Coordinates = new MyVector2(tile.Position.X + position.X, tile.Position.Y + position.Y);
            this.Count = count;

            tile.Items.Add(this);
        }


        public BaseItem(int count)
        {
            this.Coordinates = new MyVector2(0, 0);
            this.Count = count;
        }


        public void Copy(BaseItem item)
        {

        }


        public virtual Texture2D GetTexture() { return null; }


        public virtual bool CanUseContinuosly() { return true; }


        public virtual bool CanRotate() { return false; }


        public virtual void Draw(Tile tile, Camera camera) { }


        public virtual bool PrimaryUse(MyVector2Int itemRotation) { return false; }


        public static void Drop(BaseItem item)
        {
            Tile tile = Globals.World.MouseTile;
            BaseItem newItem = Cloning.DeepClone(item);
            item.Count -= 1;
            newItem.Count = 1;
            newItem.Coordinates = Globals.World.WorldMousePos;
            if (item.Count == 0)
            {
                Globals.World.Player.HeldItem = null;
            }
            newItem.AddToTile(tile);
        }


        public void DrawSimple(Tile tile, Camera camera, Texture2D texture)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(new MyVector2(Coordinates.X - 0.25f, Coordinates.Y - 0.25f));
            Game.ItemSpritebatch.Draw(texture, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale / 2, camera.Scale / 2), Color.White);
        }


        public bool CheckForNewTile(Tile tile)
        {
            if (Coordinates.X < tile.Position.X || Coordinates.Y < tile.Position.Y || Coordinates.X > tile.Position.X + 1 || Coordinates.Y > tile.Position.Y + 1)
            {
                return true;
            }
            return false;
        }


        public void UpdateTiles(Tile tile)
        {
            tile.Items.Remove(this);
            tile = Globals.World.GlobalCordsToTile(Coordinates.ToMyVector2Int());
            tile.Items.Add(this);
            tile.Items = tile.Items.OrderBy(item => item.Coordinates.X + item.Coordinates.Y).ToList();
        }


        public void RemoveFromTile(Tile tile)
        {
            tile.Items.Remove(this);
        }


        public void AddToTile(Tile tile)
        {
            tile.Items.Add(this);
            tile.Items = tile.Items.OrderBy(item => item.Coordinates.X + item.Coordinates.Y).ToList();
        }
    }
}