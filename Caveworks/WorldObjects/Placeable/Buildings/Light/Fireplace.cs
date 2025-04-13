using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    public class Fireplace : BaseBuilding
    {
        const float BurnTime = 1200; // 20 minutes
        float FireTimer = 0;
        float Brightness;

        public Fireplace(Tile tile) : base(tile, 1)
        {
            Brightness = LightManager.MaxLightStrength;
        }


        public override bool HasCollision() { return true; } // can player collide with it


        public override int GetLightLevel() { return (int)Brightness; } // how bright is the building


        public override BaseItem ToItem()
        {
            if (Brightness > 0)
            {
                return new FireplaceItem(1);
            }
            else
            {
                return new RawStoneItem(1);
            }
        }


        public override void Update(float deltaTime)
        {
            FireTimer += deltaTime;
            if (FireTimer > 1)
            {
                FireTimer = 0;
            }

            Brightness -= (LightManager.MaxLightStrength * deltaTime) / BurnTime;
            if (Brightness < 0)
            {
                Brightness = 0;
            }
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            Texture2D texture;
            if (FireTimer < 0.34)
            {
                texture = Textures.Fireplace;
            }
            else if (FireTimer < 0.67)
            {
                texture = Textures.Fireplace2;
            }
            else
            {
                texture = Textures.Fireplace3;
            }

            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(texture, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);

            float overlayStrength = 1 - Brightness / LightManager.MaxLightStrength;
            if (overlayStrength > 0.5 && overlayStrength < 1) { overlayStrength = 0.5f; }
            Game.WallSpritebatch.Draw(Textures.FireplaceMask, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.FromNonPremultiplied(new Vector4(1, 1, 1, overlayStrength)));
        }
    }
}
