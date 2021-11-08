using SubrightEditor.Nodes.Variables;
using System;
using System.Collections.Generic;
using System.Text;
using String = SubrightEditor.Nodes.Variables.String;

namespace SubrightEditor.Nodes
{
    public class StringNode : NodeObject
    {
        String objectContained;

        public StringNode(string name) : base(name)
        {
            if(name == string.Empty)
            {
                name = "Untitled String";
            }
        }
    }
}
