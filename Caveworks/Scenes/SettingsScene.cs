using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class SettingsScene : IScene
    {
        private static TextBox backgroundBox = new TextBox(new Vector2(800, 400), Globals.GetUITextBoxColor(), 4, "", Fonts.menuButtonFont);

        private static TextBox fullscreenText = new TextBox(new Vector2(350, 60), Globals.GetUITextBoxColor(), 2, "Fullscreen:", Fonts.menuButtonFont);
        private static Button fullscreenButton = new Button(new Vector2(350, 60), Globals.GetUIButtonColor(), 2, GameWindow.IsFullscreen().ToString(), Fonts.menuButtonFont);

        private static TextBox volumeText = new TextBox(new Vector2(350, 60), Globals.GetUITextBoxColor(), 2, "Volume:", Fonts.menuButtonFont);
        private static Slider volumeSlider = new Slider(new Vector2(350, 20), Globals.GetUIButtonColor(), 2, Globals.GetGlobalVolume());

        private static UiElement[] uiElements = { backgroundBox, fullscreenText, fullscreenButton, volumeText, volumeSlider};


        public SettingsScene()
        {
            backgroundBox.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2), Anchor.Middle);

            fullscreenText.Place(new Vector2(GameWindow.GetWindowSize().X / 2 - 200, GameWindow.GetWindowSize().Y / 2 - 140), Anchor.Middle);
            fullscreenButton.Place(new Vector2(GameWindow.GetWindowSize().X / 2 + 200, GameWindow.GetWindowSize().Y / 2 - 140), Anchor.Middle);

            volumeText.Place(new Vector2(GameWindow.GetWindowSize().X / 2 - 200, GameWindow.GetWindowSize().Y / 2 - 70), Anchor.Middle);
            volumeSlider.Place(new Vector2(GameWindow.GetWindowSize().X / 2 + 200, GameWindow.GetWindowSize().Y / 2 - 70), Anchor.Middle);
        }


        public void Update(GameTime gameTime)
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (fullscreenButton.IsPressed(MouseKey.Left))
            {
                GameWindow.ToggleFullscreen();
                fullscreenButton.ChangeText(GameWindow.IsFullscreen().ToString());
                Globals.SetActiveScene(new SettingsScene());
            }

            Globals.SetGlobalVolume(volumeSlider.GetValue());
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
