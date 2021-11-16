using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System.Collections.Generic;
using System.IO;

namespace SubrightTilemapCreator
{
    public class TileSelector : GameObject
    {
        public TileSelector() : base(Vector3.zero, Vector3.zero, "TileSelector") { }

        public List<GameObject> sprites = new List<GameObject>();

        public override void Start()
        {
            base.Start();
            //search or load all sprites into a container
            string path = Path.Combine(Directory.GetCurrentDirectory(), "textures/");
            if (Directory.Exists(path))
            {
                var directoryFiles = Directory.GetFiles(path, "*.png", SearchOption.AllDirectories);
                for (int i = 0; i < directoryFiles.Length; i++)
                {
                    if (!directoryFiles[i].Contains("titlescreen"))
                    {
                        Sprite spr = new Sprite("sprite" + i, directoryFiles[i]);
                        spr.Start();
                        sprites.Add(AssembleObject(spr));
                    }
                }
            }
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            for (int i = 0; i < sprites.Count; i++)
            {
                GameObject spr = sprites[i];
                spr.Update(ref cam2, ref cam3);
            }
            Draw2D(ref cam2);
        }

        int offset = 0;
        public void Draw2DGrid(int XOffset, int YOffset, int slices, int size)
        {
            for (int x = 0; x < slices; x++)
            {
                Raylib.DrawLine((x + size), 0, (x + size) * slices, 0, Raylib_cs.Color.BLACK);
                for (int y = 0; y < slices; y++)
                {
                    Raylib.DrawLine(0, (y + size), 0, (y + size) + slices, Raylib_cs.Color.BLACK);
                }
            }
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            Raylib.ClearBackground(Raylib_cs.Color.BLUE);
            Draw2DGrid(32, 32, 20, 30);
            if (sprites != null)
            {
                //render all of the sprites here
                if (Raylib.GetMouseWheelMove() > 0)
                {
                    if (offset < 0)
                    {
                        offset += 10;
                    }
                }
                else if (Raylib.GetMouseWheelMove() < 0)
                {
                    offset -= 10;
                }

                for (int i = 0; i < sprites.Count; i++)
                {
                    GameObject spriteCalc = sprites[i];
                    Sprite spri = (Sprite)spriteCalc.GetComponent(spriteCalc.name);

                    if (spri != null)
                    {
                        Vector2 offsetPosition = new Vector2(10, ((10 * i) + offset) + (spri.containedSprite.height));
                        if (spriteCalc.position.X == offsetPosition.X && spriteCalc.position.Y == offsetPosition.Y)
                        {
                            spriteCalc.Draw2D(ref cam);
                            if (Raylib.GetMouseX() > 10 && Raylib.GetMouseX() < 26 && Raylib.GetMouseY() > offsetPosition.Y && Raylib.GetMouseY() < offsetPosition.Y + 16)
                            {
                                Raylib.DrawText(spri.path, 30, (int)offsetPosition.Y, 8, Raylib_cs.Color.WHITE);
                            }
                        }
                        else
                        {
                            spriteCalc.position = new Vector3(offsetPosition.X, offsetPosition.Y, 0);
                        }
                    }
                }
            }
        }
    }
}
