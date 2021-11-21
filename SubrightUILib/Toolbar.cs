using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace SubrightEngine2.UI
{
    public class Toolbar : GameObject
    {
        //stores all the tools at the top of the engine....
        public Toolbar() : base(new Vector3(0, 0, 0), new Vector3(Raylib.GetScreenWidth(), 12, 0), "Toolbar") { }

        /// <summary>
        /// Update method extended to Draw 2D Graphics.
        /// </summary>
        /// <param name="cam2"></param>
        /// <param name="cam3"></param>
        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        /// <summary>
        /// Draw the toolbar to have all activies placed on such.
        /// </summary>
        /// <param name="cam"></param>
        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            DrawRectangle(Vector2.zero, new Vector2(Raylib.GetScreenWidth(), 12), Program.backgroundColor);
            DrawText("Toolbar", new Vector2(2, 2), 8, Program.textColor);
        }
    }
}