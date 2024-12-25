using Microsoft.Xna.Framework;

namespace Caveworks
{
    internal class MainMenuScene : IScene
    {
        private static Button continueButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Continue", Fonts.menuButtonFont);
        private static Button startButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Start", Fonts.menuButtonFont);
        private static Button settingsButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Settings", Fonts.menuButtonFont);
        private static Button creditsButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Credits", Fonts.menuButtonFont);

        private static UiElement[] uiElements = {continueButton, startButton, settingsButton, creditsButton };


        public MainMenuScene()
        {
            if (!Globals.ExistsSaveFile())
            {
                continueButton.Deactivate();
            }

            continueButton.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 - 140), Anchor.Middle);
            startButton.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 - 70), Anchor.Middle);
            settingsButton.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2), Anchor.Middle);
            creditsButton.Place(new Vector2(GameWindow.GetWindowSize().X / 2, GameWindow.GetWindowSize().Y / 2 + 70), Anchor.Middle);
        }


        public void Update(GameTime gameTime) // do every frame
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (continueButton.IsPressed(MouseKey.Left))
            {
                Globals.SetActiveScene(null);
            }

            if (startButton.IsPressed(MouseKey.Left))
            {
                Globals.SetActiveScene(new StartScene());
            }

            if (settingsButton.IsPressed(MouseKey.Left))
            {
                Globals.SetActiveScene(new SettingsScene());
            }

            if (creditsButton.IsPressed(MouseKey.Left))
            {
                Globals.SetActiveScene(new CreditsScene());
            }
        }


        public void Draw(GameTime gameTime) // draw everything
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Draw();
            }
        }
    }
}
