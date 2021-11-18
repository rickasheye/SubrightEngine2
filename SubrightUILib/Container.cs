using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightEngine2.UI
{
    public class Container : UIElement
    {
        //A Container to feed objects to therefore correctly sort.
        public GameObject focused;
        public Vector2 offset;
        public float offsetAfterY;

        public Container(Vector2 position, Vector2 size, Vector2 offset, float offsetAfterY) : base("UIContainer", position, size) { UpdatePositionOfObjects(); this.offset = offset; this.offsetAfterY = offsetAfterY; }

        /// <summary>
        /// Update all positions and sizes of the objects in children of this container object.
        /// </summary>
        public void UpdatePositionOfObjects()
        {
            //If focused is null please "re-inject"  
            if (connectedObject != null && connectedObject.childrenObjects != null)
            {
                if (connectedObject.childrenObjects.Count > 0)
                {
                    if (focused == null)
                    {
                        focused = connectedObject.childrenObjects[SubrightEngine2.EngineStuff.Random.Range(connectedObject.childrenObjects.Count)];
                        Debug.Log("picked a random value for this container", false);
                    }

                    for (int i = 0; i < connectedObject.childrenObjects.Count; i++)
                    {
                        GameObject m = connectedObject.childrenObjects[i];
                        m.position = new Vector3(position.X + offset.X, (position.Y + offset.Y) * (i + offsetAfterY), 0);
                        m.size = new Vector3(size.X, size.Y, 0);
                    }
                }
                //Debug.Log("there is " + connectedObject.childrenObjects.Count);
            }
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            Raylib.DrawRectangleLines((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, Raylib_cs.Color.BLACK);
        }
    }
}
