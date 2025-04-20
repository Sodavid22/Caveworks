using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Caveworks
{
    [Serializable]
    public class Recipe
    {
        public BaseItem[] Ingredients;
        public BaseItem Result;
        public float CraftingTime;


        public const int RowLength = 10;
        public const int BoxSize = 64;
        public const int BoxSpacing = 68;
        public const int BoxOffset = 4;
        public const int Border = 2;
        public const int BorderOffset = 20;

        public Recipe(BaseItem[] ingredients, BaseItem result, float time)
        {
            Ingredients = ingredients;
            Result = result;
            CraftingTime = time;
        }


        public bool ItemIsIngredient(BaseItem item)
        {
            foreach (BaseItem ingredient in Ingredients)
            {
                if (ingredient.GetType() == item.GetType())
                {
                    return true;
                }
            }
            return false;
        }


        public void Draw(Vector2 position, float craftingSpeed)
        {
            // background
            int width = BoxSpacing * Ingredients.Length + Border * 2 + BoxOffset * 2;
            int height = BoxSpacing + Border * 2 + BorderOffset + BoxOffset * 2;
            Rectangle backRectangle = new Rectangle((int)position.X - Border, (int)position.Y - Border, width, height);
            Rectangle frontRectangle = new Rectangle(backRectangle.X + Border, backRectangle.Y + Border, backRectangle.Width - Border * 2, backRectangle.Height - Border * 2);
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, backRectangle, Color.Black);
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, frontRectangle, Color.FromNonPremultiplied(Globals.InventoryBoxColor));

            Rectangle boxRectangle;
            Vector2 textSize;
            for (int i = 0; i < Ingredients.Length; i++) // draw buttons
            {
                boxRectangle = new Rectangle(frontRectangle.X + (BoxSpacing * i) + Border + BoxOffset, frontRectangle.Y + Border + BoxOffset, BoxSize, BoxSize);
                textSize = Fonts.MediumFont.MeasureString(Ingredients[i].Count.ToString());
                Game.MainSpriteBatch.Draw(Ingredients[i].GetTexture(), boxRectangle, Color.White);
                Game.MainSpriteBatch.DrawString(Fonts.MediumFont, Ingredients[i].Count.ToString(), new Vector2((int)(boxRectangle.X + BoxSize/2 - textSize.X/2), (int)(boxRectangle.Y + BoxSize/2 - textSize.Y/2)), Color.Black);
            }

            Game.MainSpriteBatch.DrawString(Fonts.SmallFont, CraftingTime / craftingSpeed + " sec", new Vector2((int)(frontRectangle.X + BoxOffset * 2), (int)(frontRectangle.Y + BoxSpacing + BoxOffset)), Color.Black);
        }
    }
}
