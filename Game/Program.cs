using System;
using SubrightEngine2.EngineStuff;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            SubrightEngine2.Program.SetExtension(new Reference(), false);
            SubrightEngine2.Program.Initialise(args, false, true);
        }
    }
}