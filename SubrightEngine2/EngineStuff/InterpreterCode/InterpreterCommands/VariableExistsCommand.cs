using System;

namespace SubrightEngine2.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class VariableExistsCommand : InterpreterCommand
    {
        public VariableExistsCommand() : base("Checking if the variable actually exists", "evar")
        {
        }

        public override void ExecuteCommand(string[] args)
        {
            base.ExecuteCommand(args);
            if (args != null && args.Length >= 0 && !args[0].Contains(""))
                Debug.Log("does " + args[0] + " exist? = " + checkVariableExists(args[0]));
            else
                Debug.Log("Unfortunately you are missing a VARIABLE NAME");
        }
    }
}