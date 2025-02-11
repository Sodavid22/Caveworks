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

        // creatures
        public static Texture2D Player {  get; private set; }

        // items
        public static Texture2D RawIronOre { get; private set; }

        // buildings

        public static Texture2D SlowBelt { get; private set; }

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

            // creatures:
            Textures.Player = contentManager.Load<Texture2D>("Creatures/Player");

            // items:
            Textures.RawIronOre = contentManager.Load<Texture2D>("Items/Ores/RawIronOre");

            // buildings:
            Textures.SlowBelt = contentManager.Load<Texture2D>("Buildings/Belts/SlowBelt");
        }
    }
}
