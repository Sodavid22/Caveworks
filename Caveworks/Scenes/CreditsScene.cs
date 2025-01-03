using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class CreditsScene : IScene
    {
        readonly static TextBox madeByTextBox = new TextBox(new Vector2(600, 60), Globals.UITextBoxColor, 2, "Made by: David Sobek", Fonts.MenuButtonFont);

        readonly static UiElement[] uiElements = {madeByTextBox };


        public CreditsScene()
        {
            madeByTextBox.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2 - 140), Anchor.Middle);
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
