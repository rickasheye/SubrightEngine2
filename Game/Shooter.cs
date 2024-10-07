using SubrightEngine2.EngineStuff;

namespace Game
{
    public class Shooter : Tower
    {
        public Shooter(Vector2 position, SpriteBackground background) : base(position, "Shooter", "./textures/shooter.png", background, 240)
        {
            targetAffect = 2;
            cost = 20;
            atkspeed = 100;
        }

        public Shooter()
        {
            background = GameObject.FindWithParentType<SpriteBackground>();
            ReloadTexture("./textures/shooter.png");
            name = "Shooter";
            cost = 20;
            targetAffect = 2;
            atkspeed = 100;
        }

        public override void Shoot(Balloon target)
        {
            base.Shoot(target);
            //wanna shoot a spike once in direction of the balloon
            //lets make a spike entity.
            for (int i = 0; i < targetAffect; i++)
            {
                Spike spike = (Spike)Instantiate(new Spike(position.ToVector2, target, false, new Vector2(0, 0), range, false));
            }
            //GetComponent<SpriteRenderer>().RotateTowardsVector2(target.position.ToVector2);
        }
    }
}