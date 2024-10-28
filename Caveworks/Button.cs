using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Caveworks
{
    public class Button
    {
        public Rectangle buttonRectangle; // position and size
        public float[] color = new float[3]; // RGB color
        public int borderSize; // size of button border
        public string text; // displayed text

        public Button(Rectangle rectangle, float[] color, string text, int borderSize)
        {
            this.buttonRectangle = rectangle;
            this.color = color;
            this.text = text;
            this.borderSize = borderSize;
        }

        // find out if mouse is hovering over the button
        public bool IsUnderCursor()
        {
            Vector2 mousePosition = KeyboardManager.GetMousePosition();

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
            Game.spriteBatch.Draw(Globals.whitePixel, new Rectangle(buttonRectangle.X - borderSize, buttonRectangle.Y - borderSize, buttonRectangle.Width + borderSize*2, buttonRectangle.Height + borderSize*2), Color.FromNonPremultiplied(new Vector4(0, 0, 0, 1)));

            if (IsUnderCursor())
            {                
                //draw darker
                Game.spriteBatch.Draw(Globals.whitePixel, buttonRectangle, Color.FromNonPremultiplied(new Vector4(color[0] - 0.3f, color[1] - 0.3f, color[2] - 0.3f, 1)));
            }
            else
            {
                // draw normally
                Game.spriteBatch.Draw(Globals.whitePixel, buttonRectangle, Color.FromNonPremultiplied(new Vector4(color[0], color[1], color[2], 1)));
            }
        }

        public void UpdatePosition(Vector2 position)
        {
            buttonRectangle = new Rectangle((int)position.X, (int)position.Y, buttonRectangle.Width, buttonRectangle.Height);
        }
    }
}
