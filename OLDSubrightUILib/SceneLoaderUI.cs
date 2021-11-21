using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical
{
    public class SceneLoaderUI
    {
        public SceneUI currentScene;
        public List<SceneUI> currentScenes = new List<SceneUI>();

        public void LoadScene(SceneUI scene)
        {
            if (sceneExists(scene.name))
            {
                currentScene = scene;
            }
            else
            {
                Debug.Log("Incorrect scene!");
            }
        }

        public void AddScene(SceneUI scene)
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

        public void RemoveScene(SceneUI scene)
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

        public SceneUI getScene(string name)
        {
            foreach (SceneUI currentScene in currentScenes)
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
            foreach(SceneUI currentScene in this.currentScenes)
            {
                if(currentScene.name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
