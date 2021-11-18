using Raylib_cs;

namespace SubrightEngine2.UI
{
    public class EngineTextButton : EngineTextBox
    {
        //A button with text.
        public string textContent;

        public EngineTextButton(string name, string textContent) : base(name)
        {
            this.textContent = textContent;
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        /// <summary>
        /// Used to draw the text onto the screen but alongside a button.
        /// </summary>
        /// <param name="cam"></param>
        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            DrawText(textContent, connectedObject.position.X - (textContent.Length * 10) - 8, connectedObject.position.Y, 10, Program.textColor);
        }
    }
}