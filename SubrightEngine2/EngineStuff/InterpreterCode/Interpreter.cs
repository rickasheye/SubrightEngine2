using System;
using System.Collections.Generic;
using System.IO;

namespace SubrightEngine2.EngineStuff.InterpreterCode
{
    public class Interpreter
    {
        public List<InterpreterCommand> commands = new List<InterpreterCommand>();

        public Dictionary<string, List<InterpreterCommand>> LoadedCommandFiles =
            new Dictionary<string, List<InterpreterCommand>>();

        public Dictionary<string, object> storedVariables = new Dictionary<string, object>();

        //find all commands and add them to commands list by grabbing InterpreterCommand classes
        public void AddFind()
        {
            //find all classes that extend on InterpreterCommand
            //add them to the commands list
            foreach (var type in typeof(InterpreterCommand).Assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(InterpreterCommand)))
                {
                    //add to list
                    InterpreterCommand tower = (InterpreterCommand)Activator.CreateInstance(type);
                    commands.Add(tower);
                }
            }
        }

        public Interpreter()
        {
            AddFind();
            //starting point load the commands through a seperate path
            LoadedCommandFiles = LoadCommands();
            //load the pre-determined files before this too
        }

        public void Execute(string executePath, string args)
        {
            if (LoadedCommandFiles.Count > 0)
                for (var i = 0; i < LoadedCommandFiles[executePath].Count; i++)
                {
                    var cmd = LoadedCommandFiles[executePath][i];
                    if (cmd != null)
                    {
                        var arguments = args.Replace(cmd.commandName + " ", "").Split(" ");
                        if (arguments == null && arguments.Length < 0)
                        {
                            cmd.ExecuteCommand();
                        }
                        else
                        {
                            //change to a different variable if possible
                            for (var m = 0; m < arguments.Length; m++)
                            {
                                var arg = arguments[i];
                                if (!arg.Contains(""))
                                {
                                    if (!isNumber(arg))
                                    {
                                        //try to parse as a variable if not
                                        if (isVariable(arg))
                                        {
                                            if (returnVariable(arg) != null)
                                            {
                                                arguments[i] = returnVariable(arg).ToString();
                                            }
                                            else
                                            {
                                                //the variable at line
                                                if (Program.debug)
                                                    Debug.Log("At line " + i +
                                                              " there seems to be no object supported by the variable?");
                                            }
                                        }
                                        else
                                        {
                                            if (Program.debug)
                                                Debug.Log("At line " + i +
                                                          " there seems to be a variable but it doesnt exist in the registry?");
                                        }
                                    }
                                    else
                                    {
                                        if (Program.debug)
                                            Debug.Log(
                                                "This given string is a number but was intentionally scanned for as a variable (ignoring) at line " +
                                                i);
                                    }
                                }
                            }

                            cmd.ExecuteCommand(arguments);
                        }
                    }
                    else
                    {
                        Debug.Log("Invalid command detected");
                    }
                }
        }

        public Vector2 convertTextVector2(string convert)
        {
            var vector = new Vector2(0, 0);
            if (isVector2(convert))
            {
                var splitString = convert.Split("/");
                if (splitString.Length <= 1)
                {
                    vector.X = int.Parse(splitString[0]);
                    vector.Y = int.Parse(splitString[1]);
                }
            }

            return vector;
        }

        public Vector3 convertTextVector3(string convert)
        {
            var vector = new Vector3(0, 0, 0);
            if (isVector3(convert))
            {
                var splitString = convert.Split("/");
                if (splitString.Length <= 2)
                {
                    vector.X = int.Parse(splitString[0]);
                    vector.Y = int.Parse(splitString[1]);
                    vector.Z = int.Parse(splitString[2]);
                }
            }

            return vector;
        }

        public bool isNumber(string number)
        {
            return int.TryParse(number, out var name);
        }

        public bool isVariable(string name)
        {
            if (storedVariables[name] != null) return true;
            return false;
        }

        public object returnVariable(string name)
        {
            if (isVariable(name))
                return storedVariables[name];
            return null;
        }

        public bool isVector3(string convertm)
        {
            var splitString = convertm.Split("/");
            if (splitString.Length <= 2)
            {
                var number1 = 0;
                var number = int.TryParse(splitString[0], out number1);
                if (number)
                {
                    var number2 = 0;
                    var number3 = int.TryParse(splitString[1], out number2);
                    if (number3)
                    {
                        var number4 = 0;
                        var number5 = int.TryParse(splitString[2], out number4);
                        if (number5) return true;
                    }
                }
            }

            return false;
        }

        public bool isVector2(string convertm)
        {
            var splitString = convertm.Split("/");
            if (splitString.Length <= 1)
            {
                var number1 = 0;
                var number = int.TryParse(splitString[0], out number1);
                if (number)
                {
                    var number2 = 0;
                    var number3 = int.TryParse(splitString[1], out number2);
                    if (number3) return true;
                }
            }

            return false;
        }

        public Dictionary<string, List<InterpreterCommand>> LoadCommands()
        {
            //load the interpreter commands from file
            var commandList = new Dictionary<string, List<InterpreterCommand>>();
            if (SubrightEngine2.Program.saveFile)
            {
                var codeDir = Path.Combine(Environment.CurrentDirectory, "code/");
                if (Directory.Exists(codeDir))
                {
                    //search for every code file in this directory
                    var directoryFiles = Directory.GetFiles(codeDir, "*.se", SearchOption.AllDirectories);
                    foreach (var m in directoryFiles)
                    {
                        //index everyfile
                        var fileCode = File.ReadAllLines(m);
                        var cmds = new List<InterpreterCommand>();
                        for (var i = 0; i < fileCode.Length; i++)
                        {
                            var mCode = fileCode[i];
                            InterpreterCommand command = null;
                            for (var p = 0; p < commands.Count; p++)
                            {
                                var cmd = commands[p];
                                if (cmd.commandName == mCode)
                                {
                                    command = cmd;
                                    break;
                                }
                            }

                            if (command != null) cmds.Add(command);
                        }

                        if (cmds != null)
                            //add the code list.
                            commandList.Add(m, cmds);
                    }
                }
                else
                {
                    Directory.CreateDirectory(codeDir);
                    LoadCommands();
                }
            }
            else
            {
                //unable to save save files is disabled
                Debug.Log("Unable to load/create InterpreterCommand files as save files are disabled!");
            }

            return commandList;
        }

        public void Save()
        {
            //?????
        }
    }
}