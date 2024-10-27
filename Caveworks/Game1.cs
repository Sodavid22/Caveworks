using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D whiteRectangle;
        public int testInt; //Useless

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() // TODO: Add your initialization logic here
        {
            // set screen parameters
            graphics.PreferredBackBufferWidth = Globals.screenWidth;
            graphics.PreferredBackBufferHeight = Globals.screenHeight;
            graphics.ApplyChanges();

            // create sprite batch
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create empty texture for future use
            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
        }

        protected override void LoadContent() // TODO: use this.Content to load your game content here
        {

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
                graphics.ToggleFullScreen();
                graphics.ApplyChanges();
            }
        }

        protected override void Draw(GameTime gameTime) // TODO: Add your drawing code here
        {
            spriteBatch.Begin();
            spriteBatch.Draw(whiteRectangle, new Rectangle(100, 100, 200, 200), Color.FromNonPremultiplied(new Vector4(255, 0, 0, 0.5f)));
            spriteBatch.Draw(whiteRectangle, new Rectangle(150, 200, 200, 200), Color.FromNonPremultiplied(new Vector4(0, 255, 0, 0.5f)));
            spriteBatch.Draw(whiteRectangle, new Rectangle(200, 150, 200, 200), Color.FromNonPremultiplied(new Vector4(0, 0, 255, 0.5f)));
            spriteBatch.End();
        }
    }
}
