using Microsoft.Xna.Framework;
using System;

namespace Caveworks
{
    [Serializable]
    internal class RawIronOre : BaseItem
    {

        public RawIronOre(Tile tile, MyVector2 position, int count) : base(tile, position, count) 
        {
            StackSize = 100;
        }

        public override void Draw(Tile tile, Camera camera)
        {
            MyVector2 screenCoordinates = camera.WorldToScreenCords(new MyVector2(Coordinates.X - 0.25f, Coordinates.Y - 0.25f));
            Game.ItemSpritebatch.Draw(Textures.RawIronOre, new Rectangle((int)(screenCoordinates.X), (int)(screenCoordinates.Y), camera.Scale / 2, camera.Scale / 2), Color.White);
        }
    }
}
