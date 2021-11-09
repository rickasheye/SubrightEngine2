using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical
{
    public class Scene
    {
        public string name;
        public int id;
        public List<GUIOption> guiOptions = new List<GUIOption>();

        public AssetLoader loaderAsset;

        bool loadedScene = false;

        public Scene(int id)
        {
            this.id = id;
            if (loaderAsset == null)
            {
                //setup the asset loader!
                loaderAsset = new AssetLoader();
            }
        }

        public Scene(string name)
        {
            this.name = name;
            this.id = Reference.loader.currentScenes.Count + 1;
            if (loaderAsset == null)
            {
                //setup the asset loader!
                loaderAsset = new AssetLoader();
            }
        }

        public virtual void LoadScene()
        {
            foreach(GUIOption option in guiOptions)
            {
                option.Start();
            }
        }

        bool keyboardOnly = true;
        bool debugGUI = false;
        public virtual void UpdateScene(Camera2D cam)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                if(debugGUI == false)
                {
                    Debug.Log("Debug GUI mode set to true!");
                    debugGUI = true;
                }else if (debugGUI)
                {
                    Debug.Log( "Debug GUI mode set to false!");
                    debugGUI = false;
                }
            }

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
                    if (Raylib.GetMouseX() > option.position.X && Raylib.GetMouseY() > option.position.Y && Raylib.GetMouseX() < option.position.X + option.size.X && Raylib.GetMouseY() < option.position.Y + option.size.Y)
                    {
                        option.focused = true;
                    }
                    else
                    {
                        option.focused = false;
                    } 
                }

                if (debugGUI)
                {
                    Raylib.DrawRectangleLines((int)option.position.X, (int)option.position.Y, (int)option.size.X, (int)option.size.Y, Raylib_cs.Color.GRAY);
                    if (option.focused)
                    {
                        Raylib.DrawRectangle((int)option.position.X, (int)option.position.Y, (int)option.size.X, (int)option.size.Y, Raylib_cs.Color.GREEN);
                    }
                    else
                    {
                        Raylib.DrawRectangle((int)option.position.X, (int)option.position.Y, (int)option.size.X, (int)option.size.Y, Raylib_cs.Color.RED);
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
