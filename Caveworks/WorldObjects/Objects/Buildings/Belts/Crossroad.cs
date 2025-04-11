using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class Crossroad : BaseBuilding
    {
        public const float Offset = 0.01f;


        public Crossroad(Tile tile) : base(tile, 1) { }


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
            return new CrossroadItem(1);
        }


        public override bool AccteptsItems(BaseBuilding building)
        {
            Tile targetTile;
            if (building.Position.Y == Tile.Position.Y && Tile.Items.Count == 0) // left right
            {
                targetTile = Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(Tile.Position.X - building.Position.X, 0));
                if (targetTile.Building != null)
                {
                    if (targetTile.Building.IsTransportBuilding() && targetTile.Building.AccteptsItems(this))
                    {
                        return true;
                    }
                }
            }
            if (building.Position.X == Tile.Position.X && Tile.Items.Count == 0 ) // up down
            {
                targetTile = Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(0, Tile.Position.Y - building.Position.Y));
                if (targetTile.Building != null)
                {
                    if (targetTile.Building.IsTransportBuilding() && targetTile.Building.AccteptsItems(this))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public override void Update(float deltaTime)
        {
            foreach (BaseItem item in Tile.Items.ToArray())
            {
                if (MathF.Abs(Tile.Position.X + 0.5f - item.Coordinates.X) > MathF.Abs(Tile.Position.Y + 0.5f - item.Coordinates.Y)) // X axis or Y axis
                {
                    if (item.Coordinates.X < Tile.Position.X + 0.5f) // left to right
                    {
                        item.Coordinates.X = Tile.Position.X + 1 + Offset;
                        item.UpdateTiles(Tile);
                    }
                    else if (item.Coordinates.X > Tile.Position.X + 0.5f) // right to left
                    {
                        item.Coordinates.X = Tile.Position.X - Offset;
                        item.UpdateTiles(Tile);
                    }
                }
                else
                {
                    if (item.Coordinates.Y < Tile.Position.Y + 0.5f) // up to down
                    {
                        item.Coordinates.Y = Tile.Position.Y + 1 + Offset;
                        item.UpdateTiles(Tile);
                    }
                    else if (item.Coordinates.Y > Tile.Position.Y + 0.5f) // down to up
                    {
                        item.Coordinates.Y = Tile.Position.Y - Offset;
                        item.UpdateTiles(Tile);
                    }
                }
            }
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(Textures.Crossroad, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
