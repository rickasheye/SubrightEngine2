using Raylib_cs;
using SCPBreakdown.EngineStuff;

namespace SubrightEngine2.UI.Windows
{
    public class GamePropertiesWindow : Window
    {
        private UIElement element;
        public GamePropertiesWindow(Vector3 position, Vector3 size) : base(position, size, "Game Properties")
        {
            //element = UIElement.CreateElement(new EngineButton("Engine button"), this);
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
            
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            
        }
    }
}