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


        public static void Load(ContentManager contentManager)
        {
            // create empty texture
            Textures.EmptyTexture = new Texture2D(Game.Graphics.GraphicsDevice, 1, 1);
            Textures.EmptyTexture.SetData(new[] { Color.White });

            // background
            Textures.MenuBackground = contentManager.Load<Texture2D>("factorio_background");

            // floors:
            Textures.StoneFloor = contentManager.Load<Texture2D>("Floors/StoneFloor");
        }
    }
}
