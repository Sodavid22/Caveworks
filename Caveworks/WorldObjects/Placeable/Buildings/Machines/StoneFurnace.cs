using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class StoneFurnace : BaseMachine
    {
        float FireTimer = 0;


        public StoneFurnace(Tile tile):base(tile, 2, 3, Globals.World.RecipeList.StoneFurnaceRecipes, 1)
        {

        }


        public override int GetSoundType()
        {
            if (Crafter.CraftingProgress > 0)
            {
                return 1;
            }
            return 0;
        }


        public override int GetSize() { return 2; }


        public override int GetLightLevel()
        {
            if (Crafter.CraftingProgress > 0)
            {
                return LightManager.MinLightForMaxBrightness;
            }
            return 0;
        }


        public override void Update(float deltaTime)
        {
            Crafter.Update(deltaTime);
            FireTimer += deltaTime;
            if (FireTimer > 1)
            {
                FireTimer = 0;
            }
        }


        public override BaseItem ToItem()
        {
            return new StoneFurnaceItem(1);
        }


        public override void Draw(Camera camera, float deltaTime)
        {
            Texture2D texture = Textures.StoneFurnace;
            if (Crafter.CraftingProgress > 0)
            {
                if (FireTimer < 0.34)
                {
                    texture = Textures.StoneFurnaceLit;
                }
                else if (FireTimer < 0.67)
                {
                    texture = Textures.StoneFurnaceLit2;
                }
                else
                {
                    texture = Textures.StoneFurnaceLit3;
                }
            }

            MyVector2Int screenCoordinates = camera.WorldToScreenCords(Position);
            Game.WallSpritebatch.Draw(texture, new Rectangle(screenCoordinates.X, screenCoordinates.Y, camera.Scale * 2, camera.Scale * 2), Color.White);
        }
    }
}
