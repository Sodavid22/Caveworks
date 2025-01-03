using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Caveworks
{
    public class Fonts
    {
        public static SpriteFont MenuButtonFont { get; private set; }
        public static SpriteFont DefaultFont { get; private set; }
        public static SpriteFont SmallFont { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            DefaultFont = contentManager.Load<SpriteFont>("Fonts/Verdana12");
            MenuButtonFont = contentManager.Load<SpriteFont>("Fonts/Verdana32");
            SmallFont = contentManager.Load<SpriteFont>("Fonts/Verdana08");
        }
    }
}
