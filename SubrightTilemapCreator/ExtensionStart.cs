using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace SubrightTilemapCreator
{
    internal class ExtensionStart : Extension
    {
        private Sprite spriteSelected;
        private TileSelector selector;

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