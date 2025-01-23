using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Caveworks
{
    [Serializable]
    public class BaseCreature
    {
        public Tile Tile;
        public MyVector2 Coordinates;
        public MyVector2 Velocity;
        public float Rotation;

        public float HitboxSize;
        public int Health;
        public float MaxSpeed;


        public BaseCreature(Tile tile)
        {
            this.Tile = tile;
            this.Coordinates = new MyVector2(tile.Coordinates.X + 0.5f, tile.Coordinates.Y + 0.5f);
            this.Velocity = new MyVector2(0, 0);
            this.Rotation = 0;
        }


        public virtual void Update(float deltaTime)
        {
            Move(deltaTime);
        }


        public virtual void Draw(Camera camera)
        {

        }


        public void Move(float deltaTime)
        {
            MyVector2 newCoordinates = new MyVector2(Coordinates.X + Velocity.X * deltaTime, Coordinates.Y);
            Tile newTile = null;


            if (CheckForColision(this.Tile, newCoordinates))
            {
                newCoordinates.X = Coordinates.X;
            }

            newCoordinates.Y = Coordinates.Y + Velocity.Y * deltaTime;

            if (CheckForColision(this.Tile, newCoordinates))
            {
                newCoordinates.Y = Coordinates.Y;
            }

            if (newCoordinates.X > Tile.Coordinates.X + 1 || newCoordinates.X < Tile.Coordinates.X || newCoordinates.Y > Tile.Coordinates.Y + 1 || newCoordinates.Y < Tile.Coordinates.Y)
            {
                newTile = Tile.Chunk.World.GlobalCordsToTile(newCoordinates.ToMyVector2Int());
                this.Tile.Creatures.Remove(this);
                this.Tile.Chunk.Creatures.Remove(this);
                this.Tile = newTile;
                newTile.Creatures.Add(this);
                newTile.Chunk.Creatures.Add(this);
            }

            this.Coordinates = newCoordinates;
        }


        private bool CheckForColision(Tile tile, MyVector2 position) // !!! only works with walls
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
                            if (Math.Abs(checkedTile.Coordinates.X + 0.5 - position.X) < this.HitboxSize / 2 + 0.5 && Math.Abs(checkedTile.Coordinates.Y + 0.5 - position.Y) < this.HitboxSize / 2 + 0.5)
                            {
                                return true;
                            }
                        }
                        foreach (BaseCreature creature in checkedTile.Creatures)
                        {
                            if (creature != this)
                            {
                                if (Math.Abs(creature.Coordinates.X - position.X) < this.HitboxSize / 2 + creature.HitboxSize / 2 && Math.Abs(creature.Coordinates.Y - position.Y) < this.HitboxSize / 2 + creature.HitboxSize / 2)
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
