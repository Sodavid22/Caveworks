using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace Caveworks
{
    [Serializable]
    public class ResearchLabItem : BaseItem
    {
        public ResearchLabItem(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public ResearchLabItem(int count) : base(count) { }


        public override bool CanRotate()
        {
            return false;
        }


        public override Texture2D GetTexture()
        {
            return Textures.ResearchLab;
        }


        public override bool PrimaryUse(MyVector2Int itemRotation)
        {
            Tile tile = Globals.World.MouseTile;
            Tile checkedTile;
            bool canPlace = true;

            for (int x = tile.Position.X - 1; x <= tile.Position.X + 1; x++)
            {
                for (int y = tile.Position.Y - 1; y <= tile.Position.Y + 1; y++)
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
                new ResearchLab(Globals.World.GetTileByRelativePosition(tile, new MyVector2Int(-1, -1)));
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
