using Raylib_cs;
using System.Collections.Generic;

namespace SubrightEngine2.UI.Tips
{
    public class TipManager
    {
        public static List<Tip> tips = new List<Tip>();

        public TipManager()
        {
            SetupTips();
        }

        public static void SetupTips()
        {
            tips.Add(new Tip("These are windows, click and hold the title bar to drag the window."));
            tips.Add(new Tip("Click this to exit out of the window or 'hide it'"));
            tips.Add(new Tip("Use this to select an option and be certain with it"));
            tips.Add(new Tip("Use C to open and close the console"));
            tips.Add(
                new Tip("Use WASD to move any object selected. and Q, E to move 3D Objects backwards and forwards"));
            tips.Add(new Tip("This is the titlebar use it to manage the windows on the screen"));
            tips.Add(new Tip("You can toggle window rendering with P"));
            tips.Add(new Tip("Here you can load various prefabs from file or use the pre-existing ones."));
        }

        public static void RenderTip(int id)
        {
            if (tips.Count <= 0) SetupTips();
            TipRender.RenderTip(tips[id].nameTip, new SubrightEngine2.EngineStuff.Vector2(Raylib.GetMouseX(), Raylib.GetMouseY()));
        }
    }
}