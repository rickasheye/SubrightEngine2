using Raylib_cs;

namespace SubrightEngine2.UI
{
    public class EngineToggle : UIElement
    {
        public bool triggered = false;
        public EngineToggle(string name):base(name){}

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            Raylib.DrawCircle((int)connectedObject.position.X, (int)connectedObject.position.Y, connectedObject.size.X, Raylib_cs.Color.WHITE);
            if (triggered)
            {
                Raylib.DrawCircle((int) connectedObject.position.X, (int) connectedObject.position.Y, connectedObject.size.X - 2, Raylib_cs.Color.BLACK);
            }

            if (Raylib.GetMouseX() > connectedObject.position.X - connectedObject.size.X && Raylib.GetMouseX() > connectedObject.position.X + connectedObject.size.X &&
                Raylib.GetMouseY() > connectedObject.position.Y - connectedObject.size.X && Raylib.GetMouseY() < connectedObject.position.Y + connectedObject.size.X)
            {
                if (triggered)
                {
                    triggered = false;
                }else if (!triggered)
                {
                    triggered = true;
                }
            }
        }
    }
}