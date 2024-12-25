using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Net.Mime.MediaTypeNames;


namespace Caveworks
{
    public class Slider : UiElement
    {
        protected float value;
        protected bool moving;
        protected Vector2 textSize;

        protected static float hoverOverlayStrength = 0.2f;
        protected static float inactiveOverlayStrength = 0.5f;
        protected static SpriteFont font = Fonts.defaultFont;
        protected static int knobSize = 42;
        

        protected bool active = true;
        protected bool hovered;


        public Slider(Vector2 size, Vector4 color, int border, float startValue) : base(size, color, border)
        {
            this.value = startValue;
            this.textSize = font.MeasureString(value.ToString());
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

            if (hovered && MyKeyboard.IsPressed(MouseKey.Left))
            {
                moving = true;
            }

            if (MyKeyboard.IsReleased(MouseKey.Left))
            {
                moving = false;
            }

            if (moving)
            { 
                value = (float)Math.Round((MyKeyboard.GetMousePosition().X - rectangle.X) / rectangle.Width, 2);
                value = Math.Clamp(value, 0, 1);
                textSize = font.MeasureString(value.ToString());
            }
        }

        public override void Draw()
        {
            base.Draw();

            // main borders
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, rectangle, Color.Black);
            // main body
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, new Rectangle(rectangle.X + border, rectangle.Y + border, rectangle.Width - border * 2, rectangle.Height - border * 2), color);

            // knob borders
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, new Rectangle((int)(rectangle.X + (value * rectangle.Width) - knobSize/2), rectangle.Y - knobSize/4, knobSize, knobSize), Color.Black);

            // knob body
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, new Rectangle((int)(rectangle.X + (value * rectangle.Width) - knobSize/2 + border), rectangle.Y - knobSize/4 + border, knobSize - border*2, knobSize - border*2), color);

            // text
            Game.mainSpriteBatch.DrawString(font, value.ToString(), new Vector2((int)(rectangle.X + (value * rectangle.Width) - knobSize/2 + (knobSize - textSize.X) / 2), rectangle.Y - knobSize/4 + (knobSize - textSize.Y) / 2), Color.Black);
        }

        public float GetValue()
        {
            return value;
        }
    }
}
