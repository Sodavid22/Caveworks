namespace Caveworks
{
    static class RecipeList
    {
        public static Recipe SlowBeltRecipe = new Recipe(new BaseItem[3]{new RawIronOreItem(2), new RawCopperOreItem(1), new RawStoneItem(1)}, new SlowBeltItem(2), 6);
        public static Recipe IronChestRecipe = new Recipe(new BaseItem[1] { new RawIronOreItem(2) }, new IronChestItem(1), 2);
    }
}
