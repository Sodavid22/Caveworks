using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class StoneWall : BaseWall
    {
        public StoneWall(Tile tile) : base(tile) { }

        public override void Draw(Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(Tile.GlobalCoordinates);
            Rectangle wallRectangle = new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, (int)MathF.Ceiling(camera.Scale), (int)MathF.Ceiling(camera.Scale));
            Game.WallSpritebatch.Draw(Textures.StoneWall, wallRectangle, Color.White);
        }
    }
}
