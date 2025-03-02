using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Caveworks
{
    [Serializable]
    public class BaseBelt : BaseBuilding
    {
        new public static int Size = 1;
        public const int MaxItems = 4;
        public const float MinItemDistance = 0.24f;
        public const float MaxCenterDistance = 0.01f;



        public BaseBelt(Tile tile, MyVector2Int rotation) : base(tile, BaseBelt.Size)
        {
            this.Rotation = rotation;
            Collisions = true;
        }


        public void UpdateBelt(float deltaTime, float BeltSpeed)
        {
            List<BaseItem> itemList = Tile.Items.ToList();

            foreach (var item in itemList)
            {
                bool collided = false;

                if (item.CheckForNewTile())
                {
                    Tile nextTile = Tile.Chunk.World.GetTileByRelativePosition(Tile, Rotation);
                    if (nextTile.Building != null)
                    {
                        if (nextTile.Building.GetType().Equals(this.GetType()) && nextTile.Items.Count < MaxItems)
                        {
                            item.UpdateTile();
                        }
                        else { collided = true; }
                    }
                    else{ collided = true; }
                }

                if (Rotation.X == 0)
                {
                    foreach (var otherItem in itemList)
                    {
                        if (otherItem.Coordinates.Y != item.Coordinates.Y)
                        {
                            if (Rotation.Y == 1)
                            {
                                if (otherItem.Coordinates.Y - item.Coordinates.Y > 0 && otherItem.Coordinates.Y - item.Coordinates.Y < MinItemDistance)
                                {
                                    collided = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (otherItem.Coordinates.Y - item.Coordinates.Y < 0 && otherItem.Coordinates.Y - item.Coordinates.Y > -MinItemDistance)
                                {
                                    collided = true;
                                    break;
                                }
                            }
                        }
                        else if (otherItem != item)
                        {
                            otherItem.Coordinates.Y -= Rotation.Y * 0.5f;
                        }
                    }
                    if (!collided)
                    {
                        item.Coordinates.Y += Rotation.Y * BeltSpeed * deltaTime;
                    }

                    if (item.Coordinates.X - (Position.X + 0.5f) < -MaxCenterDistance)
                    {
                        item.Coordinates.X += BeltSpeed * deltaTime;
                    }
                    else if (item.Coordinates.X - (Position.X + 0.5f) > MaxCenterDistance)
                    {
                        item.Coordinates.X -= BeltSpeed * deltaTime;
                    }
                }

                if (Rotation.Y == 0)
                {
                    foreach (var otherItem in itemList)
                    {
                        if (otherItem.Coordinates.X != item.Coordinates.X)
                        {
                            if (Rotation.X == 1)
                            {
                                if (otherItem.Coordinates.X - item.Coordinates.X > 0 && otherItem.Coordinates.X - item.Coordinates.X < MinItemDistance)
                                {
                                    collided = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (otherItem.Coordinates.X - item.Coordinates.X < 0 && otherItem.Coordinates.X - item.Coordinates.X > -MinItemDistance)
                                {
                                    collided = true;
                                    break;
                                }
                            }
                        }
                        else if (otherItem != item)
                        {
                            otherItem.Coordinates.Y -= Rotation.Y * 0.5f;
                        }
                    }
                    if (!collided)
                    {
                        item.Coordinates.X += Rotation.X * BeltSpeed * deltaTime;
                    }

                    if (item.Coordinates.Y - (Position.Y + 0.5f) < -MaxCenterDistance)
                    {
                        item.Coordinates.Y += BeltSpeed * deltaTime;
                    }
                    else if (item.Coordinates.Y - (Position.Y + 0.5f) > MaxCenterDistance)
                    {
                        item.Coordinates.Y -= BeltSpeed * deltaTime;
                    }
                }
            }
        }
    }
}
