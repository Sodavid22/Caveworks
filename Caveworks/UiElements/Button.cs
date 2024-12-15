using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Caveworks
{
    public class Button : UiElement
    {
        protected string text;
        protected SpriteFont font;
        protected Vector2 textSize;

        protected static float hoverOverlayStrength = 0.2f;
        protected static float inactiveOverlayStrength = 0.5f;

        protected bool active = true;
        protected bool hovered;

        public Button(Vector2 size, Vector4 color, int border, string text, SpriteFont font) : base(size, color, border)
        {
            this.text = text;
            this.font = font;
        }

        public override void Load(Vector2 position, Anchor anchor)
        {
            base.Load(position, anchor);
            textSize = font.MeasureString(text);
        }

       public override void Update()
        {
            Vector2 mousePosition = MyKeyboard.GetMousePosition();

            if (mousePosition.X > base.rectangle.X && mousePosition.X < base.rectangle.X + base.rectangle.Width)
            {
                if (mousePosition.Y > base.rectangle.Y && mousePosition.Y < base.rectangle.Y + base.rectangle.Height)
                {
                    hovered = true;
                    return;
                }
                else { hovered = false;}
            }
            else { hovered= false;}
        }

        public override void Draw()
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
            // draw text
            Game.mainSpriteBatch.DrawString(font, text, new Vector2((int)(rectangle.X + rectangle.Width / 2 - textSize.X / 2), (int)(rectangle.Y + rectangle.Height / 2 - textSize.Y / 2)), Color.Black);
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