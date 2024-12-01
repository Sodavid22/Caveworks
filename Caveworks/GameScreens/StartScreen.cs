using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Caveworks.GameScreens
{
    internal class StartScreen
    {
        public static void Load()
        {

        }
        public static void Update()
        {

        }
        public static void Draw()
        {
            Game.mainSpriteBatch.Draw(Globals.menuBackground, new Rectangle(0, 0, (int)Globals.GetScreenSize().X, (int)Globals.GetScreenSize().Y), Color.White);
        }
    }
}
