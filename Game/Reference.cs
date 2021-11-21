using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;

namespace Game
{
    public class Reference : Extension
    {
        private InitCycle cycle;
        public override void Start()
        {
            base.Start();
            SubrightEngine2.Program.debug = true;
            cycle = new InitCycle(false);
            //SubrightEngine2.Program.SetWindowTitle("Subright Engine Editor");
            cycle.Start();
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            cycle.Update(ref cam2, ref cam3);
            Raylib.ClearBackground(Raylib_cs.Color.SKYBLUE);
        }

        public override void Dispose()
        {
            base.Dispose();
            cycle.Dispose();
        }
    }
}