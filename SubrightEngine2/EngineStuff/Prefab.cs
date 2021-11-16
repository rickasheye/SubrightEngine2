using SubrightEngine2.EngineStuff.BaseComponents;
using System;
using System.Collections.Generic;

namespace SubrightEngine2.EngineStuff
{
    [Serializable]
    public class Prefab
    {
        public List<GameObject> gameObjects = new List<GameObject>();

        //this is a prefab it stores certain information about other objects.
        public string name = "Untitled";

        public Prefab(List<GameObject> objectsAdd)
        {
            gameObjects = objectsAdd;
            name = gameObjects[0].name;
        }

        public Prefab(GameObject objectAdd)
        {
            gameObjects.Add(objectAdd);
            name = objectAdd.name;
        }

        public Prefab(Component com, Vector3 position, Vector3 size, string name)
        {
            var m = new GameObject(position, size, name);
            com.connectedObject = m;
            m.AddComponent(com);
            this.name = name;
            gameObjects.Add(m);
        }

        public Prefab(Component com)
        {
            var m = new GameObject(Vector3.Zero, Vector3.One, com.name);
            com.connectedObject = m;
            m.AddComponent(com);
            name = com.name;
            gameObjects.Add(m);
        }

        public Prefab()
        {
            //new instance
        }

        public void loadPrefab(ref List<GameObject> objects)
        {
            objects.AddRange(gameObjects);
        }
    }
}