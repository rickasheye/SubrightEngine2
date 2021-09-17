namespace SCPBreakdown.EngineStuff.BaseComponents._2DComponents
{
    public class SpriteCollider : Collider{
    
        public SpriteCollider(Vector2 offsetPosition, Vector2 offsetSize):base(new Vector3(offsetPosition.X, offsetPosition.Y, 0), new Vector3(offsetSize.X, offsetSize.Y, 0), "Sprite Collider"){}

        public override bool intersect(Rectangle rect1, Rectangle rect2)
        {
            //return base.intersect(amin, amax, bmin, bmax);
            return rect1.x < rect2.x + rect2.width && rect1.x + rect1.width > rect2.x &&
                   rect1.y < rect2.y + rect2.height && rect1.y + rect1.height > rect2.y;
        }
    }
}