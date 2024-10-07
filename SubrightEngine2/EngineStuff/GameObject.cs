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

        public Vector3 position = Vector3.Zero;
        public Vector3 size = Vector3.Zero;
        public bool hideableFromHireachy = false;

        public bool voidStart = false;
        public bool enabled = true;

        public GameObject(Vector3 position, Vector3 size, string name, bool voidStart)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            this.voidStart = voidStart;
            Program.loader.currentScene.GameObjects.Add(this);
            Start();
        }

        public GameObject(Vector3 position, Vector3 size, string name)
        {
            this.name = name;
            this.position = position;
            this.size = size;
            this.voidStart = false;
            Program.loader.currentScene.GameObjects.Add(this);
            Start();
        }

        public GameObject(Vector3 position, string name)
        {
            this.name = name;
            this.position = position;
            this.size = Vector3.Zero;
            this.voidStart = false;
            Program.loader.currentScene.GameObjects.Add(this);
            Start();
        }

        public GameObject(string name)
        {
            this.name = name;
            this.position = Vector3.Zero;
            this.size = Vector3.Zero;
            this.voidStart = false;
            Program.loader.currentScene.GameObjects.Add(this);
            Start();
        }

        public GameObject()
        {
            this.name = "";
            this.position = Vector3.Zero;
            this.size = Vector3.Zero;
            this.voidStart = false;
            Program.loader.currentScene.GameObjects.Add(this);
            Start();
        }

        public void Enable()
        {
            enabled = true;
            //enable all of the components
            for (var i = 0; i < components.Count; i++)
            {
                if (components[i].connectedObject != this) components[i].connectedObject = this;
                components[i].enabled = true;
            }
        }

        public void Disable()
        {
            enabled = false;
            //disable all of the components
            for (var i = 0; i < components.Count; i++)
            {
                if (components[i].connectedObject != this) components[i].connectedObject = this;
                components[i].enabled = false;
            }
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

        public virtual void Update(ref Camera2D cam)
        {
            //checking if the position of the object includes being outside of the rendering camera view and position
            if (position.X > cam.Target.X + cam.Offset.X || position.X < cam.Target.X - cam.Offset.X || position.Y > cam.Target.Y + cam.Offset.Y || position.Y < cam.Target.Y - cam.Offset.Y)
            {
                Disable();
            }
            else
            {
                Enable();
            }

            if (enabled == true)
            {
                Raylib.BeginMode2D(cam);
                if (Program.gameStart == true || voidStart == true)
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
                            p.Update(ref cam);
                            p.position = new Vector3(position.X + p.position.X, position.Y + p.position.Y,
                                position.Z + p.position.Z);
                            p.size = new Vector3(size.X + p.size.X, size.Y + p.size.Y, size.Z + p.size.Z);
                        }
                    }
                }
                Draw2D(ref cam);
                Raylib.EndMode2D();
            }
            else
            {
                //function is disabled.
            }
        }

        public virtual void Update(ref Camera3D cam)
        {
            //checking if the position of the object includes being outside of the rendering camera view and position
            if (position.X > cam.Position.X + cam.Target.X || position.X < cam.Position.X - cam.Target.X || position.Y > cam.Position.Y + cam.Target.Y || position.Y < cam.Position.Y - cam.Target.Y || position.Z > cam.Position.Z + cam.Target.Z || position.Z < cam.Position.Z - cam.Target.Z)
            {
                Disable();
            }
            else
            {
                Enable();
            }

            if (enabled == true)
            {
                Raylib.BeginMode3D(cam);
                if (Program.gameStart == true || voidStart == true)
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
                            p.Update(ref cam);
                            p.position = new Vector3(position.X + p.position.X, position.Y + p.position.Y,
                                position.Z + p.position.Z);
                            p.size = new Vector3(size.X + p.size.X, size.Y + p.size.Y, size.Z + p.size.Z);
                        }
                    }
                }
                Draw3D(ref cam);
                Raylib.EndMode3D();
            }
            else
            {
                //function is disabled.
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
            if (enabled == true)
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
        }

        public virtual void Draw3D(ref Camera3D cam)
        {
            if (enabled)
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

        public static void Destroy(GameObject gameObject)
        {
            //look for the gameobject in children first
            for (int i = 0; i < Program.loader.currentScene.GameObjects.Count; i++)
            {
                //look for the gameobject in the children
                var chosenObject = Program.loader.currentScene.GameObjects[i];
                var foundedObject = chosenObject.childrenObjects.Find(x => x.name == gameObject.name);
                if (foundedObject != null)
                {
                    chosenObject.childrenObjects.Remove(foundedObject);
                }
            }

            //or look if its apart of its children.
            if (gameObject.parent != null)
            {
                var foundedObject = gameObject.parent.childrenObjects.Find(x => x.name == gameObject.name);
                if (foundedObject != null)
                {
                    gameObject.parent.childrenObjects.Remove(foundedObject);
                }
            }

            //find the gameobject and destroy it while using LINQ
            var foundObject = Program.loader.currentScene.GameObjects.Find(x => x.name == gameObject.name);
            if (foundObject != null)
            {
                Program.loader.currentScene.GameObjects.Remove(foundObject);
            }
        }

        public static GameObject Find(string name)
        {
            //Find gameobject in this scene using LINQ
            var foundObject = Program.loader.currentScene.GameObjects.Find(x => x.name == name);
            return foundObject;
        }

        public GameObject Instantiate(GameObject gameObject, Vector3 position, Quaternion rotation)
        {
            //add the gameobject to the scene
            Program.loader.currentScene.GameObjects.Add(gameObject);
            return Find(gameObject.name);
        }

        public GameObject Instantiate(GameObject gameObject, Vector2 position, Quaternion rotation)
        {
            return Instantiate(gameObject, new Vector3(position.X, position.Y, 0), rotation);
        }

        public GameObject Instantiate(GameObject gameObject, Vector3 position)
        {
            return Instantiate(gameObject, position, Quaternion.Identity);
        }

        public GameObject Instantiate(GameObject gameObject, Vector2 position)
        {
            return Instantiate(gameObject, new Vector3(position.X, position.Y, 0), Quaternion.Identity);
        }

        public GameObject Instantiate(GameObject gameObject)
        {
            return Instantiate(gameObject, Vector3.Zero, Quaternion.Identity);
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
            Debug.Log(name + " " + com.name);
            return null;
        }

        public T AddComponent<T>() where T : Component
        {
            Component com = (Component)Activator.CreateInstance(typeof(T));
            if (!comExists(com.name))
            {
                if (com.connectedObject == null) com.connectedObject = this;
                com.Start();
                components.Add(com);
                return GetComponent<T>();
            }

            Debug.Log("Component does already exist!");
            Debug.Log(name + " " + com.name);
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

        public static GameObject[] FindGameObjectsMatchingType<T>() where T : GameObject
        {
            List<GameObject> gameObjects = new List<GameObject>();
            //find all the gameobjects that inherit from the type
            var objects = Program.loader.currentScene.GameObjects.FindAll(x => x is T);
            gameObjects.AddRange(objects);
            //if couldnt find any objects on the base scene look for children objects in that gameobjects
            for (int i = 0; i < objects.Count; i++)
            {
                var gameObject = objects[i];
                for (int m = 0; m < gameObject.childrenObjects.Count; m++)
                {
                    var childObject = gameObject.childrenObjects[m];
                    if (childObject is T)
                    {
                        gameObjects.Add(childObject);
                    }
                }
            }

            return gameObjects.ToArray();
        }

        public GameObject[] FindChildrenMatchingType<T>() where T : GameObject
        {
            var objectsFound = childrenObjects.FindAll(x => x is T);
            return objectsFound.ToArray();
        }

        //create a get component by type simmilar to how unity engine uses it
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                var component = components[i];
                if (component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }

        public void RemoveComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                var component = components[i];
                if (component is T)
                {
                    RemoveComponent(component);
                }
            }
        }

        public static GameObject FindWithComponent<T>() where T : Component
        {
            for (int i = 0; i < SubrightEngine2.Program.loader.currentScene.GameObjects.Count; i++)
            {
                var gameObject = SubrightEngine2.Program.loader.currentScene.GameObjects[i];
                if (gameObject.GetComponent<T>() != null)
                {
                    //found the gameobject where it has this component
                    return gameObject;
                }
            }
            return null;
        }

        public static T FindWithParentType<T>() where T : GameObject
        {
            for (int i = 0; i < SubrightEngine2.Program.loader.currentScene.GameObjects.Count; i++)
            {
                var gameObject = SubrightEngine2.Program.loader.currentScene.GameObjects[i];
                //test if it could convert to the desired type
                if (gameObject is T)
                {
                    //found the gameobject where it has this component
                    return (T)gameObject;
                }
            }
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
            //child object exists
            m.parent = this;
            childrenObjects.Add(m);
            for (var i = 0; i < Program.loader.currentScene.GameObjects.Count; i++)
                if (Program.loader.currentScene.GameObjects[i].name == m.name)
                    Program.loader.currentScene.GameObjects.RemoveAt(i);
            //find if that object already exists and if so change its name
            for (int i = 0; i < childrenObjects.Count; i++)
            {
                var objectDef = childrenObjects[i];
                if (objectDef.name == m.name)
                {
                    m.name = m.name + " " + i;
                }
            }
            Debug.Log("Added object to " + name);
        }

        public void RemoveChild(GameObject m)
        {
            for (var i = 0; i < childrenObjects.Count; i++)
                if (m.name == childrenObjects[i].name)
                {
                    childrenObjects.RemoveAt(i);
                    Debug.Log("Removed object at: " + i + " on " + name);
                }
        }
    }
}