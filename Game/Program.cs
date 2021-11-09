using Newtonsoft.Json;
using Raylib_cs;
using RPGConsole.Commands;
using RPGConsole.Graphical;
using RPGConsole.Graphical.ScenesAvaliable;
using RPGConsole.InventoryBlock;
using RPGConsole.InventoryItems;
using RPGConsole.Profile;
using RPGConsole.Saving;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            SubrightEngine2.Program.SetExtension(new Reference(), false);
            SubrightEngine2.Program.Initialise(args, false, true);
        }
    }
}