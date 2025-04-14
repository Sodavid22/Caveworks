using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class IronPlate : BaseItem
    {
        public IronPlate(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public IronPlate(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.IronPlate;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
