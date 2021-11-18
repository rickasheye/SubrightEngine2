using Raylib_cs;
using SubrightEngine2.EngineStuff;
using System;
using Color = SubrightEngine2.EngineStuff.Color;

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
            position = new Vector3(Raylib.GetScreenWidth() / 2 - (size.X / 2), Raylib.GetScreenHeight() / 2 - (size.Y / 2), 0);
        }

        /// <summary>
        /// On the fly create method to create and display messagebox.
        /// </summary>
        /// <param name="message">What message do you want to show on the messagebox.</param>
        public static void CreateMessageBox(string message)
        {
            MessageBox box = new MessageBox(message);
            RallyDialog.dialogs.Add(box);
        }

        /// <summary>
        /// Update method used to draw the graphics, in this case the message boxes.
        /// </summary>
        /// <param name="cam2"></param>
        /// <param name="cam3"></param>
        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        /// <summary>
        /// The draw method used to actually draw those graphics, to display messageboxes.
        /// </summary>
        /// <param name="cam"></param>
        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (!hideRender)
            {
                DrawText(text,
                    new Vector2(position.X + (size.X / 2) - ((text.Length * 5.25f) / 4), position.Y + (size.Y / 2.25f)), 8,
                    Program.textColor);
                DrawText("X", new Vector2(position.X + size.X - 10, position.Y), 10, Color.RED);
                if (Raylib.GetMouseX() > position.X + size.X - 10 && Raylib.GetMouseX() < position.X + size.X &&
                    Raylib.GetMouseY() > position.Y && Raylib.GetMouseY() < position.Y + 10)
                {
                    if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                    {
                        hideRender = true;
                        SubrightEngine2.Program.loader.currentScene.GameObjects.Remove(this);
                    }
                }
            }
        }
    }
}