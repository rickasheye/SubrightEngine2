using System;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents;
using Vector3 = SubrightEngine2.EngineStuff.Vector3;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class UIElement : Component
    {
        public string name;

        public UIElement(string name):base(name)
        {
            this.connectedObject.position = connectedObject.position;
            this.connectedObject.size = connectedObject.size;
            this.name = name;
        }

        public static UIElement CreateElement(UIElement element, GameObject parentObject)
        {
            GameObject newHolder = new GameObject(new Vector3(element.connectedObject.position.X, element.connectedObject.position.Y, 0), new Vector3(element.connectedObject.size.X, element.connectedObject.size.Y, 0), element.name);
            newHolder.AddComponent(element);
            return element;
        }
    }
}