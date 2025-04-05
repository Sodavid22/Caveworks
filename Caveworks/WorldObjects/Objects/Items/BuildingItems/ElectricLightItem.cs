using Microsoft.Xna.Framework.Graphics;
using System;


namespace Caveworks
{
    [Serializable]
    public class ElectricLightItem : BaseItem
    {
        public ElectricLightItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }

        public ElectricLightItem(int count) : base(count) { }


        public override Texture2D GetTexture()
        {
            return Textures.ElectricLight;
        }


        public override bool PrimaryUse(MyVector2Int itemRotation)
        {
            Tile tile = Globals.World.MouseTile;

            if (tile.Wall == null & tile.Building == null & Globals.World.PlayerBody.Tile != tile)
            {
                new ElectricLight(tile);
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
