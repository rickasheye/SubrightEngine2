using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Color = Raylib_cs.Color;

namespace SubrightUITest
{
    public class Reference : Extension
    {
        static SubrightEngine2.UI.InitCycle cycle;

        public override void Start()
        {
            base.Start();
            cycle = new SubrightEngine2.UI.InitCycle(false);
            cycle.Start();
            SubrightEngine2.Program.loader.AddScene(new SceneUITEST());
            SubrightEngine2.Program.loader.LoadScene("SceneUITest");
        }

        public override void Dispose()
        {
            base.Dispose();
            cycle.Dispose();
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            Raylib.ClearBackground(Color.SKYBLUE);
            base.Update(ref cam2, ref cam3);
            cycle.Update(ref cam2, ref cam3);
        }
    }
}
