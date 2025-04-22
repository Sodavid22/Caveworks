using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class CreditsScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.LargeFont);

        readonly static TextBox madeByTextBox = new TextBox(new Vector2(700, 60), Globals.UITextBoxColor, 2, "Made by: David Sobek", Fonts.LargeFont);

        readonly static TextBox soundsTextBox = new TextBox(new Vector2(700, 60), Globals.UITextBoxColor, 2, "Sound: Freesound, Pixabay", Fonts.LargeFont);

        readonly static TextBox backgroundTextBox = new TextBox(new Vector2(700, 60), Globals.UITextBoxColor, 2, "Background: Wallpaperflare", Fonts.LargeFont);

        readonly static TextBox thanksTextBox = new TextBox(new Vector2(700, 120), Globals.UITextBoxColor, 2, "Thank you for playing!", Fonts.LargeFont);

        readonly static UiElement[] uiElements = { backgroundBox, madeByTextBox, soundsTextBox, backgroundTextBox, thanksTextBox};


        public CreditsScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2), Anchor.Middle);

            madeByTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 - 140), Anchor.Middle);

            soundsTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 - 70), Anchor.Middle);

            backgroundTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 - 0), Anchor.Middle);

            thanksTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 + 105), Anchor.Middle);
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
