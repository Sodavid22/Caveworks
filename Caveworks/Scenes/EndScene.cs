using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class EndScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.LargeFont);

        readonly static TextBox madeByTextBox = new TextBox(new Vector2(600, 60), Globals.UITextBoxColor, 1, "You Won!", Fonts.LargeFont);

        readonly static UiElement[] uiElements = { backgroundBox, madeByTextBox};


        public EndScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2), Anchor.Middle);

            madeByTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 - 140), Anchor.Middle);
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
