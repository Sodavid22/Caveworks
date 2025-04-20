using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class IronPipe: BaseItem
    {
        public IronPipe(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public IronPipe(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.IronPipe;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
