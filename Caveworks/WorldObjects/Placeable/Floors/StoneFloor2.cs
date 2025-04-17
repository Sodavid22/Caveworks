using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class StoneFloor2 : BaseFloor
    {
        public StoneFloor2(Tile tile) : base(tile) { }

        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(tile.Position);
            Game.FloorSpriteBatch.Draw(Textures.StoneFloor2, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
