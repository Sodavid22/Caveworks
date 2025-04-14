namespace Caveworks
{
    static class RecipeList
    {
        // smelting
        public static Recipe IronSmelting = new Recipe(new BaseItem[2]{new RawIronOreItem(4), new RawCoalItem(1) }, new IronPlate(4), 8);
        public static Recipe CopperSmelting = new Recipe(new BaseItem[2] { new RawCopperOreItem(4), new RawCoalItem(1) }, new CopperPlate(4), 8);

        // components
        public static Recipe IronGear = new Recipe(new BaseItem[1] { new IronPlate(2)}, new IronGear(1), 1);
        public static Recipe CopperWire = new Recipe(new BaseItem[1] { new CopperPlate(1) }, new CopperWire(1), 2);
        public static Recipe GreenCircuit = new Recipe(new BaseItem[2] { new IronPlate(1), new CopperWire(4) }, new GreenCircuit(1), 4);

        // buildings
        public static Recipe StoneFurnace = new Recipe(new BaseItem[1] { new RawStoneItem(8) }, new StoneFurnaceItem(1), 4);
        public static Recipe AssemblingMachine = new Recipe(new BaseItem[3] { new IronPlate(4), new IronGear(4), new GreenCircuit(2)}, new AsseblingMachineItem(1), 8);
        public static Recipe Drill = new Recipe(new BaseItem[3] { new IronPlate(2), new IronGear(2), new GreenCircuit(1) }, new DrillItem(1), 4);
        public static Recipe ResearchLab = new Recipe(new BaseItem[3] { new IronPlate(4), new IronGear(2), new GreenCircuit(4) }, new ResearchLabItem(1), 8);


        // light
        public static Recipe Fireplace = new Recipe(new BaseItem[2] { new RawStoneItem(4), new RawCoalItem(1)}, new FireplaceItem(1), 2);
        public static Recipe ElectricLight = new Recipe(new BaseItem[2] { new IronPlate(2), new CopperWire(4) }, new ElectricLightItem(1), 2);

        // belts
        public static Recipe SlowBelt = new Recipe(new BaseItem[2] { new IronPlate(1), new IronGear(1) }, new SlowBeltItem(2), 2);
        public static Recipe CrossRoad = new Recipe(new BaseItem[2] { new IronPlate(2), new IronGear(2) }, new CrossroadItem(1), 2);
        public static Recipe Splitter = new Recipe(new BaseItem[2] { new IronPlate(2), new IronGear(2) }, new SplitterItem(1), 2);

        // storage
        public static Recipe IronChest = new Recipe(new BaseItem[1] { new IronPlate(4) }, new IronChestItem(1), 2);
    }
}
