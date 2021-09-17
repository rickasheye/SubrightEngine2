using System;
using Raylib_cs;
using SCPBreakdown.EngineStuff;
using Color = SCPBreakdown.EngineStuff.Color;

namespace SubrightEngine2.UI.Windows
{
    [Serializable]
    public class ComponentView : Window
    {
        public ComponentView(Vector3 position, Vector3 size) : base(position, size, "Component View")
        {
            //this is the component view.
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (hideRender == false)
                if (SCPBreakdown.Program.selectedObject != null)
                {
                    if (SCPBreakdown.Program.selectedObject.components.Count <= 0)
                        DrawText("This gameObject doesnt contain any components", position.X, position.Y + 10, 10,
                            Color.WHITE);
                    else
                        for (var i = 0; i < SCPBreakdown.Program.selectedObject.components.Count; i++)
                            DrawText(SCPBreakdown.Program.selectedObject.components[i].name, position.X, position.Y + 10 + i * 10,
                                10, Color.WHITE);
                }
        }
    }
}