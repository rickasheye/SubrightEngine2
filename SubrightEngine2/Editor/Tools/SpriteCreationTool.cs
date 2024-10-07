using Raylib_cs;
using SubrightEngine2.Editor.Prompts;
using SubrightEngine2.EngineStuff.Input;

#if DEBUG
namespace SubrightEngine2.Editor.Tools
{
    public class SpriteCreationTool : SelectorWindow
    {
        private PromptColourSelector selector = null;

        public SpriteCreationTool()
        {
            name = "Sprite Creation Tool";
        }

        public override void LoadScene()
        {
            base.LoadScene();
            //load the colour selector
            if (selector == null)
            {
                selector = new PromptColourSelector();
                AddPrompt(selector);
            }
            else
            {
                selector.Reset();
                selector.Enable();
            }
            storedR = 0;
            storedG = 0;
            storedB = 0;
        }

        private int storedR = 0;
        private int storedG = 0;
        private int storedB = 0;

        public override void UpdateScene(ref Camera2D cam)
        {
            base.UpdateScene(ref cam);
            Raylib.ClearBackground(Color.White);
            Raylib.DrawText(name, 10, 10, 20, Color.Black);

            if (storedR > 0 && storedG > 0 && storedB > 0)
            {
                if (selector != null || GameObjects.Contains(selector))
                {
                    //delete it
                    selector.Reset();
                    selector.Disable();
                }
                //selected our colour now render it
                Raylib.DrawRectangle(10, 50, 40, 40, new Color(storedR, storedG, storedB, 255));
                //if selected this square bring back the colour selector
                //if the mouse is over this square select it
                if (Raylib.GetMouseX() > 10 && Raylib.GetMouseX() < 50 && Raylib.GetMouseY() > 50 && Raylib.GetMouseY() < 90)
                {
                    if (Input.GetMouseButtonDown(MouseButton.Left))
                    {
                        //reload the scene
                        LoadScene();
                    }
                }
            }
            else
            {
                //prompt is loaded lets listen to it till it has colours then
                if (selector != null)
                {
                    storedR = selector.selColourR;
                    storedG = selector.selColourG;
                    storedB = selector.selColour;
                }
            }
        }
    }
}
#endif