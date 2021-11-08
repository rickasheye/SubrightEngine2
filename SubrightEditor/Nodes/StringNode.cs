using SubrightEditor.Nodes.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightEditor.Nodes
{
    public class IntegerNode : NodeObject
    {
        Integer objectContained;

        public IntegerNode(string name) : base(name)
        {
            if(name == string.Empty)
            {
                name = "Untitled Integer";
            }
        }
    }
}
