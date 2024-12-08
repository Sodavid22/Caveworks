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
        public static Button continueButton = new Button(new Rectangle(0, 0, 250, 50), new float[] { 0, 0.3f, 0.3f }, 2, "CONTINUE", Fonts.menuButtonFont);
        public static Button startButton = new Button(new Rectangle(0, 0, 250, 50), new float[] { 0, 0.3f, 0.3f }, 2 , "START", Fonts.menuButtonFont);
        public static Button settingsButton = new Button(new Rectangle(0, 0, 250, 50), new float[] { 0, 0.3f, 0.3f }, 2, "SETTINGS", Fonts.menuButtonFont);
        public static Button creditsButton = new Button(new Rectangle(0, 0, 250, 50), new float[] { 0, 0.3f, 0.3f }, 2, "CREDITS", Fonts.menuButtonFont);

        public static Button[] buttons = {continueButton, startButton, settingsButton, creditsButton};

        public static void Load() // do once after switching to this screen
        {
            if (!Globals.activeGame)
            {
                continueButton.Deactivate();
            }
            continueButton.UpdatePosition(new Vector2((GameWindow.windowSize.X - continueButton.GetRectangle().Width) / 2, (GameWindow.windowSize.Y - continueButton.GetRectangle().Height) / 2 - 60));
            startButton.UpdatePosition(new Vector2((GameWindow.windowSize.X - startButton.GetRectangle().Width) / 2, (GameWindow.windowSize.Y - startButton.GetRectangle().Height) / 2 + 0));
            settingsButton.UpdatePosition(new Vector2((GameWindow.windowSize.X - settingsButton.GetRectangle().Width) / 2, (GameWindow.windowSize.Y - settingsButton.GetRectangle().Height) / 2 + 60));
            creditsButton.UpdatePosition(new Vector2((GameWindow.windowSize.X - creditsButton.GetRectangle().Width) / 2, (GameWindow.windowSize.Y - creditsButton.GetRectangle().Height) / 2 + 120));
        }

        public static void Update() // do every frame
        {
            if (continueButton.IsUnderCursor() && MyKeyboard.IsPressed(MouseKey.Left))
            {
                if (continueButton.IsActivated())
                {
                    Sounds.buttonClick.play(1.0f);
                    Globals.activeScreen = GameScreen.MainGame;
                }
                else
                {
                    Sounds.buttonDecline.play(1.0f);
                }
            }

            if (startButton.IsUnderCursor() && MyKeyboard.IsPressed(MouseKey.Left))
            {
                Sounds.buttonClick.play(1.0f);
                Globals.activeScreen = GameScreen.Start;
            }

            if (settingsButton.IsUnderCursor() && MyKeyboard.IsPressed(MouseKey.Left))
            {
                Sounds.buttonClick.play(1.0f);
                Globals.activeScreen = GameScreen.Settings;
                SettingsScreen.Load();
            }

            if (creditsButton.IsUnderCursor() && MyKeyboard.IsPressed(MouseKey.Left))
            {
                Sounds.buttonClick.play(1.0f);
                Globals.activeScreen = GameScreen.Credits;
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
