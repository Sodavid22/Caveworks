using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks.WorldObjects.Objects.Buildings.Storage
{
    class IronChest : BaseBuilding
    {

        public IronChest(Tile tile) : base(tile, 1)
        {
            Inventory = new Inventory(10, Globals.World.Player);
        }


        public override bool HasUI() { return true; }


        public override bool HasCollision() { return true; }


        public override BaseItem ToItem()
        {
            return new IronChestItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(new MyVector2(Position.X, Position.Y));
            Game.FloorSpriteBatch.Draw(Textures.IronChest, new Rectangle((int)screenCoordinates.X, (int)screenCoordinates.Y, camera.Scale, camera.Scale), Color.White);
        }
    }
}
