using Raylib_cs;
using SubrightEditor;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using Color = Raylib_cs.Color;

namespace SubrightEngineEditor.Windows
{
    public class GamePropertiesWindow : Window
    {
        private UIElement element;
        public GameProperties properties;

        public GamePropertiesWindow(Vector3 position, Vector3 size) : base(position, size, "Game Properties")
        {
            //element = UIElement.CreateElement(new EngineButton("Engine button"), this);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            Raylib.DrawText("" + properties.version, (int)position.X, (int)position.Y + 10, 10, Color.White);
        }
    }
}