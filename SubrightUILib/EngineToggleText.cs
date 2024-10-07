using Raylib_cs;

namespace SubrightEngine2.UI
{
    [System.Serializable]
    public class EngineToggleText : EngineToggle
    {
        //Toggle button with Text.

        public EngineToggleText(string name) : base(name)
        {
        }

        public override void OffhandDraw(ref Camera2D cam)
        {
            base.OffhandDraw(ref cam);
            DrawText(name, connectedObject.position.X + connectedObject.size.X + 5, connectedObject.position.Y, 10, Program.textColor);
        }
    }
}