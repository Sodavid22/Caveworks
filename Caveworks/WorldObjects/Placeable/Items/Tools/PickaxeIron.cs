using Caveworks.WorldObjects.Placeable.Items.Tools;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    internal class PickaxeIron : BasePickaxe
    {
        public PickaxeIron(Tile tile, MyVector2 position, int count) : base(tile, position, count) { }
        public PickaxeIron(int count) : base(count) { }

        public override bool PrimaryUse(MyVector2Int itemRotation)
        {
            bool used = base.UsePickaxe(2, 1, 4);
            Random random = new Random();
            if (used && random.NextDouble() <= 0.05)
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
            return Textures.PickaxeIron;
        }


        public override void Draw(Tile tile, Camera camera)
        {
            base.DrawSimple(tile, camera, GetTexture());
        }
    }
}
