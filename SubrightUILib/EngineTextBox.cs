using System;
using Raylib_cs;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class EngineTextBox : UIElement
    {
        public string text = "Pog";

        public EngineTextBox(string name) : base(name)
        {
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //Draw a text box somewhere
            DrawRectangleLines(connectedObject.position.X, connectedObject.position.Y, connectedObject.size.X, connectedObject.size.Y, Program.foregroundColor);
            Raylib.DrawLine((int) connectedObject.position.X + text.Length, (int) connectedObject.position.Y, (int) connectedObject.position.X + text.Length,
                (int) connectedObject.position.Y + (int) connectedObject.size.Y, Raylib_cs.Color.BLACK);
            var key = Raylib.GetKeyPressed();
            switch (key)
            {
                case 0:
                    break;
                case 81:
                    text += "Q";
                    break;
                case 87:
                    text += "W";
                    break;
                case 69:
                    text += "E";
                    break;
                case 82:
                    text += "R";
                    break;
                case 84:
                    text += "T";
                    break;
                case 89:
                    text += "Y";
                    break;
                case 85:
                    text += "U";
                    break;
                case 73:
                    text += "I";
                    break;
                case 79:
                    text += "O";
                    break;
                case 80:
                    text += "P";
                    break;
                case 65:
                    text += "A";
                    break;
                case 83:
                    text += "S";
                    break;
                case 68:
                    text += "D";
                    break;
                case 70:
                    text += "F";
                    break;
                case 71:
                    text += "G";
                    break;
                case 72:
                    text += "H";
                    break;
                case 74:
                    text += "J";
                    break;
                case 75:
                    text += "K";
                    break;
                case 76:
                    text += "L";
                    break;
                case 90:
                    text += "Z";
                    break;
                case 88:
                    text += "X";
                    break;
                case 67:
                    text += "C";
                    break;
                case 86:
                    text += "V";
                    break;
                case 66:
                    text += "B";
                    break;
                case 78:
                    text += "N";
                    break;
                case 77:
                    text += "M";
                    break;
                case 32:
                    text += " ";
                    break;
                case 259:
                    if (text.Length > 0) text = text.Remove(text.Length - 1);
                    break;
            }

            DrawText(text, connectedObject.position.X, connectedObject.position.Y, connectedObject.size.Y, Program.textColor);
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }
    }
}