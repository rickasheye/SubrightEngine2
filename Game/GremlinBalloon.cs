using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System.Collections.Generic;

namespace Game
{
    public class GremlinBalloon : Balloon
    {
        public GremlinPoision poision = null;
        public bool poisioned = false;

        public GremlinBalloon(List<Point> points) : base(points)
        {
            health = 10;
            speed = 1;
            cost = 20;
            name = "Gremlin Balloon";
            ReloadTexture("./textures/gremlin.png");
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //follow the poision if poisioned
            if (poisioned)
            {
                stop = true;
                Vector2 newPos = Vector2.MoveTowards(position.ToVector2, poision.position.ToVector2, speed);
                position = new Vector3(newPos.X, newPos.Y, 0);
                float distance = Vector2.Distance(position.ToVector2, poision.position.ToVector2);
                if (distance <= 30)
                {
                    //destroy this and the poision
                    Destroy(this);
                    Destroy(poision);
                }
            }
        }
    }
}