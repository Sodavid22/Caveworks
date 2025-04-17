using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    public class SlowBelt : BaseBelt
    {
        public static float BeltSpeed = 1;


        public SlowBelt(Tile tile, MyVector2Int rotation) : base(tile, rotation) { }


        public override BaseItem ToItem()
        {
            return new SlowBeltItem(1);
        }


        public override void Update(float deltaTime)
        {
            base.UpdateBelt(deltaTime, BeltSpeed);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            Texture2D texture;
            if (Globals.World.SlowBeltTimer < 0.0625f)
            {
                texture = Textures.SlowBelt;
            }
            else if (Globals.World.SlowBeltTimer < 0.125)
            {
                texture = Textures.SlowBelt2;
            }
            else if (Globals.World.SlowBeltTimer < 0.1875)
            {
                texture = Textures.SlowBelt3;
            }
            else
            {
                texture = Textures.SlowBelt4;
            }

            MyVector2Int screenCoordinates = camera.WorldToScreenCords(new MyVector2(Position.X + 0.5f, Position.Y + 0.5f));
            float rotation = MathF.Atan2(Rotation.Y, Rotation.X);
            Game.FloorSpriteBatch.Draw(texture, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), new Rectangle(0, 0, 16, 16), Color.White, rotation, new Vector2(8, 8), SpriteEffects.None, 0);
        }
    }
}
