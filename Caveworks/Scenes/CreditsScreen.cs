using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Caveworks.GameScreens
{
    internal class CreditsScreen
    {

        // TEST CODE
        static TextBox credits = new TextBox(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Made by David", Fonts.defaultFont);

        public static void Load()
        {
            credits.Load(new Vector2(GameWindow.windowSize.X / 2, GameWindow.windowSize.Y / 2), Anchor.Middle);
        }
        public static void Update()
        {

        }
        public static void Draw()
        {
            credits.Draw();
        }
    }
}
