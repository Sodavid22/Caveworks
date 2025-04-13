using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace Caveworks
{
    [Serializable]
    public class StoneFurnaceItem : BaseItem
    {
        public StoneFurnaceItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public StoneFurnaceItem(int count) : base(count) { }


        public override bool CanRotate()
        {
            return false;
        }


        public override Texture2D GetTexture()
        {
            return Textures.StoneFurnace;
        }


        public override bool PrimaryUse(MyVector2Int itemRotation)
        {
            Tile tile = Globals.World.MouseTile;
            Tile checkedTile;
            bool canPlace = true;

            for (int x = tile.Position.X; x <= tile.Position.X + 1; x++)
            {
                for (int y = tile.Position.Y; y <= tile.Position.Y + 1; y++)
                {
                    checkedTile = Globals.World.GlobalCordsToTile(new MyVector2Int(x, y));
                    if (checkedTile.Wall != null || checkedTile.Building != null || checkedTile == Globals.World.PlayerBody.Tile)
                    {
                        canPlace = false;
                    }
                }
            }
            if (canPlace)
            {
                new StoneFurnace(tile);
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
