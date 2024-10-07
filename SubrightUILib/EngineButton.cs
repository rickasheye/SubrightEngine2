using Raylib_cs;
using System;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class EngineButton : UIElement
    {
        //A regular button.
        public EngineButton(string name) : base(name)
        {
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //Draw the button
            DrawRectangle(connectedObject.position.X, connectedObject.position.Y, connectedObject.size.X, connectedObject.size.Y, Color.GRAY);
            if (Raylib.GetMouseX() > connectedObject.position.X && Raylib.GetMouseX() < connectedObject.position.X + connectedObject.size.X &&
                Raylib.GetMouseY() > connectedObject.position.Y && Raylib.GetMouseY() < connectedObject.position.Y + connectedObject.size.Y)
            {
                //trigger the event or hover first
                DrawRectangleLines(connectedObject.position.X, connectedObject.position.Y, connectedObject.size.X, connectedObject.size.Y, Color.LIGHTGRAY);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    //when clicked
                    DrawRectangle(connectedObject.position.X, connectedObject.position.Y, connectedObject.size.X, connectedObject.size.Y, Color.DARKGRAY);
                    //execute event
                    ExecuteEvent();
                }
            }

            DrawText(name, connectedObject.position.X, (connectedObject.position.Y + connectedObject.size.X) / 2, 8, Color.White);
            OffhandDraw(ref cam);
        }

        public virtual void OffhandDraw(ref Camera2D cam2)
        {
        }

        public virtual void ExecuteEvent()
        {
        }
    }
}