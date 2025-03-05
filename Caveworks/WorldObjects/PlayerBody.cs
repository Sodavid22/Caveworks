using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Caveworks
{
    [Serializable]
    public class PlayerBody
    {
        public Tile Tile;
        public MyVector2 Coordinates;
        public MyVector2 Velocity;
        public float Rotation;
        public float HitboxSize;
        public float MaxSpeed;


        public PlayerBody(Tile tile)
        {
            Tile = tile;
            Coordinates = new MyVector2(tile.Position.X + 0.5f, tile.Position.Y + 0.5f);
            Velocity = new MyVector2(0, 0);

            Rotation = 0;
            HitboxSize = 0.8f;
            MaxSpeed = 10;
        }


        public void Update(float deltaTime)
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

            Move(deltaTime);
        }


        public void Draw(Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(Coordinates);
            Game.CreatureSpritebatch.Draw(Textures.Player, new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale), new Rectangle(0, 0, 16, 16), Color.White, Rotation, new Vector2(8, 8), SpriteEffects.None, 0);
        }


        public void Move(float deltaTime)
        {
            if (CheckForColision(Tile, Coordinates))
            {
                Coordinates = new MyVector2(Tile.Position.X + 0.5f, Tile.Position.Y + 0.5f);
            }

            MyVector2 newCoordinates = new MyVector2(Coordinates.X + Velocity.X * deltaTime, Coordinates.Y);

            if (CheckForColision(Tile, newCoordinates))
            {
                newCoordinates.X = Coordinates.X;
            }

            newCoordinates.Y = Coordinates.Y + Velocity.Y * deltaTime;

            if (CheckForColision(Tile, newCoordinates))
            {
                newCoordinates.Y = Coordinates.Y;
            }

            if (newCoordinates.X > Tile.Position.X + 1 || newCoordinates.X < Tile.Position.X || newCoordinates.Y > Tile.Position.Y + 1 || newCoordinates.Y < Tile.Position.Y)
            {
                Tile = Tile.Chunk.World.GlobalCordsToTile(newCoordinates.ToMyVector2Int());
            }

            Coordinates = newCoordinates;
        }


        private bool CheckForColision(Tile tile, MyVector2 coordinates) // !!! only works with walls
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Tile checkedTile = this.Tile.Chunk.World.GetTileByRelativePosition(tile, new MyVector2Int(x, y));
                    if (checkedTile != null)
                    {
                        if (checkedTile.Wall != null)
                        {
                            if (Math.Abs(checkedTile.Position.X + 0.5 - coordinates.X) < this.HitboxSize / 2 + 0.5 && Math.Abs(checkedTile.Position.Y + 0.5 - coordinates.Y) < this.HitboxSize / 2 + 0.5)
                            {
                                return true;
                            }
                        }
                        if (checkedTile.Building != null)
                        {
                            if (checkedTile.Building.HasCollision())
                            {
                                if (Math.Abs(checkedTile.Position.X + 0.5 - coordinates.X) < this.HitboxSize / 2 + 0.5 && Math.Abs(checkedTile.Position.Y + 0.5 - coordinates.Y) < this.HitboxSize / 2 + 0.5)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
