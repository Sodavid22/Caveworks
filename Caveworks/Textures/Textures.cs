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

        // walls:
        public static Texture2D StoneWall { get; private set; }
        public static Texture2D RawIronOreWall { get; private set; }

        // creatures
        public static Texture2D Player {  get; private set; }

        // items
        public static Texture2D RawIronOre { get; private set; }
        public static Texture2D RawCopperOre { get; private set; }
        public static Texture2D RawStone { get; private set; }

        // buildings

        public static Texture2D SlowBelt { get; private set; }
        public static Texture2D IronChest { get; private set; }
        public static Texture2D AssemblingMachine { get; private set; }
        public static Texture2D ElectricLight { get; private set; }
        public static Texture2D Fireplace { get; private set; }
        public static Texture2D Crossroad { get; private set; }
        public static Texture2D Splitter { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            // create empty texture
            Textures.EmptyTexture = new Texture2D(Game.Graphics.GraphicsDevice, 1, 1);
            Textures.EmptyTexture.SetData(new[] { Color.White });

            // background
            Textures.MenuBackground = contentManager.Load<Texture2D>("factorio_background");

            // floors:
            Textures.StoneFloor = contentManager.Load<Texture2D>("Floors/StoneFloor");

            // walls:
            Textures.StoneWall = contentManager.Load<Texture2D>("Walls/StoneWall");
            Textures.RawIronOreWall = contentManager.Load<Texture2D>("Walls/RawIronOreWall");

            // creatures:
            Textures.Player = contentManager.Load<Texture2D>("Creatures/Player");

            // items:
            Textures.RawIronOre = contentManager.Load<Texture2D>("Items/Ores/RawIronOre");
            Textures.RawCopperOre = contentManager.Load<Texture2D>("Items/Ores/RawCopperOre");
            Textures.RawStone = contentManager.Load<Texture2D>("Items/Ores/RawStone");

            // buildings:
            Textures.SlowBelt = contentManager.Load<Texture2D>("Buildings/Belts/GreenBelt");
            Textures.IronChest = contentManager.Load<Texture2D>("Buildings/Storage/IronChest");
            Textures.AssemblingMachine = contentManager.Load<Texture2D>("Buildings/Machines/AssemblingMachine");
            Textures.ElectricLight = contentManager.Load<Texture2D>("Buildings/Light/ElectricLight");
            Textures.Fireplace = contentManager.Load<Texture2D>("Buildings/Light/Fireplace");
            Textures.Crossroad = contentManager.Load<Texture2D>("Buildings/Belts/Crossroad");
            Textures.Splitter = contentManager.Load<Texture2D>("Buildings/Belts/Splitter");
        }
    }
}
