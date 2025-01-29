using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class StoneFloor : BaseFloor
    {
        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(tile.Coordinates.ToMyVector2());
            Game.FloorSpriteBatch.Draw(Textures.StoneFloor, new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
