using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game
{
    public class Point
    {
        public Vector2 position;
        public Point previousPoint = null;
    }

    public class SpriteBackground : Entity
    {
        public List<Point> points = new List<Point>();
        public int health = 100;
        public int money = 100;

        //just to make it easier lets make a seperate array for the balloons
        public List<Tower> possibleSpawn = new List<Tower>();

        public SpawnUI spawnui;

        public int wave = 0;

        public SpriteBackground() : base(new Vector2(0, 0), new Vector2(Raylib_cs.Raylib.GetScreenWidth(), Raylib.GetScreenHeight()), "Background", "./textures/background.png", false)
        {
            //ok now we got image we want to place nodes for enemies to travel on.
            //get and read the sprite.
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            if (render != null)
            {
                render.offset = new Vector2(0, 0);
                Debug.Log("attempting to calculate image points.");
                Raylib_cs.Image image = Raylib.LoadImageFromTexture((Texture2D)render.spriteContained.containedSprite);
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var imageColor = Raylib.GetImageColor(image, x, y);
                        //use this color to identify points.
                        if (imageColor.R == 0 && imageColor.G == 0 && imageColor.B == 0)
                        {
                            Point point = new Point();
                            point.position = new Vector2(x, y);
                            Raylib.ImageDrawPixel(ref image, x, y, new Raylib_cs.Color(216, 216, 216, 255));
                            points.Add(point);
                        }
                    }
                }
                render.spriteContained.containedSprite = Raylib.LoadTextureFromImage(image);
                Raylib.UnloadImage(image);
                List<Point> orderedPoints = OrderPoints(points);
                points.Clear();
                points.AddRange(orderedPoints);
            }
            FindAllTowers();
            spawnui = new SpawnUI(this);
            AddChild(spawnui);
        }

        public bool onWhite(int X, int Y)
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                var Image = Raylib.LoadImageFromTexture(renderer.spriteContained.containedSprite);
                var ImageColor = Raylib.GetImageColor(Image, X, Y);
                if (ImageColor.R == 216 && ImageColor.G == 216 && ImageColor.B == 216)
                {
                    return true;
                }
                Raylib.UnloadImage(Image);
            }
            return false;
        }

        //find all classes that extend Tower and add them to the list.
        public void FindAllTowers()
        {
            foreach (var type in typeof(Tower).Assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(Tower)))
                {
                    //add to list
                    Tower tower = (Tower)Activator.CreateInstance(type);
                    possibleSpawn.Add(tower);
                }
            }
        }

        private static List<Point> OrderPoints(List<Point> points)
        {
            // Start with the first point
            List<Point> orderedPoints = new List<Point> { points[0] };

            while (orderedPoints.Count < points.Count)
            {
                Point lastPoint = orderedPoints.Last();
                Point closestPoint = points
                    .Except(orderedPoints)
                    .OrderBy(p => Vector2.Distance(lastPoint.position, p.position))
                    .First();
                lastPoint.previousPoint = closestPoint;
                orderedPoints.Add(closestPoint);
            }

            return orderedPoints;
        }

        private bool peekpath = false;
        private bool spawning = false;
        private int timer = 101;
        private int maxtime = 100;
        private int nospawned = 0;
        private int lastwave = 0;

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //draw the points.
            if (SubrightEngine2.Program.debug == true && peekpath == true)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Point m = points[i];
                    Raylib.DrawCircle((int)m.position.X, (int)m.position.Y, 5, Raylib_cs.Color.Red);
                    //draw the index on the point
                    Raylib.DrawText(i.ToString(), (int)m.position.X, (int)m.position.Y, 20, Raylib_cs.Color.Black);
                    if (m.previousPoint != null)
                    {
                        Raylib.DrawLineEx(new System.Numerics.Vector2(m.position.X, m.position.Y), new System.Numerics.Vector2(m.previousPoint.position.X, m.previousPoint.position.Y), 5, Raylib_cs.Color.Red);
                    }
                }
            }

            if (FindChildrenMatchingType<Balloon>().Length <= 0)
            {
                //create set of balloons
                spawning = true;
            }

            if (nospawned >= wave * 10)
            {
                nospawned = 0;
                wave++;
            }

            if (spawning == true)
            {
                if (FindChildrenMatchingType<Balloon>().Length <= wave * 10)
                {
                    timer++;
                    if (timer > maxtime)
                    {
                        Balloon balloon = new Balloon(points);
                        int wavethreshold = wave % 10;
                        Type[] types = { Type.Red };
                        switch (wavethreshold)
                        {
                            case 1:
                            case 0:
                                types = new Type[] { Type.Red };
                                break;

                            case 2:
                                types = new Type[] { Type.Red, Type.BLUE };
                                break;

                            case 3:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN };
                                break;

                            case 4:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN, Type.YELLOW };
                                break;

                            case 5:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN, Type.YELLOW, Type.ORANGE };
                                break;

                            case 6:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN, Type.YELLOW, Type.ORANGE, Type.Black };
                                break;

                            case 7:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN, Type.YELLOW, Type.ORANGE, Type.Black, Type.White };
                                break;

                            case 8:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN, Type.YELLOW, Type.ORANGE, Type.Black, Type.White, Type.MAGENTA };
                                break;

                            case 9:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN, Type.YELLOW, Type.ORANGE, Type.Black, Type.White, Type.MAGENTA, Type.GREMLIN };
                                break;

                            case 10:
                                types = new Type[] { Type.Red, Type.BLUE, Type.GREEN, Type.YELLOW, Type.ORANGE, Type.Black, Type.White, Type.MAGENTA, Type.GREMLIN, Type.MECHA };
                                break;
                        }
                        balloon.balloonType = types[SubrightEngine2.EngineStuff.Random.Range(0, types.Length)];
                        if (balloon.balloonType == Type.GREMLIN)
                        {
                            balloon = new GremlinBalloon(points);
                        }
                        else if (balloon.balloonType == Type.MECHA)
                        {
                            balloon = new mechaballoon(points);
                        }
                        balloon.Load();
                        balloon.position = new Vector3(points[0].position.X, points[0].position.Y, 0);
                        balloon.background = this;
                        AddChild(balloon);
                        maxtime = balloon.spawnRate;
                        nospawned++;
                        timer = 0;
                    }
                }
                else
                {
                    spawning = false;
                }
            }

            //push drawing the hud
            Raylib.DrawText("Health: " + health.ToString(), 10, 10, 20, Raylib_cs.Color.Black);
            Raylib.DrawText("Money: " + money.ToString(), 10, 25, 20, Raylib_cs.Color.Black);
            Raylib.DrawText("Wave: " + wave.ToString(), 10, 40, 20, Raylib_cs.Color.Black);
        }

        private static async Task DelayAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}