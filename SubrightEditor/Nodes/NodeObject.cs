using Raylib_cs;
using SubrightEngine2.EngineStuff.BaseComponents;

namespace SubrightEditor.Nodes
{
    public class NodeObject : Component
    {
        public NodeObject(string name) : base("Blank Node")
        {
            if (connectedObject.size.X < 80 || connectedObject.size.Y < 40)
            {
                connectedObject.size = new SubrightEngine2.EngineStuff.Vector3(80, 40, 0);
            }
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //custom draw object
            Raylib.DrawRectangle((int)connectedObject.position.X, (int)connectedObject.position.Y, (int)connectedObject.size.X, (int)connectedObject.size.Y, Color.GRAY);
            Raylib.DrawText(name, (int)connectedObject.position.X, (int)connectedObject.position.Y, 8, SubrightEngine2.Program.textColor.ToRaylibColor);
        }
    }
}
