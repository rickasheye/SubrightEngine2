using RPGConsole.Commands.General;
using RPGConsole.Commands.General.ConsoleExclusive;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands
{
    public enum CommandType
    {
        DEBUG, NORMAL
    }

    public class CommandRunManager
    {
        //run a command
        public List<EmptyCommand> emptyCommands = new List<EmptyCommand>();

        public CommandRunManager()
        {
            //load up the commands

            //Normal commands
            emptyCommands.Add(new ClearConsole());
            emptyCommands.Add(new exit());

            //Discord Hybrid commands
            emptyCommands.Add(new blocksaround());
            emptyCommands.Add(new crafting());
            emptyCommands.Add(new equip());
            emptyCommands.Add(new furnace());
            emptyCommands.Add(new Health());
            emptyCommands.Add(new inventoryitems());
            emptyCommands.Add(new mineblock());
            emptyCommands.Add(new move());
            emptyCommands.Add(new ontop());
            emptyCommands.Add(new placeblock());
            emptyCommands.Add(new playerposition());
        }

        public void FindCommand(string[] args)
        {
            if (args[0].Contains("!"))
            {
                args[0] = args[0].Replace("!", "");
            }
            if (!args[0].Contains("help")){
                //Check if the command exists first
                bool commandExist = false;
                //if (Reference.debugMode) { message.RespondAsync("Message commands " + args[0]); }
                int chosenCommand = 0;
                for(int m = 0; m < emptyCommands.Count - 1; m++)
                {
                    EmptyCommand commandEmpty = emptyCommands[m];
                    if(equalSplitCommand(commandEmpty.syntax, args[0].ToLower()) || args[0] == "debug")
                    {
                        commandExist = true;
                        //if (Reference.debugMode) { message.RespondAsync(commandEmpty.syntax); }
                        chosenCommand = m;
                        break;
                    }
                }
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("command exists: " + commandExist);
                //Console.ResetColor();
                if (commandExist == true)
                {
                    for (int i = 0; i < emptyCommands.Count - 1; i++)
                    {
                        //Reference.unit.AddConsoleItem("Found command");
                        EmptyCommand commandEmpty = emptyCommands[i];
                        if (commandEmpty.typeCommand == CommandType.DEBUG)
                        {
                            if (Reference.debugMode) { Debug.Log("Command is debug enabled"); }
                            if (args[0] == "debug")
                            {
                                if (Reference.debugMode) { Debug.Log("Command contains debug inside"); }
                                if (Reference.debugMode)
                                {
                                    Debug.Log("is Debug mode enabled!");
                                    if (args[1] == commandEmpty.syntax)
                                    {
                                        Debug.Log("Command contains the syntax");
                                        commandEmpty.RunCommand(args);
                                    }
                                }
                                else
                                {
                                    Debug.Log("Unfortunately these commands are only avaliable in debug mode...");
                                }
                            }
                        }
                        else
                        {
                            if (i == chosenCommand)
                            {
                                //then its a non debug command
                                commandEmpty.RunCommand(args);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("That command doesnt exist it seems!");
                }
            }
            else
            {
                /*if (Reference.discordBot)
                {
                    HelpCommand(args[1], message);
                }*/
            }
        }

        public bool equalSplitCommand(string split, string compare)
        {
            //if (Reference.debugMode) { Reference.unit.AddConsoleItem("User has entered: " + split); }
            string[] splitComamnd = split.Split('/');
            for(int i = 0; i < splitComamnd.Length; i++)
            {
                if(splitComamnd[i].ToLower() == compare.ToLower())
                {
                    //Console.ForegroundColor = ConsoleColor.Blue;
                    //Console.WriteLine("true");
                    //Console.ResetColor();
                    return true;
                }
            }
            return false;
        }

        public void HelpCommand(string type)
        {
            //parse the type
            CommandType cmdtype = CommandType.NORMAL;
            if(type.ToLower() == null || type.ToLower() == "")
            {
                string application = "";
                if (Reference.debugMode)
                {
                    application += "DEBUG/";
                }

                application += "NORMAL";
                Debug.Log("Unfortunately you did not select any one of the modes " + application);
            }
            switch (type.ToLower())
            {
                case "normal":
                    cmdtype = CommandType.NORMAL;
                    break;
                case "debug":
                    if (Reference.debugMode)
                    {
                        cmdtype = CommandType.DEBUG;
                    }
                    break;
                default:
                    if (Reference.debugMode) { Debug.Log("Unfortunately this user didnt add a mode select"); }
                    break;
            }
            List<EmptyCommand> foundCommands = emptyCommands.FindAll(t => t.typeCommand == cmdtype);
            Debug.Log("Command Type: " + cmdtype.ToString());
            if(foundCommands.ToArray().Length <= 0) { Debug.Log("Unfortunately no commands were found..."); }
            for(int i = 0; i < foundCommands.ToArray().Length; i++)
            {
                Debug.Log (foundCommands[i].commandName + " - (" + foundCommands[i].syntax + ")");
            }
        }
    }
}
