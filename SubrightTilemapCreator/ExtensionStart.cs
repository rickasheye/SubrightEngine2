using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightTilemapCreator
{
    class ExtensionStart : Extension
    {
        Sprite spriteSelected;
        TileSelector selector;

        public override void Start()
        {
            base.Start();
            selector = new TileSelector();
            selector.Start();
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            if (selector != null) { selector.Update(ref cam2, ref cam3); }
        }
    }
}
