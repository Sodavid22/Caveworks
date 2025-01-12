using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace Caveworks
{
    public class Slider : UiElement
    {
        readonly static SpriteFont font = Fonts.DefaultFont;

        protected int knobSize = 2;
        protected float sliderPosition; // value 0.0 - 1.0
        protected float value; // min - max value
        protected float minValue;
        protected float maxValue;
        protected bool moving;
        protected Vector2 textSize;
        protected bool active = true;
        protected bool hovered;


        public Slider(Vector2 size, Vector4 color, int border, float minValue, float startValue, float maxValue) : base(size, color, border)
        {
            this.sliderPosition = (startValue - minValue) / (maxValue - minValue);
            this.value = startValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.textSize = font.MeasureString(value.ToString());
            this.knobSize = (int)size.Y * 2;
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
                sliderPosition = (float)Math.Round((MyKeyboard.GetMousePosition().X - rectangle.X) / rectangle.Width, 2);
                sliderPosition = Math.Clamp(sliderPosition, 0, 1);
                value = minValue + sliderPosition * (maxValue - minValue);
                value = (float)Math.Round(value);
                textSize = font.MeasureString(value.ToString());
            }
        }


        public override void Draw()
        {
            base.Draw();

            // main borders
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.Black);

            // main body
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(rectangle.X + border, rectangle.Y + border, rectangle.Width - border * 2, rectangle.Height - border * 2), color);

            // knob borders
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle((int)(rectangle.X + (sliderPosition * rectangle.Width) - knobSize/2), rectangle.Y - knobSize/4, knobSize, knobSize), Color.Black);

            // knob body
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle((int)(rectangle.X + (sliderPosition * rectangle.Width) - knobSize/2 + border), rectangle.Y - knobSize/4 + border, knobSize - border*2, knobSize - border*2), color);

            // text
            Game.MainSpriteBatch.DrawString(font, value.ToString(), new Vector2((int)(rectangle.X + (sliderPosition * rectangle.Width) - knobSize/2 + (knobSize - textSize.X) / 2), rectangle.Y - knobSize/4 + (knobSize - textSize.Y) / 2), Color.Black);
        }


        public float GetValue()
        {
            return value;
        }
    }
}
