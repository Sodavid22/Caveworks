using System;
using System.Diagnostics;
using System.Xml;
using Caveworks.SoundEffects;
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
            continueButton.UpdatePosition(new Vector2((Globals.GetScreenSize().X - continueButton.GetRectangle().Width) / 2, (Globals.GetScreenSize().Y - continueButton.GetRectangle().Height) / 2 - 60));
            startButton.UpdatePosition(new Vector2((Globals.GetScreenSize().X - startButton.GetRectangle().Width) / 2, (Globals.GetScreenSize().Y - startButton.GetRectangle().Height) / 2 + 0));
            settingsButton.UpdatePosition(new Vector2((Globals.GetScreenSize().X - settingsButton.GetRectangle().Width) / 2, (Globals.GetScreenSize().Y - settingsButton.GetRectangle().Height) / 2 + 60));
            creditsButton.UpdatePosition(new Vector2((Globals.GetScreenSize().X - creditsButton.GetRectangle().Width) / 2, (Globals.GetScreenSize().Y - creditsButton.GetRectangle().Height) / 2 + 120));
        }

        public static void Update() // do every frame
        {
            if (continueButton.IsUnderCursor() && KeyboardManager.LeftClicked())
            {
                if (continueButton.IsActivated())
                {
                    Sounds.buttonClick.play(1.0f);
                    Globals.activeScreen = Enums.GameScreen.MainGame;
                }
                else
                {
                    Sounds.buttonDecline.play(1.0f);
                }
            }

            if (startButton.IsUnderCursor() && KeyboardManager.LeftClicked())
            {
                Sounds.buttonClick.play(1.0f);
                Globals.activeScreen = Enums.GameScreen.Start;
            }

            if (settingsButton.IsUnderCursor() && KeyboardManager.LeftClicked())
            {
                Sounds.buttonClick.play(1.0f);
                Globals.activeScreen = Enums.GameScreen.Settings;
                SettingsScreen.Load();
            }

            if (creditsButton.IsUnderCursor() && KeyboardManager.LeftClicked())
            {
                Sounds.buttonClick.play(1.0f);
                Globals.activeScreen = Enums.GameScreen.Credits;
            }
        }

        public static void Draw() // draw everything
        {
            Game.mainSpriteBatch.Draw(Globals.menuBackground, new Rectangle(0, 0, (int)Globals.GetScreenSize().X, (int)Globals.GetScreenSize().Y), Color.White);

            foreach (Button button in buttons)
            {
                button.Draw();
            }
        }
    }
}
