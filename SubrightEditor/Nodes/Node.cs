using SubrightEngine2.EngineStuff.BaseComponents;
using System.Collections.Generic;

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
