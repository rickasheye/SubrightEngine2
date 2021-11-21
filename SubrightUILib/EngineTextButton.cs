using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace SubrightEngine2.UI
{
    [System.Serializable]
    public class EngineTextButton : EngineButton
    {
        //A button with text.
        public string textContent;

        public EngineTextButton(string name, string textContent) : base(name)
        {
            this.textContent = textContent;
        }

        /// <summary>
        /// Used to draw the text onto the screen but alongside a button.
        /// </summary>
        /// <param name="cam"></param>
        public override void OffhandDraw(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            DrawText(textContent, connectedObject.position.X - (textContent.Length * 10) - 8, connectedObject.position.Y, 10, Program.textColor);
            //Debug.Log("lol");
        }
    }
}