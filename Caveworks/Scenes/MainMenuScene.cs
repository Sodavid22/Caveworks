using Microsoft.Xna.Framework;


namespace Caveworks
{
    internal class MainMenuScene : IScene
    {
        readonly static Button continueButton = new Button(new Vector2(200, 60), Globals.UIButtonColor, 2, "Continue", Fonts.MenuButtonFont);
        readonly static Button startButton = new Button(new Vector2(200, 60), Globals.UIButtonColor, 2, "Start", Fonts.MenuButtonFont);
        readonly static Button settingsButton = new Button(new Vector2(200, 60), Globals.UIButtonColor, 2, "Settings", Fonts.MenuButtonFont);
        readonly static Button creditsButton = new Button(new Vector2(200, 60), Globals.UIButtonColor, 2, "Credits", Fonts.MenuButtonFont);
        readonly static Button exitButton = new Button(new Vector2(200, 60), Globals.UIButtonColor, 2, "Exit", Fonts.MenuButtonFont);

        readonly static UiElement[] uiElements = { continueButton, startButton, settingsButton, creditsButton, exitButton };


        public MainMenuScene()
        {
            continueButton.Activate();

            if (Globals.World == null)
            {
                continueButton.Deactivate();
            }

            continueButton.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2 - 140), Anchor.Middle);
            startButton.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2 - 70), Anchor.Middle);
            settingsButton.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2), Anchor.Middle);
            creditsButton.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2 + 70), Anchor.Middle);
            exitButton.Place(new Vector2(GameWindow.WindowSize.X / 2, GameWindow.WindowSize.Y / 2 + 140), Anchor.Middle);
        }


        public void Update(GameTime gameTime) // do every frame
        {
            foreach (UiElement uiElement in uiElements)
            {
                uiElement.Update();
            }

            if (continueButton.IsPressed(MouseKey.Left))
            {
                Globals.ActiveScene = new WorldScene();
            }

            if (startButton.IsPressed(MouseKey.Left))
            {
                Globals.ActiveScene = new StartScene();
            }

            if (settingsButton.IsPressed(MouseKey.Left))
            {
                Globals.ActiveScene = new SettingsScene();
            }

            if (creditsButton.IsPressed(MouseKey.Left))
            {
                Globals.ActiveScene = new CreditsScene();
            }

            if (exitButton.IsPressed(MouseKey.Left))
            {
                SaveManager.SaveGame();
                Game.Self.Exit();
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
