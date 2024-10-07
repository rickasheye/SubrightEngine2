using Raylib_cs;
using System;

namespace SubrightEngine2.EngineStuff.BaseComponents
{
    public enum ContainmentType
    {
        Disjoint, Contains, Intersects
    }

    [Serializable]
    public class Collider : Component
    {
        public Vector3 offset_position;
        public Vector3 offset_size;

        public Collider(Vector3 offsetPosition, Vector3 offsetSize, string nameCollider) : base(nameCollider)
        {
            this.offset_position = offsetPosition;
            this.offset_size = offsetSize;
        }

        public virtual bool intersect(Rectangle rect1, Rectangle rect2)
        {
            Debug.Log("No collider data has been processed");
            return false;
        }

        public virtual bool intersect(Vector3 amin, Vector3 amax, Vector3 bmin, Vector3 bmax)
        {
            Debug.Log("No collider data has been processed");
            return false;
        }

        public virtual void OnCollision(GameObject onCollideWith)
        {
        }

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            if (Program.showCollision && connectedObject.position.Z > 0)
            {
                //then is 3d create a 3d shape.
                Raylib.DrawCube(new System.Numerics.Vector3(offset_position.X, offset_position.Y, offset_position.Z), offset_size.X + 1, offset_size.Y + 1, offset_size.Z + 1, Raylib_cs.Color.Green);
            }
        }
    }
}