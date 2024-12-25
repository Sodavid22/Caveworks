﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Caveworks
{
    public class TextBox : UiElement
    {
        protected string text;
        protected SpriteFont font;
        protected Vector2 textSize;

        public TextBox(Vector2 size, Vector4 color, int border, string text, SpriteFont font) : base(size, color, border)
        {
            this.text = text;
            this.font = font;
        }


        public override void Place(Vector2 position, Anchor anchor)
        {
            base.Place(position, anchor);
            textSize = font.MeasureString(text);
        }

        public override void Draw()
        {
            // borders
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, rectangle, Color.Black);

            // main body
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, new Rectangle(rectangle.X + border, rectangle.Y + border, rectangle.Width - border * 2, rectangle.Height - border * 2), color);

            // text
            Game.mainSpriteBatch.DrawString(font, text, new Vector2((int)(rectangle.X + rectangle.Width/2 - textSize.X/2), (int)(rectangle.Y + rectangle.Height/2 - textSize.Y/2)), Color.Black);
        }
    }
}
