using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents;
using System;
using Color = SubrightEngine2.EngineStuff.Color;
using Vector3 = SubrightEngine2.EngineStuff.Vector3;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class UIElement : Component
    {
        public new string name = "Untitled UI Element";
        public Vector2 position = Vector2.zero;
        public Vector2 size = Vector2.zero;

        public UIElement(string name) : base(name)
        {
        }

        public UIElement(string name, Vector2 position, Vector2 size) : base(name)
        {
            this.position = position;
            this.size = size;
        }

        public static UIElement CreateElement(UIElement element, GameObject parentObject)
        {
            GameObject newHolder = new GameObject(new Vector3(element.connectedObject.position.X, element.connectedObject.position.Y, 0), new Vector3(element.connectedObject.size.X, element.connectedObject.size.Y, 0), element.name);
            newHolder.AddComponent(element);
            return element;
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (Program.debug)
            {
                Raylib.DrawRectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, Color.Red.ToRaylibColor);
                Raylib.DrawText("GameObject Position: " + connectedObject.position.X + ", " + connectedObject.position.Y + " UIElement Position: " + position.X + ", " + position.Y + " UIElemnt Size: " + size.X + ", " + size.Y, (int)position.X, (int)position.Y, 8, Color.Black.ToRaylibColor);
                Raylib.DrawRectangleLines((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, Color.GREEN.ToRaylibColor);
                Raylib.DrawText("Name: " + name, (int)position.X, (int)position.Y + 10, 8, Color.Black.ToRaylibColor);
            }
        }
    }
}