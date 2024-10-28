using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        Texture2D texture;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() // TODO: Add your initialization logic here
        {
            base.Initialize();

            // set screen parameters
            graphics.PreferredBackBufferWidth = Globals.DISPLAY_WIDTH/2;
            graphics.PreferredBackBufferHeight = Globals.DISPLAY_HEIGHT/2;
            graphics.HardwareModeSwitch = false;
            graphics.ApplyChanges();

            // create sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create empty texture for future use
            Globals.whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            Globals.whitePixel.SetData(new[] { Color.White });

            // set current screen to main menu
            Globals.activeScreen = 0;
        }

        protected override void LoadContent() // TODO: use this.Content to load your game content here
        {
            texture = Content.Load<Texture2D>("Backgrounds/factorio_background");
        }

        protected override void Update(GameTime gameTime) // TODO: Add your update logic here
        {
            KeyboardManager.Update();

            // turn off the game
            if (KeyboardManager.IsHeld(Keys.Escape))
            {
                Exit();
            }

            // toggle fullscreen
            if (KeyboardManager.IsPressed(Keys.F11))
            {
                if (Globals.isFullscreen)
                {
                    // disable fullscreen
                    Globals.isFullscreen = false;
                    graphics.PreferredBackBufferWidth = Globals.DISPLAY_WIDTH/2;
                    graphics.PreferredBackBufferHeight = Globals.DISPLAY_HEIGHT/2;
                    graphics.ToggleFullScreen();
                    graphics.IsFullScreen = false;
                    graphics.ApplyChanges();
                }
                else
                {
                    // enable fullscreen
                    Globals.isFullscreen = true;
                    graphics.PreferredBackBufferWidth = Globals.DISPLAY_WIDTH;
                    graphics.PreferredBackBufferHeight = Globals.DISPLAY_HEIGHT;
                    graphics.ToggleFullScreen();
                    graphics.IsFullScreen= true;
                    graphics.ApplyChanges();
                }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime) // TODO: Add your drawing code here
        {
            spriteBatch.Begin();

            spriteBatch.Draw(Globals.menuBackground, new Rectangle(100, 100, 100, 100), Color.White);

            if (Globals.activeScreen == 0)
            {
                MainMenuScreen.Update();
                MainMenuScreen.Draw();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
