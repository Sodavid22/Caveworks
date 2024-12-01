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
        public static void Load()
        {

        }
        public static void Update()
        {

        }
        public static void Draw()
        {
            Game.mainSpriteBatch.Draw(Globals.menuBackground, new Rectangle(0, 0, (int)Globals.GetScreenSize().X, (int)Globals.GetScreenSize().Y), Color.White);
            Game.mainSpriteBatch.DrawString(Fonts.menuButtonFont, "Made by: David Sobek", new Vector2((Globals.GetScreenSize().X - Fonts.menuButtonFont.MeasureString("Made by: David Sobek").X) / 2, Globals.GetScreenSize().Y / 2), Color.Yellow);
        }
    }
}
