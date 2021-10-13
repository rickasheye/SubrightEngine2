using System;

namespace SubrightEngine2.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class RemoveVariableCommand : InterpreterCommand
    {
        public RemoveVariableCommand() : base("Removes variable on given!", "rvar")
        {
        }

        public override void ExecuteCommand(string[] args)
        {
            base.ExecuteCommand(args);
            if (args != null && args.Length >= 0 && !args[0].Contains(""))
                DeleteVariable(args[0]);
            else
                Debug.Log("Unfortunately you are missing a VARIABLE NAME");
        }
    }
}