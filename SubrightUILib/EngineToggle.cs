using Raylib_cs;

namespace SubrightEngine2.UI
{
    [System.Serializable]
    public class EngineToggle : UIElement
    {
        //Toggle to use comes out to this boolean variable under.
        public bool triggered = false;

        public EngineToggle(string name) : base(name)
        {
        }

        /// <summary>
        /// Draw the toggle button.
        /// </summary>
        /// <param name="cam"></param>
        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            Raylib.DrawCircle((int)connectedObject.position.X, (int)connectedObject.position.Y, connectedObject.size.X, Program.backgroundColor.ToRaylibColor);
            if (triggered)
            {
                Raylib.DrawCircle((int)connectedObject.position.X, (int)connectedObject.position.Y, connectedObject.size.X - 2, Program.foregroundColor.ToRaylibColor);
            }

            if (Raylib.GetMouseX() > connectedObject.position.X - connectedObject.size.X && Raylib.GetMouseX() > connectedObject.position.X + connectedObject.size.X &&
                Raylib.GetMouseY() > connectedObject.position.Y - connectedObject.size.X && Raylib.GetMouseY() < connectedObject.position.Y + connectedObject.size.X)
            {
                if (triggered)
                {
                    triggered = false;
                }
                else if (!triggered)
                {
                    triggered = true;
                }
            }
            OffhandDraw(ref cam);
        }

        public virtual void OffhandDraw(ref Camera2D cam)
        {
        }
    }
}