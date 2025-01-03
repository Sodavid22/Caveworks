using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Caveworks
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatch MainSpriteBatch { get; private set; }
        public static Game Self { get; private set; }


        public Game()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Self = this;
        }


        protected override void Initialize() // TODO: Add your initialization logic here
        {
            base.Initialize();

            // setup game window
            GameWindow.Initialize(Graphics);

            // uncap framerate
            GameWindow.ToggleVSync(this, Graphics);

            // create sprite batches
            MainSpriteBatch = new SpriteBatch(GraphicsDevice);
        }


        protected override void LoadContent() // TODO: use this.Content to load your game content here
        {
            // load all textures
            Textures.Load(Content);

            // load all fonts
            Fonts.Load(Content);

            // load all sounds
            Sounds.Load(Content);

            // load save files
            SaveManager.LoadGame();

            // set starting screen
            Globals.ActiveScene = new MainMenuScene();
        }


        protected override void Update(GameTime gameTime) // TODO: Add your update logic here
        {
            // update keyboard state
            MyKeyboard.Update();

            // go to main menu
            if (MyKeyboard.IsPressed(KeyBindings.MENU_KEY))
            {
                Sounds.ButtonClick2.play(1.0f);
                Globals.ActiveScene = new MainMenuScene();
            }

            // turn off the game
            if (MyKeyboard.IsHeld(KeyBindings.SHUTDOWN_KEY))
            {
                SaveManager.SaveGame();
                Exit();
            }

            // toggle FPS counter
            if (MyKeyboard.IsPressed(KeyBindings.FPS_KEY))
            {
                Sounds.ButtonClick2.play(1.0f);
                FpsCounter.Toggle();
            }

            // update scenes
            IScene scene = Globals.ActiveScene;
            scene.Update(gameTime);

            FpsCounter.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime) // TODO: Add your drawing code here
        {
            GraphicsDevice.Clear(Color.White);
            MainSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // draw background
            Game.MainSpriteBatch.Draw(Textures.MenuBackground, new Rectangle(0, 0, (int)GameWindow.WindowSize.X, (int)GameWindow.WindowSize.Y), Color.White);

            // draw screens
            IScene scene = Globals.ActiveScene;
            scene.Draw(gameTime);

            // draw FPS counter
            if (FpsCounter.Active)
            {
                MainSpriteBatch.DrawString(Fonts.DefaultFont, (FpsCounter.Fps + " FPS").ToString(), new Vector2(0, 0), Color.White);
            }

            MainSpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
