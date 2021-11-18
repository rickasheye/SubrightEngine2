using System;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class ContextItem
    {
        //Context item to give to the menu to render.
        public string name;

        public ContextItem(string name)
        {
            this.name = name;
        }

        public virtual void Execute()
        {
        }
    }
}