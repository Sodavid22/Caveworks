using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Caveworks
{
    public class Button : UiElement
    {        
        const float hoverOverlayStrength = 0.2f;
        const float inactiveOverlayStrength = 0.5f;

        protected string text;
        protected SpriteFont font;
        protected Texture2D texture;
        protected Vector2 textSize;
        protected bool active = true;
        protected bool hovered;


        public Button(Vector2 size, Vector4 color, int border, string text, SpriteFont font) : base(size, color, border)
        {
            this.text = text;
            this.font = font;
        }


        public Button(Vector2 size, Vector4 color, int border, Texture2D texture) : base(size, color, border)
        {
            this.texture = texture;
        }


        public override void Place(Vector2 position, Anchor anchor)
        {
            base.Place(position, anchor);
            if (text != null)
            {
                textSize = font.MeasureString(text);
            }
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
                else { hovered = false;}
            }
            else { hovered= false;}
        }


        public override void Draw()
        {
            // borders
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.Black);

            // main body
            Game.MainSpriteBatch.Draw(Textures.EmptyTexture, new Rectangle(rectangle.X + border, rectangle.Y + border, rectangle.Width - border * 2, rectangle.Height - border * 2), color);

            if (!active) // darken if inactive
            {
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.FromNonPremultiplied(1, 1, 1, (int)(color.A * inactiveOverlayStrength)));
            }
            else if (hovered) // darken if hovered
            {
                Game.MainSpriteBatch.Draw(Textures.EmptyTexture, rectangle, Color.FromNonPremultiplied(1, 1, 1, (int)(color.A * hoverOverlayStrength)));
            }

            // draw text
            if (text != null)
            {
                Game.MainSpriteBatch.DrawString(font, text, new Vector2((int)(rectangle.X + rectangle.Width / 2 - textSize.X / 2), (int)(rectangle.Y + rectangle.Height / 2 - textSize.Y / 2)), Color.Black);
            }
            // draw image
            else
            {
                Game.MainSpriteBatch.Draw(texture, new Rectangle(rectangle.X + border + 2, rectangle.Y + border + 2, rectangle.Width - border * 2 - 4, rectangle.Height - border * 2 - 4), Color.White);
            }
            
        }


        public bool IsPressed(MouseKey mouseKey)
        {
            if (MyKeyboard.IsPressed(mouseKey))
            {
                if (hovered)
                {
                    if (active)
                    {
                        Sounds.ButtonClick.play(1.0f);
                        return true;
                    }
                    else
                    {
                        Sounds.ButtonDecline.play(1.0f);
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