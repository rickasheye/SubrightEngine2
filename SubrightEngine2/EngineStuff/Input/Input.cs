using Raylib_cs;
using System;

namespace SubrightEngine2.EngineStuff.Input
{
    public static class Input
    {
        public static bool GetButtonDown(KeyboardKey key, bool pause)
        {
            if (Raylib.IsKeyDown(key) && pause == false)
            {
                return true;
            }

            return false;
        }

        public static bool GetButtonDown(KeyboardKey key)
        {
            return GetButtonDown(key, false);
        }

        public static bool GetButtonDown(string key, bool pause)
        {
            return GetButtonDown((KeyboardKey)Enum.Parse(typeof(KeyboardKey), key), pause);
        }

        public static bool GetButtonDown(string key)
        {
            return GetButtonDown(key, false);
        }

        public static bool GetButtonPressed(KeyboardKey key, bool pause)
        {
            if (Raylib.IsKeyPressed(key) && pause == false)
            {
                return true;
            }

            return false;
        }

        public static bool GetButtonPressed(KeyboardKey key)
        {
            return GetButtonPressed(key, false);
        }

        public static bool GetButtonPressed(string key, bool pause)
        {
            return GetButtonPressed((KeyboardKey)Enum.Parse(typeof(KeyboardKey), key), pause);
        }

        public static bool GetButtonPressed(string key)
        {
            return GetButtonPressed(key, false);
        }

        public static bool GetMouseButtonPressed(MouseButton button, bool pause)
        {
            if (Raylib.IsMouseButtonPressed(button) && pause == false)
            {
                return true;
            }

            return false;
        }

        public static bool GetMouseButtonPressed(MouseButton button)
        {
            return GetMouseButtonPressed(button, false);
        }

        public static bool GetMouseButtonPressed(string buttonname, bool pause)
        {
            return GetMouseButtonPressed((MouseButton)Enum.Parse(typeof(MouseButton), buttonname), pause);
        }

        public static bool GetMouseButtonPressed(string buttonname)
        {
            return GetMouseButtonPressed(buttonname, false);
        }

        public static bool GetMouseButtonDown(MouseButton button, bool pause)
        {
            if (Raylib.IsMouseButtonDown(button) && pause == false)
            {
                return true;
            }

            return false;
        }

        public static bool GetMouseButtonDown(MouseButton button)
        {
            return GetMouseButtonDown(button, false);
        }

        public static bool GetMouseButtonDown(string buttonname, bool pause)
        {
            return GetMouseButtonDown((MouseButton)Enum.Parse(typeof(MouseButton), buttonname), pause);
        }

        public static bool GetMouseButtonDown(string buttonname)
        {
            return GetMouseButtonDown(buttonname, false);
        }

        public static bool GetMouseButtonUp(MouseButton button, bool pause)
        {
            if (Raylib.IsMouseButtonUp(button) && pause == false)
            {
                return true;
            }

            return false;
        }

        public static bool GetMouseButtonUp(MouseButton button)
        {
            return GetMouseButtonUp(button, false);
        }

        public static bool GetMouseButtonUp(string buttonname, bool pause)
        {
            return GetMouseButtonUp((MouseButton)Enum.Parse(typeof(MouseButton), buttonname), pause);
        }

        public static bool GetMouseButtonUp(string buttonname)
        {
            return GetMouseButtonUp(buttonname, false);
        }

        public static float GetMouseWheel(bool pause)
        {
            if (pause == false)
            {
                return Raylib.GetMouseWheelMove();
            }

            return 0;
        }

        public static float GetMouseWheel()
        {
            return GetMouseWheel(false);
        }
    }
}