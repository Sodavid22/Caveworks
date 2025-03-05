using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class SlowBeltItem : BaseItem
    {
        public SlowBeltItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public SlowBeltItem(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.SlowBelt;
        }


        public override bool PrimaryUse(MyVector2Int itemRotation)
        {
            Tile tile = Globals.World.MouseTile;

            if (tile.Wall == null & tile.Building == null)
            {
                new SlowBelt(tile, new MyVector2Int(itemRotation.X, itemRotation.Y));
                Sounds.PlayPlaceSound();
                Count -= 1;
                if (Count == 0)
                {
                    Globals.World.Player.HeldItem = null;
                }
                return true;
            }
            return false;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
