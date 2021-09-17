using System;

namespace SCPBreakdown.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class HelpCommand : InterpreterCommand
    {
        public HelpCommand() : base("Help Command", "help")
        {
        }

        public override void ExecuteCommand()
        {
            base.ExecuteCommand();
            //execute the command
            for (var i = 0; i < Program.interpret.commands.Count; i++)
            {
                var command = Program.interpret.commands[i];
                Debug.Log("" + command.commandName);
            }
        }
    }
}