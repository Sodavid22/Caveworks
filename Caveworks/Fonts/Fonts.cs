using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Caveworks
{
    public class Fonts
    {
        public static SpriteFont LargeFont { get; private set; }
        public static SpriteFont SmallFont { get; private set; }
        public static SpriteFont MediumFont { get; private set; }

        public static void Load(ContentManager contentManager)
        {
            SmallFont = contentManager.Load<SpriteFont>("Fonts/Verdana12Bold");
            MediumFont = contentManager.Load<SpriteFont>("Fonts/Verdana16");
            LargeFont = contentManager.Load<SpriteFont>("Fonts/Verdana32");
        }
    }
}
