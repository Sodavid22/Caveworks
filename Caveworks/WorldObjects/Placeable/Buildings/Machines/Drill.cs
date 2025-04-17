using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Caveworks
{
    [Serializable]
    public class Drill : BaseBuilding
    {
        public const float DrillSpeed = 1;
        public float DrillTimer = 0;


        public Drill(Tile tile, MyVector2Int rotation) : base(tile, 1)
        {
            this.Rotation = rotation;
        }


        public override bool HasCollision() { return true; }


        public override BaseItem ToItem()
        {
            return new DrillItem(1);
        }


        public override void Update(float deltaTime)
        {
            DrillTimer += DrillSpeed * deltaTime;

            Tile wallTile = Globals.World.GetTileByRelativePosition(Tile, Rotation);
            Tile dropTile = Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(-Rotation.X, -Rotation.Y));


            if (wallTile.Wall != null)
            {
                if (DrillTimer > wallTile.Wall.GetDrillTime())
                {
                    if (dropTile.Wall == null && dropTile.Building != null)
                    {
                        if (dropTile.Building.IsTransportBuilding() && dropTile.Building.AccteptsItems(this))
                        {
                            BaseItem item = wallTile.Wall.GetItem(wallTile);
                            item.Coordinates = new MyVector2(Tile.Position.X + 0.5f - Rotation.X * 0.6f, Tile.Position.Y + 0.5f - Rotation.Y * 0.6f);
                            item.AddToTile(dropTile);
                            DrillTimer = 0;
                        }
                    }
                }
            }
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(new MyVector2(Position.X + 0.5f, Position.Y + 0.5f));
            float rotation = MathF.Atan2(Rotation.Y, Rotation.X);
            Game.WallSpritebatch.Draw(Textures.Drill, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), new Rectangle(0, 0, 16, 16), Color.White, rotation, new Vector2(8, 8), SpriteEffects.None, 0);
        }
    }
}
