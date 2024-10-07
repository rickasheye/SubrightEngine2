using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace SubrightEngine2.UI.Tips
{
    public class TipRender
    {
        public static void RenderTip(string tip, Vector2 position)
        {
            Raylib.DrawRectangle(Raylib.GetMouseX(), Raylib.GetMouseY() - 12, tip.Length * 6, 10, Raylib_cs.Color.Gray);
            Raylib.DrawText(tip, Raylib.GetMouseX() + 2, Raylib.GetMouseY() - 12, 10, Raylib_cs.Color.White);
        }
    }
}