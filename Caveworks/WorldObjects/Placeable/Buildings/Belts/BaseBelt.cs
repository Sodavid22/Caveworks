using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Caveworks
{
    [Serializable]
    public class BaseBelt : BaseBuilding
    {
        public const int MaxItems = 4;
        public const float MinItemDistance = 0.24f;
        public const float MaxCenterDistance = 0.01f;



        public BaseBelt(Tile tile, MyVector2Int rotation) : base(tile, 1)
        {
            this.Rotation = rotation;
        }


        public override bool IsTransportBuilding()
        {
            return true;
        }


        public override bool AccteptsItems(BaseBuilding building)
        {
            if (Tile.Items.Count < MaxItems)
            {
                return true;
            }
            return false;
        }


        public void UpdateBelt(float deltaTime, float BeltSpeed)
        {
            TryToGetItem();

            List<BaseItem> itemList = Tile.Items.ToList();

            foreach (var item in itemList)
            {
                bool collided = false;

                if (item.CheckForNewTile(Tile))
                {
                    collided = !TryToGiveItem(item); // if you cant give item act like there is a wall
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


        private bool TryToGiveItem(BaseItem item)
        {
            Tile nextTile = Globals.World.GetTileByRelativePosition(Tile, Rotation);
            if (nextTile.Building != null)
            {
                if (nextTile.Building.AccteptsItems(this))
                {
                    if (nextTile.Building.Inventory == null) // no inventory
                    {
                        item.UpdateTiles(Tile);
                        return true;
                    }
                    else if (nextTile.Building.Crafter == null) // only inventory
                    {
                        if (nextTile.Building.Inventory.TryAddItem(item))
                        {
                            item.RemoveFromTile(Tile);
                        }
                    }
                    else
                    {
                        if (nextTile.Building.Crafter.SelectedRecipe != null)
                        {
                            if (nextTile.Building.Crafter.SelectedRecipe.ItemIsIngredient(item))
                            {
                                if (nextTile.Building.Inventory.CountItems(item) < BaseMachine.ItemLimit)
                                {
                                    if (nextTile.Building.Inventory.TryAddItem(item))
                                    {
                                        item.RemoveFromTile(Tile);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }


        private void TryToGetItem()
        {
            Tile backTile = Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(-Rotation.X, -Rotation.Y));

            if (Tile.Items.Count < MaxItems)
            {
                if (backTile.Building != null)
                {
                    if (backTile.Building.Inventory != null)
                    {
                        if (backTile.Building.Crafter != null) // assembling machine
                        {
                            if (backTile.Building.Crafter.SelectedRecipe != null)
                            {
                                if (backTile.Building.Inventory.CountItems(backTile.Building.Crafter.SelectedRecipe.Result) > 0)
                                {
                                    BaseItem newItem = Cloning.DeepClone(backTile.Building.Crafter.SelectedRecipe.Result);
                                    newItem.Count = 1;
                                    backTile.Building.Inventory.RemoveItems(newItem);
                                    AddItem(newItem);
                                }
                            }
                        }
                        else // storage building
                        {
                            if (backTile.Building.Inventory.GetFirstItem() != null)
                            {
                                BaseItem newItem = Cloning.DeepClone(backTile.Building.Inventory.GetFirstItem());
                                newItem.Count = 1;
                                backTile.Building.Inventory.RemoveItems(newItem);
                                AddItem(newItem);
                            }
                        }
                    }
                }
            }
        }


        private void AddItem(BaseItem item)
        {
            if (Rotation.X == 0) // up or down
            {
                item.Coordinates.X = Tile.Position.X + 0.5f;
                if (Rotation.Y == 1) // down
                {
                    item.Coordinates.Y = Tile.Position.Y;
                }
                else // down
                {
                    item.Coordinates.Y = Tile.Position.Y + 1;
                }
            }
            else // left or right
            {
                item.Coordinates.Y = Tile.Position.Y + 0.5f;
                if (Rotation.X == 1) // right
                {
                    item.Coordinates.X = Tile.Position.X;
                }
                else // left
                {
                    item.Coordinates.X = Tile.Position.X + 1;
                }
            }
            item.AddToTile(Tile);
        }
    }
}
