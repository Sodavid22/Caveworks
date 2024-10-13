using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D playerTexture;
        Vector2 playerPositon;
        float playerSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            playerPositon = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2);
            playerSpeed = 100f;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("BlueMan");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float playerMoveDistance = playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                playerPositon.X -= playerMoveDistance;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                playerPositon.X += playerMoveDistance;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                playerPositon.Y -= playerMoveDistance;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerPositon.Y += playerMoveDistance;
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(playerTexture, playerPositon, Color.White);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
