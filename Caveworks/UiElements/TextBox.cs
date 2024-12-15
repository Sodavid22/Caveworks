using Microsoft.Xna.Framework;
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


        public override void Load(Vector2 position, Anchor anchor)
        {
            base.Load(position, anchor);
            textSize = font.MeasureString(text);
        }

        public override void Draw()
        {
            base.Draw();
            Game.mainSpriteBatch.DrawString(font, text, new Vector2((int)(rectangle.X + rectangle.Width/2 - textSize.X/2), (int)(rectangle.Y + rectangle.Height/2 - textSize.Y/2)), Color.Black);
        }
    }
}
