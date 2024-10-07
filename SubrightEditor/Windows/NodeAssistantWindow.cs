using Raylib_cs;
using SubrightEditor.Nodes;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;

namespace SubrightEngineEditor.Windows
{
    public class NodeAssistantWindow : Window
    {
        public Node nodeModule;

        public NodeAssistantWindow(Vector3 position, Vector3 size) : base(position, size, "Node Assistant")
        {
            nodeModule = new Node();
            nodeModule.objects.Add(new StringNode("string"));
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            for (int i = 0; i < nodeModule.objects.Count; i++)
            {
                NodeObject objectModules = nodeModule.objects[i];
                objectModules.Draw2D(ref cam);
            }
        }
    }
}