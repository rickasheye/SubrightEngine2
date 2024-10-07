using System.Collections.Generic;

namespace SubrightEngine2.EngineStuff.BaseComponents._2DComponents
{
    public class SpriteCollider : Collider
    {
        private Vector2 offset_position = Vector2.Zero;
        private Vector2 offset_size = Vector2.Zero;

        public SpriteCollider(Vector2 offsetPosition, Vector2 offsetSize) : base(new Vector3(offsetPosition.X, offsetPosition.Y, 0), new Vector3(offsetSize.X, offsetSize.Y, 0), "Sprite Collider")
        {
        }

        public override bool intersect(Rectangle rect1, Rectangle rect2)
        {
            //return base.intersect(amin, amax, bmin, bmax);
            return rect1.x < rect2.x + rect2.width && rect1.x + rect1.width > rect2.x &&
                   rect1.y < rect2.y + rect2.height && rect1.y + rect1.height > rect2.y;
        }

        public override void Draw2D(ref Raylib_cs.Camera2D cam2)
        {
            base.Draw2D(ref cam2);
            //when running please check collision with other objects.
            List<GameObject> objects = Program.loader.currentScene.GameObjects;
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].GetComponent("SpriteCollider") != null && objects[i] != connectedObject)
                {
                    //get intersecting sprite collider
                    SpriteCollider spriteCollider = (SpriteCollider)objects[i].GetComponent<SpriteCollider>();
                    if (intersect(new Rectangle((int)(connectedObject.position.X + offset_position.X), (int)(connectedObject.position.Y + offset_position.Y), (int)offset_size.X, (int)offset_size.Y), new Rectangle((int)(objects[i].position.X + spriteCollider.offset_position.X), (int)(objects[i].position.Y + spriteCollider.offset_position.Y), (int)spriteCollider.offset_size.X, (int)spriteCollider.offset_size.Y)))
                    {
                        //then it is colliding.
                        //Debug.Log("Colliding with " + objects[i].name);
                        this.OnCollision(objects[i]);
                    }
                }
            }
        }
    }
}