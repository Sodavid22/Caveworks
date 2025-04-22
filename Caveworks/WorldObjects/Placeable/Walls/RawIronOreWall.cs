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

        public override bool IsDestructible() { return false; }


        public override int GetHardness() { return 6; }


        public override int GetDrillTime() { return 4; }


        public override BaseItem GetItem(Tile wallTile)
        {
            return new RawIronOreItem(1);
        }


        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(tile.Position);
            Rectangle wallRectangle = new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale);
            Game.WallSpritebatch.Draw(Textures.RawIronOreWall, wallRectangle, Color.White);
        }
    }
}
