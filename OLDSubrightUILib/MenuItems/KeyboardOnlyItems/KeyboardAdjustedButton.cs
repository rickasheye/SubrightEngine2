using Raylib_cs;
using SubrightEngine2.EngineStuff;
using Color = SubrightEngine2.EngineStuff.Color;

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
                Raylib.DrawText("> " + name, (int)position.X, (int)position.Y, (int)size.X / (int)size.Y, Color.Black.ToRaylibColor);
            }
            else if (focused == false)
            {
                Raylib.DrawText(name, (int)position.X, (int)position.Y, (int)size.X / (int)size.Y, Color.Black.ToRaylibColor);
            }
        }
    }
}