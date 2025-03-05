using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks.WorldObjects.Objects.Buildings.Storage
{
    [Serializable]
    class IronChest : BaseBuilding
    {
        public IronChest(Tile tile) : base(tile, 1)
        {
            Inventory = new Inventory(10, Globals.World.Player);
        }


        public override bool HasCollision() { return true; }


        public override bool HasUI() { return true; }


        public override void OpenUI()
        {
            Inventory.Open(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2 - 50));
            Sounds.ButtonClick.Play(1);
        }


        public override void UpdateUI()
        {
            Inventory.Update();
        }


        public override void DrawUI()
        {
            Inventory.Draw();
        }

        public override void CloseUI()
        {
            Inventory.Close();
        }


        public override BaseItem ToItem()
        {
            return new IronChestItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(new MyVector2(Position.X, Position.Y));
            Game.FloorSpriteBatch.Draw(Textures.IronChest, new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
