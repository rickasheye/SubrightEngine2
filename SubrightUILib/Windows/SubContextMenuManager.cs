using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace SubrightEngine2.UI.Windows
{
    [Serializable]
    public class SubContextMenuManager : GameObject
    {
        //More of a window manager now
        public static List<Window> windows = new List<Window>();
        public static Window focusedWindow = null;
        public static bool lockedWindows = false;

        public SubContextMenuManager() : base(new Vector3(0, 0, 0), new Vector3(Raylib.GetScreenWidth(), 10, 0),
            "ContextManager")
        {
            //organise

            //add all of the windows
            AddWindow(new HireachyWindow(new Vector3(90, 90, 0), new Vector3(100, 100, 0)));
            AddWindow(new ModelsWindow(new Vector3(30, 30, 0), new Vector3(100, 100, 0)));
            AddWindow(new AboutWindow(new Vector3(120, 120, 0), new Vector3(100, 100, 0)));
            AddWindow(new PrefabViewerWindow(new Vector3(140, 140, 0), new Vector3(100, 100, 0)));
            AddWindow(new InspectorWindow(new Vector3(60, 60, 0), new Vector3(100, 100, 0)));
            AddWindow(new ComponentView(new Vector3(40, 40, 0), new Vector3(100, 100, 0)));
            AddWindow(new GamePropertiesWindow(new Vector3(78, 90, 0), new Vector3(100, 100, 0)));
            AddWindow(new WindowSelectorWindow());

            var loadText = Path.Combine(Environment.CurrentDirectory, "windowconfig.json");
            if (File.Exists(loadText))
            {
                var output = File.ReadAllText(loadText);
                var positions = JsonConvert.DeserializeObject<Dictionary<string, Vector3>>(output);

                if (positions != null && positions.Count == windows.Count)
                {
                    for (var i = 0; i < positions.Count; i++)
                    {
                        if (positions.Keys.ToList()[i] == windows[i].name)
                        {
                            windows[i].position = positions[windows[i].name];
                        }
                    }
                }
            }
            else
            {
                File.Create(loadText);
            }
            //Program.objects.AddRange(windows);
        }

        public void DisposeWindows()
        {
            //Dispose of the windows
            var windPosi = new Dictionary<string, Vector3>();
            for (var i = 0; i < windows.Count; i++) windPosi.Add(windows[i].name, windows[i].position);
            var directory = Path.Combine(Environment.CurrentDirectory, "windowconfig.json");
            var jsonConv = JsonConvert.SerializeObject(windPosi);
            File.WriteAllText(directory, jsonConv);
            windows.Clear();
        }

        public void AddWindow(Window window)
        {
            if (checkWindowExists(window.name))
            {
                Debug.Log("Check if the window is duplicated twice if not idk");
            }
            else
            {
                //Window isnt spawned
                windows.Add(window);
                Debug.Log("Added window: " + window);
            }
        }

        public bool checkWindowExists(string name)
        {
            for (var i = 0; i < windows.Count; i++)
                if (windows[i].name == name)
                    return true;
            return false;
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            for (var i = 0; i < windows.Count; i++)
            {
                windows[i].Update(ref cam2, ref cam3);
            }
        }
    }
}