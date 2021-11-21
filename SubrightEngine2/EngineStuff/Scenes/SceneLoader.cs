using SubrightEngine2.EngineStuff;
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

        public void LoadScene(string name)
        {
            bool defined = false;
            for(int i = 0; i < currentScenes.Count; i++)
            {
                if(currentScenes[i].name == name)
                {
                    LoadScene(currentScenes[i]);
                    Debug.Log("Loading scene " + name);
                    defined = true;
                }
            }

            if(defined == false)
            {
                //if defined please explain
                Debug.Log("Unfortunately your scene you described doesnt exist!");
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
            foreach (Scene currentScene in this.currentScenes)
            {
                if (currentScene.name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
