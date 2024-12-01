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
        public static Button toggleFullScreenButton = new Button(new Rectangle(0, 0, 500, 50), new float[] { 0, 0.3f, 0.3f }, 2, "Fullscreen: " + Globals.isFullscreen, Fonts.menuButtonFont);

        public static Button[] buttons = { toggleFullScreenButton};

        public static void Load()
        {
            toggleFullScreenButton.UpdatePosition(new Vector2((Globals.GetScreenSize().X - toggleFullScreenButton.GetRectangle().Width) / 2, (Globals.GetScreenSize().Y - toggleFullScreenButton.GetRectangle().Height) / 2 - 180));
        }

        public static void Update() // do every frame
        {
            if (toggleFullScreenButton.IsUnderCursor() && KeyboardManager.LeftClicked())
            {
                Sounds.buttonClick.play(1.0f);

                if (Globals.isFullscreen)
                {    // disable fullscreen
                    Globals.isFullscreen = false;
                    Game.graphics.PreferredBackBufferWidth = Globals.DISPLAY_WIDTH / 2;
                    Game.graphics.PreferredBackBufferHeight = Globals.DISPLAY_HEIGHT / 2;
                    Game.graphics.ToggleFullScreen();
                    Game.graphics.IsFullScreen = false;
                    Game.graphics.ApplyChanges();
                    toggleFullScreenButton.ChangeText("Fullscreen: " + Globals.isFullscreen);
                }
                else
                {   // enable fullscreen
                    Globals.isFullscreen = true;
                    Game.graphics.PreferredBackBufferWidth = Globals.DISPLAY_WIDTH;
                    Game.graphics.PreferredBackBufferHeight = Globals.DISPLAY_HEIGHT;
                    Game.graphics.ToggleFullScreen();
                    Game.graphics.IsFullScreen = true;
                    Game.graphics.ApplyChanges();
                    toggleFullScreenButton.ChangeText("Fullscreen: " + Globals.isFullscreen);
                }
                SettingsScreen.Load();
            }

        }
        public static void Draw()
        {
            Game.mainSpriteBatch.Draw(Globals.menuBackground, new Rectangle(0, 0, (int)Globals.GetScreenSize().X, (int)Globals.GetScreenSize().Y), Color.White);

            foreach (Button button in buttons)
            {
                button.Draw();
            }
        }
    }
}
