using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    public class YellowBelt : BaseBelt
    {
        public YellowBelt(Tile tile, MyVector2Int rotation) : base(tile, rotation)
        {
            BeltSpeed = 2;
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(new MyVector2(Position.X + 0.5f, Position.Y + 0.5f));
            float rotation = MathF.Atan2(Rotation.Y, Rotation.X);
            Game.FloorSpriteBatch.Draw(Textures.SlowBelt, new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale), new Rectangle(0, 0, 16, 16), Color.White, rotation, new Vector2(8, 8), SpriteEffects.None, 0);
        }
    }
}
