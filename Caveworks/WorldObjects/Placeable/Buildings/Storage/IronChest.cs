﻿using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    class IronChest : BaseBuilding
    {
        public IronChest(Tile tile) : base(tile, 1)
        {
            Inventory = new Inventory(10, Globals.World.Player);
        }


        public override bool AccteptsItems(BaseBuilding building)
        {
            return true;
        }


        public override bool HasCollision() { return true; }


        public override bool HasUI() { return true; }


        public override void OpenUI()
        {
            Inventory.OpenUI(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2));
        }


        public override void UpdateUI()
        {
            Inventory.UpdateUI();
        }


        public override void DrawUI()
        {
            Inventory.DrawUI();
        }

        public override void CloseUI()
        {
            Inventory.CloseUI();
        }


        public override BaseItem ToItem()
        {
            return new IronChestItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(Textures.IronChest, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
