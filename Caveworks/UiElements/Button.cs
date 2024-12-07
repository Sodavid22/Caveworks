using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Caveworks
{
    public class Button
    {
        Rectangle buttonRectangle; // position and size
        float[] color = new float[3]; // RGB color
        int borderSize; // size of button border
        string text; // displayed text
        SpriteFont font;
        Vector2 textSize;
        bool active;

        public Button(Rectangle rectangle, float[] color, int borderSize, string text, SpriteFont font)
        {
            buttonRectangle = rectangle;
            this.color = color;
            this.borderSize = borderSize;
            this.text = text;
            this.font = font;
            textSize = font.MeasureString(text);
            active = true;
        }

        // find out if mouse is hovering over the button
        public bool IsUnderCursor()
        {
            Vector2 mousePosition = MyKeyboard.GetMousePosition();

            if (mousePosition.X > buttonRectangle.X && mousePosition.X < buttonRectangle.X + buttonRectangle.Width)
            {
                if (mousePosition.Y > buttonRectangle.Y && mousePosition.Y < buttonRectangle.Y + buttonRectangle.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            // draw button background
            Game.mainSpriteBatch.Draw(Textures.emptyTexture, new Rectangle(buttonRectangle.X - borderSize, buttonRectangle.Y - borderSize, buttonRectangle.Width + borderSize * 2, buttonRectangle.Height + borderSize * 2), Color.FromNonPremultiplied(new Vector4(0, 0, 0, 1)));

            if (IsUnderCursor() && active)
            {   // draw darker
                Game.mainSpriteBatch.Draw(Textures.emptyTexture, buttonRectangle, Color.FromNonPremultiplied(new Vector4(color[0] * 0.8f, color[1] * 0.8f, color[2] * 0.8f, 1)));
            }
            else if (active)
            {   // draw normally
                Game.mainSpriteBatch.Draw(Textures.emptyTexture, buttonRectangle, Color.FromNonPremultiplied(new Vector4(color[0], color[1], color[2], 1)));
            }
            else // draw dark
            {
                Game.mainSpriteBatch.Draw(Textures.emptyTexture, buttonRectangle, Color.FromNonPremultiplied(new Vector4(color[0] * 0.5f, color[1] * 0.5f, color[2] * 0.5f, 1)));
            }
            //draw text
            Game.mainSpriteBatch.DrawString(font, text, new Vector2(buttonRectangle.X + buttonRectangle.Width / 2 - textSize.X / 2, buttonRectangle.Y + buttonRectangle.Height / 2 - textSize.Y / 2), Color.White);

        }

        public Rectangle GetRectangle()
        {
            return buttonRectangle;
        }

        public void UpdatePosition(Vector2 position)
        {
            buttonRectangle = new Rectangle((int)position.X, (int)position.Y, buttonRectangle.Width, buttonRectangle.Height);
        }

        public void ChangeText(string text)
        {
            this.text = text;
            textSize = font.MeasureString(text);
        }

        public bool IsActivated()
        {
            return active;
        }

        public void Activate()
        {
            active = true;
        }

        public void Deactivate()
        {
            active = false;
        }

    }
}
