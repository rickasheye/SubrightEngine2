using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General.ConsoleExclusive
{
    public class ClearConsole : EmptyCommand
    {
        public ClearConsole():base("Clear console information", "clear/clearconsole/cc/clearboard", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Console.Clear();
        }
    }
}
