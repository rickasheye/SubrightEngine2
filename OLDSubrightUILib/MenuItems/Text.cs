using Raylib_cs;
using SubrightEngine2.EngineStuff;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole.Graphical.MenuItems
{
    public class Text : GUIOption
    {
        public string text;
        public int textSize;
        public Color textColor;

        public Text(string text, Vector2 position, int textSize, Color textColor) : base(new Vector2(0, 0), position)
        {
            this.text = text;
            this.textSize = textSize;
            this.textColor = textColor;
        }

        public override void DrawObject()
        {
            base.DrawObject();
            Raylib.DrawText(text, (int)position.X, (int)position.Y, textSize, textColor.ToRaylibColor);
        }
    }
}