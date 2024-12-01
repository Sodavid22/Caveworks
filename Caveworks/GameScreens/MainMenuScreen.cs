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
        public static Button startButton = new Button(new Rectangle(0, 0, 200, 50), new float[] { 0, 0.3f, 0.3f }, 2 , "START", Fonts.menuButtonFont);


        public static void Update()
        {
            startButton.UpdatePosition(new Vector2((Globals.GetScreenSize().X - startButton.GetRectangle().Width) / 2, (Globals.GetScreenSize().Y - startButton.GetRectangle().Height) / 2));
            if (startButton.IsUnderCursor() && KeyboardManager.LeftClicked())
            {
                Sounds.buttonClickInstance.Play();
            }
        }

        public static void Draw()
        {
            Game.mainSpriteBatch.Draw(Globals.menuBackground, new Rectangle(0, 0, (int)Globals.GetScreenSize().X, (int)Globals.GetScreenSize().Y), Color.White);
            Game.mainSpriteBatch.DrawString(Fonts.menuButtonFont, "CAVEWORKS", new Vector2(Globals.GetScreenSize().X / 2 - 255, Globals.GetScreenSize().Y / 2 - 200), Color.SteelBlue);
            startButton.Draw();
        }
    }
}
