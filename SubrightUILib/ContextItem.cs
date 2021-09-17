using System;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class ContextItem
    {
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