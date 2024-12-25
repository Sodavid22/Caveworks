using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Net.Mime.MediaTypeNames;


namespace Caveworks
{
    public class Slider : UiElement
    {
        protected float value;

        protected static float hoverOverlayStrength = 0.2f;
        protected static float inactiveOverlayStrength = 0.5f;
        protected static SpriteFont font = Fonts.defaultFont;
        protected static Vector2 textSize = font.MeasureString("0.5");

        protected bool active = true;
        protected bool hovered;


        public Slider(Vector2 size, Vector4 color, int border, float startValue) : base(size, color, border)
        {
            this.value = startValue;
        }

        public override void Place(Vector2 position, Anchor anchor)
        {
            base.Place(position, anchor);
        }

        public override void Update()
        {
            Vector2 mousePosition = MyKeyboard.GetMousePosition();

            if (mousePosition.X > base.rectangle.X && mousePosition.X < base.rectangle.X + base.rectangle.Width)
            {
                if (mousePosition.Y > base.rectangle.Y && mousePosition.Y < base.rectangle.Y + base.rectangle.Height)
                {
                    hovered = true;
                }
                else { hovered = false; }
            }
            else { hovered = false; }

            if (hovered && MyKeyboard.IsHeld(MouseKey.Left))
            { 
                value = (float)Math.Round((MyKeyboard.GetMousePosition().X - rectangle.X) / rectangle.Width, 2);
            }
        }

        public override void Draw()
        {
            base.Draw();

            // borders
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, rectangle, Color.Black);
            // main body
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, new Rectangle(rectangle.X + border, rectangle.Y + border, rectangle.Width - border * 2, rectangle.Height - border * 2), color);
            // text
            Game.mainSpriteBatch.DrawString(font, value.ToString(), new Vector2((int)(rectangle.X + rectangle.Width / 2 - textSize.X / 2), (int)(rectangle.Y + rectangle.Height / 2 - textSize.Y / 2)), Color.Black);
        }

        public float GetValue()
        {
            return value;
        }
    }
}
