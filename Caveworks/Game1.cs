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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() // TODO: Add your initialization logic here
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
        }

        protected override void LoadContent() // TODO: use this.Content to load your game content here
        {

        }

        protected override void Update(GameTime gameTime) // TODO: Add your update logic here
        {

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
