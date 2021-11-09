using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical.MenuItems.KeyboardOnlyItems
{
    public class KeyboardAdjustedButton : Option
    {
        public KeyboardAdjustedButton(string text, Vector2 size, Vector2 position) : base(text, size, position)
        { }

        public override void DrawObject()
        {
            base.DrawObject();
            if (focused == true)
            {
                Raylib.DrawText("> " + name, (int)position.X, (int)position.Y, (int)size.X / (int)size.Y, Raylib_cs.Color.BLACK);
            }
            else if (focused == false)
            {
                Raylib.DrawText(name, (int)position.X, (int)position.Y, (int)size.X / (int)size.Y, Raylib_cs.Color.BLACK);
            }
        }
    }
}
