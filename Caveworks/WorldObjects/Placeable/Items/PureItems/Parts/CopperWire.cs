using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class CopperWire : BaseItem
    {
        public CopperWire(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public CopperWire(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.CopperWire;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
