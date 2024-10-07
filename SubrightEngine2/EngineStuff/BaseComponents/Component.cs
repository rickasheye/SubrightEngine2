using Raylib_cs;
using System;

namespace SubrightEngine2.EngineStuff.BaseComponents
{
    [Serializable]
    public class Component : Drawable
    {
        public GameObject connectedObject;
        public string name;
        public bool enabled = true;

        public Component(string name)
        {
            this.name = name;
        }

        public virtual void Start()
        {
            if (connectedObject == null)
            {
                connectedObject = GameObject.getOwner(this);
                Debug.LogWarning("Unfortunately this object doesnt have any connected object");
            }
        }

        public virtual void Draw2D(ref Camera2D cam)
        {
            if (enabled == false)
            {
                //do not execute this function
                return;
            }
        }

        public virtual void Draw3D(ref Camera3D cam)
        {
            if (enabled == false)
            {
                //do not execute this function
                return;
            }
        }
    }
}