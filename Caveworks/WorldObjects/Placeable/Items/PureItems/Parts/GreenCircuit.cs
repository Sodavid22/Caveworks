using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class GreenCircuit : BaseItem
    {
        public GreenCircuit(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public GreenCircuit(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.GreenCircuit;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
