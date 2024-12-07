using System;
using System.Diagnostics;
using System.Xml;
using Caveworks.SoundEffects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks.GameScreens
{
    public static class SettingsScreen
    {
        public static Button toggleFullScreenButton = new Button(new Rectangle(0, 0, 500, 50), new float[] { 0, 0.3f, 0.3f }, 2, "Fullscreen: " + GameWindow.isFullscreen, Fonts.menuButtonFont);

        public static Button[] buttons = { toggleFullScreenButton};

        public static void Load()
        {
            toggleFullScreenButton.UpdatePosition(new Vector2((GameWindow.windowSize.X - toggleFullScreenButton.GetRectangle().Width) / 2, (GameWindow.windowSize.Y - toggleFullScreenButton.GetRectangle().Height) / 2 - 180));
        }

        public static void Update() // do every frame
        {
            if (toggleFullScreenButton.IsUnderCursor() && MyKeyboard.IsPressed(MouseKey.Left))
            {
                Sounds.buttonClick.play(1.0f);
                GameWindow.ToggleFullscreen();
                toggleFullScreenButton.ChangeText("Fullscreen: " + GameWindow.isFullscreen);
                SettingsScreen.Load();
            }

        }
        public static void Draw()
        {
            foreach (Button button in buttons)
            {
                button.Draw();
            }
        }
    }
}
