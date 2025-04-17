using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Caveworks
{
    [Serializable]
    class ResearchLab : BaseBuilding
    {
        float ResearchCooldown = 2;
        float ResearchTimer = 0;


        public ResearchLab(Tile tile) : base(tile, 3)
        {
            Inventory = new Inventory(1, Globals.World.Player);
        }


        public override bool AccteptsItems(BaseBuilding building)
        {
            return true;
        }


        public override int GetLightLevel() { return LightManager.MinLightForMaxBrightness; }


        public override int GetSize() { return 3; }


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
            return new ResearchLabItem(1);
        }


        public override void Update(float deltaTime)
        {
            if (Inventory.Items[0] != null && Globals.World.Research.RemainingItems.Count > 0)
            {
                ResearchTimer += deltaTime;

                if (ResearchTimer > ResearchCooldown)
                {
                    foreach (BaseItem item in Globals.World.Research.RemainingItems)
                    {
                        if (item.GetType() == Inventory.Items[0].GetType())
                        {
                            if (item.Count > 0)
                            {
                                ResearchTimer = 0;
                                item.Count -= 1;
                                Inventory.Items[0].Count -= 1;
                                if (Inventory.Items[0].Count == 0)
                                {
                                    Inventory.Items[0] = null;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(Textures.ResearchLab, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale * 3, camera.Scale * 3), Color.White);
        }
    }
}
