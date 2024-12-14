using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Caveworks.GameScreens
{
    public static class MainMenuScreen
    {
        public static Button continueButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Continue", Fonts.menuButtonFont);
        public static Button startButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Start", Fonts.menuButtonFont);
        public static Button settingsButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Settings", Fonts.menuButtonFont);
        public static Button creditsButton = new Button(new Vector2(200, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Credits", Fonts.menuButtonFont);

        public static Button[] buttons = {continueButton, startButton, settingsButton, creditsButton};

        public static void Load() // do once after switching to this screen
        {
            if (!Globals.activeGame)
            {
                continueButton.Deactivate();
            }
            continueButton.Load(new Vector2(GameWindow.windowSize.X / 2, GameWindow.windowSize.Y / 2 - 140), Anchor.Middle);
            startButton.Load(new Vector2(GameWindow.windowSize.X / 2, GameWindow.windowSize.Y / 2 - 70), Anchor.Middle);
            settingsButton.Load(new Vector2(GameWindow.windowSize.X / 2, GameWindow.windowSize.Y / 2), Anchor.Middle);
            creditsButton.Load(new Vector2(GameWindow.windowSize.X / 2, GameWindow.windowSize.Y / 2 + 70), Anchor.Middle);
        }

        public static void Update() // do every frame
        {
            foreach (Button button in buttons)
            {
                button.Update();
            }

            if (continueButton.IsPressed(MouseKey.Left))
            {
                Globals.activeScreen = GameScreen.MainGame;
            }

            if (startButton.IsPressed(MouseKey.Left))
            {
                Globals.activeScreen = GameScreen.Start;
                StartScreen.Load();
            }

            if (settingsButton.IsPressed(MouseKey.Left))
            {
                Globals.activeScreen = GameScreen.Settings;
                SettingsScreen.Load();
            }

            if (creditsButton.IsPressed(MouseKey.Left))
            {
                Globals.activeScreen = GameScreen.Credits;
                CreditsScreen.Load();
            }
        }

        public static void Draw() // draw everything
        {
            foreach (Button button in buttons)
            {
                button.Draw();
            }
        }
    }
}
