using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class StoneWall : BaseWall
    {
        public StoneWall(Tile tile)
        {
            tile.Wall = this;
        }

        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(tile.Position.ToMyVector2());
            Rectangle wallRectangle = new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale);
            Game.WallSpritebatch.Draw(Textures.StoneWall, wallRectangle, Color.White);
        }


        public override int GetHardness() { return 4; }


        public override bool IsDestructible()
        {
            return true;
        }
    }
}
