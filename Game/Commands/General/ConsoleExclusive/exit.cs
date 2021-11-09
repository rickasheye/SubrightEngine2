using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class exit : EmptyCommand
    {
        public exit():base("exit this instance...", "exit", CommandType.NORMAL)
        {

        }

        public override void RunCommand(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
