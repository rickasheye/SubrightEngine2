using Raylib_cs;
using System;
using System.Collections.Generic;

namespace SubrightEngine2.EngineStuff.Scenes
{
    [Serializable]
    public class Scene
    {
        public string name;
        public int id;

        public AssetLoader loaderAsset;

        //only used to say we init the scene.
        private bool loadedScene = false;

        //add methods to add these gameobjects in...
        public List<GameObject> GameObjects = new List<GameObject>();

        public bool loadRenderIfNotPriority = false;
        public bool notPriority = true;

        public Scene()
        {
            this.name = "Undefined";
            this.id = Program.loader.currentScenes.Count + 1;
            if (loaderAsset == null)
            {
                //setup the asset loader!
                loaderAsset = new AssetLoader();
            }
            LoadScene();
        }

        public Scene(string name)
        {
            this.name = name;
            this.id = Program.loader.currentScenes.Count + 1;
            if (loaderAsset == null)
            {
                //setup the asset loader!
                loaderAsset = new AssetLoader();
            }
            LoadScene();
        }

        public virtual void LoadScene()
        {
            Debug.Log("Attempting to load scene : " + name);
            foreach (GameObject m in GameObjects)
            {
                m.Start();
            }
            loadedScene = true;
            Debug.Log("Loaded scene : " + name);
        }

        public virtual void UpdateScene(ref Camera2D cam2, ref Camera3D cam3)
        {
            if (loadedScene == false)
            {
                LoadScene();
            }
            UpdateScene(ref cam2);
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject m = GameObjects[i];
                m.Draw2D(ref cam2);
                m.Draw3D(ref cam3);
            }
        }

        public virtual void UpdateScene(ref Camera2D cam)
        {
            if (loadedScene == false)
            {
                LoadScene();
            }

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject m = GameObjects[i];
                m.Update(ref cam);
            }
        }

        public void AddGameObject(GameObject objectUsed)
        {
            //check or find if this gameobject has been added or not.
            if (GameObjects.Contains(objectUsed))
            {
                Debug.LogWarning("This gameobject has already been added to the scene!");
            }
            else
            {
                GameObjects.Add(objectUsed);
            }
        }

        public void RemoveGameObject(GameObject objectUsed)
        {
            //check or find if this gameobject has been added or not.
            if (GameObjects.Contains(objectUsed))
            {
                GameObjects.Remove(objectUsed);
            }
            else
            {
                Debug.LogWarning("This gameobject has not been added to the scene!");
            }
        }
    }
}