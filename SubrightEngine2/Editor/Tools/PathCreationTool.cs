using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using Color = Raylib_cs.Color;

#if DEBUG
namespace SubrightEngine2.Editor.Tools
{
    public class PathCreationTool : SelectorWindow
    {
        public List<Vector2> points = new List<Vector2>();

        public PathCreationTool()
        {
            name = "Path Creation Tool";
        }

        public override void LoadScene()
        {
            base.LoadScene();
            //name = "Path Creation Tool";
        }

        public override void UpdateScene(ref Camera2D cam)
        {
            base.UpdateScene(ref cam);
            Raylib.DrawText(name, 10, 10, 20, Color.Black);

            //draw the points
            for (int i = 0; i < points.Count; i++)
            {
                Raylib.DrawCircle((int)points[i].X, (int)points[i].Y, 5, Color.Black);
                if (i + 1 < points.Count)
                {
                    Raylib.DrawLineEx(new System.Numerics.Vector2(points[i].X, points[i].Y), new System.Numerics.Vector2(points[i + 1].X, points[i + 1].Y), 2, Color.Black);
                }
            }

            //draw a line from the current point to the mouse and a dot
            if (points.Count > 0)
            {
                Raylib.DrawLineEx(new System.Numerics.Vector2(points[points.Count - 1].X, points[points.Count - 1].Y), new System.Numerics.Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()), 2, Color.Red);
                Raylib.DrawCircle(Raylib.GetMouseX(), Raylib.GetMouseY(), 5, Color.Red);
            }
            else { Raylib.DrawCircle(Raylib.GetMouseX(), Raylib.GetMouseY(), 5, Color.Red); }

            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                points.Add(new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()));
            }
            else if (Raylib.IsMouseButtonPressed(MouseButton.Right))
            {
                if (points.Count > 0)
                {
                    points.RemoveAt(points.Count - 1);
                }
            }

            //save the points to a file. potentially a file explorer in the future?
            if (Raylib.IsKeyPressed(KeyboardKey.S))
            {
                string path = "Assets/Paths/";
                string name = "Path" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".path";
                string fullPath = path + name;
                System.IO.Directory.CreateDirectory(path);
                System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath);
                file.WriteLine(points.Count);
                for (int i = 0; i < points.Count; i++)
                {
                    file.WriteLine(points[i].X + "," + points[i].Y);
                }
                file.Close();
                Console.WriteLine("Path saved to " + fullPath);
            }
        }
    }
}
#endif