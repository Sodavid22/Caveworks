﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;

namespace Caveworks
{
    [Serializable]
    class Elevator : BaseBuilding
    {
        float ResearchCooldown = 2;
        float ResearchTimer = 0;


        public Elevator(Tile tile) : base(tile, 3)
        {
            Inventory = new Inventory(1, Globals.World.Player);
        }


        public override bool AccteptsItems(BaseBuilding building)
        {
            if (Inventory.GetFirstItem() != null)
            {
                if (Inventory.GetFirstItem().Count > 7)
                {
                    return false;
                }
            }
            return true;
        }


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
            return new ElevatorItem(1);
        }


        public override void Update(float deltaTime)
        {
            if (Inventory.Items[0] != null)
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
                                item.Count -= 1;
                            }
                            break;
                        }
                    }

                    ResearchTimer = 0;
                    Inventory.Items[0].Count -= 1;
                    if (Inventory.Items[0].Count == 0)
                    {
                        Inventory.Items[0] = null;
                    }
                }
            }
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            Texture2D texture;
            if (Inventory.Items[0] == null)
            {
                texture = Textures.Elevator;
            }
            else
            {
                texture = Textures.Elevator2;
            }
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(texture, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale * 3, camera.Scale * 3), Color.White);
        }
    }
}
