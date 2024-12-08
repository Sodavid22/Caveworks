using System;
using System.Diagnostics;
using System.Xml;
using Caveworks.GameScreens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch mainSpriteBatch;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() // TODO: Add your initialization logic here
        {
            base.Initialize();

            // setup game window
            GameWindow.Initialize(graphics);

            // uncap framerate
            GameWindow.ToggleVSync(this, graphics);

            // create sprite batches
            mainSpriteBatch = new SpriteBatch(GraphicsDevice);

            // set starting screen
            Globals.activeScreen = GameScreen.MainMenu;
            MainMenuScreen.Load();
        }

        protected override void LoadContent() // TODO: use this.Content to load your game content here
        {
            // create empty texture
            Textures.emptyTexture = new Texture2D(GraphicsDevice, 1, 1);
            Textures.emptyTexture.SetData(new[] { Color.White });

            // load all textures
            Textures.Load(Content);

            // load all fonts
            Fonts.Load(Content);

            // load all sounds
            Sounds.Load(Content);
        }

        protected override void Update(GameTime gameTime) // TODO: Add your update logic here
        {
            // update keyboard state
            MyKeyboard.Update();

            // go to main menu
            if (MyKeyboard.IsPressed(KeyBindings.MENU_KEY))
            {
                Sounds.buttonClick2.play(1.0f);
                Globals.activeScreen = GameScreen.MainMenu;
                MainMenuScreen.Load();
            }

            // turn off the game
            if (MyKeyboard.IsHeld(KeyBindings.SHUTDOWN_KEY))
            {
                Exit();
            }

            // toggle FPS counter
            if (MyKeyboard.IsPressed(KeyBindings.FPS_KEY))
            {
                Sounds.buttonClick2.play(1.0f);
                FpsCounter.Toggle();
            }

            // update screens
            if (Globals.activeScreen == GameScreen.MainMenu)
            {
                MainMenuScreen.Update();
            }
            else if (Globals.activeScreen == GameScreen.Start)
            {
                StartScreen.Update();
            }
            else if (Globals.activeScreen == GameScreen.Settings)
            {
                SettingsScreen.Update();
            }
            else if (Globals.activeScreen == GameScreen.Credits)
            {
                CreditsScreen.Update();
            }

            FpsCounter.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) // TODO: Add your drawing code here
        {
            GraphicsDevice.Clear(Color.White);
            mainSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // draw background
            Game.mainSpriteBatch.Draw(Textures.menuBackground, new Rectangle(0, 0, (int)GameWindow.windowSize.X, (int)GameWindow.windowSize.Y), Color.White);

            // draw screens
            if (Globals.activeScreen == GameScreen.MainMenu)
            {
                MainMenuScreen.Draw();
            }
            else if (Globals.activeScreen == GameScreen.Start)
            {
                StartScreen.Draw();
            }
            else if (Globals.activeScreen == GameScreen.Settings)
            {
                SettingsScreen.Draw();
            }
            else if (Globals.activeScreen == GameScreen.Credits)
            {
                CreditsScreen.Draw();
            }

            // draw FPS counter
            if (FpsCounter.IsActive())
            {
                mainSpriteBatch.DrawString(Fonts.defaultFont, (FpsCounter.GetFps() + " FPS").ToString(), new Vector2(0, 0), Color.White);
            }

            mainSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
