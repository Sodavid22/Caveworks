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
            Game.mainSpriteBatch.DrawString(Fonts.menuButtonFont, "Made by: David Sobek", new Vector2((GameWindow.windowSize.X - Fonts.menuButtonFont.MeasureString("Made by: David Sobek").X) / 2, GameWindow.windowSize.Y / 2), Color.Yellow);
        }
    }
}
