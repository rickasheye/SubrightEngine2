using System;
using Raylib_cs;

namespace SubrightEngine2.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class SetTitle : InterpreterCommand
    {
        public SetTitle():base("Sets the title of the editor", "settitle"){}

        public override void ExecuteCommand(string[] args)
        {
            base.ExecuteCommand(args);
            if (args.Length >= 0 && args[0] != null)
            {
                Raylib.SetWindowTitle(args[0]);
            }
        }
    }
}