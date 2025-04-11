using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class CrossroadItem : BaseItem
    {
        public CrossroadItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public CrossroadItem(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.Crossroad;
        }


        public override bool PrimaryUse(MyVector2Int itemRotation)
        {
            Tile tile = Globals.World.MouseTile;

            if (tile.Wall == null & tile.Building == null & Globals.World.PlayerBody.Tile != tile)
            {
                new Crossroad(tile);
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
