using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace Caveworks
{
    [Serializable]
    public class RecipeCrafter
    {
        public const int RowLength = 10;
        public const int ButtonSize = 72;
        public const int ButtonSpacing = 80;
        public const int Border = 2;
        public const int BorderOffset = 12;

        public const int ProgressbarOffset = 10;
        public const int ProgressbarScale = 40;

        MyVector2Int WindowPosition;
        public Inventory Inventory;
        public List<Recipe> RecipeList;
        public Recipe SelectedRecipe;
        public int SelectedRecipePosition;
        public float CraftingProgress;

        [NonSerialized]
        Button[] Buttons;


        public RecipeCrafter(List<Recipe> recipeList, Inventory inventory)
        {
            RecipeList = recipeList;
            Inventory = inventory;
            SelectedRecipe = null;
            SelectedRecipePosition = -1;
            CraftingProgress = 0;
        }


        public void Update(float deltaTime) // called every frame
        {
            if (SelectedRecipe != null)
            {
                bool canCraft = true;

                foreach (BaseItem recipeItem in SelectedRecipe.Ingredients)
                {
                    if (Inventory.CountItems(recipeItem) < recipeItem.Count)
                    {
                        canCraft = false;
                        break;
                    }
                }

                if (canCraft)
                {
                    CraftingProgress += deltaTime;
                }

                if (CraftingProgress > SelectedRecipe.CraftingTime)
                {
                    if (Inventory.CountItems(SelectedRecipe.Result) < BaseMachine.ItemLimit)
                    {
                        if (Inventory.TryAddItem(Cloning.DeepClone(SelectedRecipe.Result)))
                        {
                            CraftingProgress = 0;
                            foreach (BaseItem recipeItem in SelectedRecipe.Ingredients)
                            {
                                Inventory.RemoveItems(recipeItem);
                            }
                        }
                    }
                }
            }
        }


        public void OpenUI(MyVector2Int position)
        {
            WindowPosition = position;
            Buttons = new Button[RecipeList.Count];

            for (int i = 0; i < RecipeList.Count; i++)
            {
                int x = i % RowLength;
                int y = i / RowLength;

                Buttons[i] = new Button(new Vector2(ButtonSize, ButtonSize), Globals.CraftingBoxColor, 2, RecipeList[i].Result.Count.ToString(), Fonts.MediumFont);
                Buttons[i].SetTexture(RecipeList[i].Result.GetTexture());
                Buttons[i].Place(new Vector2(position.X + x * ButtonSpacing, position.Y - y * ButtonSpacing - BorderOffset*2 - Border), Anchor.BottomLeft);
            }
        }


        public void CloseUI()
        {
            Buttons = new Button[RecipeList.Count];
        }


        public void UpdateUI() // only called when inventory is opened
        {
            for (int i = 0; i < RecipeList.Count; i++)
            {
                Buttons[i].Update();
            }

            for (int i = 0; i < RecipeList.Count; i++)
            {
                if (Buttons[i].IsPressed(MouseKey.Left))
                {
                    if (SelectedRecipe != RecipeList[i])
                    {
                        SelectedRecipe = RecipeList[i];
                        SelectedRecipePosition = i;
                        CraftingProgress = 0;
                    }
                    else
                    {
                        SelectedRecipe = null;
                        SelectedRecipePosition = -1;
                        CraftingProgress = 0;
                    }
                }
            }
        }


        public void DrawUI()
        {
            // background
            int width = ButtonSpacing * (RowLength - 1) + ButtonSize + Border * 2 + BorderOffset * 2;
            int height = ButtonSpacing * ((int)MathF.Ceiling(RecipeList.Count / (float)RowLength) - 1) + ButtonSize + Border * 2 + BorderOffset * 2 + ProgressbarOffset;
            Rectangle backRectangle = new Rectangle(WindowPosition.X - Border - BorderOffset, WindowPosition.Y - Border - BorderOffset - height, width, height);
            Rectangle frontRectangle = new Rectangle(backRectangle.X + Border, backRectangle.Y + Border, backRectangle.Width - Border*2, backRectangle.Height - Border*2);
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, backRectangle, Color.Black);
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, frontRectangle, Color.FromNonPremultiplied(Globals.InventoryBoxColor));
            
            if (SelectedRecipe != null)// crafting progress
            {
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(frontRectangle.X, frontRectangle.Y, (int)(SelectedRecipe.CraftingTime * ProgressbarScale + Border), ProgressbarOffset), Color.Black);
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(frontRectangle.X, frontRectangle.Y, (int)(Math.Min(CraftingProgress, SelectedRecipe.CraftingTime) * ProgressbarScale), ProgressbarOffset - Border), Color.LightSkyBlue);
            }
            else
            {
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(frontRectangle.X, frontRectangle.Y, ProgressbarOffset, ProgressbarOffset), Color.Black);
            }


            for (int i = 0; i < RecipeList.Count; i++) // draw buttons
            {
                Buttons[i].Draw();
                if (i == SelectedRecipePosition) // highlight selected recipe
                {
                    Game.MainSpriteBatch.Draw(Textures.EmptyTexture, Buttons[i].GetRectangle(), Color.FromNonPremultiplied(new Vector4(0, 0, 1, 0.1f)));
                }
            }

            for (int i = 0; i < RecipeList.Count; i++) // draw recipe info
            {
                if (Buttons[i].IsHovered())
                {
                    RecipeList[i].Draw(MyKeyboard.GetMousePosition());
                }
            }
        }
    }
}
