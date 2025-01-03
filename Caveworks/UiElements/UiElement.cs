using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class UiElement
    {
        protected Rectangle rectangle;
        protected Vector2 position;
        protected Anchor anchorPoint;
        protected Vector2 size;
        protected Color color;
        protected int border;


        public UiElement(Vector2 size, Vector4 color, int border)
        {
            this.size = size;
            this.color = Color.FromNonPremultiplied(color);
            this.border = border;
        }


        private void CreateRectangle()
        {   
            Vector2 newPosition = new Vector2(position.X, position.Y);

            if (anchorPoint == Anchor.TopRight)
            {
               newPosition.X = position.X - size.X;
            }
            else if (anchorPoint == Anchor.BottomLeft)
            {
                newPosition.Y = position.Y - size.Y;
            }
            else if (anchorPoint == Anchor.BottomRight)
            {
                newPosition.X = position.X - size.X;
                newPosition.Y = position.Y - size.Y;
            }
            else if (anchorPoint == Anchor.Middle)
            {
                newPosition.X = position.X - size.X/2;
                newPosition.Y = position.Y - size.Y/2;
            }

            this.rectangle = new Rectangle((int)newPosition.X, (int)newPosition.Y, (int)size.X, (int)size.Y);
        }


        public virtual void Place(Vector2 position, Anchor anchor)
        {
            this.position = position;
            this.anchorPoint = anchor;
            CreateRectangle();
        }


        public virtual void Update()
        {
            return;
        }


        public virtual void Draw()
        {
            return;
        }
    }
}
