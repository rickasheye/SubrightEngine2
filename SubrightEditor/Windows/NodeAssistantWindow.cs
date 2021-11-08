using Raylib_cs;
using SubrightEditor.Nodes;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightEditor.Windows
{
    public class NodeAssistantWindow : Window
    {
        public Node nodeModule;

        public NodeAssistantWindow(Vector3 position, Vector3 size):base(position, size, "Node Assistant")
        {
            nodeModule = new Node();
            nodeModule.objects.Add(new StringNode("string"));
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            for(int i = 0; i < nodeModule.objects.Count; i++)
            {
                NodeObject objectModules = nodeModule.objects[i];
                objectModules.Draw2D(ref cam);
            }
        }
    }
}
