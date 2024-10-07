using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using System;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngineEditor.Windows
{
    [Serializable]
    public class ComponentView : Window
    {
        public ComponentView(Vector3 position, Vector3 size) : base(position, size, "Component View")
        {
            //this is the component view.
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (hideRender == false)
                if (SubrightEngine2.Program.selectedObject != null)
                {
                    if (SubrightEngine2.Program.selectedObject.components.Count <= 0)
                        DrawText("This gameObject doesnt contain any components", position.X, position.Y + 10, 10,
                            Color.White);
                    else
                        for (var i = 0; i < SubrightEngine2.Program.selectedObject.components.Count; i++)
                            DrawText(SubrightEngine2.Program.selectedObject.components[i].name, position.X, position.Y + 10 + i * 10,
                                10, Color.White);
                }
        }
    }
}