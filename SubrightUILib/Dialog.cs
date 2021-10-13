using System;
using Raylib_cs;
using SubrightEngine2.EngineStuff;
using Color = SubrightEngine2.EngineStuff.Color;
using Vector2 = SubrightEngine2.EngineStuff.Vector2;
using Vector3 = SubrightEngine2.EngineStuff.Vector3;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class Dialog : GameObject
    {
        public bool hideRender;
        public Dialog(Vector2 position, Vector2 size, string name):base(new Vector3(position.X, position.Y, 0), new Vector3(size.X, size.Y, 0), name){}

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //using to render a ui.
            if (!hideRender)
            {
                DrawRectangle(new Vector2(position.X, position.Y), new Vector2(size.X, size.Y), Color.GRAY);
                DrawRectangleLines(new Vector2(position.X, position.Y), new Vector2(size.X, size.Y), Color.LIGHTGRAY);
            }
        }
    }
}