using SubrightEngine2.EngineStuff;
using Random = SubrightEngine2.EngineStuff.Random;

namespace Game
{
    public class Mine : Tower
    {
        public Mine(Vector2 position, SpriteBackground background) : base(position, "Mine", "./textures/mine.png", background, 240)
        {
            targetAffect = 1;
            inactivetower = true;
            cost = 5;
            targetAffect = 1;
            atkspeed = 20;
        }

        public Mine()
        {
            background = GameObject.FindWithParentType<SpriteBackground>();
            ReloadTexture("./textures/mine.png");
            name = "Mine";
            inactivetower = true;
            cost = 5;
            targetAffect = 1;
            atkspeed = 20;
        }

        public override void Shoot(Balloon target)
        {
            base.Shoot(target);
            //spawn coins
            Vector2 posi = new Vector2(Random.Range((int)position.X - 10, (int)position.X + 10), Random.Range((int)position.Y - 10, (int)position.Y + 10));
            Coin coin = new Coin(posi);
            //AddChild(coin);
        }
    }
}