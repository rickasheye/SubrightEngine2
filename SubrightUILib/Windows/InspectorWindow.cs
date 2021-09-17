using System;
using Raylib_cs;
using SCPBreakdown.EngineStuff;
using Color = SCPBreakdown.EngineStuff.Color;

namespace SubrightEngine2.UI.Windows
{
    [Serializable]
    public class InspectorWindow : Window
    {
        public InspectorWindow(Vector3 position, Vector3 size) : base(position, size, "Inspector")
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
            DisplayContext();
        }

        public void DisplayContext()
        {
            if (hideRender == false)
            {
                if (SCPBreakdown.Program.selectedObject == null)
                {
                    DrawText("No object has been selected!", (int) position.X, (int) position.Y + 9, 8, Color.WHITE);
                }
                else
                {
                    //display everything about the object and offer changes.
                    DrawText("Name: " + SCPBreakdown.Program.selectedObject.name, (int) position.X, (int) position.Y + 10, 10,
                        Color.WHITE);
                    DrawText("Position: " + SCPBreakdown.Program.selectedObject.position.ToString, (int) position.X,
                        (int) position.Y + 22, 10, Color.WHITE);
                    DrawText("Size: " + SCPBreakdown.Program.selectedObject.size.ToString, (int) position.X, (int) position.Y + 32,
                        10, Color.WHITE);
                    DrawText("Components: " + SCPBreakdown.Program.selectedObject.components.Count, (int) position.X,
                        (int) position.Y + 42, 10, Color.WHITE);
                }
            }
        }
    }
}