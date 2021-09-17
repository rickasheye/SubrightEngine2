namespace SCPBreakdown.EngineStuff.InterpreterCode
{
    public class InterpreterCommand
    {
        public string commandName = "untitled";
        public string offsetName = "Untitled";

        public InterpreterCommand(string offsetName, string commandName)
        {
            this.offsetName = offsetName;
            this.commandName = commandName;
        }

        public virtual void ExecuteCommand(string[] args)
        {
        }

        public virtual void ExecuteCommand(ref object arg1, ref object arg2, ref object arg3, ref object arg4,
            ref object arg5, ref object arg6)
        {
        }

        public virtual void ExecuteCommand(ref object arg1, ref object arg2, ref object arg3, ref object arg4,
            ref object arg5)
        {
        }

        public virtual void ExecuteCommand(ref object arg1, ref object arg2, ref object arg3, ref object arg4)
        {
        }

        public virtual void ExecuteCommand(ref object arg1, ref object arg2, ref object arg3)
        {
        }

        public virtual void ExecuteCommand(ref object arg1, ref object arg2)
        {
        }

        public virtual void ExecuteCommand(ref object arg1)
        {
        }

        public virtual void ExecuteCommand()
        {
        }

        public virtual void LoadCommand()
        {
        }

        public void StoreVariable(string varName, object varContent)
        {
            if (!checkVariableExists(varName))
            {
                Program.interpret.storedVariables.Add(varName, varContent);
                Debug.Log("Variable has sucessfully been added under the name of: " + varName);
            }
            else
            {
                Debug.Log(
                    "Unfortunately that variable does exist! so please delete it before creating a new one of the same name!");
            }
        }

        public void DeleteVariable(string varName)
        {
            if (checkVariableExists(varName))
            {
                //variable exists so remove
                Program.interpret.storedVariables.Remove(varName);
                Debug.Log("Variable has sucessfully been removed from game");
            }
            else
            {
                Debug.Log("There is no variable that exists by that name!");
            }
        }

        public bool checkVariableExists(string varName)
        {
            foreach (var m in Program.interpret.storedVariables.Keys)
                if (varName == m)
                    return true;
            //break;
            return false;
        }
    }
}