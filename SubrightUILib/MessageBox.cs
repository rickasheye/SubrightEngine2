using System;
using Raylib_cs;
using SCPBreakdown.EngineStuff;
using Color = SCPBreakdown.EngineStuff.Color;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class MessageBox : Dialog
    {
        private static bool boxActive = false;

        public string text = "Untitled MessageBox";
        
        public MessageBox(string message) : base(new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
            new Vector2(Raylib.GetScreenWidth() / 4, Raylib.GetScreenHeight() / 4), "Dialog Box")
        {
            text = message;
            //CreateMessageBox(message);
            position = new Vector3(Raylib.GetScreenWidth() / 2 - (size.X/2), Raylib.GetScreenHeight() / 2 - (size.Y/2), 0);
        }

        public static void CreateMessageBox(string message)
        {
            MessageBox box = new MessageBox(message);
            RallyDialog.dialogs.Add(box);
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (!hideRender)
            {
                DrawText(text,
                    new Vector2(position.X + (size.X / 2) - ((text.Length * 5.25f) / 4), position.Y + (size.Y / 2.25f)), 8,
                    Color.WHITE);
                DrawText("X", new Vector2(position.X + size.X - 10, position.Y), 10, Color.RED);
                if (Raylib.GetMouseX() > position.X + size.X - 10 && Raylib.GetMouseX() < position.X + size.X &&
                    Raylib.GetMouseY() > position.Y && Raylib.GetMouseY() < position.Y + 10)
                {
                    if (SCPBreakdown.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        hideRender = true;
                        SCPBreakdown.Program.objects.Remove(this);
                    }
                }
            }
        }
    }
}