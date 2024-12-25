using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class CreditsScene : IScene
    {
        private static TextBox madeByTextBox = new TextBox(new Vector2(600, 60), new Vector4(0.5f, 0.5f, 0.5f, 1), 2, "Made by: David Sobek", Fonts.menuButtonFont);

        private static UiElement[] uiElements = {madeByTextBox };


        public CreditsScene()
        {
            madeByTextBox.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 - 140), Anchor.Middle);
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }
        }


        public void Draw(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Draw();
            }
        }
    }
}
