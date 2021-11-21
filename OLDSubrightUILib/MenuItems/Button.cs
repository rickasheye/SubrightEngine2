using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole.Graphical.MenuItems
{
    public class Button : GUIOption
    {
        public string text;
        public int textSize;
        public Color backColor;
        public Color backColorPressed;
        public Color foregroundColor;

        public Button(string text, Vector2 position, Vector2 size, int textSize, Color backColor, Color backColorPressed, Color foregroundColor):base(size, position) {
            this.text = text;
            this.textSize = textSize;
            this.backColor = backColor;
            this.backColorPressed = backColorPressed;
            this.foregroundColor = foregroundColor;
        }

        public override void DrawObject()
        {
            base.DrawObject();
            int posX = (int)position.X;
            int posY = (int)position.Y;
            int sizeX = (int)size.X;
            int sizeY = (int)size.Y;
            if (focused)
            {
                Raylib.DrawRectangle(posX, posY, sizeX, sizeY, backColorPressed.ToRaylibColor);
                if (!Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON) || !Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                    MouseOverObject();
                }
                else
                {
                    TriggerObject();
                }
            }
            else
            {
                Raylib.DrawRectangle(posX, posY, sizeX, sizeY, backColor.ToRaylibColor);
            }
            Raylib.DrawText(text, posX, posY, sizeX - sizeY, foregroundColor.ToRaylibColor);
            Raylib.DrawRectangleLines(posX, posY, sizeX, sizeY, foregroundColor.ToRaylibColor);
        }

        public virtual void MouseOverObject()
        {

        }

        public virtual void TriggerObject()
        {

        }
    }
}
