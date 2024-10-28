﻿using System;
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
        }

        public static void Draw()
        {
                startButton.Draw();
        }
    }
}
