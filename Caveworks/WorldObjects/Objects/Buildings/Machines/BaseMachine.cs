
using System;
using System.Collections;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class BaseMachine : BaseBuilding
    {
        public BaseMachine(Tile tile, int size, int inventorySize) : base(tile, size)
        {
            this.Inventory = new Inventory(inventorySize, Globals.World.Player);
            this.Crafter = new RecipeCrafter(new List<Recipe> { RecipeList.SlowBeltRecipe, RecipeList.IronChestRecipe }, Inventory);
        }
    }
}
