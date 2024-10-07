using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;

namespace Game
{
    public class SpikeSpawner : Tower
    {
        public SpikeSpawner(Vector2 position, SpriteBackground background) : base(position, "Spike Spawner", "./textures/spikespawner.png", background, 240)
        {
            targetAffect = 2;
            inactivetower = true;
            cost = 40;
            targetAffect = 1;
            atkspeed = 60;
        }

        public SpikeSpawner()
        {
            background = GameObject.FindWithParentType<SpriteBackground>();
            ReloadTexture("./textures/spikespawner.png");
            name = "Spike Spawner";
            inactivetower = true;
            cost = 40;
            targetAffect = 1;
            atkspeed = 60;
        }

        public override void Shoot(Balloon target)
        {
            base.Shoot(target);
            //dont shoot just spawn a spike. on the pathway.
            SpawnSpike(target);
        }

        public void SpawnSpike(Balloon target)
        {
            Spike spike = (Spike)Instantiate(new Spike(position.ToVector2, target, true, new Vector2(0, 0), range, true));
            if (background != null)
            {
                //get the background and drop spikes on the closest point
                var renderer = background.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    //get all the closest pixels in range
                    foreach (Point point in background.points)
                    {
                        if (Vector2.Distance(position.ToVector2, point.position) < range)
                        {
                            //throw a point to end up here.
                            spike.position = new Vector3(SubrightEngine2.EngineStuff.Random.Range((int)point.position.X - 10, (int)point.position.X + 10), SubrightEngine2.EngineStuff.Random.Range((int)point.position.Y - 10, (int)point.position.Y + 10), 0);
                            break;
                        }
                    }
                }
            }

            AddChild(spike);
            if (spike.position.ToVector2 == Vector2.zero || spike.position.ToVector2 == position.ToVector2)
            {
                //recalculate the position.
                SpawnSpike(target);
                Destroy(spike);
            }
        }
    }
}