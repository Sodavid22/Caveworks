using System;
using System.Diagnostics;
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
        Vector2 playerOrigin;
        float playerSpeed;
        float playerRotation;

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
            playerOrigin = new Vector2(playerTexture.Width/2, playerTexture.Height/2);
            playerSpeed = 100f;
            playerRotation = 0;
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerTexture = Content.Load<Texture2D>("BlueMan");
            

            
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

            Vector2 mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 playerDirection = new Vector2(mousePosition.X - playerPositon.X, mousePosition.Y - playerPositon.Y);
            Debug.WriteLine(playerDirection.X + " " + playerDirection.Y);
            playerRotation = (float)Math.Atan2(playerDirection.Y, playerDirection.X) + (float)Math.PI/2;


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointWrap);
            _spriteBatch.Draw(playerTexture, playerPositon, null,  Color.White, playerRotation, playerOrigin, 2f, SpriteEffects.None, 0f);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
