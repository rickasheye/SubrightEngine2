using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical
{
    public class SceneLoader
    {
        public Scene currentScene;
        public List<Scene> currentScenes = new List<Scene>();

        public void LoadScene(Scene scene)
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
            foreach(Scene currentScene in this.currentScenes)
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
