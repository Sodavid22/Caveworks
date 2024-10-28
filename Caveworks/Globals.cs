using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;


namespace Caveworks
{
    public static class Globals // all global variables
    {
        public static int DISPLAY_WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; // monitor width in pixels
        public static int DISPLAY_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; // monitor height in pixels

        public static bool isFullscreen = false; // is in fullscreen mode

        public static Texture2D whitePixel; // empty texture for future use

        public static int activeScreen; // witch screen should be loaded, 0 - main menu

        public static Texture2D menuBackground;

        public static Vector2 GetScreenSize() // get curerent screen width
        {
            return new Vector2(Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight);
        }
    }
}
