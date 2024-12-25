using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class SettingsScene : IScene
    {
        private static Button fullscreenButton = new Button(new Vector2(400, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Fullscreen: ", Fonts.menuButtonFont);
        
        private static Slider volumeSlider = new Slider(new Vector2(400, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, Globals.GetGlobalVolume());

        private static UiElement[] uiElements = {fullscreenButton, volumeSlider};


        public SettingsScene()
        {
            fullscreenButton.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 - 140), Anchor.Middle);
            fullscreenButton.ChangeText("Fullscreen: " + GameWindow.IsFullscreen());

            volumeSlider.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 - 70), Anchor.Middle);
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
                fullscreenButton.ChangeText("Fullscreen: " + GameWindow.IsFullscreen());
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
