using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;
using Color = Raylib_cs.Color;

namespace Game
{
    public class Tower : Entity
    {
        public SpriteBackground background;
        public int targetAffect;
        public int range = 240;
        public int cost = 10;
        public int atkspeed = 100;

        //tower that doesnt need to move anything.
        public bool inactivetower = false;

        public int upgradePercentage = 0;

        public Tower(Vector2 position, string name, string path, SpriteBackground background, int setRange) : base(position, new Vector2(60, 60), name, path, true)
        {
            this.background = background;
            this.range = setRange;
        }

        public Tower(string name, string path, SpriteBackground background, int setRange) : base(new Vector2(0, 0), new Vector2(60, 60), name, path, true)
        {
            this.background = background;
            this.range = setRange;
        }

        public Tower(string name, string path, SpriteBackground background) : base(new Vector2(0, 0), new Vector2(60, 60), name, path, true)
        {
            this.background = background;
            this.range = 240;
        }

        public Tower(string name, string path) : base(new Vector2(0, 0), new Vector2(60, 60), name, path, true)
        {
            this.range = 240;
            FindBackground();
        }

        public Tower(string name) : base(new Vector2(0, 0), new Vector2(60, 60), name, "./textures/error.png", true)
        {
            this.range = 240;
            FindBackground();
        }

        public Tower() : base(new Vector2(0, 0), new Vector2(60, 60), "Untitled Tower", "./textures/error.png", true)
        {
            this.range = 240;
            FindBackground();
        }

        public void FindBackground()
        {
            background = GameObject.FindWithParentType<SpriteBackground>();
        }

        public int timer = 0;
        public Balloon targetBalloon = null;

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //when we render we want to find the nearest enemy.
            float closeDistance = 9999;
            for (int i = 0; i < background.FindChildrenMatchingType<Balloon>().Length; i++)
            {
                var balloon = background.FindChildrenMatchingType<Balloon>()[i];
                float distance = Vector3.Distance(position, balloon.position);
                if (distance < closeDistance && distance < (range == 0 ? 9999 : range))
                {
                    closeDistance = distance;
                    targetBalloon = (Balloon)balloon;
                }
            }

            var sprite = GetComponent<SpriteRenderer>().spriteContained.containedSprite;
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Raylib_cs.Rectangle(position.X - (sprite.Width / 2), position.Y - (sprite.Width / 2), sprite.Width, sprite.Height)))
            {
                //draw the tower info.
                var mousePos = Raylib.GetMousePosition();
                Raylib.DrawRectangle((int)mousePos.X, (int)mousePos.Y, 100, 100, Raylib_cs.Color.White);
                Raylib.DrawText(name, (int)mousePos.X, (int)mousePos.Y, 10, Raylib_cs.Color.Black);
                Raylib.DrawText("Cost: " + cost, (int)mousePos.X, (int)mousePos.Y + 10, 10, Raylib_cs.Color.Black);
                Raylib.DrawText("Range: " + range, (int)mousePos.X, (int)mousePos.Y + 20, 10, Raylib_cs.Color.Black);
                Raylib.DrawText("Damage: " + targetAffect, (int)mousePos.X, (int)mousePos.Y + 30, 10, Raylib_cs.Color.Black);
                Raylib.DrawText("Attack Speed: " + atkspeed / 60 + "s", (int)mousePos.X, (int)mousePos.Y + 40, 10, Raylib_cs.Color.Black);
                //user click left to upgrade text
                Raylib.DrawText("Click Left Upgrade", (int)mousePos.X, (int)mousePos.Y + 60, 10, Raylib_cs.Color.Black);
                Raylib.DrawText("Click Middle Sell", (int)mousePos.X, (int)mousePos.Y + 70, 10, Raylib_cs.Color.Black);
                if (upgradePercentage <= 100)
                {
                    Raylib.DrawText("Upgrade Percentage: " + upgradePercentage + "%", (int)mousePos.X, (int)mousePos.Y + 50, 10, Raylib_cs.Color.Black);
                    //check on the upgrades
                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        //upgrade the tower.
                        if (background.money >= cost)
                        {
                            background.money -= cost;
                            cost += 10;
                            upgradePercentage += 10;
                            targetAffect += 1;
                            atkspeed -= 5;
                            range += 10;
                        }
                    }
                }
                else
                {
                    Raylib.DrawText("Upgrade Percentage: MAX", (int)mousePos.X, (int)mousePos.Y + 50, 10, Raylib_cs.Color.Black);
                }
                //user click left to upgrade text
                if (upgradePercentage <= 100)
                {
                    if (Raylib.IsMouseButtonPressed(MouseButton.Middle))
                    {
                        //sell the tower.
                        background.money += cost;
                        Destroy(this);
                    }
                }
            }

#if DEBUG
            Raylib.DrawCircleLines((int)position.X, (int)position.Y, range, Color.White);
#endif

            if (targetBalloon != null || inactivetower == true)
            {
                //then we want to shoot at it.
#if DEBUG
                if (SubrightEngine2.Program.debug && targetBalloon != null) { Raylib.DrawLineEx(new System.Numerics.Vector2(position.X, position.Y), new System.Numerics.Vector2(targetBalloon.position.X, targetBalloon.position.Y), 5, Raylib_cs.Color.Red); }
#endif
                timer++;
                if (timer > atkspeed)
                {
                    Shoot(targetBalloon);
                    timer = 0;
                }
            }
        }

        public virtual void Shoot(Balloon target)
        {
            if (target != null)
            {
                target.health -= targetAffect;
            }
        }
    }
}