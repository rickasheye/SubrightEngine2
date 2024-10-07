using SubrightEngine2.Editor.Prompts;
using SubrightEngine2.EngineStuff.Scenes;

#if DEBUG
namespace SubrightEngine2.Editor.Tools
{
    public class SelectorWindow : Scene
    {
        //subclass to define the window for easier use using the selector.
        public SelectorWindow()
        {
            name = "Selector Window";
        }

        public void AddPrompt(Prompt prompt)
        {
            if (!GameObjects.Contains(prompt))
            {
                AddGameObject(prompt);
                prompt.Start();
                SubrightEngine2.EngineStuff.Debug.Log(name + " added prompt: " + prompt.name);
            }
            else
            {
                SubrightEngine2.EngineStuff.Debug.Log(name + " could not add prompt: " + prompt.name);
            }
        }

        public void RemovePrompt(Prompt prompt)
        {
            if (GameObjects.Contains(prompt))
            {
                RemoveGameObject(prompt);
                SubrightEngine2.EngineStuff.Debug.Log(name + " removed prompt: " + prompt.name);
            }
            else
            {
                SubrightEngine2.EngineStuff.Debug.Log(name + " could not remove prompt: " + prompt.name);
            }
        }
    }
}
#endif