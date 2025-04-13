using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class RawCopperOreItem : BaseItem
    {
        public RawCopperOreItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public RawCopperOreItem(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.RawCopperOre;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
