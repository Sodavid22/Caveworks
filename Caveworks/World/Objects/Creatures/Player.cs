using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Caveworks
{
    [Serializable]
    internal class Player : BaseCreature
    {
        public Player(Tile tile) : base(tile)
        {
            HitboxSize = 0.8f;
            Health = 100;
            MaxSpeed = 3;
            Rotation = 0;
        }


        public override void Update(float deltaTime)
        {
            if (MyKeyboard.IsHeld(Keys.D))
            {
                Velocity.X = MaxSpeed;
            }
            else if (MyKeyboard.IsHeld(Keys.A))
            {
                Velocity.X = -MaxSpeed;
            }
            else { Velocity.X = 0; }

            if (MyKeyboard.IsHeld(Keys.S))
            {
                Velocity.Y = MaxSpeed;
            }
            else if (MyKeyboard.IsHeld(Keys.W))
            {
                Velocity.Y = -MaxSpeed;
            }
            else { Velocity.Y = 0; }

            if (!(Velocity.X == 0 && Velocity.Y == 0))
            {
                float atan2 = (float)Math.Atan2(Velocity.Y, Velocity.X);
                Rotation = atan2;
            }
            base.Update(deltaTime);
        }


        public override void Draw(Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(new MyVector2(GlobalCords.X + 0.5f, GlobalCords.Y + 0.5f));
            Rectangle playerRectangle = new Rectangle((int)screenCoordinates.X - camera.Scale / 2, (int)screenCoordinates.Y - camera.Scale / 2, camera.Scale, camera.Scale);
            Game.CreatureSpritebatch.Draw(Textures.Player, playerRectangle, new Rectangle(0, 0, 32, 32), Color.White, Rotation, new Vector2(16, 16), SpriteEffects.None, 0);
        }
    }
}
