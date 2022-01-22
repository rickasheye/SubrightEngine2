using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI.Windows;
using SubrightEngineEditor.Windows;

namespace SubrightEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            SubrightEngine2.Program.SetExtension(new EditorReference(), false);
            SubrightEngine2.Program.Initialise(args, false, false);
        }
    }
}
