using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace Caveworks
{
    [Serializable]
    public class Player
    {
        public World World;
        public Inventory Inventory;
        public bool InventoryOpened;
        public BaseItem HeldItem;
        public MyVector2Int ItemRotation;
        public int WallHits = 0;
        public BaseBuilding OpenedBuilding;


        public Player(World world)
        {
            World = world;
            Inventory = new Inventory(30, this);
            InventoryOpened = false;
            HeldItem = null;
            ItemRotation = new MyVector2Int(1, 0);
        }


        public void CloseUi()
        {
            Inventory.CloseUI();
            InventoryOpened = false;
            if (OpenedBuilding != null)
            {
                OpenedBuilding.CloseUI();
                OpenedBuilding = null;
            }
        }


        public void Update()
        {
            if (World.MouseTile != World.LastMouseTile) // reset wall mining
            {
                WallHits = 0;
            }

            if (MyKeyboard.IsPressed(KeyBindings.PLAYER_INVENTORY_KEY))
            {
                TogglePlayerInventory();
            }

            if (InventoryOpened)
            {
                Inventory.UpdateUI();
            }

            if (OpenedBuilding != null)
            {
                OpenedBuilding.UpdateUI();
            }

            if (MyKeyboard.IsPressed(KeyBindings.ROTATE_KEY))
            {
                UpdateRotation();
            }

            if (MyKeyboard.IsPressed(KeyBindings.CANCEL_KEY))  
            {
                StopHoldingItem();
            }

            if (MyKeyboard.IsHeld(KeyBindings.PICKUP_KEY))
            {
                PickupItems();
            }

            if (!InventoryOpened)
            {
                if (MyKeyboard.IsHeld(MouseKey.Right))
                {
                    if (World.MouseTile.Building != null)
                    {
                        DeconstructBuilding();
                    }
                }

                if (HeldItem != null) // holding item
                {
                    if (MyKeyboard.IsPressed(MouseKey.Left)) // use item
                    {
                        HeldItem.PrimaryUse(ItemRotation);
                    }
                    else if (MyKeyboard.IsHeld(MouseKey.Left) && World.MouseTile != World.LastMouseTile) // use items contnuosly
                    {
                        HeldItem.PrimaryUse(ItemRotation);
                    }
                    else if (MyKeyboard.IsPressed(KeyBindings.DROP_KEY) && World.MouseTile.Wall == null) // drop item
                    {
                        BaseItem.Drop(HeldItem);
                        Sounds.Woosh.Play(1);
                    }
                }
                else // empty hand
                {
                    if (MyKeyboard.IsPressed(MouseKey.Left))
                    {
                        if (World.MouseTile.Wall != null) // mine walls
                        {
                            if (WallHits >= World.MouseTile.Wall.GetHardness() - 1)
                            {
                                World.MouseTile.Wall.WhenMined(this, World.MouseTile);
                                WallHits = 0;
                                Sounds.Pickaxe.Play(1);
                            }
                            else if (World.MouseTile.Wall.IsDestructible())
                            {
                                WallHits += 1;
                                Sounds.Pickaxe.Play(1);
                            }
                        }
                        else if (World.MouseTile.Building != null) // open building UI
                        {
                            if (World.MouseTile.Building.HasUI())
                            {
                                OpenedBuilding = World.MouseTile.Building;
                                OpenedBuilding.OpenUI();
                                TogglePlayerInventory();
                            }
                        }
                    }
                }
            }
        }


        public void Draw()
        {
            if (InventoryOpened) // draw inventory
            {
                Inventory.DrawUI();
            }

            if (OpenedBuilding != null)
            {
                OpenedBuilding.DrawUI();
            }

            if (HeldItem != null) // draw held items
            {
                Rectangle rectangle;
                float itemRotationAngle;

                if (InventoryOpened)
                {
                    rectangle = new Rectangle((int)MyKeyboard.GetMousePosition().X, (int)MyKeyboard.GetMousePosition().Y, Inventory.ButtonSize / 2, Inventory.ButtonSize / 2);
                }
                else
                {
                    MyVector2Int screenCords = World.Camera.WorldToScreenCords(new MyVector2(World.MouseTile.Position.X + 0.5f, World.MouseTile.Position.Y + 0.5f));
                    rectangle = new Rectangle(screenCords.X, screenCords.Y, World.Camera.Scale * (HeldItem.GetTexture().Width) / 16, World.Camera.Scale * (HeldItem.GetTexture().Height) / 16);
                }

                if (HeldItem.CanRotate())
                {

                    itemRotationAngle = World.RotationToAngle(ItemRotation);
                }
                else
                {
                    itemRotationAngle = 0;
                }

                Game.MainSpriteBatch.Draw(HeldItem.GetTexture(), rectangle, new Rectangle(0, 0, HeldItem.GetTexture().Width, HeldItem.GetTexture().Height), Color.FromNonPremultiplied(new Vector4(1, 1, 1, 0.5f)), itemRotationAngle, new Vector2(HeldItem.GetTexture().Width / 2, HeldItem.GetTexture().Height / 2), SpriteEffects.None, 0);
                Game.MainSpriteBatch.DrawString(Fonts.SmallFont, HeldItem.Count.ToString(), new Vector2((int)MyKeyboard.GetMousePosition().X + 16, (int)MyKeyboard.GetMousePosition().Y + 2), Color.Black);
            }
            else if (World.MouseTile.Wall != null && !InventoryOpened) // block mining progress
            {
                int wallHardness = World.MouseTile.Wall.GetHardness();
                int rectangleSize = (wallHardness - WallHits) * World.Camera.Scale / wallHardness;

                MyVector2Int screenCords = World.Camera.WorldToScreenCords(new MyVector2(World.MouseTile.Position.X + 0.5f, World.MouseTile.Position.Y + 0.5f));
                Rectangle rectangle = new Rectangle(screenCords.X - rectangleSize / 2, screenCords.Y - rectangleSize / 2, rectangleSize, rectangleSize);
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.FromNonPremultiplied(new Vector4(0.5f, 0.5f, 0.5f, 0.5f)));
            }
        }


        private void TogglePlayerInventory()
        {
            InventoryOpened = !InventoryOpened;
            Sounds.ButtonClick.Play(1);

            if (InventoryOpened)
            {
                Inventory.OpenUI(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2 + Inventory.ButtonSpacing + Inventory.Border * 4 + Inventory.BorderOffset));
            }
            else
            {
                Inventory.CloseUI();
                if (OpenedBuilding != null)
                {
                    OpenedBuilding.CloseUI();
                    OpenedBuilding = null;
                }
            }
        }


        private void UpdateRotation()
        {
            if (ItemRotation.X == 1) // right to down
            {
                ItemRotation.X = 0;
                ItemRotation.Y = 1;
            }
            else if (ItemRotation.Y == 1) // down to left
            {
                ItemRotation.X = -1;
                ItemRotation.Y = 0;
            }
            else if (ItemRotation.X == -1) // left to up
            {
                ItemRotation.X = 0;
                ItemRotation.Y = -1;
            }
            else // up to right
            {
                ItemRotation.X = 1;
                ItemRotation.Y = 0;
            }
        }

        private void StopHoldingItem()
        {
            if (HeldItem != null) // stop holding item
            {
                if (Inventory.TryAddItem(HeldItem))
                {
                    HeldItem = null;
                    Sounds.Woosh.Play(1);
                }
                else
                {
                    BaseItem.Drop(HeldItem);
                    HeldItem = null;
                    Sounds.Woosh.Play(1);
                }
            }
        }


        private void PickupItems()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Tile checkedTile = World.GetTileByRelativePosition(World.PlayerBody.Tile, new MyVector2Int(x, y));

                    foreach (BaseItem item in checkedTile.Items.ToList())
                    {
                        if (Inventory.TryAddItem(item))
                        {
                            item.RemoveFromTile(checkedTile);
                        }
                    }
                }
            }
        }


        private void DeconstructBuilding()
        {
            bool emptyBuilding = true;
            if (World.MouseTile.Building.Inventory != null) // has inventory
            {
                for (int i = 0; i < World.MouseTile.Building.Inventory.Size; i++) // clear inventory
                {
                    if (World.MouseTile.Building.Inventory.Items[i] != null)
                    {
                        if (Inventory.TryAddItem(World.MouseTile.Building.Inventory.Items[i]))
                        {
                            World.MouseTile.Building.Inventory.Items[i] = null;
                        }
                        else
                        {
                            emptyBuilding = false;
                        }
                    }
                }
            }
            if (emptyBuilding)
            {
                if (World.Player.Inventory.TryAddItem(World.MouseTile.Building.ToItem())) // if not full inventory
                {
                    BaseBuilding.DeleteBuilding(World.MouseTile.Building);
                    Sounds.PlayPlaceSound();
                }
                else if (MyKeyboard.IsPressed(MouseKey.Right))
                {
                    Sounds.ButtonDecline.Play(1);
                }
            }
            else if (MyKeyboard.IsPressed(MouseKey.Right))
            {
                Sounds.ButtonDecline.Play(1);
            }
        }
    }
}
