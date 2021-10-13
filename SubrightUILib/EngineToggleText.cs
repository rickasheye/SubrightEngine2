using Raylib_cs;

namespace SubrightEngine2.UI
{
    public class EngineToggleText : EngineToggle
    {
        public EngineToggleText(string name) : base(name)
        {
            
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            DrawText(name, connectedObject.position.X + connectedObject.size.X + 5, connectedObject.position.Y, 10, SubrightEngine2.EngineStuff.Color.WHITE);
        }
    }
}