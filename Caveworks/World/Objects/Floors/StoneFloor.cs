using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class StoneFloor : BaseFloor
    {
        public StoneFloor(Tile tile) : base(tile) { }

        public override void Draw(Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(Tile.GlobalCoordinates);
            Rectangle floorRectangle = new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, (int)MathF.Ceiling(camera.Scale), (int)MathF.Ceiling(camera.Scale));
            Game.FloorSpriteBatch.Draw(Textures.StoneFloor, floorRectangle, Color.White);
        }
    }
}
