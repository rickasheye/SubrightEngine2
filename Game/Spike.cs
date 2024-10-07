using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace Game
{
    public class Spike : Entity
    {
        public Balloon targetBalloon;
        public bool singleDirection = false;
        private int deathrange = 240;
        private Vector2 spawnLoc = Vector2.zero;
        private bool inactivespike = false;

        public Spike(Vector2 position, Balloon targetBalloon, bool singleDirection, Vector2 Direction, int deathrange, bool inactivespike) : base(position, new SubrightEngine2.EngineStuff.Vector2(30, 30), "Spike", "./textures/spike.png", false)
        {
            this.targetBalloon = targetBalloon;
            this.singleDirection = singleDirection;
            this.directionShooting = Direction;
            spawnLoc = position;
            if (directionShooting == Vector2.zero && singleDirection == false)
            {
                //create a direction
                if (targetBalloon != null)
                {
                    directionShooting = targetBalloon.position.ToVector2 - position;
                }
            }
            Debug.Log("Spike alive " + directionShooting.X + " : " + directionShooting.Y + " Single Direction? " + singleDirection.ToString());
            this.deathrange = deathrange;
        }

        public Vector2 directionShooting = Vector2.zero;
        public Vector2 lastDirection = Vector2.zero;

        public void TimeoutDirection()
        {
            //time out if it hasnt moved far
            if (lastDirection == position)
            {
                //destroy this spike
                Destroy(this);
            }
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            float distanceLoc = Vector2.Distance(spawnLoc, position.ToVector2);
            if (distanceLoc > deathrange)
            {
                //destroy this
                Destroy(this);
            }

            if (!inactivespike)
            {
                //kill the target balloon if nearby.
                if (!singleDirection)
                {
                    var newPos = Vector2.MoveTowards(new Vector2(position.X, position.Y), targetBalloon.position.ToVector2, 4);
                    position = new Vector3(newPos.X, newPos.Y, 0);
                }
                else
                {
                    //move in a single direction.
                    position += new Vector3(directionShooting.X, directionShooting.Y, 0);
                }
            }

#if DEBUG
            Raylib.DrawCircleLines((int)position.X, (int)position.Y, 60, Raylib_cs.Color.White);
#endif
            if (targetBalloon != null)
            {
                float distance = Vector2.Distance(position.ToVector2, targetBalloon.position.ToVector2);
                if (distance <= 60)
                {
                    //destroy the balloon
                    targetBalloon.health -= 1;
                    Destroy(this);
                }
            }

            if (position.X > Raylib.GetRenderWidth() && position.Y > Raylib.GetRenderHeight() && position.X < 0 && position.Y < 0)
            {
                //kill this instance of a spike
                Destroy(this);
            }

            TimeoutDirection();
        }
    }
}