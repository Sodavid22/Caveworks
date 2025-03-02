using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Caveworks
{
    [Serializable]
    public class Player : BaseCreature
    {
        public Player(Tile tile) : base(tile)
        {
            HitboxSize = 0.8f;
            Health = 100;
            MaxSpeed = 10;
            Rotation = 0;

            Tile.Chunk.World.Camera.player = this;
        }


        public override void Update(float deltaTime)
        {
            MyVector2Int direction = new MyVector2Int(0, 0);

            if (MyKeyboard.IsHeld(Keys.D))
            {
                direction.X += 1;
            }
            else if (MyKeyboard.IsHeld(Keys.A))
            {
                direction.X -= 1;
            }

            if (MyKeyboard.IsHeld(Keys.S))
            {
                direction.Y += 1;
            }
            else if (MyKeyboard.IsHeld(Keys.W))
            {
                direction.Y -= 1;
            }

            if (Math.Abs(direction.X) + Math.Abs(direction.Y) == 2)
            {
                Velocity.X = MaxSpeed * direction.X / 1.414f;
                Velocity.Y = MaxSpeed * direction.Y / 1.414f;
            }
            else
            {
                Velocity.X = MaxSpeed * direction.X;
                Velocity.Y = MaxSpeed * direction.Y;
            }

            if (!(Velocity.X == 0 && Velocity.Y == 0))
            {
                Rotation = (float)Math.Atan2(Velocity.Y, Velocity.X);
            }

            base.Update(deltaTime);
        }


        public override void Draw(Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(Coordinates);
            Game.CreatureSpritebatch.Draw(Textures.Player, new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale), new Rectangle(0, 0, 16, 16), Color.White, Rotation, new Vector2(8, 8), SpriteEffects.None, 0);
        }
    }
}
