using System;

namespace SubrightTilemapCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            SubrightEngine2.Program.SetExtension(new ExtensionStart(), false);
            SubrightEngine2.Program.Initialise(args, false, true);
        }
    }
}
