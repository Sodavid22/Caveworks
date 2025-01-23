using SharpDX.Direct3D11;
using System;

namespace Caveworks
{
    [Serializable]
    public class BaseCreature
    {
        public Tile Tile;
        public MyVector2 Position;
        public MyVector2 Velocity;
        public float Rotation;

        public float HitboxSize;
        public int Health;
        public float MaxSpeed;


        public BaseCreature(Tile tile, MyVector2 position)
        {
            this.Position = position;
            this.Velocity = new MyVector2(0, 0);
            this.Rotation = 0;
        }

        public void Update()
        {

        }
        /*
        public bool Move()
        {
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;

            if (Position.X > 1)
            {
                Position.X -= 1;
                if (Tile.ChunkCoordinates)
            }
        }

        private bool CheckForColision(MyVector2 position)
        {

        }
        */
    }
}
