using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;

namespace Game
{
    public class Entity : GameObject
    {
        //run a entity.
        private string pathEntityDraw = "./textures/entity.png";

        private bool collider = false;

        public Entity(Vector2 position, Vector2 size, string name, string spritepath, bool needCollider) : base(name)
        {
            pathEntityDraw = spritepath;
            collider = needCollider;
            if (pathEntityDraw == null || pathEntityDraw == "")
            {
                pathEntityDraw = "./textures/entity.png";
                //where is it
                Debug.Log(name + " has no spritepath, using default spritepath");
            }
            //should be loading the sprite soon enough.
            SpriteRenderer render = new SpriteRenderer("Entity", pathEntityDraw);
            render.offset = new Vector2(-size.X / 2, -size.Y / 2);
            //use conversions as the proper thing is not implemented!!!
            this.position = new Vector3(position.X, position.Y, 0);
            this.size = new Vector3(size.X, size.Y, 0);
            //run into converting to image on the sprite then back
            render.spriteContained.ReSizeSprite(new Vector2(size.X, size.Y));
            //add the component finally.
            this.AddComponent(render);
        }

        public void ReloadTexture(string path)
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            if (render != null)
            {
                render.spriteContained = new Sprite("Entity", path);
                render.spriteContained.ReSizeSprite(new Vector2(size.X, size.Y));
            }
        }

        public void ReloadTexture(Sprite path)
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            if (render != null)
            {
                render.spriteContained = path;
                render.spriteContained.ReSizeSprite(new Vector2(size.X, size.Y));
            }
        }

        public override void Start()
        {
            base.Start();
            //run when the game starts but add shit to it.
            if (collider == true)
            {
                SpriteCollider collidable = new SpriteCollider(Vector2.Zero, Vector2.Zero);
                this.AddComponent(collidable);
            }
        }
    }
}