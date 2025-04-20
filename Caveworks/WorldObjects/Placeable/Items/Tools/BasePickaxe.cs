using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace Caveworks.WorldObjects.Placeable.Items.Tools
{
    [Serializable]
    class BasePickaxe : BaseItem
    {
        public BasePickaxe(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public BasePickaxe(int count) : base(count) { }


        public override bool CanUseContinuosly() { return false; }


        public virtual bool UsePickaxe(int strength, int radius, int efficiency)
        {
            Tile mouseTile = Globals.World.MouseTile;
            Player player = Globals.World.Player;
            bool used = false;

            if (mouseTile.Wall != null) // mine walls
            {
                used = true;
                player.WallHits += strength;
                Sounds.Pickaxe.Play(1);
                
                if (player.WallHits >= mouseTile.Wall.GetHardness())
                {
                    player.WallHits = 0;
                    Tile tile;

                    for (int x = mouseTile.Position.X - radius; x <= mouseTile.Position.X + radius; x++)
                    {
                        for (int y = mouseTile.Position.Y - radius; y <= mouseTile.Position.Y + radius; y++)
                        {
                            tile = Globals.World.GlobalCordsToTile(new MyVector2Int(x, y));
                            if (tile.Wall != null)
                            {
                                if (tile.Wall.IsDestructible())
                                {
                                    player.PlayerInventory.TryAddItem(tile.Wall.GetItem(mouseTile));
                                    Globals.World.GlobalCordsToTile(new MyVector2Int(x, y)).Wall = null;
                                }
                                else
                                {
                                    if (x == mouseTile.Position.X && y == mouseTile.Position.Y)
                                    {
                                        for (int i = 0; i < efficiency; i++)
                                        {
                                            player.PlayerInventory.TryAddItem(tile.Wall.GetItem(mouseTile));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return used;
        }
    }
}
