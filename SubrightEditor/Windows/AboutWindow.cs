using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using System;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngineEditor.Windows
{
    [Serializable]
    public class AboutWindow : Window
    {
        public AboutWindow(Vector3 position, Vector3 size) : base(position, size, "About Engine", Context.SETTINGS,
            false)
        {
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (hideRender == false)
            {
                DrawRectangle((int)(position.X + size.X) / 4, (int)position.Y + 12,
                    (int)(size.X - (position.X + size.X / 4)), 40, Color.White);
                DrawText("Subright Engine 2", position.X, position.Y + 55, 8, Color.White);
            }
        }
    }
}