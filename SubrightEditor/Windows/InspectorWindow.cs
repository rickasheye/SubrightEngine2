using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using System;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngineEditor.Windows
{
    [Serializable]
    public class InspectorWindow : Window
    {
        public InspectorWindow(Vector3 position, Vector3 size) : base(position, size, "Inspector")
        {
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
                    DrawText("No object has been selected!", (int)position.X, (int)position.Y + 9, 8, Color.White);
                }
                else
                {
                    //display everything about the object and offer changes.
                    DrawText("Name: " + SubrightEngine2.Program.selectedObject.name, (int)position.X, (int)position.Y + 10, 10,
                        Color.White);
                    DrawText("Position: " + SubrightEngine2.Program.selectedObject.position.ToString, (int)position.X,
                        (int)position.Y + 22, 10, Color.White);
                    DrawText("Size: " + SubrightEngine2.Program.selectedObject.size.ToString, (int)position.X, (int)position.Y + 32,
                        10, Color.White);
                    DrawText("Components: " + SubrightEngine2.Program.selectedObject.components.Count, (int)position.X,
                        (int)position.Y + 42, 10, Color.White);
                }
            }
        }
    }
}