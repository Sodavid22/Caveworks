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
            Globals.World.Player.Inventory.Close();
            Globals.World.Player.InventoryOpened = false;
        }


        public void Update()
        {
            if (MyKeyboard.IsPressed(KeyBindings.PLAYER_INVENTORY_KEY)) // open inventory
            {
                InventoryOpened = !InventoryOpened;
                Sounds.ButtonClick.Play(1);

                if (InventoryOpened)
                {
                    Inventory.Open(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2 + 50));
                }
                else
                {
                    Inventory.Close();
                }
            }

            if (InventoryOpened)
            {
                Inventory.Update();
            }

            if (MyKeyboard.IsPressed(KeyBindings.ROTATE_KEY))
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

            if (MyKeyboard.IsPressed(KeyBindings.CANCEL_KEY) && HeldItem != null) // stop holding item
            {
                if (Inventory.TryAddItem(HeldItem))
                {
                    HeldItem = null;
                }
            }

            if (World.MouseTile != World.LastMouseTile) // reset wall mining
            {
                WallHits = 0;
            }

            if (MyKeyboard.IsHeld(KeyBindings.PICKUP_KEY))
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
                                checkedTile.Items.Remove(item);
                            }
                        }
                    }
                }
            }

            if (!InventoryOpened)
            {
                if (HeldItem != null) // holding item
                {
                    if (MyKeyboard.IsPressed(MouseKey.Left)) // use item
                    {
                        HeldItem.PrimaryUse(ItemRotation);
                    }
                    else if (MyKeyboard.IsPressed(MouseKey.Right)) // secondary use item
                    {
                        HeldItem.SecondaryUse(ItemRotation);
                    }
                    else if (MyKeyboard.IsPressed(KeyBindings.DROP_KEY)) // drop item
                    {
                        BaseItem.Drop(HeldItem);
                    }
                    else if (MyKeyboard.IsHeld(MouseKey.Left) && World.MouseTile != World.LastMouseTile) // use items contnuosly
                    {
                        HeldItem.PrimaryUse(ItemRotation);
                    }
                    else if (HeldItem != null && !InventoryOpened && MyKeyboard.IsHeld(MouseKey.Right) && World.MouseTile != World.LastMouseTile) // secondary use item continuosly
                    {
                        HeldItem.SecondaryUse(ItemRotation);
                    }
                }
                else // empty hand
                {
                    if (MyKeyboard.IsPressed(MouseKey.Left)) // break walls
                    {
                        if (World.MouseTile.Wall != null)
                        {
                            if (WallHits >= World.MouseTile.Wall.GetHardness() - 1)
                            {
                                World.MouseTile.Wall = null;
                                WallHits = 0;
                                Sounds.ButtonClick2.Play(1); // TEMPORARY
                            }
                            else if (World.MouseTile.Wall.IsDestructible())
                            {
                                WallHits += 1;
                                Sounds.ButtonClick.Play(1); // TEMPORARY
                            }
                        }
                    }
                    else if (MyKeyboard.IsHeld(MouseKey.Right)) // deconstruct buildings
                    {
                        if (World.MouseTile.Building != null)
                        {
                            if (World.Player.Inventory.TryAddItem(World.MouseTile.Building.ToItem()))
                            {
                                BaseBuilding.DeleteBuilding(World.MouseTile.Building);
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
                Inventory.Draw();
            }

            if (HeldItem != null) // draw held items
            {
                Rectangle rectangle;
                if (InventoryOpened)
                {
                    rectangle = new Rectangle((int)MyKeyboard.GetMousePosition().X, (int)MyKeyboard.GetMousePosition().Y, Inventory.ButtonSize / 2, Inventory.ButtonSize / 2);
                }
                else
                {
                    MyVector2 screenCords = World.Camera.WorldToScreenCords(new MyVector2(World.MouseTile.Position.X + 0.5f, World.MouseTile.Position.Y + 0.5f));
                    rectangle = new Rectangle((int)screenCords.X, (int)screenCords.Y, World.Camera.Scale * (HeldItem.GetTexture().Width) / 16, World.Camera.Scale * (HeldItem.GetTexture().Height) / 16);
                }
                Game.MainSpriteBatch.Draw(HeldItem.GetTexture(), rectangle, new Rectangle(0, 0, HeldItem.GetTexture().Width, HeldItem.GetTexture().Height), Color.FromNonPremultiplied(new Vector4(1,1,1,0.5f)), World.RotationToAngle(ItemRotation), new Vector2(HeldItem.GetTexture().Width / 2, HeldItem.GetTexture().Height / 2), SpriteEffects.None, 0);
                Game.MainSpriteBatch.DrawString(Fonts.DefaultFont, HeldItem.Count.ToString(), new Vector2((int)MyKeyboard.GetMousePosition().X + 16, (int)MyKeyboard.GetMousePosition().Y + 2), Color.Black);
            }
            else if (World.MouseTile.Wall != null) // block mining progress
            {
                int wallHardness = World.MouseTile.Wall.GetHardness();
                int rectangleSize = (wallHardness - WallHits) * World.Camera.Scale / wallHardness;

                MyVector2 screenCords = World.Camera.WorldToScreenCords(new MyVector2(World.MouseTile.Position.X + 0.5f, World.MouseTile.Position.Y + 0.5f));
                Rectangle rectangle = new Rectangle((int)screenCords.X - rectangleSize / 2, (int)screenCords.Y - rectangleSize / 2, rectangleSize, rectangleSize);
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.FromNonPremultiplied(new Vector4(0.5f, 0.5f, 0.5f, 0.5f)));
            }
        }
    }
}
