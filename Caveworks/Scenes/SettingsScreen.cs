using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks.GameScreens
{
    public static class SettingsScreen
    {
        public static Button toggleFullScreenButton = new Button(new Vector2(400, 60), new Vector4(0, 0.5f, 0.5f, 1), 2, "Fullscreen", Fonts.menuButtonFont);

        public static void Load()
        {
            toggleFullScreenButton.ChangeText("Fullscreen: " + GameWindow.isFullscreen);
            toggleFullScreenButton.Load(new Vector2(GameWindow.windowSize.X / 2, GameWindow.windowSize.Y / 2 - 100), Anchor.Middle);
        }

        public static void Update()
        {
            toggleFullScreenButton.Update();

            if (toggleFullScreenButton.IsPressed(MouseKey.Left))
            {
                GameWindow.ToggleFullscreen();
                toggleFullScreenButton.ChangeText("Fullscreen: " + GameWindow.isFullscreen);
                SettingsScreen.Load();
            }

        }
        public static void Draw()
        {
            toggleFullScreenButton.Draw();
        }
    }
}
