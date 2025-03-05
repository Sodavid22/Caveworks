using System;
using Microsoft.Xna.Framework;

namespace Caveworks
{
    [Serializable]
    public class Inventory
    {
        public const int RowLength = 10;
        public const int ButtonSize = 64;
        public const int ButtonSpacing = 70;
        public const int Border = 10;

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


        public bool TryAddItem(BaseItem item, int position)
        {
            if (Items[position].GetType() == item.GetType())
            {
                if (Items[position].Count + item.Count <= BaseItem.StackSize)
                {
                    Items[position].Count += item.Count;
                    item.Count = 0;
                    return true;
                }
                else
                {
                    item.Count -= BaseItem.StackSize - Items[position].Count;
                    Items[position].Count = BaseItem.StackSize;
                    return false;
                }
            }
            else if (Items[position] == null)
            {
                Items[position] = item;
                return true;
            }
            return false;
        }


        public BaseItem RemoveItem(int position)
        {
            BaseItem item = Items[position];
            Items[position] = null;
            return item;
        }


        public void OpenUI(MyVector2Int position)
        {
            this.WindowPosition = position;
            Buttons = new Button[Size];

            for (int i = 0; i < Size; i++)
            {
                int x = i % RowLength;
                int y = i / RowLength;

                Buttons[i] = new Button(new Vector2(ButtonSize, ButtonSize), Globals.InventoryButtonColor, 2, "", Fonts.DefaultFont);
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
                            Player.HeldItem = Cloning.DeepClone(Items[i]);
                            Player.HeldItem.Count = Items[i].Count / 2;
                            Items[i].Count -= Player.HeldItem.Count;
                            Sounds.Woosh.Play(1);
                        }
                    }
                    else if (Player.HeldItem != null)
                    {
                        if (Items[i] == null) // put one item to empty tile
                        {
                            Items[i] = Cloning.DeepClone(Player.HeldItem);
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
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(WindowPosition.X - Border - 2, WindowPosition.Y - Border - 2, ButtonSpacing * (RowLength - 1) + ButtonSize + Border * 2 + 4, ButtonSpacing * (Size / RowLength - 1) + ButtonSize + Border * 2 + 4), Color.Black);
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(WindowPosition.X - Border, WindowPosition.Y - Border, ButtonSpacing*(RowLength - 1) + ButtonSize + Border*2, ButtonSpacing*(Size / RowLength - 1) + ButtonSize + Border*2), Color.FromNonPremultiplied(Globals.InventoryBoxColor));

            foreach (Button button in Buttons)
            {
                button.Draw();
            }
        }
    }
}
