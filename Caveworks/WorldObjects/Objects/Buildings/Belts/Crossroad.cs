using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Caveworks
{
    [Serializable]
    public class Crossroad : BaseBuilding
    {
        public const float Offset = 0.01f;
        public BaseItem XItem;
        public BaseItem YItem;


        public Crossroad(Tile tile) : base(tile, 1) { }


        public override bool HasCollision()
        {
            return true;
        }


        public override BaseItem ToItem()
        {
            if (XItem != null)
            {
                XItem.UpdateTiles(Tile);
            }
            if (YItem != null)
            {
                YItem.UpdateTiles(Tile);
            }
            return new CrossroadItem(1);
        }


        public override bool AccteptsItems(BaseBuilding building)
        {
            if (building.Position.Y == Tile.Position.Y && XItem == null)
            {
                return true;
            }
            if (building.Position.X == Tile.Position.X && YItem == null)
            {
                return true;
            }
            return false;
        }


        public override void Update(float deltaTime)
        {
            if (Tile.Items.Count > 0)
            {
                if (MathF.Abs(Tile.Position.X + 0.5f - Tile.Items[0].Coordinates.X) > MathF.Abs(Tile.Position.Y + 0.5f - Tile.Items[0].Coordinates.Y)) // displaced on X axis
                {
                    XItem = Tile.Items[0];
                    Tile.Items[0].RemoveFromTile(Tile);
                }
                else  // displaced on Y axis
                {
                    YItem = Tile.Items[0];
                    Tile.Items[0].RemoveFromTile(Tile);
                }
            }

            if (XItem != null)
            {
                if (XItem.Coordinates.X < Tile.Position.X + 0.5f) // left to right
                {
                    if (Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(1, 0)).Items.Count < 4)
                    {
                        XItem.Coordinates.X = Tile.Position.X + 1 + Offset;
                        XItem.UpdateTiles(Tile);
                        XItem = null;
                    }
                }
                else if (XItem.Coordinates.X > Tile.Position.X + 0.5f) // right to left
                {
                    if (Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(-1, 0)).Items.Count < 4)
                    {
                        XItem.Coordinates.X = Tile.Position.X - Offset;
                        XItem.UpdateTiles(Tile);
                        XItem = null;
                    }
                }
            }
            if (YItem != null)
            { 
                if (YItem.Coordinates.Y < Tile.Position.Y + 0.5f) // up to down
                {
                    if (Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(0, 1)).Items.Count < 4)
                    {
                        YItem.Coordinates.Y = Tile.Position.Y + 1 + Offset;
                        YItem.UpdateTiles(Tile);
                        YItem = null;
                    }
                }
                else if (YItem.Coordinates.Y > Tile.Position.Y + 0.5f) // down to up
                {
                    if (Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(0, -1)).Items.Count < 4)
                    {
                        YItem.Coordinates.Y = Tile.Position.Y - Offset;
                        YItem.UpdateTiles(Tile);
                        YItem = null;
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
