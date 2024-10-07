using SubrightEngine2.EngineStuff;

namespace Game
{
    public class GremlinPoision : Tower
    {
        public GremlinPoision(Vector2 position, SpriteBackground background) : base(position, "Gremlin Poision", "./textures/gremlinpoision.png", background, 240)
        {
            targetAffect = 1;
            inactivetower = true;
            cost = 5;
            targetAffect = 1;
            atkspeed = 20;
        }

        public GremlinPoision()
        {
            background = GameObject.FindWithParentType<SpriteBackground>();
            ReloadTexture("./textures/gremlinpoision.png");
            name = "Gremlin Poision";
            inactivetower = true;
            cost = 5;
            targetAffect = 1;
            atkspeed = 20;
        }

        public override void Shoot(Balloon target)
        {
            base.Shoot(target);
            //find the nearest balloon if its a gremlin use it
            if (target is GremlinBalloon)
            {
                //poision it or make it follow the path to this and then kill the balloon and this
                var gremlinInstance = (GremlinBalloon)target;
                gremlinInstance.poision = this;
                gremlinInstance.poisioned = true;
            }
        }
    }
}