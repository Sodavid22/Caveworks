using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Caveworks
{
    public class Scene
    {
        protected static List<UiElement> uiElements;

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
