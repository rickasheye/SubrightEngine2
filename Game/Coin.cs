using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;

namespace Game
{
    public class Coin : GameObject
    {
        public Coin(Vector2 position) : base(new Vector3(position.X, position.Y, 0), new Vector3(60, 60, 0), "Coin")
        {
            //Render that coin
            SpriteRenderer render = (SpriteRenderer)AddComponent<SpriteRenderer>();
            render.connectedObject = this;
            render.spriteContained.LoadSprite(name, "./textures/coin.png");
        }

        public Coin() : base(new Vector3(0, 0, 0), new Vector3(60, 60, 0), "Coin")
        {
            //Render that coin
            SpriteRenderer render = (SpriteRenderer)AddComponent<SpriteRenderer>();
            render.connectedObject = this;
            render.spriteContained.LoadSprite(name, "./textures/coin.png");
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //Render that coin
            var sprite = GetComponent<SpriteRenderer>().spriteContained.containedSprite;
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Raylib_cs.Rectangle(position.X - (sprite.Width / 2), position.Y - (sprite.Width / 2), sprite.Width, sprite.Height)))
            {
                //collect this such coin
                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    //destroy this coin
                    Destroy(this);
                    //add money to the player
                    GameObject.FindWithParentType<SpriteBackground>().money += 1;
                }
            }
        }
    }
}