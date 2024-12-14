using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Caveworks
{
    public class Button : TextBox
    {
        protected static float hoverOverlayStrength = 0.2f;
        protected static float inactiveOverlayStrength = 0.5f;

        protected bool active;
        protected bool hovered;

        public Button(Vector2 size, Vector4 color, int border, string text, SpriteFont font) : base(size, color, border, text, font)
        {
        }

        new public void Load(Vector2 position, Anchor anchor)
        {
            base.Load(position, anchor);
            active = true;
        }

        public void Update()
        {
            if (IsUnderCursor())
            {
                hovered = true;
            }
            else
            {
                hovered = false;
            }
        }

        new public void Draw() 
        {
            base.Draw();
            if (!active)
            {
                Game.mainSpriteBatch.Draw(Textures.emptyTexture, rectangle, Color.FromNonPremultiplied(1, 1, 1, (int)(color.A * inactiveOverlayStrength)));
            }
            else if (hovered)
            {
                Game.mainSpriteBatch.Draw(Textures.emptyTexture, rectangle, Color.FromNonPremultiplied(1, 1, 1, (int)(color.A * hoverOverlayStrength)));
            }
        }

        public bool IsUnderCursor()
        {
            Vector2 mousePosition = MyKeyboard.GetMousePosition();

            if (mousePosition.X > base.rectangle.X && mousePosition.X < base.rectangle.X + base.rectangle.Width)
            {
                if (mousePosition.Y > base.rectangle.Y && mousePosition.Y < base.rectangle.Y + base.rectangle.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsPressed(MouseKey mouseKey)
        {
            if (MyKeyboard.IsPressed(mouseKey))
            {
                if (hovered)
                {
                    if (active)
                    {
                        Sounds.buttonClick.play(1.0f);
                        return true;
                    }
                    else
                    {
                        Sounds.buttonDecline.play(1.0f);
                    }
                }
            }
            return false;
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
