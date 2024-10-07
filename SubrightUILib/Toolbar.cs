using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class Toolbar : GameObject
    {
        //stores all the tools at the top of the engine....
        public Toolbar() : base(new Vector3(0, 0, 0), new Vector3(Raylib.GetScreenWidth(), 12, 0), "Toolbar") { }

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