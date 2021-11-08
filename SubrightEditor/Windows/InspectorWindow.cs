using System;
using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngineEditor.Windows
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
                if (SubrightEngine2.Program.selectedObject == null)
                {
                    DrawText("No object has been selected!", (int) position.X, (int) position.Y + 9, 8, Color.WHITE);
                }
                else
                {
                    //display everything about the object and offer changes.
                    DrawText("Name: " + SubrightEngine2.Program.selectedObject.name, (int) position.X, (int) position.Y + 10, 10,
                        Color.WHITE);
                    DrawText("Position: " + SubrightEngine2.Program.selectedObject.position.ToString, (int) position.X,
                        (int) position.Y + 22, 10, Color.WHITE);
                    DrawText("Size: " + SubrightEngine2.Program.selectedObject.size.ToString, (int) position.X, (int) position.Y + 32,
                        10, Color.WHITE);
                    DrawText("Components: " + SubrightEngine2.Program.selectedObject.components.Count, (int) position.X,
                        (int) position.Y + 42, 10, Color.WHITE);
                }
            }
        }
    }
}