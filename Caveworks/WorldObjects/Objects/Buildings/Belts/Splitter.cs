using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Caveworks
{
    [Serializable]
    public class Splitter : BaseBuilding
    {
        public const float Offset = 0.01f;
        public BaseItem Item;
        public MyVector2Int Direction;
        public BaseBuilding AcceptedBuilding;


        public Splitter(Tile tile) : base(tile, 1)
        {
            Direction = new MyVector2Int(1, 0);
        }


        public override bool HasCollision()
        {
            return true;
        }


        public override bool IsTransportBuilding()
        {
            return true;
        }


        public override BaseItem ToItem()
        {
            if (Item != null)
            {
                Item.UpdateTiles(Tile);
            }
            return new SplitterItem(1);
        }


        public override bool AccteptsItems(BaseBuilding building)
        {
            if (Item == null && Tile.Items.Count == 0)
            {
                AcceptedBuilding = building;
                return true;
            }
            return false;
        }


        public override void Update(float deltaTime)
        {
            if (Tile.Items.Count > 0)
        {
                Item = Tile.Items[0];
                Item.Coordinates.X = Tile.Position.X + 0.5f;
                Item.Coordinates.Y = Tile.Position.Y + 0.5f;
                Tile.Items[0].RemoveFromTile(Tile);
            }

            if (Item != null)
            {
                Tile targetTile = Globals.World.GetTileByRelativePosition(Tile, Direction);
                if (targetTile.Building != null)
                {
                    if (targetTile.Building.IsTransportBuilding() && targetTile.Building != AcceptedBuilding && targetTile.Building.AccteptsItems(this))
                    {
                        if (targetTile.Building.Rotation != null)
                        {
                            if (Globals.World.GetTileByRelativePosition(targetTile, targetTile.Building.Rotation) != this.Tile) // not facing this
                            {
                                Item.Coordinates.X += (float)Direction.X / 2 + Offset * Direction.X;
                                Item.Coordinates.Y += (float)Direction.Y / 2 + Offset * Direction.Y;
                                Item.UpdateTiles(Tile);
                                Item = null;
                            }
                        }
                        else
                        {
                            Item.Coordinates.X += (float)Direction.X / 2 + Offset * Direction.X;
                            Item.Coordinates.Y += (float)Direction.Y / 2 + Offset * Direction.Y;
                            Item.UpdateTiles(Tile);
                            Item = null;
                        }
                    }
                }
                ChangeRotation();
            }
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(Textures.Splitter, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }


        private void ChangeRotation()
        {
            if (Direction.X == 1) // right to down
            {
                Direction.X = 0;
                Direction.Y = 1;
            }
            else if (Direction.Y == 1) // down to left
            {
                Direction.X = -1;
                Direction.Y = 0;
            }
            else if (Direction.X == -1) // left to up
            {
                Direction.X = 0;
                Direction.Y = -1;
            }
            else // up to right
            {
                Direction.X = 1;
                Direction.Y = 0;
            }
        }
    }
}
