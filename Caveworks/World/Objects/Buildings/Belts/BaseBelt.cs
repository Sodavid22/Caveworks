using System;
using System.Linq;

namespace Caveworks
{
    [Serializable]
    public class BaseBelt : BaseBuilding
    {
        public static float BeltSpeed;


        public BaseBelt(Tile tile, MyVector2Int rotation) : base(tile)
        {
            this.Rotation = rotation;
            Collisions = true;
        }


        public override void Update(float deltaTime)
        {
            foreach (var item in Tile.Items.ToList())
            {
                if (Rotation.X == 0 && Math.Abs(item.Coordinates.X - (Position.X + 0.5f)) > 0.1)
                {
                    if (item.Coordinates.X > Position.X + 0.5f)
                    {
                        item.Move(new MyVector2(-BeltSpeed * deltaTime, 0));
                    }
                    else
                    {
                        item.Move(new MyVector2(BeltSpeed * deltaTime, 0));
                    }
                }
                if (Rotation.Y == 0 && Math.Abs(item.Coordinates.Y - (Position.Y + 0.5f)) > 0.1)
                {
                    if (item.Coordinates.Y > Position.Y + 0.5f)
                    {
                        item.Move(new MyVector2(0, -BeltSpeed * deltaTime));
                    }
                    else
                    {
                        item.Move(new MyVector2(0, BeltSpeed * deltaTime));
                    }
                }

                item.Move(new MyVector2(Rotation.X * BeltSpeed * deltaTime, Rotation.Y * BeltSpeed * deltaTime));
            }
        }
    }
}
