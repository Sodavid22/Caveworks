using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Caveworks
{
    [Serializable]
    public class Inventory
    {
        public const int RowLength = 10;
        public const int ButtonSize = 72;
        public const int ButtonSpacing = 80;
        public const int Border = 2;
        public const int BorderOffset = 12;

        public BaseItem[] Items;
        public int Size;
        MyVector2Int WindowPosition;
        public Player Player;

        [NonSerialized]
        Button[] Buttons;


        public Inventory(int size, Player player)
        {
            Items = new BaseItem[size];
            Size = size;
            Player = player;
        }


        public BaseItem GetFirstItem()
        {
            foreach (BaseItem item in Items)
            {
                return item;
            }
            return null;
        }


        public int CountItems(BaseItem item)
        {
            int count = 0;
            for (int i = 0; i < Size; i++)
            {
                if (Items[i] != null)
                {
                    if (Items[i].GetType() == item.GetType())
                    {
                        count += Items[i].Count;
                    }
                }
            }
            return count;
        }


        public bool RemoveItems(BaseItem item)
        {
            int remaining = item.Count;

            for (int i = 0; i < Size; i++)
            {
                if (Items[i] != null)
                {
                    if (Items[i].GetType() == item.GetType())
                    {
                        Items[i].Count -= remaining;
                        if (Items[i].Count <= 0) // not enough items
                        {
                            remaining = -Items[i].Count;
                            Items[i] = null;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


        public bool TryAddItem(BaseItem item)
        {
            for (int i = 0; i < Size; i++)
            {
                if (Items[i] == null)
                {
                    Items[i] = item;
                    return true;
                }
                else if (Items[i].GetType() == item.GetType())
                {
                    if (Items[i].Count + item.Count <= BaseItem.StackSize)
                    {
                        Items[i].Count += item.Count;
                        item.Count = 0;
                        return true;
                    }
                    else
                    {
                        item.Count -= BaseItem.StackSize - Items[i].Count;
                        Items[i].Count = BaseItem.StackSize;
                    }
                }
            }
            return false;
        }


        public void OpenUI(MyVector2Int position)
        {
            WindowPosition = position;
            Buttons = new Button[Size];

            for (int i = 0; i < Size; i++)
            {
                int x = i % RowLength;
                int y = i / RowLength;

                Buttons[i] = new Button(new Vector2(ButtonSize, ButtonSize), Globals.InventoryButtonColor, 2, "", Fonts.MediumFont);
                Buttons[i].Place(new Vector2(position.X + x * ButtonSpacing, position.Y + y * ButtonSpacing), Anchor.TopLeft);
                Buttons[i].Mute();
            }
        }


        public void CloseUI()
        {
            Buttons = new Button[Size];
        }


        public void UpdateUI()
        {
            for (int i = 0; i < Size; i++)
            {
                if (Items[i] != null)
                {
                    Buttons[i].SetText(Items[i].Count.ToString());
                    Buttons[i].SetTexture(Items[i].GetTexture());
                }
                else
                {
                    Buttons[i].SetText(null);
                    Buttons[i].SetTexture(null);
                }
                Buttons[i].Update();
            }

            for (int i = 0; i < Size; i++)
            {
                if (Buttons[i].IsPressed(MouseKey.Left))
                {
                    if (Player.HeldItem == null) // take items to hand
                    {
                        if (Items[i] != null)
                        {
                            Player.HeldItem = Items[i];
                            Items[i] = null;
                            Sounds.Woosh.Play(1); ;
                        }
                    }
                    else
                    {
                        if (Items[i] == null) // put items from hand to empty slot
                        {
                            Items[i] = Player.HeldItem;
                            Player.HeldItem = null;
                            Sounds.Woosh.Play(1); ;
                        }
                        else if (Items[i].GetType() == Player.HeldItem.GetType())
                        {
                            if (Items[i].Count + Player.HeldItem.Count <= BaseItem.StackSize) // put items from hand to slot
                            {
                                Items[i].Count += Player.HeldItem.Count;
                                Player.HeldItem = null;
                                Sounds.Woosh.Play(1); ;
                            }
                            else // put SOME items from hand to slot
                            {
                                Player.HeldItem.Count -= BaseItem.StackSize - Items[i].Count;
                                Items[i].Count = BaseItem.StackSize;
                                Sounds.Woosh.Play(1);
                            }
                        }
                    }
                }
                else if (Buttons[i].IsPressed(MouseKey.Right))
                {
                    if (Player.HeldItem == null && Items[i] != null)
                    {
                        if (Items[i].Count > 1) // take half items
                        {
                            Player.HeldItem = Items[i].DeepClone();
                            Player.HeldItem.Count = Items[i].Count / 2;
                            Items[i].Count -= Player.HeldItem.Count;
                            Sounds.Woosh.Play(1);
                        }
                    }
                    else if (Player.HeldItem != null)
                    {
                        if (Items[i] == null) // put one item to empty tile
                        {
                            Items[i] = Player.HeldItem.DeepClone();
                            Items[i].Count = 1;
                            Player.HeldItem.Count -= 1;
                            if (Player.HeldItem.Count == 0)
                            {
                                Player.HeldItem = null;
                            }
                            Sounds.Woosh.Play(1);
                        }
                        else if (Items[i].GetType() == Player.HeldItem.GetType()) // add one item to tile
                        {
                            if (Items[i].Count < BaseItem.StackSize)
                            {
                                Items[i].Count += 1;
                                Player.HeldItem.Count -= 1;
                                if (Player.HeldItem.Count == 0)
                                {
                                    Player.HeldItem = null;
                                }
                            }
                            Sounds.Woosh.Play(1);
                        }
                    }
                }
            }
        }


        public void DrawUI()
        {
            // background
            Rectangle backRectangle = new Rectangle(WindowPosition.X - Border - BorderOffset, WindowPosition.Y - Border - BorderOffset, ButtonSpacing * (RowLength - 1) + ButtonSize + Border * 2 + BorderOffset * 2, ButtonSpacing * ((int)MathF.Ceiling(Size / (float)RowLength) - 1) + ButtonSize + Border * 2 + BorderOffset * 2);
            Rectangle frontRectangle = new Rectangle(backRectangle.X + Border, backRectangle.Y + Border, backRectangle.Width - Border * 2, backRectangle.Height - Border * 2);
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, backRectangle, Color.Black);
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, frontRectangle, Color.FromNonPremultiplied(Globals.InventoryBoxColor));

            foreach (Button button in Buttons)
            {
                button.Draw();
            }
        }
    }
}
