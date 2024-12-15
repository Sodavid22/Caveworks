using Microsoft.Xna.Framework;

namespace Caveworks
{
    public interface IScene
    {
        protected static UiElement[] uiElements;

        public virtual void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Draw();
            }
        }
    }
}
