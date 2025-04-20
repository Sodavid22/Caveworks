using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class ElectricEngine : BaseItem
    {
        public ElectricEngine(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public ElectricEngine(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.ElectricEngine;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
