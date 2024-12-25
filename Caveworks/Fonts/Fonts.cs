using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.Metadata;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Caveworks
{
    public class Fonts
    {
        public static SpriteFont menuButtonFont;
        public static SpriteFont defaultFont;
        public static SpriteFont smallFont;

        public static void Load(ContentManager contentManager)
        {
            defaultFont = contentManager.Load<SpriteFont>("Fonts/Verdana12");
            menuButtonFont = contentManager.Load<SpriteFont>("Fonts/Verdana32");
            smallFont = contentManager.Load<SpriteFont>("Fonts/Verdana08");
        }
    }
}
