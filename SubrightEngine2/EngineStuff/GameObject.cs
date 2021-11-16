using Raylib_cs;
using SubrightEngine2.EngineStuff.BaseComponents;
using System;
using System.Collections.Generic;

namespace SubrightEngine2.EngineStuff
{
    [Serializable]
    public class GameObject : Drawable
    {
        public List<GameObject> childrenObjects = new List<GameObject>();

        public List<Component> components = new List<Component>();
        public string name;

        public GameObject parent;

        public Vector3 position;
        public Vector3 size;

        public bool voidStart = false;

        public GameObject(Vector3 position, Vector3 size, string name, bool voidStart)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            this.voidStart = voidStart;
            Start();
        }

        public GameObject(Vector3 position, Vector3 size, string name)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            this.voidStart = false;
            Start();
        }

        public GameObject(Vector3 position, string name)
        {
            this.name = name;
            this.position = position;
            this.size = Vector3.zero;
            this.voidStart = false;
            Start();
        }

        public GameObject(string name)
        {
            this.name = name;
            this.position = Vector3.zero;
            this.size = Vector3.zero;
            this.voidStart = false;
        }

        public GameObject()
        {
            this.name = "";
            this.position = Vector3.zero;
            this.size = Vector3.zero;
            this.voidStart = false;
            Start();
        }

        public virtual void Start()
        {
            //start or connect all of the components
            if (Program.gameStart == true || voidStart == true)
            {
                for (var i = 0; i < components.Count; i++)
                {
                    if (components[i].connectedObject != this) components[i].connectedObject = this;
                    components[i].Start();
                }
            }
        }

        public virtual void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            Raylib.BeginMode3D(cam3);
            Draw3D(ref cam3);
            Raylib.EndMode3D();

            Raylib.BeginMode2D(cam2);
            //execute around here.
            Draw2D(ref cam2);
            Raylib.EndMode2D();

            if (Program.gameStart == true || voidStart == true)
            {
                for (var i = 0; i < components.Count; i++)
                {
                    var com = components[i];
                    if (com.connectedObject != this) com.connectedObject = this;
                    com.Update(ref cam2, ref cam3);
                }

                for (int m = 0; m < childrenObjects.Count; m++)
                {
                    GameObject p = childrenObjects[m];
                    if (p != null)
                    {
                        p.Update(ref cam2, ref cam3);
                        p.position = new Vector3(position.X + p.position.X, position.Y + p.position.Y,
                            position.Z + p.position.Z);
                        p.size = new Vector3(size.X + p.size.X, size.Y + p.size.Y, size.Z + p.size.Z);
                    }
                }
            }
        }

        public static GameObject AssembleObject(Component com)
        {
            GameObject m = new GameObject();
            m.name = com.name;
            m.AddComponent(com);
            return m;
        }

        public virtual void Draw2D(ref Camera2D cam)
        {
            for (var i = 0; i < components.Count; i++)
            {
                var com = components[i];
                if (com.connectedObject != this) com.connectedObject = this;
                com.Draw2D(ref cam);
            }

            for (int m = 0; m < childrenObjects.Count; m++)
            {
                GameObject p = childrenObjects[m];
                if (p != null)
                {
                    p.Draw2D(ref cam);
                }
            }
        }

        public virtual void Draw3D(ref Camera3D cam)
        {
            for (var i = 0; i < components.Count; i++)
            {
                var com = components[i];
                if (com.connectedObject != this) com.connectedObject = this;
                com.Draw3D(ref cam);
            }

            for (int m = 0; m < childrenObjects.Count; m++)
            {
                GameObject p = childrenObjects[m];
                if (p != null)
                {
                    p.Draw3D(ref cam);
                }
            }
        }

        //used to retrieve all of the components
        public static GameObject getOwner(Component com)
        {
            for (var i = 0; i < Program.loader.currentScene.GameObjects.Count; i++)
            {
                var chosenObject = Program.loader.currentScene.GameObjects[i];
                var assembledObjects = new List<GameObject>();
                assembledObjects.Add(chosenObject);
                assembledObjects.AddRange(chosenObject.childrenObjects);
                for (var m = 0; m < assembledObjects.Count; m++)
                {
                    var asmObject = assembledObjects[m];
                    for (var p = 0; p < asmObject.components.Count; p++)
                    {
                        var comp = asmObject.components[p];
                        if (comp == com) return asmObject;
                    }
                }
            }

            return null;
        }

        public Component AddComponent(Component com)
        {
            if (!comExists(com.name))
            {
                if (com.connectedObject == null) com.connectedObject = this;
                com.Start();
                components.Add(com);
                return GetComponent(com);
            }

            Debug.Log("Component does already exist!");
            return null;
        }

        public bool comExists(string name)
        {
            for (var i = 0; i < components.Count; i++)
                if (components[i].name == name)
                    return true;
            return false;
        }

        public Component GetComponent(Component com)
        {
            for (var i = 0; i < components.Count; i++)
                if (components[i].name == com.name)
                    return com;
            return null;
        }

        public Component GetComponent(string name)
        {
            for (var i = 0; i < components.Count; i++)
                if (components[i].name == name)
                    return components[i];
            return null;
        }

        public void RemoveComponent(Component com)
        {
            for (var i = 0; i < components.Count; i++)
                if (com.name == components[i].name)
                {
                    components.RemoveAt(i);
                    break;
                }
        }

        public void AddChild(GameObject m)
        {
            if (!childObjectExists(m))
            {
                //child object exists
                m.parent = this;
                childrenObjects.Add(m);
                for (var i = 0; i < Program.loader.currentScene.GameObjects.Count; i++)
                    if (Program.loader.currentScene.GameObjects[i].name == m.name)
                        Program.loader.currentScene.GameObjects.RemoveAt(i);
            }
            else
            {
                Debug.LogWarning("Child object already exists under that name in the parent!");
            }
        }

        public void RemoveChild(GameObject m)
        {
            if (childObjectExists(m))
            {
                for (var i = 0; i < childrenObjects.Count; i++)
                    if (m.name == childrenObjects[i].name)
                    {
                        childrenObjects.RemoveAt(i);
                        Debug.Log("Removed object at: " + i + " on " + name);
                    }
            }
            else
            {
                Debug.LogWarning("Object doesnt exist to be removed!");
            }
        }

        private bool childObjectExists(GameObject m)
        {
            for (var i = 0; i < childrenObjects.Count; i++)
                if (childrenObjects[i].name == m.name)
                    return true;
            return false;
        }
    }
}