namespace Caveworks
{
    static class RecipeList
    {
        public static Recipe Stonification = new Recipe(new BaseItem[2]{new RawIronOreItem(2), new RawCopperOreItem(2)}, new RawStoneItem(4), 2);
        public static Recipe IronChestRecipe = new Recipe(new BaseItem[1] { new RawIronOreItem(2) }, new IronChestItem(1), 4);
    }
}
