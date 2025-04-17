using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class StoneWallPermanent : BaseWall
    {
        public StoneWallPermanent(Tile tile)
        {
            tile.Wall = this;
        }

        public override bool IsDestructible() { return false; }


        public override int GetHardness() { return 9; }


        public override int GetDrillTime() { return 2; }


        public override BaseItem GetItem(Tile wallTile)
        {
            return new RawStoneItem(1);
        }


        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(tile.Position);
            Rectangle wallRectangle = new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale);
            Game.WallSpritebatch.Draw(Textures.StoneWallPermanent, wallRectangle, Color.White);
        }
    }
}
