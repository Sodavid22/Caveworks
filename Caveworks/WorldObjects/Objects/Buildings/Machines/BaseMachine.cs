
using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class BaseMachine : BaseBuilding
    {
        public const int ItemLimit = 10;

        public BaseMachine(Tile tile, int size, int inventorySize) : base(tile, size)
        {
            this.Inventory = new Inventory(inventorySize, Globals.World.Player);
            this.Crafter = new RecipeCrafter(new List<Recipe> { RecipeList.Stonification, RecipeList.IronChestRecipe }, Inventory);
        }


        public override bool AccteptsItems() { return true; }


        public override bool HasCollision() { return true; }


        public override bool HasUI() { return true; }


        public override void OpenUI()
        {
            Inventory.OpenUI(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2));
            Crafter.OpenUI(new MyVector2Int((int)GameWindow.Size.X / 2 - ((Inventory.ButtonSpacing * (Inventory.RowLength - 1) + Inventory.ButtonSize) / 2), (int)GameWindow.Size.Y / 2));
            Sounds.ButtonClick.Play(1);
        }


        public override void UpdateUI()
        {
            Inventory.UpdateUI();
            Crafter.UpdateUI();
        }


        public override void DrawUI()
        {
            Inventory.DrawUI();
            Crafter.DrawUI();
        }


        public override void CloseUI()
        {
            Inventory.CloseUI();
            Crafter.CloseUI();
        }
    }
}
