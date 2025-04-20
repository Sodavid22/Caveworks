using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class StoneWallStrongest : BaseWall
    {
        public StoneWallStrongest(Tile tile)
        {
            tile.Wall = this;
        }


        public override int GetHardness() { return 8; }


        public override int GetDrillTime() { return 4; }


        public override BaseItem GetItem(Tile wallTile)
        {
            return new RawStoneItem(1);
        }


        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(tile.Position);
            Rectangle wallRectangle = new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale);
            Game.WallSpritebatch.Draw(Textures.StoneWallStrongest, wallRectangle, Color.White);
        }
    }
}
