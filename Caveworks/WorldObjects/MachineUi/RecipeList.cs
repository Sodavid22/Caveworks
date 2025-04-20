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
        AssemblingMachineRecipes = new List<Recipe> { StoneFurnace, PickaxeStone, PickaxeIron, IronGear, IronPipe, CopperWire, GreenCircuit, AssemblingMachine, Drill, ElectricLight, SlowBelt, CrossRoad, Splitter, IronChest, ElectricEngine};
        StoneFurnaceRecipes = new List<Recipe> { IronSmelting, CopperSmelting };
        }


        // smelting
        public Recipe IronSmelting = new Recipe(new BaseItem[2] { new RawIronOreItem(4), new RawCoalItem(1) }, new IronPlate(4), 8);
        public Recipe CopperSmelting = new Recipe(new BaseItem[2] { new RawCopperOreItem(4), new RawCoalItem(1) }, new CopperPlate(4), 8);

        // components
        public Recipe IronGear = new Recipe(new BaseItem[1] { new IronPlate(1) }, new IronGear(1), 1);
        public Recipe IronPipe = new Recipe(new BaseItem[1] { new IronPlate(1) }, new IronPipe(2), 1);
        public Recipe CopperWire = new Recipe(new BaseItem[1] { new CopperPlate(1) }, new CopperWire(2), 1);
        public Recipe GreenCircuit = new Recipe(new BaseItem[2] { new IronPlate(1), new CopperWire(4) }, new GreenCircuit(1), 1);
        public Recipe ElectricEngine = new Recipe(new BaseItem[3] { new IronPlate(2), new IronGear(2), new CopperWire(2) }, new ElectricEngine(1), 1);


        // buildings
        public Recipe StoneFurnace = new Recipe(new BaseItem[1] { new RawStoneItem(8) }, new StoneFurnaceItem(1), 4);
        public Recipe AssemblingMachine = new Recipe(new BaseItem[3] { new IronPlate(4), new IronGear(4), new GreenCircuit(2), }, new AsseblingMachineItem(1), 4);
        public Recipe Drill = new Recipe(new BaseItem[2] { new IronPlate(2), new IronGear(2)}, new DrillItem(1), 4);
        public Recipe Elevator = new Recipe(new BaseItem[2] { new IronPlate(4), new IronGear(8)}, new ElevatorItem(1), 4);

        // light
        public Recipe Fireplace = new Recipe(new BaseItem[2] { new RawStoneItem(4), new RawCoalItem(1) }, new FireplaceItem(1), 2);
        public Recipe ElectricLight = new Recipe(new BaseItem[2] { new IronPlate(2), new CopperWire(8) }, new ElectricLightItem(1), 2);

        // belts
        public Recipe SlowBelt = new Recipe(new BaseItem[2] { new IronPlate(1), new IronPipe(1) }, new SlowBeltItem(2), 2);
        public Recipe CrossRoad = new Recipe(new BaseItem[2] { new IronPlate(2), new IronPipe(4) }, new CrossroadItem(1), 2);
        public Recipe Splitter = new Recipe(new BaseItem[2] { new IronPlate(2), new IronPipe(4) }, new SplitterItem(1), 2);

        // storage
        public Recipe IronChest = new Recipe(new BaseItem[1] { new IronPlate(4) }, new IronChestItem(1), 2);

        // tools
        public Recipe PickaxeStone = new Recipe(new BaseItem[1] { new RawStoneItem(4) }, new PickaxeStone(1), 2);
        public Recipe PickaxeIron = new Recipe(new BaseItem[1] { new IronPlate(4) }, new PickaxeIron(1), 2);
    }
}
