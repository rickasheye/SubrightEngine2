using System;
using Raylib_cs;
using SubrightEngine2.EngineStuff;
using Color = SubrightEngine2.EngineStuff.Color;

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
                if (SubrightEngine2.Program.selectedObject != null)
                {
                    if (SubrightEngine2.Program.selectedObject.components.Count <= 0)
                        DrawText("This gameObject doesnt contain any components", position.X, position.Y + 10, 10,
                            Color.WHITE);
                    else
                        for (var i = 0; i < SubrightEngine2.Program.selectedObject.components.Count; i++)
                            DrawText(SubrightEngine2.Program.selectedObject.components[i].name, position.X, position.Y + 10 + i * 10,
                                10, Color.WHITE);
                }
        }
    }
}