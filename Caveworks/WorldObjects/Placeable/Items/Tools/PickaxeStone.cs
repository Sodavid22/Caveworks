using Caveworks.WorldObjects.Placeable.Items.Tools;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class PickaxeStone : BasePickaxe
    {
        public PickaxeStone(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public PickaxeStone(int count) : base(count) { }

        public override bool PrimaryUse(MyVector2Int itemRotation)
        {
            bool used = base.UsePickaxe(1, 1);
            Random random = new Random();
            if (used && random.NextDouble() <= 0.1)
            {
                Count -= 1;
                if (Count == 0)
                {
                    Globals.World.Player.HeldItem = null;
                }
            }
            return used;
        }

        public override Texture2D GetTexture()
        {
            return Textures.PickaxeStone;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
