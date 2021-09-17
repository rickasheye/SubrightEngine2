using System;
using Raylib_cs;

namespace SCPBreakdown.EngineStuff.BaseComponents
{
    [Serializable]
    public class Component : Drawable
    {
        public GameObject connectedObject;
        public string name;

        public Component(string name)
        {
            this.name = name;
        }

        public virtual void Start()
        {
            if (connectedObject == null)
            {
                connectedObject = GameObject.getOwner(this);
                Debug.Log("Unfortunately this object doesnt have any connected object", LogType.WARNING);
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
        }

        public virtual void Draw2D(ref Camera2D cam)
        {
        }

        public virtual void Draw3D(ref Camera3D cam)
        {
        }
    }
}