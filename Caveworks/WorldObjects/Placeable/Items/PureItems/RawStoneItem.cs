using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class RawStoneItem : BaseItem
    {
        public RawStoneItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public RawStoneItem(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.RawStone;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
