using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Caveworks
{
    public static class GameWindow
    {
        // monitor size in pixels
        private static Vector2 DISPLAY_SIZE = new Vector2((int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, (int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

        // game window size in pixels
        public static Vector2 WindowSize { get; private set; } = new Vector2((int)DISPLAY_SIZE.X/2, (int)DISPLAY_SIZE.Y/2);

        // is in fullscreen mode
        public static bool IsFullscreen {get; private set;} = false;

        // is vertical synchronization enabled
        private static bool vSync = true;


        private static Vector2 UpdateWindowSize() // get curerent screen width
        {
            return new Vector2(Game.Graphics.PreferredBackBufferWidth, Game.Graphics.PreferredBackBufferHeight);
        }


        public static void ToggleVSync(Game game, GraphicsDeviceManager graphicsDeviceManager)
        {
            if (vSync)
            {
                graphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
                game.IsFixedTimeStep = false;
                Game.Graphics.ApplyChanges();
                vSync = false;
            }
            else
            {
                graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
                game.IsFixedTimeStep = true;
                Game.Graphics.ApplyChanges();
                vSync = true;
            }
        }


        public static void ToggleFullscreen()
        {
            if (IsFullscreen)
            {   // disable fullscreen
                Game.Graphics.PreferredBackBufferWidth = (int) DISPLAY_SIZE.X / 2;
                Game.Graphics.PreferredBackBufferHeight = (int) DISPLAY_SIZE.Y / 2;
                Game.Graphics.ToggleFullScreen();
                Game.Graphics.IsFullScreen = false;
                Game.Graphics.ApplyChanges();
                WindowSize = UpdateWindowSize();
                IsFullscreen = false;
            }
            else
            {   // enable fullscreen
                Game.Graphics.PreferredBackBufferWidth = (int) DISPLAY_SIZE.X;
                Game.Graphics.PreferredBackBufferHeight = (int) DISPLAY_SIZE.Y;
                Game.Graphics.ToggleFullScreen();
                Game.Graphics.IsFullScreen = true;
                Game.Graphics.ApplyChanges();
                WindowSize = UpdateWindowSize();
                IsFullscreen = true;
            }
        }


        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager)
        {   // set screen parameters
            graphicsDeviceManager.PreferredBackBufferWidth = (int)WindowSize.X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)WindowSize.Y;
            graphicsDeviceManager.HardwareModeSwitch = false;
            graphicsDeviceManager.ApplyChanges();
        }
    }
}
