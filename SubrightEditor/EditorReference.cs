using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightEditor
{
    class EditorReference : Extension
    {
        public InitCycle cycle;

        public override void Start()
        {
            base.Start();
            cycle = new InitCycle();
            cycle.Start();
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            cycle.Update(ref cam2, ref cam3);
        }

        public override void Dispose()
        {
            base.Dispose();
            cycle.Dispose();
        }
    }
}
