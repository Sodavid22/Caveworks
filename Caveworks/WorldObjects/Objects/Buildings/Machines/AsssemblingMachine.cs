using Caveworks.WorldObjects;
using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class AsssemblingMachine : BaseMachine
    {
        public AsssemblingMachine(Tile tile):base(tile, 3, 5)
        {

        }


        public override int GetLightLevel() { return LightManager.MinLightForMaxBrightness; }


        public override bool HasCollision() { return true; }


        public override int GetSize() { return 3; }


        public override bool HasUI() { return true; }


        public override void Update(float deltaTime)
        {
            Crafter.Update(deltaTime);
        }


        public override void OpenUI()
        {
            Inventory.OpenUI(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2));
            Crafter.OpenUI(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2));
            Sounds.ButtonClick.Play(1);
        }


        public override void UpdateUI()
        {
            Inventory.UpdateUI();
            Crafter.UpdateUI();
        }


        public override void DrawUI()
        {
            Inventory.DrawUI();
            Crafter.DrawUI();
        }


        public override void CloseUI()
        {
            Inventory.CloseUI();
            Crafter.CloseUI();
        }


        public override BaseItem ToItem()
        {
            return new AsseblingMachineItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(Textures.AssemblingMachine, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale * 3, camera.Scale * 3), Color.White);
        }
    }
}
