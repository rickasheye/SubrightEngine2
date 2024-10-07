using Raylib_cs;
using SubrightEngine2.EngineStuff.BaseComponents;
using System.Collections.Generic;
using System.Numerics;

namespace SubrightEngine2.Editor.Tools
{
    public class SpriteMapManipulator : Component
    {
        public SpriteMapManipulator() : base("Sprite Map Manipulator")
        {
        }

        public class SpriteDrawn
        {
            public SpriteDrawn(Vector2 beginPos, Vector2 endPos, string name)
            {
                this.beginPos = beginPos;
                this.endPos = endPos;
                this.name = name;
            }

            public Vector2 beginPos = new Vector2();
            public Vector2 endPos = new Vector2();
            public string name = "Untitled";
        }

        public List<SpriteDrawn> spritesDrawn = new List<SpriteDrawn>();
        private bool drawingSprite = false;

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //draw over the sprite map
            //draw a pixel on the sprite map following where mouse is
            //draw a line from the current point to the mouse and a dot
            Raylib.DrawCircle(Raylib.GetMouseX(), Raylib.GetMouseY(), 5, Color.Red);
            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && drawingSprite == false)
            {
                //draw a pixel on the sprite map following where mouse is
                //draw a line from the current point to the mouse and a dot
                drawingSprite = true;
                spritesDrawn.Add(new SpriteDrawn(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), "Untitled"));
            }

            if (spritesDrawn[spritesDrawn.Count].name == "Untitled" && drawingSprite)
            {
                if (Raylib.IsMouseButtonPressed(MouseButton.Right))
                {
                    drawingSprite = false;
                }
            }

            if (drawingSprite)
            {
                spritesDrawn[spritesDrawn.Count].endPos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());
            }

            //draw all "sprites"
            for (int i = 0; i < spritesDrawn.Count; i++)
            {
                Raylib.DrawRectangleLines((int)spritesDrawn[i].beginPos.X, (int)spritesDrawn[i].beginPos.Y, (int)spritesDrawn[i].endPos.X - (int)spritesDrawn[i].beginPos.X, (int)spritesDrawn[i].endPos.Y - (int)spritesDrawn[i].beginPos.Y, Color.Black);
                Raylib.DrawText(spritesDrawn[i].name, (int)spritesDrawn[i].beginPos.X, (int)spritesDrawn[i].beginPos.Y, 20, Color.Black);
            }

            //move the map around and the sprites
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                cam.Offset.X -= 5;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                cam.Offset.X += 5;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                cam.Offset.Y -= 5;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                cam.Offset.Y += 5;
            }
        }
    }
}