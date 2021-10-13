using System;
using Raylib_cs;

namespace SubrightEngine2.EngineStuff
{
    [Serializable]
    public enum Renderer
    {
        RAYLIB
    }

    [Serializable]
    public class Drawable
    {
        //this class serves as the renderer for all things 2d at the moment!
        public void DrawRectangle(Vector2 position, Vector2 size, Color color, Renderer render)
        {
            //color
            switch (render)
            {
                case Renderer.RAYLIB:
                default:
                    //render with raylib?
                    Raylib.DrawRectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y,
                        new Raylib_cs.Color(color.R, color.G, color.B, 255));
                    break;
            }
        }

        //render with vectors without choosing renderer
        public void DrawRectangle(Vector2 position, Vector2 size, Color color)
        {
            DrawRectangle(position, size, color, Renderer.RAYLIB);
        }

        //render with integers with choosing an renderer
        public void DrawRectangle(int X, int Y, int Width, int Height, Color color, Renderer render)
        {
            DrawRectangle(new Vector2(X, Y), new Vector2(Width, Height), color, render);
        }

        //render with integers without choosing a renderer
        public void DrawRectangle(int X, int Y, int Width, int Height, Color color)
        {
            DrawRectangle(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB);
        }

        //render with floats with a renderer
        public void DrawRectangle(float X, float Y, float Width, float Height, Color color, Renderer render)
        {
            DrawRectangle(new Vector2(X, Y), new Vector2(Width, Height), color, render);
        }

        //render with floats without choosing a renderer
        public void DrawRectangle(float X, float Y, float Width, float Height, Color color)
        {
            DrawRectangle(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB);
        }

        //render with lines instead of a full rect
        public void DrawRectangleLines(Vector2 position, Vector2 size, Color color, Renderer render)
        {
            //color
            switch (render)
            {
                case Renderer.RAYLIB:
                default:
                    //render with raylib?
                    Raylib.DrawRectangleLines((int) position.X, (int) position.Y, (int) size.X, (int) size.Y,
                        new Raylib_cs.Color(color.R, color.G, color.B, 255));
                    break;
            }
        }

        //render with vectors without choosing renderer
        public void DrawRectangleLines(Vector2 position, Vector2 size, Color color)
        {
            DrawRectangleLines(position, size, color, Renderer.RAYLIB);
        }

        //render with integers with choosing an renderer
        public void DrawRectangleLines(int X, int Y, int Width, int Height, Color color, Renderer render)
        {
            DrawRectangleLines(new Vector2(X, Y), new Vector2(Width, Height), color, render);
        }

        //render with integers without choosing a renderer
        public void DrawRectangleLines(int X, int Y, int Width, int Height, Color color)
        {
            DrawRectangleLines(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB);
        }

        //render with floats with a renderer
        public void DrawRectangleLines(float X, float Y, float Width, float Height, Color color, Renderer render)
        {
            DrawRectangleLines(new Vector2(X, Y), new Vector2(Width, Height), color, render);
        }

        //render with floats without choosing a renderer
        public void DrawRectangleLines(float X, float Y, float Width, float Height, Color color)
        {
            DrawRectangleLines(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB);
        }

        //render a text
        public void DrawText(string text, Vector2 position, int size, Color color, Renderer render)
        {
            //color
            switch (render)
            {
                case Renderer.RAYLIB:
                default:
                    //render with raylib?
                    Raylib.DrawText(text, (int) position.X, (int) position.Y, size,
                        new Raylib_cs.Color(color.R, color.G, color.B, 255));
                    break;
            }
        }

        //render with vectors without choosing renderer
        public void DrawText(string text, Vector2 position, int size, Color color)
        {
            DrawText(text, position, size, color, Renderer.RAYLIB);
        }

        //render with integers with choosing an renderer
        public void DrawText(string text, int X, int Y, int Size, Color color, Renderer render)
        {
            DrawText(text, new Vector2(X, Y), Size, color, render);
        }

        //render with integers without choosing a renderer
        public void DrawText(string text, int X, int Y, int Size, Color color)
        {
            DrawText(text, new Vector2(X, Y), Size, color, Renderer.RAYLIB);
        }

        //render with floats with a renderer
        public void DrawText(string text, float X, float Y, float Size, Color color, Renderer render)
        {
            DrawText(text, new Vector2(X, Y), (int) Size, color, render);
        }

        //render with floats without choosing a renderer
        public void DrawText(string text, float X, float Y, float Size, Color color)
        {
            DrawText(text, new Vector2(X, Y), (int) Size, color, Renderer.RAYLIB);
        }
        
        //this part renders with on a image
        public void DrawRectangleImage(Vector2 position, Vector2 size, Color color, Renderer render, ref Image image)
        {
            //color
            switch (render)
            {
                case Renderer.RAYLIB:
                default:
                    //render with raylib?
                    Raylib.ImageDrawRectangle(ref image, (int) position.X, (int) position.Y, (int) size.X, (int) size.Y,
                        new Raylib_cs.Color(color.R, color.G, color.B, 255));
                    break;
            }
        }

        //render with vectors without choosing renderer
        public void DrawRectangleImage(Vector2 position, Vector2 size, Color color, ref Image image)
        {
            DrawRectangleImage(position, size, color, Renderer.RAYLIB, ref image);
        }

        //render with integers with choosing an renderer
        public void DrawRectangleImage(int X, int Y, int Width, int Height, Color color, Renderer render, ref Image image)
        {
            DrawRectangleImage(new Vector2(X, Y), new Vector2(Width, Height), color, render, ref image);
        }

        //render with integers without choosing a renderer
        public void DrawRectangleImage(int X, int Y, int Width, int Height, Color color, ref Image image)
        {
            DrawRectangleImage(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB, ref image);
        }

        //render with floats with a renderer
        public void DrawRectangleImage(float X, float Y, float Width, float Height, Color color, Renderer render, ref Image image)
        {
            DrawRectangleImage(new Vector2(X, Y), new Vector2(Width, Height), color, render, ref image);
        }

        //render with floats without choosing a renderer
        public void DrawRectangleImage(float X, float Y, float Width, float Height, Color color, ref Image image)
        {
            DrawRectangleImage(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB, ref image);
        }

        //render with lines instead of a full rect
        public void DrawRectangleLinesImage(Vector2 position, Vector2 size, Color color, Renderer render, ref Image image)
        {
            //color
            switch (render)
            {
                case Renderer.RAYLIB:
                default:
                    //render with raylib?
                    Raylib.ImageDrawRectangleLines(ref image, new Raylib_cs.Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y),
                        1, new Raylib_cs.Color(color.R, color.G, color.B, 255));
                    break;
            }
        }

        //render with vectors without choosing renderer
        public void DrawRectangleLinesImage(Vector2 position, Vector2 size, Color color, ref Image image)
        {
            DrawRectangleLinesImage(position, size, color, Renderer.RAYLIB, ref image);
        }

        //render with integers with choosing an renderer
        public void DrawRectangleLinesImage(int X, int Y, int Width, int Height, Color color, Renderer render, ref Image image)
        {
            DrawRectangleLinesImage(new Vector2(X, Y), new Vector2(Width, Height), color, render, ref image);
        }

        //render with integers without choosing a renderer
        public void DrawRectangleLinesImage(int X, int Y, int Width, int Height, Color color, ref Image image)
        {
            DrawRectangleLinesImage(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB, ref image);
        }

        //render with floats with a renderer
        public void DrawRectangleLinesImage(float X, float Y, float Width, float Height, Color color, Renderer render, ref Image image)
        {
            DrawRectangleLinesImage(new Vector2(X, Y), new Vector2(Width, Height), color, render, ref image);
        }

        //render with floats without choosing a renderer
        public void DrawRectangleLinesImage(float X, float Y, float Width, float Height, Color color, ref Image image)
        {
            DrawRectangleLinesImage(new Vector2(X, Y), new Vector2(Width, Height), color, Renderer.RAYLIB, ref image);
        }

        //render a text
        public void DrawTextImage(string text, Vector2 position, int size, Color color, Renderer render, ref Image image)
        {
            //color
            switch (render)
            {
                case Renderer.RAYLIB:
                default:
                    //render with raylib?
                    Raylib.ImageDrawText(ref image, text, (int) position.X, (int) position.Y, size,
                        new Raylib_cs.Color(color.R, color.G, color.B, 255));
                    break;
            }
        }

        //render with vectors without choosing renderer
        public void DrawTextImage(string text, Vector2 position, int size, Color color, ref Image image)
        {
            DrawTextImage(text, position, size, color, Renderer.RAYLIB, ref image);
        }

        //render with integers with choosing an renderer
        public void DrawTextImage(string text, int X, int Y, int Size, Color color, Renderer render, ref Image image)
        {
            DrawTextImage(text, new Vector2(X, Y), Size, color, render, ref image);
        }

        //render with integers without choosing a renderer
        public void DrawTextImage(string text, int X, int Y, int Size, Color color, ref Image image)
        {
            DrawTextImage(text, new Vector2(X, Y), Size, color, Renderer.RAYLIB, ref image);
        }

        //render with floats with a renderer
        public void DrawTextImage(string text, float X, float Y, float Size, Color color, Renderer render, ref Image image)
        {
            DrawTextImage(text, new Vector2(X, Y), (int) Size, color, render, ref image);
        }

        //render with floats without choosing a renderer
        public void DrawTextImage(string text, float X, float Y, float Size, Color color, ref Image image)
        {
            DrawTextImage(text, new Vector2(X, Y), (int) Size, color, Renderer.RAYLIB, ref image);
        }
    }
}