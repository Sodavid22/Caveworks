using Microsoft.Xna.Framework;


namespace Caveworks
{
    public class CreditsScene : IScene
    {
        readonly static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.UITextBoxColor, 4, "", Fonts.MenuButtonFont);

        readonly static TextBox madeByTextBox = new TextBox(new Vector2(600, 60), Globals.UITextBoxColor, 2, "Made by: David Sobek", Fonts.MenuButtonFont);

        // TESTCODE
        readonly static Button imageButton = new Button(new Vector2(64, 64), Globals.UIButtonColor, 2, Textures.Player);

        readonly static UiElement[] uiElements = { backgroundBox, madeByTextBox, imageButton };


        public CreditsScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2), Anchor.Middle);

            madeByTextBox.Place(new Vector2(GameWindow.Size.X / 2, GameWindow.Size.Y / 2 - 140), Anchor.Middle);

            imageButton.Place(new Vector2(10, 10), Anchor.TopLeft);
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (imageButton.IsPressed(MouseKey.Left))
            {
                Sounds.ButtonClick.Play(1);
            }

            if (imageButton.IsPressed(MouseKey.Right))
            {
                Sounds.ButtonClick2.Play(1);
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
