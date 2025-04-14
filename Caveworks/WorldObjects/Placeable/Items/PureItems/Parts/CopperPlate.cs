using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class CopperPlate : BaseItem
    {
        public CopperPlate(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public CopperPlate(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.CopperPlate;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
