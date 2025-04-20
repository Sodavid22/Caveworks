using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Caveworks
{
    public static class Textures
    {
        // empty texture
        public static Texture2D EmptyTexture { get; private set; }

        // game background
        public static Texture2D MenuBackground { get; private set; }

        // floors:
        public static Texture2D StoneFloor { get; private set; }
        public static Texture2D StoneFloor2 { get; private set; }

        // walls:
        public static Texture2D StoneWall { get; private set; }
        public static Texture2D StoneWallStronger { get; private set; }
        public static Texture2D StoneWallStrongest { get; private set; }
        public static Texture2D StoneWallPermanent{ get; private set; }
        public static Texture2D RawIronOreWall { get; private set; }
        public static Texture2D RawCopperOreWall { get; private set; }
        public static Texture2D RawCoalWall { get; private set; }

        // creatures
        public static Texture2D Player {  get; private set; }

        // items
        public static Texture2D RawIronOre { get; private set; }
        public static Texture2D RawCopperOre { get; private set; }
        public static Texture2D RawStone { get; private set; }
        public static Texture2D RawCoal { get; private set; }
        public static Texture2D CopperPlate { get; private set; }
        public static Texture2D IronPlate { get; private set; }
        public static Texture2D CopperWire { get; private set; }
        public static Texture2D IronGear { get; private set; }
        public static Texture2D GreenCircuit { get; private set; }
        public static Texture2D PickaxeStone { get; private set; }


        // buildings

        public static Texture2D SlowBelt { get; private set; }
        public static Texture2D SlowBelt2 { get; private set; }
        public static Texture2D SlowBelt3 { get; private set; }
        public static Texture2D SlowBelt4 { get; private set; }
        public static Texture2D IronChest { get; private set; }
        public static Texture2D AssemblingMachine { get; private set; }
        public static Texture2D AssemblingMachineWorking { get; private set; }
        public static Texture2D ElectricLight { get; private set; }
        public static Texture2D Fireplace { get; private set; }
        public static Texture2D Fireplace2 { get; private set; }
        public static Texture2D Fireplace3 { get; private set; }
        public static Texture2D FireplaceMask { get; private set; }
        public static Texture2D Crossroad { get; private set; }
        public static Texture2D Splitter { get; private set; }
        public static Texture2D StoneFurnace { get; private set; }
        public static Texture2D StoneFurnaceLit { get; private set; }
        public static Texture2D StoneFurnaceLit2 { get; private set; }
        public static Texture2D StoneFurnaceLit3 { get; private set; }
        public static Texture2D Drill { get; private set; }
        public static Texture2D Drill2 { get; private set; }
        public static Texture2D Drill3 { get; private set; }
        public static Texture2D Elevator { get; private set; }
        public static Texture2D Elevator2 { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            // create empty texture
            Textures.EmptyTexture = new Texture2D(Game.Graphics.GraphicsDevice, 1, 1);
            Textures.EmptyTexture.SetData(new[] { Color.White });

            // background
            Textures.MenuBackground = contentManager.Load<Texture2D>("factorio_background");

            // floors:
            Textures.StoneFloor = contentManager.Load<Texture2D>("Floors/StoneFloor");
            Textures.StoneFloor2 = contentManager.Load<Texture2D>("Floors/StoneFloor2");

            // walls:
            Textures.StoneWall = contentManager.Load<Texture2D>("Walls/StoneWall");
            Textures.StoneWallStronger = contentManager.Load<Texture2D>("Walls/StoneWallStronger");
            Textures.StoneWallStrongest = contentManager.Load<Texture2D>("Walls/StoneWallStrongest");
            Textures.StoneWallPermanent = contentManager.Load<Texture2D>("Walls/StoneWallPermanent");
            Textures.RawIronOreWall = contentManager.Load<Texture2D>("Walls/RawIronOreWall");
            Textures.RawCopperOreWall = contentManager.Load<Texture2D>("Walls/RawCopperOreWall");
            Textures.RawCoalWall = contentManager.Load<Texture2D>("Walls/RawCoalWall");

            // creatures:
            Textures.Player = contentManager.Load<Texture2D>("Creatures/Player");

            // items:
            Textures.RawIronOre = contentManager.Load<Texture2D>("Items/Ores/RawIronOre");
            Textures.RawCopperOre = contentManager.Load<Texture2D>("Items/Ores/RawCopperOre");
            Textures.RawStone = contentManager.Load<Texture2D>("Items/Ores/RawStone");
            Textures.RawCoal = contentManager.Load<Texture2D>("Items/Ores/RawCoal");
            Textures.CopperPlate = contentManager.Load<Texture2D>("Items/Parts/CopperPlate");
            Textures.IronPlate = contentManager.Load<Texture2D>("Items/Parts/IronPlate");
            Textures.CopperWire = contentManager.Load<Texture2D>("Items/Parts/CopperWire");
            Textures.IronGear = contentManager.Load<Texture2D>("Items/Parts/IronGear");
            Textures.GreenCircuit = contentManager.Load<Texture2D>("Items/Parts/GreenCircuit");
            Textures.PickaxeStone = contentManager.Load<Texture2D>("Items/Tools/PickaxeStone");

            // buildings:
            Textures.SlowBelt = contentManager.Load<Texture2D>("Buildings/Belts/GreenBelt");
            Textures.SlowBelt2 = contentManager.Load<Texture2D>("Buildings/Belts/GreenBelt2");
            Textures.SlowBelt3 = contentManager.Load<Texture2D>("Buildings/Belts/GreenBelt3");
            Textures.SlowBelt4 = contentManager.Load<Texture2D>("Buildings/Belts/GreenBelt4");
            Textures.IronChest = contentManager.Load<Texture2D>("Buildings/Storage/IronChest");
            Textures.AssemblingMachine = contentManager.Load<Texture2D>("Buildings/Machines/AssemblingMachine");
            Textures.AssemblingMachineWorking = contentManager.Load<Texture2D>("Buildings/Machines/AssemblingMachineWorking");
            Textures.ElectricLight = contentManager.Load<Texture2D>("Buildings/Light/ElectricLight");
            Textures.Fireplace = contentManager.Load<Texture2D>("Buildings/Light/Fireplace");
            Textures.Fireplace2 = contentManager.Load<Texture2D>("Buildings/Light/Fireplace2");
            Textures.Fireplace3 = contentManager.Load<Texture2D>("Buildings/Light/Fireplace3");
            Textures.FireplaceMask = contentManager.Load<Texture2D>("Buildings/Light/FireplaceMask");
            Textures.Crossroad = contentManager.Load<Texture2D>("Buildings/Belts/Crossroad");
            Textures.Splitter = contentManager.Load<Texture2D>("Buildings/Belts/Splitter");
            Textures.StoneFurnace = contentManager.Load<Texture2D>("Buildings/Machines/StoneFurnace");
            Textures.StoneFurnaceLit = contentManager.Load<Texture2D>("Buildings/Machines/StoneFurnaceLit");
            Textures.StoneFurnaceLit2 = contentManager.Load<Texture2D>("Buildings/Machines/StoneFurnaceLit2");
            Textures.StoneFurnaceLit3 = contentManager.Load<Texture2D>("Buildings/Machines/StoneFurnaceLit3");
            Textures.Drill = contentManager.Load<Texture2D>("Buildings/Machines/Drill");
            Textures.Drill2 = contentManager.Load<Texture2D>("Buildings/Machines/Drill2");
            Textures.Drill3 = contentManager.Load<Texture2D>("Buildings/Machines/Drill3");
            Textures.Elevator = contentManager.Load<Texture2D>("Buildings/Machines/Elevator");
            Textures.Elevator2 = contentManager.Load<Texture2D>("Buildings/Machines/Elevator2");
        }
    }
}
