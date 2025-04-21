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
        public float TextureTimer = 0;
        Tile WallTile;
        Tile DropTile;


        public Drill(Tile tile, MyVector2Int rotation) : base(tile, 1)
        {
            this.Rotation = rotation;
            WallTile = Globals.World.GetTileByRelativePosition(Tile, Rotation);
            DropTile = Globals.World.GetTileByRelativePosition(Tile, new MyVector2Int(-Rotation.X, -Rotation.Y));
        }


        public override bool HasCollision() { return true; }


        public override int GetSoundType()
        {
            if (WallTile != null)
            {
                return 4;
            }
            return 0;
        }


        public override BaseItem ToItem()
        {
            return new DrillItem(1);
        }


        public override void Update(float deltaTime)
        {
            DrillTimer += DrillSpeed * deltaTime;

            if (WallTile.Wall != null)
            {
                TextureTimer += deltaTime * 3;
                if (TextureTimer > 1)
                {
                    TextureTimer = 0.01f;
                }
                if (DrillTimer > WallTile.Wall.GetDrillTime())
                {
                    if (DropTile.Wall == null && DropTile.Building != null)
                    {
                        if (DropTile.Building.IsTransportBuilding() && DropTile.Building.AccteptsItems(this))
                        {
                            BaseItem item = WallTile.Wall.GetItem(WallTile);
                            item.Coordinates = new MyVector2(Tile.Position.X + 0.5f - Rotation.X * 0.6f, Tile.Position.Y + 0.5f - Rotation.Y * 0.6f);
                            item.AddToTile(DropTile);
                            DrillTimer = 0;
                        }
                    }
                }
            }
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            Texture2D texture;

            if (TextureTimer < 0.34)
            {
                texture = Textures.Drill;
            }
            else if (TextureTimer < 0.67)
            {
                texture = Textures.Drill2;
            }
            else
            {
                texture = Textures.Drill3;
            }

            MyVector2Int screenCoordinates = camera.WorldToScreenCords(new MyVector2(Position.X + 0.5f, Position.Y + 0.5f));
            float rotation = MathF.Atan2(Rotation.Y, Rotation.X);
            Game.WallSpritebatch.Draw(texture, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), new Rectangle(0, 0, 16, 16), Color.White, rotation, new Vector2(8, 8), SpriteEffects.None, 0);
        }
    }
}
