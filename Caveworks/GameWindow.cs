using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Caveworks
{
    public static class GameWindow
    {
        // monitor size in pixels:
        private static Vector2 DISPLAY_SIZE = new Vector2((int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, (int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

        // game window size in pixels:
        private static Vector2 windowSize = new Vector2((int)DISPLAY_SIZE.X/2, (int)DISPLAY_SIZE.Y/2);

        // is in fullscreen mode
        private static bool isFullscreen = false;

        // is vertical synchronization enabled
        private static bool vSync = true;

        public static bool IsFullscreen()
        {
            return isFullscreen;
        }

        public static Vector2 GetWindowSize()
        {
            return windowSize;
        }

        public static Vector2 UpdateWindowSize() // get curerent screen width
        {
            return new Vector2(Game.graphics.PreferredBackBufferWidth, Game.graphics.PreferredBackBufferHeight);
        }

        public static void ToggleVSync(Game game, GraphicsDeviceManager graphicsDeviceManager)
        {
            if (vSync)
            {
                graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
                game.IsFixedTimeStep = false;
                Game.graphics.ApplyChanges();
                vSync = false;
            }
            else
            {
                graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
                game.IsFixedTimeStep = true;
                Game.graphics.ApplyChanges();
                vSync = true;
            }
        }

        public static void ToggleFullscreen()
        {
            if (isFullscreen)
            {   // disable fullscreen
                Game.graphics.PreferredBackBufferWidth = (int) DISPLAY_SIZE.X / 2;
                Game.graphics.PreferredBackBufferHeight = (int) DISPLAY_SIZE.Y / 2;
                Game.graphics.ToggleFullScreen();
                Game.graphics.IsFullScreen = false;
                Game.graphics.ApplyChanges();
                windowSize = UpdateWindowSize();
                isFullscreen = false;
            }
            else
            {   // enable fullscreen
                Game.graphics.PreferredBackBufferWidth = (int) DISPLAY_SIZE.X;
                Game.graphics.PreferredBackBufferHeight = (int) DISPLAY_SIZE.Y;
                Game.graphics.ToggleFullScreen();
                Game.graphics.IsFullScreen = true;
                Game.graphics.ApplyChanges();
                windowSize = UpdateWindowSize();
                isFullscreen = true;
            }
        }

        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager)
        {   // set screen parameters
            graphicsDeviceManager.PreferredBackBufferWidth = (int)GameWindow.windowSize.X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)GameWindow.windowSize.Y;
            graphicsDeviceManager.HardwareModeSwitch = false;
            graphicsDeviceManager.ApplyChanges();
        }
    }
}
