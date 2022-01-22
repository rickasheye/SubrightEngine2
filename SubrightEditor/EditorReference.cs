using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using SubrightEngine2.UI.Windows;
using SubrightEngineEditor.Windows;

namespace SubrightEditor
{
    class EditorReference : Extension
    {
        public InitCycle cycle;
        SubContextMenuManager man;

        public override void Start()
        {
            base.Start();
            cycle = new InitCycle(true);
            cycle.Start();
            man = new SubContextMenuManager();
            man.AddWindow(new HireachyWindow(new Vector3(90, 90, 0), new Vector3(100, 100, 0)));
            man.AddWindow(new ModelsWindow(new Vector3(30, 30, 0), new Vector3(100, 100, 0)));
            man.AddWindow(new AboutWindow(new Vector3(120, 120, 0), new Vector3(100, 100, 0)));
            man.AddWindow(new PrefabViewerWindow(new Vector3(140, 140, 0), new Vector3(100, 100, 0)));
            man.AddWindow(new InspectorWindow(new Vector3(60, 60, 0), new Vector3(100, 100, 0)));
            man.AddWindow(new ComponentView(new Vector3(40, 40, 0), new Vector3(100, 100, 0)));
            man.AddWindow(new GamePropertiesWindow(new Vector3(78, 90, 0), new Vector3(100, 100, 0)));
            man.AddWindow(new NodeAssistantWindow(new Vector3(88, 98, 0), new Vector3(100, 100, 0)));
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            Raylib.ClearBackground(Raylib_cs.Color.BLUE);
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
