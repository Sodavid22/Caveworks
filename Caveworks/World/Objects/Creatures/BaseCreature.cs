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
            MyVector2 newPosition = new MyVector2(Coordinates.X + Velocity.X * deltaTime, Coordinates.Y);
            Tile newTile = null;

            if (newPosition.X > Tile.Coordinates.X + 1 || newPosition.X < Tile.Coordinates.X)
            {
                newTile = Tile.Chunk.World.GlobalCordsToTile(newPosition.ToMyVector2Int());
                this.Tile.Creatures.Remove(this);
                this.Tile.Chunk.Creatures.Remove(this);
                this.Tile = newTile;
                newTile.Creatures.Add(this);
                newTile.Chunk.Creatures.Add(this);
            }

            if (!CheckForColision(this.Tile, newPosition))
            {
                this.Coordinates = newPosition;
            }

            newPosition = new MyVector2(Coordinates.X, Coordinates.Y + Velocity.Y * deltaTime);
            newTile = null;

            if (newPosition.Y > Tile.Coordinates.Y + 1 || newPosition.Y < Tile.Coordinates.Y)
            {
                newTile = Tile.Chunk.World.GlobalCordsToTile(newPosition.ToMyVector2Int());
                this.Tile.Creatures.Remove(this);
                this.Tile.Chunk.Creatures.Remove(this);
                this.Tile = newTile;
                newTile.Creatures.Add(this);
                newTile.Chunk.Creatures.Add(this);
            }

            if (!CheckForColision(this.Tile, newPosition))
            {
                this.Coordinates = newPosition;
            }
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
                    }
                }
            }
            return false;
        }
    }
}
