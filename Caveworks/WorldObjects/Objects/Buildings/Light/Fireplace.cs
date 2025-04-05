using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    public class Fireplace : BaseBuilding
    {
        public Fireplace(Tile tile) : base(tile, 1) { }


        public override bool HasCollision() { return true; } // can player collide with it


        public override int GetLightLevel() { return LightManager.MaxLightStrength - 4000; } // how bright is the building


        public override BaseItem ToItem()
        {
            return new FireplaceItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(Textures.Fireplace, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
