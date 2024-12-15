using Microsoft.Xna.Framework;

namespace Caveworks
{
    public class SettingsScene : IScene
    {
        private static Button fullscreenButton = new Button(new Vector2(400, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Fullscreen: ", Fonts.menuButtonFont);

        private static Button[] buttons = {fullscreenButton};

        public SettingsScene()
        {
            fullscreenButton.Load(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 - 140), Anchor.Middle);
            fullscreenButton.ChangeText("Fullscreen: " + GameWindow.IsFullscreen());
        }
        public void Update(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Update();
            }

            if (fullscreenButton.IsPressed(MouseKey.Left))
            {
                GameWindow.ToggleFullscreen();
                fullscreenButton.ChangeText("Fullscreen: " + GameWindow.IsFullscreen());
                Globals.SetActiveScene(new SettingsScene());
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Button button in buttons)
            {
                button.Draw();
            }
        }
    }
}
