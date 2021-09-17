using System;

namespace SCPBreakdown.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class ExitCommand : InterpreterCommand
    {
        public ExitCommand() : base("Exit engine", "exit")
        {
        }

        public override void ExecuteCommand()
        {
            base.ExecuteCommand();
            Environment.Exit(0);
        }
    }
}