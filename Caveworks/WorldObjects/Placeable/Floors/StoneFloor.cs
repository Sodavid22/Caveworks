using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class StoneFloor : BaseFloor
    {
        public StoneFloor(Tile tile) : base(tile) { }

        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(tile.Position);
            Game.FloorSpriteBatch.Draw(Textures.StoneFloor, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
