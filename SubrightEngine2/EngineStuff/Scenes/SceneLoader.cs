using Raylib_cs;
using System;
using System.Collections.Generic;

namespace SubrightEngine2.EngineStuff.Scenes
{
    [Serializable]
    public class SceneLoader
    {
        public Scene currentScene;
        public List<Scene> currentScenes = new List<Scene>();

        public void LoadScene(Scene scene)
        {
            if (sceneExists(scene.name))
            {
                currentScene = scene;
                Debug.Log("Loading scene " + scene.name);
            }
            else
            {
                Debug.Log("Incorrect scene!");
            }
        }

        private bool toggleDebuginfo = false;

        public void UpdateScene(ref Camera2D cam2, ref Camera3D cam3)
        {
            int currentCountLoadRender = 0;
            for (int i = 0; i < currentScenes.Count; i++)
            {
                Scene currentScene_ = currentScenes[i];
                if (currentScene_.loadRenderIfNotPriority == true || currentScene.name == currentScene_.name)
                {
                    currentScene_.UpdateScene(ref cam2, ref cam3);
                    currentCountLoadRender++;
                }

                if (currentScene.name != currentScene_.name)
                {
                    currentScene_.notPriority = true;
                }
                else
                {
                    currentScene_.notPriority = false;
                }
            }

            if (Program.debug && toggleDebuginfo == true)
            {
                //render the scene properties
                Raylib.DrawText("Scene: " + currentScene.name + " Scenes in memory: " + currentScenes.Count + " Current scenes rendering: " + currentCountLoadRender, 10, 20, 20, Raylib_cs.Color.Black);
                //render the names for the scenes loaded
                for (int i = 0; i < currentScenes.Count; i++)
                {
                    Raylib.DrawText("Scene: " + currentScenes[i].name, 10, 40 + (i * 20), 20, Raylib_cs.Color.Black);
                }
            }

            if (Program.debug && Raylib.IsKeyPressed(KeyboardKey.F1))
            {
                toggleDebuginfo = !toggleDebuginfo;
            }
            currentCountLoadRender = 0;
        }

        public void LoadScene(string name)
        {
            bool defined = false;
            for (int i = 0; i < currentScenes.Count; i++)
            {
                if (currentScenes[i].name == name)
                {
                    LoadScene(currentScenes[i]);
                    Debug.Log("Loading scene " + name);
                    defined = true;
                }
            }

            if (defined == false)
            {
                //if defined please explain
                Debug.Log("Unfortunately your scene you described doesnt exist!");
            }
        }

        public void LoadScene(int id)
        {
            LoadScene(currentScenes[id]);
        }

        public void AddScene(Scene scene)
        {
            if (sceneExists(scene.name))
            {
                //scene exists so dont add the scene
                Debug.Log("scene exists so cancelling!");
            }
            else
            {
                currentScenes.Add(scene);
            }
        }

        public void RemoveScene(Scene scene)
        {
            if (sceneExists(scene.name))
            {
                currentScenes.Remove(scene);
            }
            else
            {
                Debug.Log("scene doesnt exist so cancelling!");
            }
        }

        public Scene getScene(string name)
        {
            foreach (Scene currentScene in currentScenes)
            {
                if (currentScene.name == name)
                {
                    return currentScene;
                }
            }
            return null;
        }

        public void CleanScene()
        {
            currentScene = null;
        }

        public bool sceneExists(string sceneName)
        {
            if (getScene(sceneName) != null)
            {
                return true;
            }
            return false;
        }
    }
}