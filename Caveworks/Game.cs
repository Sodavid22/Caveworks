using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Caveworks
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatch FloorSpriteBatch { get; private set; }
        public static SpriteBatch ItemSpritebatch { get; private set; }
        public static SpriteBatch CreatureSpritebatch { get; private set; }
        public static SpriteBatch WallSpritebatch { get; private set; }
        public static SpriteBatch ShadowSpriteBatch { get; private set; }
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
            FloorSpriteBatch = new SpriteBatch(GraphicsDevice);
            ItemSpritebatch = new SpriteBatch(GraphicsDevice);
            CreatureSpritebatch = new SpriteBatch(GraphicsDevice);
            WallSpritebatch = new SpriteBatch(GraphicsDevice);
            ShadowSpriteBatch = new SpriteBatch(GraphicsDevice);
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
            SaveManager.LoadSettings();
            Globals.ExistsSave = SaveManager.ExistsWorldSave();

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
                Sounds.ButtonClick2.Play(1.0f);
                Globals.ActiveScene = new MainMenuScene();
                if (Globals.World != null)
                {
                    Globals.World.Player.CloseUi();
                }
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
                Sounds.ButtonClick2.Play(1.0f);
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

            FloorSpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            ItemSpritebatch.Begin(samplerState: SamplerState.PointClamp);
            CreatureSpritebatch.Begin(samplerState: SamplerState.PointClamp);
            WallSpritebatch.Begin(samplerState: SamplerState.PointClamp);
            ShadowSpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            MainSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // draw background
            Game.FloorSpriteBatch.Draw(Textures.MenuBackground, new Rectangle(0, 0, (int)GameWindow.Size.X, (int)GameWindow.Size.Y), Color.White);

            // draw screens
            IScene scene = Globals.ActiveScene;
            scene.Draw(gameTime);

            // draw FPS counter
            if (FpsCounter.Active)
            {
                MainSpriteBatch.DrawString(Fonts.DefaultFont, (FpsCounter.Fps + " FPS").ToString(), new Vector2(0, 0), Color.White);
                MainSpriteBatch.DrawString(Fonts.DefaultFont, (1 / FpsCounter.Fps + " Frame Time").ToString(), new Vector2(0, 20), Color.White);
            }

            FloorSpriteBatch.End();
            ItemSpritebatch.End();
            CreatureSpritebatch.End();
            WallSpritebatch.End();
            ShadowSpriteBatch.End();
            MainSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
