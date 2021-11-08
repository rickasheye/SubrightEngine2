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

        public static void AddWindow(Window window)
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

        public static bool checkWindowExists(string name)
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