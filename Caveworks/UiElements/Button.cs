using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Caveworks
{
    public class Button : UiElement
    {        
        const float hoverOverlayStrength = 0.2f;
        const float inactiveOverlayStrength = 0.5f;

        protected string Text;
        protected SpriteFont Font;
        protected Texture2D Texture;
        protected Vector2 textSize;
        protected bool Active = true;
        protected bool Hovered;
        protected bool Muted = false;


        public Button(Vector2 size, Vector4 color, int border, string text, SpriteFont font) : base(size, color, border)
        {
            this.Text = text;
            this.Font = font;
        }


        public Button(Vector2 size, Vector4 color, int border, Texture2D texture) : base(size, color, border)
        {
            this.Texture = texture;
        }


        public override void Place(Vector2 position, Anchor anchor)
        {
            base.Place(position, anchor);
            if (Text != null)
            {
                textSize = Font.MeasureString(Text);
            }
        }


       public override void Update()
        {
            Vector2 mousePosition = MyKeyboard.GetMousePosition();

            if (mousePosition.X > base.rectangle.X && mousePosition.X < base.rectangle.X + base.rectangle.Width)
            {
                if (mousePosition.Y > base.rectangle.Y && mousePosition.Y < base.rectangle.Y + base.rectangle.Height)
                {
                    Hovered = true;
                }
                else { Hovered = false;}
            }
            else { Hovered= false;}
        }


        public override void Draw()
        {
            // borders
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.Black);

            // main body
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(rectangle.X + border, rectangle.Y + border, rectangle.Width - border * 2, rectangle.Height - border * 2), color);

            if (!Active) // darken if inactive
            {
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.FromNonPremultiplied(1, 1, 1, (int)(color.A * inactiveOverlayStrength)));
            }
            else if (Hovered) // darken if hovered
            {
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.FromNonPremultiplied(1, 1, 1, (int)(color.A * hoverOverlayStrength)));
            }


            // draw image
            if (Texture != null)
            {
                Game.MainSpriteBatch.Draw(Texture, new Rectangle(rectangle.X + border + 2, rectangle.Y + border + 2, rectangle.Width - border * 2 - 4, rectangle.Height - border * 2 - 4), Color.White);
            }
            // draw text
            if (Text != null)
            {
                Game.MainSpriteBatch.DrawString(Font, Text, new Vector2((int)(rectangle.X + rectangle.Width / 2 - textSize.X / 2), (int)(rectangle.Y + rectangle.Height / 2 - textSize.Y / 2)), Color.Black);
            }
        }


        public bool IsPressed(MouseKey mouseKey)
        {
            if (MyKeyboard.IsPressed(mouseKey))
            {
                if (Hovered)
                {
                    if (Active)
                    {
                        if (!Muted) { Sounds.ButtonClick.Play(1.0f); }
                        return true;
                    }
                    else
                    {
                        if (!Muted) { Sounds.ButtonDecline.Play(1.0f); }
                    }
                }
            }
            return false;
        }


        public void SetTexture(Texture2D texture)
        {
            Texture = texture;
        }


        public void SetText(string text)
        {
            this.Text = text;
            if (Text != null)
            {
                textSize = Font.MeasureString(text);
            }
        }


        public void SetColor(Color color)
        {
            this.color = color;
        }


        public Rectangle GetRectangle()
        {
            return rectangle;
        }


        public bool IsActivated()
        {
            return Active;
        }


        public void Activate()
        {
            Active = true;
        }


        public void Deactivate()
        {
            Active = false;
        }


        public void Mute()
        {
            Muted = true;
        }


        public bool IsHovered()
        {
            return Hovered;
        }
    }
}