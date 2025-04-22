using System;
using System.Collections.Generic;

namespace Caveworks
{
    [Serializable]
    public class RecipeList
    {
        public List<Recipe> PlayerRecipes;
        public List<Recipe> AssemblingMachineRecipes;
        public List<Recipe> StoneFurnaceRecipes;

        public RecipeList()
        {
        PlayerRecipes = new List<Recipe> { StoneFurnace, Elevator, Fireplace, PickaxeStone, PickaxeIron, IronGear };
        AssemblingMachineRecipes = new List<Recipe> { StoneFurnace, Elevator, Fireplace, PickaxeStone, PickaxeIron, IronGear, IronPipe, CopperWire, GreenCircuit, ElectricEngine, AssemblingMachine, Drill, ElectricLight, SlowBelt, CrossRoad, Splitter, IronChest};
        StoneFurnaceRecipes = new List<Recipe> { IronSmelting, CopperSmelting };
        }


        // smelting
        public static Recipe IronSmelting = new Recipe(new BaseItem[2] { new RawIronOreItem(4), new RawCoalItem(1) }, new IronPlate(4), 8); // 8 machines
        public static Recipe CopperSmelting = new Recipe(new BaseItem[2] { new RawCopperOreItem(4), new RawCoalItem(1) }, new CopperPlate(4), 8); // 8 machines

        // components
        public static Recipe IronGear = new Recipe(new BaseItem[1] { new IronPlate(1) }, new IronGear(1), 1); // 1 iron
        public static Recipe IronPipe = new Recipe(new BaseItem[1] { new IronPlate(1) }, new IronPipe(1), 1); // 1 iron
        public static Recipe CopperWire = new Recipe(new BaseItem[1] { new CopperPlate(1) }, new CopperWire(1), 1); // 1 copper
        public static Recipe GreenCircuit = new Recipe(new BaseItem[2] { new IronPlate(1), new CopperWire(4) }, new GreenCircuit(1), 2); // 1 iron, 2 copper
        public static Recipe ElectricEngine = new Recipe(new BaseItem[2] { new IronGear(1), new CopperWire(2) }, new ElectricEngine(1), 2); // 2 iron, 1 copper


        // buildings
        public static Recipe StoneFurnace = new Recipe(new BaseItem[1] { new RawStoneItem(8) }, new StoneFurnaceItem(1), 4); // - 8 stone
        public static Recipe AssemblingMachine = new Recipe(new BaseItem[3] { new IronPlate(4), new IronGear(4), new GreenCircuit(2), }, new AsseblingMachineItem(1), 4); // 10 iron, 4 copper
        public static Recipe Drill = new Recipe(new BaseItem[3] { new IronPlate(2), new IronGear(2), new ElectricEngine(2)}, new DrillItem(1), 4); // 6 iron, 2 copper
        public static Recipe Elevator = new Recipe(new BaseItem[2] { new IronPlate(4), new IronGear(4)}, new ElevatorItem(1), 4); // 8 iron

        // light
        public static Recipe Fireplace = new Recipe(new BaseItem[2] { new RawStoneItem(2), new RawCoalItem(1) }, new FireplaceItem(1), 2); // 2 stone b, 1 coal
        public static Recipe ElectricLight = new Recipe(new BaseItem[2] { new IronPlate(2), new CopperWire(8) }, new ElectricLightItem(1), 2); // 2 iron, 4 copper

        // belts
        public static Recipe SlowBelt = new Recipe(new BaseItem[2] { new IronPlate(1), new IronPipe(1) }, new SlowBeltItem(1), 1); // 2 iron
        public static Recipe CrossRoad = new Recipe(new BaseItem[2] { new IronPlate(2), new IronPipe(2) }, new CrossroadItem(1), 2); // 4 iron
        public static Recipe Splitter = new Recipe(new BaseItem[2] { new IronPlate(2), new IronPipe(2) }, new SplitterItem(1), 2); // 4 iron

        // storage
        public static Recipe IronChest = new Recipe(new BaseItem[1] { new IronPlate(8) }, new IronChestItem(1), 2); // 8 iron

        // tools
        public static Recipe PickaxeStone = new Recipe(new BaseItem[1] { new RawStoneItem(8) }, new PickaxeStone(1), 2); // 8 stone
        public static Recipe PickaxeIron = new Recipe(new BaseItem[1] { new IronPlate(8) }, new PickaxeIron(1), 2); // 8 iron
    }
}
