using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public static class Input
    {
        public enum MouseButton
        {
            Left, Right
        }

        private static MouseState currentMouseState;
        private static MouseState oldMouseState;

        private static KeyboardState currentKeyboardState;
        private static KeyboardState oldKeyboardState;

        public static bool IsKeyClicked(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !oldKeyboardState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static bool IsMouseButtonClicked(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed,
                MouseButton.Right => currentMouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton != ButtonState.Pressed,
                _ => false,
            };
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            return button switch
            {
                MouseButton.Left => currentMouseState.LeftButton == ButtonState.Pressed,
                MouseButton.Right => currentMouseState.RightButton == ButtonState.Pressed,
                _ => false,
            };
        }

        public static Vector2 GetMousePosition()
        {
            return new Vector2(currentMouseState.X, currentMouseState.Y);
        }

        public static void PreUpdate()
        {
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
        }

        public static void PostUpdate()
        {

            oldMouseState = currentMouseState;
            oldKeyboardState = currentKeyboardState;
        }

    }
}
