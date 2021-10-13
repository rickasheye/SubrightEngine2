using System;

namespace SubrightEngine2.EngineStuff.InterpreterCode.InterpreterCommands
{
    [Serializable]
    public class CreateGameObjectCommand : InterpreterCommand
    {
        public CreateGameObjectCommand() : base("Create a gameobject!", "cgobject")
        {
        }

        public override void ExecuteCommand(string[] args)
        {
            base.ExecuteCommand(args);
            var position = Vector3.zero;
            var size = Vector3.zero;
            var name = "";
            if (args != null && args.Length >= 0 && !args[0].Contains(""))
            {
                name = args[0];
                if (args.Length >= 1 && !args[1].Contains(""))
                {
                    //strip variable from this
                    var grabbedVar = args[1];
                    var splitVar1 = grabbedVar.Split('/');

                    //check if there are integers in this of course
                    if (splitVar1.Length > 2)
                    {
                        //bad
                        Debug.Log("BAD VARIABLE GIVEN FOR POSITION IN A VECTOR3");
                        return;
                    }

                    var varX = int.Parse(splitVar1[0]);
                    var varY = int.Parse(splitVar1[1]);
                    var varZ = int.Parse(splitVar1[2]);
                    position = new Vector3(varX, varY, varZ);
                    if (Program.debug) Debug.Log("Sucessfully added position variable!!!");
                    //create a gameObject from this i guess
                    if (args.Length >= 2 && !args[2].Contains(""))
                    {
                        //create the size variable!
                    }
                }
            }
            else
            {
                Debug.Log("Unfortunately you are missing a GAMEOBJECT NAME");
            }
        }

        public bool gameObjectExists(string name)
        {
            for (var i = 0; i < Program.objects.Count; i++)
            {
                var create = Program.objects[i];
                if (create.name == name) return true;
            }

            return false;
        }
    }
}