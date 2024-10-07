using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace Game
{
    public class PinkSpikeShooter : Tower
    {
        public PinkSpikeShooter(Vector2 position, SpriteBackground background) : base(position, "Pink Spike Shooter", "./textures/pinkspikeshooter.png", background, 120)
        {
            targetAffect = 4;
            cost = 10;
            atkspeed = 80;
        }

        public PinkSpikeShooter()
        {
            background = GameObject.FindWithParentType<SpriteBackground>();
            ReloadTexture("./textures/pinkspikeshooter.png");
            name = "Pink Spike Shooter";
            targetAffect = 4;
            cost = 10;
            atkspeed = 80;
        }

        public Vector2[] directions = { new Vector2(0, -1), new Vector2(0, 1), new Vector2(1, 0), new Vector2(-1, 0) };

        public override void Shoot(Balloon target)
        {
            //base.Shoot(target);
            //wanna shoot a spike in all directions.
            //lets make a spike entity.
            for (int i = 0; i < directions.Length; i++)
            {
                Spike spike = (Spike)Instantiate(new Spike(position.ToVector2, target, true, directions[i], range, false));
            }
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
#if DEBUG
            for (int i = 0; i < directions.Length; i++)
            {
                var direction = directions[i];
                Raylib.DrawLine((int)position.X, (int)position.Y, (int)position.X + (int)direction.X + 60, (int)position.Y + (int)direction.Y + 60, Raylib_cs.Color.White);
            }
#endif
        }
    }
}