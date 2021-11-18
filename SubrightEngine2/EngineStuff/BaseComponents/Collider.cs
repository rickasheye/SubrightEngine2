using Raylib_cs;
using System;
using System.Collections.Generic;

namespace SubrightEngine2.EngineStuff.BaseComponents
{
    [Serializable]
    public class Collider : Component
    {
        private List<GameObject> collisionObjects = new List<GameObject>();
        public Vector3 offset_position;
        public Vector3 offset_size;

        public Collider(Vector3 offsetPosition, Vector3 offsetSize, string nameCollider) : base(nameCollider)
        {
            this.offset_position = offsetPosition;
            this.offset_size = offsetSize;
            //dispose of this once we are done with it
            var objectsWithCollision = new List<GameObject>();
            for (var i = 0; i < Program.loader.currentScene.GameObjects.Count; i++)
            {
                var trueCollision = false;
                var coms = Program.loader.currentScene.GameObjects[i].components;
                for (var m = 0; m < coms.Count; m++)
                    if (coms[m].Equals(typeof(BoxCollider)))
                    {
                        trueCollision = true;
                        break;
                    }

                if (trueCollision) objectsWithCollision.Add(Program.loader.currentScene.GameObjects[i]);
            }

            collisionObjects.AddRange(objectsWithCollision);
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

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            //check for collision amongst objects.
            for (var i = 0; i < collisionObjects.Count; i++)
            {
                var previousCollision = Vector3.zero;
                var collision = collisionObjects[i];
                var prevCol = new Vector3(connectedObject.position.X - 1, connectedObject.position.Y - 1,
                    connectedObject.position.Z - 1);
                var prevColSize = new Vector3(connectedObject.size.X - 1, connectedObject.size.Y - 1,
                    connectedObject.size.Z - 1);
                if (intersect(prevCol, prevCol + prevColSize, collision.position, collision.position + collision.size))
                    previousCollision = connectedObject.position;

                if (intersect(connectedObject.position, connectedObject.position + connectedObject.size,
                    collision.position, collision.position + collision.size))
                    connectedObject.position = previousCollision;
            }
            Draw3D(ref cam3);
        }

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            if (Program.showCollision && connectedObject.position.Z > 0)
            {
                //then is 3d create a 3d shape.
                Raylib.DrawCube(new System.Numerics.Vector3(offset_position.X, offset_position.Y, offset_position.Z), offset_size.X + 1, offset_size.Y + 1, offset_size.Z + 1, Raylib_cs.Color.GREEN);
            }
        }
    }
}