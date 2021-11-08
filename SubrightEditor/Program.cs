using SubrightEditor.Windows;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI.Windows;
using SubrightEngineEditor.Windows;
using System;

namespace SubrightEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            SubContextMenuManager.AddWindow(new HireachyWindow(new Vector3(90, 90, 0), new Vector3(100, 100, 0)));
            SubContextMenuManager.AddWindow(new ModelsWindow(new Vector3(30, 30, 0), new Vector3(100, 100, 0)));
            SubContextMenuManager.AddWindow(new AboutWindow(new Vector3(120, 120, 0), new Vector3(100, 100, 0)));
            SubContextMenuManager.AddWindow(new PrefabViewerWindow(new Vector3(140, 140, 0), new Vector3(100, 100, 0)));
            SubContextMenuManager.AddWindow(new InspectorWindow(new Vector3(60, 60, 0), new Vector3(100, 100, 0)));
            SubContextMenuManager.AddWindow(new ComponentView(new Vector3(40, 40, 0), new Vector3(100, 100, 0)));
            SubContextMenuManager.AddWindow(new GamePropertiesWindow(new Vector3(78, 90, 0), new Vector3(100, 100, 0)));
            SubContextMenuManager.AddWindow(new NodeAssistantWindow(new Vector3(88, 98, 0), new Vector3(100, 100, 0)));
            SubrightEngine2.Program.SetExtension(new EditorReference(), false);
            SubrightEngine2.Program.Initialise(args, false, false);
        }
    }
}
