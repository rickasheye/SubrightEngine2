using SubrightEditor.Nodes.Variables;
using SubrightEngine2.EngineStuff.BaseComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightEditor.Nodes
{
    public class Node : Component
    {
        public Node() : base("NodeContainer")
        {

        }

        public List<NodeObject> objects = new List<NodeObject>();
    }
}
