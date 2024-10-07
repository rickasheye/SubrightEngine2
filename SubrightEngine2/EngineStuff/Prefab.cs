using Newtonsoft.Json;
using SubrightEngine2.EngineStuff.BaseComponents;
using System;
using System.Collections.Generic;
using System.IO;

namespace SubrightEngine2.EngineStuff
{
    [Serializable]
    public class Prefab
    {
        public List<GameObject> childGameObjects = new List<GameObject>();

        //this is a prefab it stores certain information about other objects.
        public string name = "Untitled";

        public Prefab(List<GameObject> objectsAdd)
        {
            childGameObjects = objectsAdd;
            name = childGameObjects[0].name;
        }

        public Prefab(GameObject objectAdd)
        {
            childGameObjects.Add(objectAdd);
            name = objectAdd.name;
        }

        public Prefab(Component com, Vector3 position, Vector3 size, string name)
        {
            var m = new GameObject(position, size, name);
            com.connectedObject = m;
            m.AddComponent(com);
            this.name = name;
            childGameObjects.Add(m);
        }

        public Prefab(Component com)
        {
            var m = new GameObject(Vector3.Zero, Vector3.One, com.name);
            com.connectedObject = m;
            m.AddComponent(com);
            name = com.name;
            childGameObjects.Add(m);
        }

        public Prefab()
        {
            //new instance
        }

        public static Prefab LoadPrefab(string prefabLocation)
        {
            //load from file.
            if (File.Exists(prefabLocation))
            {
                Prefab fab = JsonConvert.DeserializeObject<Prefab>(File.ReadAllText(prefabLocation));
                return fab;
            }
            else
            {
                Debug.LogError("Unable to find prefab");
                return null;
            }
        }

        public void LoadPrefab()
        {
            //Prefab loadedPrefab = LoadPrefab(prefabLocation);
        }

        public static void SavePrefab(string prefabLocation, Prefab fab)
        {
            if (!File.Exists(prefabLocation))
            {
                File.Create(prefabLocation);
                Debug.LogWarning("Prefab file isnt created. creating");
                SavePrefab(prefabLocation, fab);
            }
            else
            {
                Debug.Log("Saving prefab to file");
                string savedFile = JsonConvert.SerializeObject(fab);
                File.WriteAllText(savedFile, prefabLocation);
                Debug.Log("Saved prefab " + savedFile);
            }
        }

        public void SavePrefab(string prefabLocation)
        {
            SavePrefab(prefabLocation, this);
        }
    }
}