﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Caveworks
{
    public static class GameWindow
    {
        // monitor size in pixels
        private static Vector2 DISPLAY_SIZE = new Vector2((int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, (int)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

        // game window size in pixels
        public static Vector2 Size { get; private set; } = new Vector2(1280, 720);

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
                Game.Graphics.PreferredBackBufferWidth = (int) 1280;
                Game.Graphics.PreferredBackBufferHeight = (int) 720;
                Game.Graphics.ToggleFullScreen();
                Game.Graphics.IsFullScreen = false;
                Game.Graphics.ApplyChanges();
                Size = UpdateWindowSize();
                IsFullscreen = false;
            }
            else
            {   // enable fullscreen
                Game.Graphics.PreferredBackBufferWidth = (int) DISPLAY_SIZE.X;
                Game.Graphics.PreferredBackBufferHeight = (int) DISPLAY_SIZE.Y;
                Game.Graphics.ToggleFullScreen();
                Game.Graphics.IsFullScreen = true;
                Game.Graphics.ApplyChanges();
                Size = UpdateWindowSize();
                IsFullscreen = true;
            }
        }


        public static void Initialize(GraphicsDeviceManager graphicsDeviceManager)
        {   // set screen parameters
            graphicsDeviceManager.PreferredBackBufferWidth = (int)Size.X;
            graphicsDeviceManager.PreferredBackBufferHeight = (int)Size.Y;
            graphicsDeviceManager.HardwareModeSwitch = false;
            graphicsDeviceManager.ApplyChanges();
        }
    }
}
