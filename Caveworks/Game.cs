using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

            // set starting screen
            Globals.SetActiveScene(new MainMenuScene());
        }

        protected override void Update(GameTime gameTime) // TODO: Add your update logic here
        {
            // update keyboard state
            MyKeyboard.Update();

            // go to main menu
            if (MyKeyboard.IsPressed(KeyBindings.MENU_KEY))
            {
                Sounds.ButtonClick2.play(1.0f);
                Globals.SetActiveScene(new MainMenuScene());
            }

            // turn off the game
            if (MyKeyboard.IsHeld(KeyBindings.SHUTDOWN_KEY))
            {
                Exit();
            }

            // toggle FPS counter
            if (MyKeyboard.IsPressed(KeyBindings.FPS_KEY))
            {
                Sounds.ButtonClick2.play(1.0f);
                FpsCounter.Toggle();
            }

            // update scenes
            Scene scene = Globals.GetActiveScene();
            scene.Update(gameTime);

            FpsCounter.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) // TODO: Add your drawing code here
        {
            GraphicsDevice.Clear(Color.White);
            mainSpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // draw background
            Game.mainSpriteBatch.Draw(Textures.menuBackground, new Rectangle(0, 0, (int)GameWindow.GetWindowSize().X, (int)GameWindow.GetWindowSize().Y), Color.White);

            // draw screens
            Scene scene = Globals.GetActiveScene();
            scene.Draw(gameTime);

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
