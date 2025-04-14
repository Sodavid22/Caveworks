using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class RawCoalItem : BaseItem
    {
        public RawCoalItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public RawCoalItem(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.RawCoal;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
