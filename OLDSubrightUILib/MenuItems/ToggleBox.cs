using Raylib_cs;
using SubrightEngine2.EngineStuff;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole.Graphical.MenuItems
{
    public class ToggleBox : GUIOption
    {
        public bool toggled = false;
        public string text;

        public ToggleBox(string text, Vector2 position, Vector2 size) : base(size, position)
        {
            this.text = text;
        }

        public override void DrawObject()
        {
            base.DrawObject();
            Raylib.DrawRectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, Raylib_cs.Color.White);
            Raylib.DrawRectangleLines((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, Raylib_cs.Color.Black);
            if (toggled == false) { Raylib.DrawRectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4, Color.Red.ToRaylibColor); }
            else
            {
                Raylib.DrawRectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4, Color.GREEN.ToRaylibColor);
            }
            if (focused)
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    if (toggled == true)
                    {
                        toggled = false;
                    }
                    else if (toggled == false)
                    {
                        toggled = true;
                    }
                }
            }
            Raylib.DrawText(text, (int)position.X + (int)size.X + 5, (int)position.Y, (int)position.X - (int)position.Y, Raylib_cs.Color.Black);
        }
    }
}