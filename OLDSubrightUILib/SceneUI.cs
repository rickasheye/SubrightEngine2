using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using SubrightEngine2.EngineStuff.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RPGConsole.Graphical
{
    public class SceneUI : Scene
    {
        public string name;
        public int id;
        public List<GUIOption> guiOptions = new List<GUIOption>();

        bool loadedScene = false;

        public SceneUI(string name) : base(name) { }

        public virtual void LoadScene()
        {
            foreach(GUIOption option in guiOptions)
            {
                option.Start();
            }
        }

        bool keyboardOnly = true;

        public override void UpdateScene(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.UpdateScene(ref cam2, ref cam3);
            if(loadedScene == false)
            {
                LoadScene();
                loadedScene = true;
            }

            List<GUIOption> newOptions = new List<GUIOption>();
            newOptions.AddRange(guiOptions);
            GUIOption renderContainer = null;
            if(renderContainer != null)
            {
                renderContainer.DrawObject();
            }

            int i = 0;
            foreach (GUIOption option in newOptions)
            {
                if(option is EmptyContainer && i <= 0)
                {
                    newOptions.Insert(0, option);
                }
                option.DrawObject();
                if (!keyboardOnly)
                {
                    if (Raylib.GetMouseX() > option.position.X && Raylib.GetMouseY() > option.position.Y && Raylib.GetMouseX() < option.position.X + option.size.Y && Raylib.GetMouseY() < option.position.Y + option.size.Y)
                    {
                        option.focused = true;
                    }
                    else
                    {
                        option.focused = false;
                    } 
                }
                i++;
            }
            guiOptions.Clear();
            guiOptions.AddRange(newOptions);
            newOptions.Clear();
            newOptions = null;
        }
    }
}
