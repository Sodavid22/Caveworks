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
        public Rectangle rectangle;
        public Color color;
        public string text;

        public Button(Rectangle rectangle, Vector4 color, string text)
        {
            this.rectangle = rectangle;
            this.color = Color.FromNonPremultiplied(color);
            this.text = text;
        }

        public bool IsClicked() // TODO
        {
            return false;
        }
    }
}
