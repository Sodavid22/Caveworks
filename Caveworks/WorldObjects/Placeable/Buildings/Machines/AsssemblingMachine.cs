using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Caveworks
{
    [Serializable]
    public class AsssemblingMachine : BaseMachine
    {
        public AsssemblingMachine(Tile tile):base(tile, 3, 4, Globals.World.RecipeList.AssemblingMachineRecipes, 1) { }


        public override int GetSize() { return 3; }


        public override int GetSoundType()
        {
            if (Crafter.CraftingProgress > 0)
            {
                return 2;
            }
            return 0;
        }


        public override void Update(float deltaTime)
        {
            Crafter.Update(deltaTime);
        }


        public override BaseItem ToItem()
        {
            return new AsseblingMachineItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            Texture2D texture;
            if (Crafter.CraftingProgress > 0)
            {
                texture = Textures.AssemblingMachineWorking;
            }
            else
            {
                texture = Textures.AssemblingMachine;
            }
            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(texture, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale * 3, camera.Scale * 3), Color.White);
        }
    }
}
