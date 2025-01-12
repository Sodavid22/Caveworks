using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    public class FloorBase
    {
        static readonly int textureSize = 1;
        public Tile Tile { get; set; }
        public Texture2D Texture { get; set; }

        public FloorBase(Tile tile) 
        {
            this.Tile = tile;
        }

        public void Draw(Camera camera)
        {
            Vector2 screenCoordinates = camera.WrldToScrnCords(Tile.Coordinates);
            Rectangle floorRectangle = new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, (int)MathF.Ceiling(textureSize * camera.Scale), (int)MathF.Ceiling(textureSize * camera.Scale));
            Game.FloorSpriteBatch.Draw(Textures.StoneFloor, floorRectangle, Color.White);
        }
    }
}
