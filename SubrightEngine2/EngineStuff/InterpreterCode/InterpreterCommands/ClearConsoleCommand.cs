using System;

namespace SubrightEngine2.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class ClearConsoleCommand : InterpreterCommand
    {
        public ClearConsoleCommand() : base("Clears the console", "cls")
        {
        }

        public override void ExecuteCommand()
        {
            base.ExecuteCommand();
            Console.Clear();
        }
    }
}