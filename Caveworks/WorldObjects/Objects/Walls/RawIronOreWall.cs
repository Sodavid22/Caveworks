using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class RawIronOreWall : BaseWall
    {
        public RawIronOreWall(Tile tile)
        {
            tile.Wall = this;
        }

        public override bool IsDestructible() { return true; }


        public override int GetHardness() { return 10; }


        public override void WhenMined(Player player, Tile wallTile)
        {
            player.Inventory.TryAddItem(new RawIronOreItem(1));
        }


        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(tile.Position.ToMyVector2());
            Rectangle wallRectangle = new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale);
            Game.WallSpritebatch.Draw(Textures.RawIronOreWall, wallRectangle, Color.White);
        }
    }
}
