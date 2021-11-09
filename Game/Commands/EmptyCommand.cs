using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands
{
    public class EmptyCommand
    {
        //command
        public string commandName;
        public string syntax;
        public CommandType typeCommand;

        public EmptyCommand(string commandName, string syntax, CommandType type)
        {
            this.commandName = commandName;
            this.syntax = syntax;
            typeCommand = type;
        }
        
        public virtual void RunCommand(string[] args)
        {

        }
    }
}
