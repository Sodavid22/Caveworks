using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class RawIronOreItem : BaseItem
    {
        public RawIronOreItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public RawIronOreItem(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.RawIronOre;
        }


        public override bool SecondaryUse(MyVector2Int itemRotation)
        {
            Drop(this);
            return true;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
