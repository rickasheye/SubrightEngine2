using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;

namespace Game
{
    public class Placeable : Entity
    {
        public Sprite mimicpath;
        private SpriteBackground background;
        private string towername = "Untitled Placeable";

        public Placeable(Sprite mimicpath, string towername, SpriteBackground background) : base(new SubrightEngine2.EngineStuff.Vector2(0, 0), new SubrightEngine2.EngineStuff.Vector2(60, 60), "Untitled Placeable", "./textures/error.png", true)
        {
            ReloadTexture(mimicpath);
            this.towername = towername;
            this.background = background;
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //draw the certain placeable.
            position = new SubrightEngine2.EngineStuff.Vector3(Raylib.GetMousePosition().X, Raylib.GetMousePosition().Y, 0);
            if (Raylib.IsMouseButtonDown(MouseButton.Right))
            {
                if (!background.onWhite((int)position.X, (int)position.Y))
                {
                    //place the so called item.
                    //create a duplicate of the tower selected
                    Debug.Log("given tower " + towername);
                    Tower newTower = MakeNewTower(towername);
                    if (newTower != null)
                    {
                        background.money -= newTower.cost;
                        background.AddChild(newTower);
                        Destroy(this);
                    }
                }
                else
                {
                    //unable to place.
                }
            }
        }

        public Tower MakeNewTower(string tower)
        {
            switch (tower.ToLower())
            {
                case "pink spike shooter":
                    PinkSpikeShooter shooter = new PinkSpikeShooter();
                    shooter.position = position;
                    return shooter;
                    break;

                case "shooter":
                    Shooter shooter2 = new Shooter();
                    shooter2.position = position;
                    return shooter2;
                    break;

                case "spike spawner":
                    SpikeSpawner spawner = new SpikeSpawner();
                    spawner.position = position;
                    return spawner;
                    break;

                case "shock tower":
                    ShockTower shockTower = new ShockTower();
                    shockTower.position = position;
                    return shockTower;
                    break;

                case "gremlin poision":
                    GremlinPoision poision = new GremlinPoision();
                    poision.position = position;
                    return poision;
                    break;

                case "mine":
                    Mine mine = new Mine();
                    mine.position = position;
                    return mine;
                    break;
            }
            Debug.Log("unable to make tower unidentified");
            return null;
        }
    }
}