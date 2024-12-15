using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Caveworks
{
    public static class MyKeyboard
    {
        private static KeyboardState currentKeyboardState;
        private static KeyboardState lastKeyboardState;
        private static MouseState currentMouseState;
        private static MouseState lastMouseState;

        public static void Update()
        {
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        public static bool IsPressed(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool IsPressed(MouseKey key)
        {
            if (key == MouseKey.Left)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    return true;
                }
            }
            else if (key == MouseKey.Right)
            {
                if (currentMouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsHeld(Keys key)
        {
            if (currentKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        public static bool IsHeld(MouseKey key)
        {
            if (key == MouseKey.Left)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            else if (key == MouseKey.Right)
            {
                if (currentMouseState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
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

        public static bool IsReleased(MouseKey key)
        {
            if (key == MouseKey.Left)
            {
                if (currentMouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            else if (key == MouseKey.Right)
            {
                if (currentMouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            return false;
        }

        public static Vector2 GetMousePosition()
        {
            MouseState mouse = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouse.X, mouse.Y);
            return mousePosition;
        }
    }
}
