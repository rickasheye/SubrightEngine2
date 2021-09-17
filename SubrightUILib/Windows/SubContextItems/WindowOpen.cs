using System;
using SCPBreakdown.EngineStuff;

namespace SubrightEngine2.UI.Windows.SubContextItems
{
    [Serializable]
    public class WindowOpen : ContextItem
    {
        public Window wind;

        public WindowOpen(Window wind, string name) : base(name)
        {
            this.wind = wind;
        }

        public override void Execute()
        {
            base.Execute();
            if (wind.hideRender)
            {
                wind.ownHide = false;
                wind.hideRender = false;
                Debug.Log("Set window " + wind.name + " to render");
            }
        }
    }
}