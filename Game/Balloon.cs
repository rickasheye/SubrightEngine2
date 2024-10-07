using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;
using System.Collections.Generic;

namespace Game
{
    public enum Type
    {
        Red, BLUE, GREEN, YELLOW, ORANGE, MAGENTA, White, Black, GREMLIN, MECHA
    }

    public class Balloon : Entity
    {
        public List<Point> Points = null;
        public int index = 0;
        public float speed = 3f;
        public SpriteBackground background;
        public Type balloonType = Type.Red;
        public int health = 1;
        public int cost = 1;
        public int spawnRate = 100;
        public bool stop = false;

        public Balloon(List<Point> points) : base(Vector2.zero, new Vector2(60, 60), "Balloon Entity", "./textures/balloon.png", true)
        {
            Points = points;
            Load();
        }

        public override void Start()
        {
            base.Start();
            Load();
        }

        public void Load()
        {
            var render = GetComponent<SpriteRenderer>();
            if (render != null)
            {
                //get the sprite and move it up
                render.offset.Y = -30;
                switch (balloonType)
                {
                    case Type.BLUE:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.Blue);
                        health = 2;
                        speed = 2;
                        cost = 2;
                        spawnRate = 100;
                        break;

                    case Type.GREEN:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.Green);
                        health = 4;
                        speed = 4;
                        cost = 4;
                        spawnRate = 50;
                        break;

                    case Type.Red:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.Red);
                        health = 1;
                        speed = 1;
                        cost = 1;
                        spawnRate = 150;
                        break;

                    case Type.YELLOW:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.Yellow);
                        health = 3;
                        speed = 3;
                        cost = 8;
                        spawnRate = 25;
                        break;

                    case Type.ORANGE:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.Orange);
                        health = 5;
                        speed = 5;
                        cost = 16;
                        spawnRate = 12;
                        break;

                    case Type.Black:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.Black);
                        health = 10;
                        speed = 10;
                        cost = 32;
                        spawnRate = 6;
                        break;

                    case Type.White:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.White);
                        health = 20;
                        speed = 20;
                        cost = 64;
                        spawnRate = 3;
                        break;

                    case Type.MAGENTA:
                        render.spriteContained.SetHighlightColor(Raylib_cs.Color.Magenta);
                        health = 40;
                        speed = 40;
                        cost = 128;
                        spawnRate = 1;
                        break;
                }
            }
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (Points != null)
            {
                //go from point to point.
                if (index <= Points.Count - 1 && index >= 0)
                {
                    if (stop == false)
                    {
                        Vector2 newPos = Vector2.MoveTowards(position.ToVector2, Points[index].position, speed);
                        position = new Vector3(newPos.X, newPos.Y, 0);
                        float Distance = Vector2.Distance(position.ToVector2, Points[index].position);
                        if (Distance < 3)
                        {
                            index++;
                        }
                    }
                    else
                    {
                        Debug.Log("Waiting for next ballooon");
                    }
                }
                else
                {
                    background.health -= 1 * health;
                    //find this baloon in background and delete it
                    background.RemoveChild(this);
                }

                if (health <= 0)
                {
                    background.RemoveChild(this);
                    background.money += cost;
                }
            }
#if DEBUG
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Raylib_cs.Rectangle(position.X, position.Y, size.X, size.Y)))
            {
                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    //running or using to get the mouse down
                    speed = speed * 10;
                }
            }
#endif
        }
    }
}