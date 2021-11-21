using Raylib_cs;
using SubrightEngine2.EngineStuff;
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

        bool loadedScene = false;
        public List<GameObject> GameObjects = new List<GameObject>();

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
            foreach(GameObject m in GameObjects)
            {
                m.Start();
            }
            loadedScene = true;
        }

        public virtual void UpdateScene(ref Camera2D cam2, ref Camera3D cam3)
        {
            if (loadedScene == false)
            {
                LoadScene();
            }

            foreach(GameObject m in GameObjects)
            {
                m.Update(ref cam2, ref cam3);
            }
        }
    }
}
