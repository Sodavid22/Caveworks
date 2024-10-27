using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public static class KeyboardManager
    {
        public static KeyboardState currentKeyboardState;
        public static KeyboardState lastKeyboardState;


        public static void Update()
        {
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public static bool IsHeld(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool IsPressed(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool IsReleased(Keys key)
        {
            if (!currentKeyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

    }
}
