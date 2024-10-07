using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;

namespace Game
{
    public class SpawnUI : Entity
    {
        private SpriteBackground background;

        public SpawnUI(SpriteBackground background) : base(Vector2.zero, new Vector2(100, 100), "SpawnUI", "./textures/background.png", false)
        {
            RemoveComponent<SpriteRenderer>();
            this.background = background;
        }

        private bool placing = false;

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //draw the ui grab all of the possible spawns
            //and draw them on the screen.
            for (int i = 0; i < background.possibleSpawn.Count; i++)
            {
                //draw the tower.
                var tower = background.possibleSpawn[i];
                var Sprite = tower.GetComponent<SpriteRenderer>().spriteContained.containedSprite;
                Raylib.DrawTexture(Sprite, ((Sprite.Width + 15) * i) + 15, (Raylib.GetRenderHeight() - 15) - Sprite.Height, Raylib_cs.Color.White);

                if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Raylib_cs.Rectangle(((Sprite.Width + 15) * i) + 15, (Raylib.GetRenderHeight() - 15) - Sprite.Height, Sprite.Width, Sprite.Height)))
                {
                    //draw the tower info.
                    var mousePos = Raylib.GetMousePosition();
                    Raylib.DrawRectangle((int)mousePos.X, (int)mousePos.Y, 100, 100, Raylib_cs.Color.White);
                    Raylib.DrawText(tower.name, (int)mousePos.X, (int)mousePos.Y, 10, Raylib_cs.Color.Black);
                    Raylib.DrawText("Cost: " + tower.cost, (int)mousePos.X, (int)mousePos.Y + 10, 10, Raylib_cs.Color.Black);
                    Raylib.DrawText("Range: " + tower.range, (int)mousePos.X, (int)mousePos.Y + 20, 10, Raylib_cs.Color.Black);
                    Raylib.DrawText("Damage: " + tower.targetAffect, (int)mousePos.X, (int)mousePos.Y + 30, 10, Raylib_cs.Color.Black);
                    Raylib.DrawText("Attack Speed: " + tower.atkspeed / 60 + "s", (int)mousePos.X, (int)mousePos.Y + 40, 10, Raylib_cs.Color.Black);
                    if (Raylib.IsMouseButtonDown(MouseButton.Left) && placing == false)
                    {
                        //spawn the tower.
                        if (background.money >= tower.cost)
                        {
                            //create a duplicate of the tower selected
                            Placeable placeable = new Placeable(tower.GetComponent<SpriteRenderer>().spriteContained, tower.name, background);
                            placing = true;
                            AddChild(placeable);
                        }
                    }
                }

                if (Raylib.IsMouseButtonDown(MouseButton.Right))
                {
                    if (placing == true)
                    {
                        placing = false;
                    }
                }
            }
        }
    }
}