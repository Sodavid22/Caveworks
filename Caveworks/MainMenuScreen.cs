using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Caveworks
{
    public static class MainMenuScreen
    {
        public static Button startButton = new Button(new Rectangle(0, 0, 200, 50), new float[] {0,1,0}, "Start", 2);


        public static void Update()
        {
            startButton.UpdatePosition(new Vector2((Globals.GetScreenSize().X - startButton.buttonRectangle.Width) / 2 , (Globals.GetScreenSize().Y - startButton.buttonRectangle.Height) / 2));

            // USELESS TEST CODE
            if (startButton.IsUnderCursor() && KeyboardManager.IsHeldMouse(1))
            {
                startButton.borderSize += 1;
            }
            if (startButton.IsUnderCursor() && KeyboardManager.IsHeldMouse(2))
            {
                startButton.borderSize -= 1;
            }
        }

        public static void Draw()
        {
            startButton.Draw();
            Game.spriteBatch.DrawString(Globals.arial, "CAVEWORKS", new Vector2(Globals.GetScreenSize().X / 2 - 255, Globals.GetScreenSize().Y / 2 - 200), Color.SteelBlue);
        }
    }
}
