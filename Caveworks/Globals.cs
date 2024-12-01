using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Caveworks
{
    public static class Globals // all global variables
    {
        public static int DISPLAY_WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; // monitor width in pixels
        public static int DISPLAY_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; // monitor height in pixels

        public static bool isFullscreen = false; // is in fullscreen mode

        public static Texture2D whitePixel; // empty texture for future use

        public static Enums.GameScreen activeScreen = Enums.GameScreen.MainMenu;// witch screen should be loaded, 0 - main menu

        public static Texture2D menuBackground; // background

        public static bool activeGame = false; // is there a game to load

        public static float gameVolume = 1.0f; // global game volume

        public static Vector2 GetScreenSize() // get curerent screen width
        {
            return new Vector2(Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight);
        }
    }
}
