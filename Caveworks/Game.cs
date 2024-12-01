using System;
using System.Diagnostics;
using System.Xml;
using Caveworks.GameScreens;
using Caveworks.SoundEffects;
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

            // uncap framerate
            graphics.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            // set screen parameters
            graphics.PreferredBackBufferWidth = Globals.DISPLAY_WIDTH/2;
            graphics.PreferredBackBufferHeight = Globals.DISPLAY_HEIGHT/2;
            graphics.HardwareModeSwitch = false;
            graphics.ApplyChanges();

            // create sprite batches
            mainSpriteBatch = new SpriteBatch(GraphicsDevice);

            // create empty texture for use by other classes
            Globals.whitePixel = new Texture2D(GraphicsDevice, 1, 1);
            Globals.whitePixel.SetData(new[] { Color.White });

            // set current screen to main menu
            Globals.activeScreen = Enums.GameScreen.MainMenu;
            MainMenuScreen.Load();
        }

        protected override void LoadContent() // TODO: use this.Content to load your game content here
        {
            // load background
            Globals.menuBackground = Content.Load<Texture2D>("factorio_background");

            // load fonts
            Fonts.defaultFont = Content.Load<SpriteFont>("Fonts/TitilliumWeb16");
            Fonts.menuButtonFont = Content.Load<SpriteFont>("Fonts/TitilliumWebSemiBold32");

            // load sounds (use instances, not originals !!!)
            Sounds.buttonClick = new MySoundEffect(Content.Load<SoundEffect>("SoundEffects/UI/modern-ui-hover"), 0.5f);
            Sounds.buttonDecline = new MySoundEffect(Content.Load<SoundEffect>("SoundEffects/UI/modern-ui-decline"), 0.5f);
            Sounds.buttonClick2 = new MySoundEffect(Content.Load<SoundEffect>("SoundEffects/UI/ui-button-click"), 0.5f);
        }

        protected override void Update(GameTime gameTime) // TODO: Add your update logic here
        {
            // update keyboard state
            KeyboardManager.Update();

            // go to main menu
            if (KeyboardManager.IsPressed(Keys.Escape))
            {
                Sounds.buttonClick2.play(1.0f);
                Globals.activeScreen = Enums.GameScreen.MainMenu;
                MainMenuScreen.Load();
            }

            // turn off the game
            if (KeyboardManager.IsHeld(Keys.F4))
            {
                Exit();
            }

            // toggle FPS counter
            if (KeyboardManager.IsPressed(Keys.F3))
            {
                Sounds.buttonClick2.play(1.0f);
                FpsCounter.Toggle();
            }

            // update screens
            if (Globals.activeScreen == Enums.GameScreen.MainMenu)
            {
                MainMenuScreen.Update();
            }
            else if (Globals.activeScreen == Enums.GameScreen.Start)
            {
                StartScreen.Update();
            }
            else if (Globals.activeScreen == Enums.GameScreen.Settings)
            {
                SettingsScreen.Update();
            }
            else if (Globals.activeScreen == Enums.GameScreen.Credits)
            {
                CreditsScreen.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) // TODO: Add your drawing code here
        {
            GraphicsDevice.Clear(Color.White);
            mainSpriteBatch.Begin();

            // draw screens
            if (Globals.activeScreen == Enums.GameScreen.MainMenu)
            {
                MainMenuScreen.Draw();
            }
            else if (Globals.activeScreen == Enums.GameScreen.Start)
            {
                StartScreen.Draw();
            }
            else if (Globals.activeScreen == Enums.GameScreen.Settings)
            {
                SettingsScreen.Draw();
            }
            else if (Globals.activeScreen == Enums.GameScreen.Credits)
            {
                CreditsScreen.Draw();
            }

            // draw FPS counter
            if (FpsCounter.IsActive())
            {
                FpsCounter.Update(gameTime);
                mainSpriteBatch.DrawString(Fonts.defaultFont, (FpsCounter.GetFps() + " FPS").ToString(), new Vector2(0, 0), Color.White);
            }


            mainSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
