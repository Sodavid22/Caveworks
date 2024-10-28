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
        public static MouseState currentMouseState;
        public static MouseState lastMouseState;
        public static KeyboardState[] lastKeyboardStates;


        public static void Update()
        {
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public static bool IsPressedKey(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool IsHeldKey(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool IsReleasedKey(Keys key)
        {
            if (!currentKeyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static Vector2 GetMousePosition()
        {
            MouseState mouse = Mouse.GetState();
            Vector2 mousPosition = new Vector2(mouse.X, mouse.Y);
            return mousPosition;
        }

        public static bool IsPressedMouse(int button)
        {
            if (button == 1 && currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            if (button == 2 && currentMouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
            {
                return true;
            }
            if (button == 3 && currentMouseState.MiddleButton == ButtonState.Pressed && lastMouseState.MiddleButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public static bool IsHeldMouse(int button)
        {
            if (button == 1 && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            if (button == 2 && currentMouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            if (button == 3 && currentMouseState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }

        public static bool IsReleasedMouse(int button)
        {
            if (button == 1 && currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            if (button == 2 && currentMouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            if (button == 3 && currentMouseState.MiddleButton == ButtonState.Released && lastMouseState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
        }
    }
}
