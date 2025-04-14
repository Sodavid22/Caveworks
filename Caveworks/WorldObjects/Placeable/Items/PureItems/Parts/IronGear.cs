using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class IronGear : BaseItem
    {
        public IronGear(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public IronGear(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.IronGear;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
