using System;

namespace SubrightEngine2.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class CreateVariableCommand : InterpreterCommand
    {
        
        public CreateVariableCommand() : base("Create a Variable", "cvar")
        {
        }

        public override void ExecuteCommand(string[] args)
        {
            base.ExecuteCommand(args);
            if (args != null && args.Length >= 0 && !args[0].Contains(""))
            {
                if (args.Length >= 1 && !args[1].Contains(""))
                    //create the variable
                    StoreVariable(args[0], args[1]);
                else
                    Debug.Log("Unfortunately you are missing a VARIABLE");
            }
            else
            {
                Debug.Log("Unfortunately you are missing a VARIABLE NAME");
            }
        }
    }
}