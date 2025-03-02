using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Caveworks
{
    [Serializable]
    public class BaseItem
    {
        public Tile Tile;
        public MyVector2 Coordinates;
        public int Count;
        public const int StackSize = 50;
        static float DropCooldown = 0;

        public BaseItem(Tile tile, MyVector2 position, int count)
        {
            this.Tile = tile;
            this.Coordinates = new MyVector2(tile.Position.X + position.X, tile.Position.Y + position.Y);
            this.Count = count;

            Tile.Items.Add(this);
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


        public virtual void Draw(Tile tile, Camera camera) { }


        public virtual bool PrimaryUse(MyVector2Int itemRotation) { return false; }


        public virtual bool SecondaryUse(MyVector2Int itemRotation) { return false; }


        public static void Drop(BaseItem item)
        {
            Tile tile = Globals.World.MouseTile;
            BaseItem newItem = Cloning.DeepClone(item);
            newItem.Count = 1;
            newItem.Tile = tile;
            newItem.Coordinates = Globals.World.WorldMousePos;
            item.Count -= 1;
            if (item.Count == 0)
            {
                Globals.World.PlayerHand = null;
            }
            tile.Items.Add(newItem);
        }


        public void DrawSimple(Tile tile, Camera camera, Texture2D texture)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(new MyVector2(Coordinates.X - 0.25f, Coordinates.Y - 0.25f));
            Game.ItemSpritebatch.Draw(texture, new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale / 2, camera.Scale / 2), Color.White);
        }

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