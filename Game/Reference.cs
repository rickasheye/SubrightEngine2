using Raylib_cs;
using RPGConsole.Commands;
using RPGConsole.Graphical;
using RPGConsole.Graphical.ScenesAvaliable;
using RPGConsole.Profile;
using RPGConsole.Saving;
using SubrightEngine2.EngineStuff;
using System;
using System.Diagnostics;
using System.IO;
using Debug = SubrightEngine2.EngineStuff.Debug;

namespace RPGConsole
{
    public class Reference : Extension
    {

        public override void Start()
        {
            base.Start();
            //load the gameobjects
            StartGame();
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
        }

        public static void Draw2D(ref Camera2D cam2)
        {
            Raylib.BeginMode2D(cam2);
            Raylib.ClearBackground(Raylib_cs.Color.SKYBLUE);
            Raylib.EndMode2D();
        }

        public override void Dispose()
        {
            base.Dispose();
            if (SubrightEngine2.Program.debug == true) { Debug.Log("unloaded all textures!"); }

            supportProfiles.ExitProfile();
            string settingsPath = Path.Combine(Environment.CurrentDirectory, "system.json");
            settings.SaveSettings(settingsPath);
        }

        public static ProfileSupport supportProfiles;
        public static Generator gen;

        public static bool cmdMode = false;

        public static Player player;
        public static SystemSettings settings;

        public static ulong masteriddiscord = 745584917253193790;
        public static bool answermaster = false;
        //public static List<storedTexture> storedTextures = new List<storedTexture>();

        public static SaveFileManager manager = new SaveFileManager();

        public static DateTime timeStart;

        static void StartGame()
        {
            timeStart = Process.GetCurrentProcess().StartTime;
            SubrightEngine2.EngineStuff.Debug.Log("Started time at: " + timeStart.ToString());
            //get the discord bot ready if enabled!
            string settingsPath = Path.Combine(Environment.CurrentDirectory, "system.json");
            settings = SystemSettings.LoadSystemSettings(settings, settingsPath);

            string dirpath = Path.Combine(Environment.CurrentDirectory, "saves/");
            if (Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }

            if (SubrightEngine2.Program.debug == true)
            {
                Debug.Log("Hello World... debug mode enabled!");
            }
            supportProfiles = new ProfileSupport();
            if (gen == null)
            {
                gen = new Generator(512, 512);
            }

            //cmdMode = settings.cmdMode;
            if (SubrightEngine2.Program.debug == true) { Debug.Log("cmd mode is " + cmdMode + " but the config is read as " + settings.cmdMode); }
            supportProfiles.InstallProfiles();

            player = new Player();
            if (supportProfiles.profiles.Count == 0)
            {
                SubrightEngine2.EngineStuff.Debug.Log("Enter in a username to use!");
                string username = "";
                username = System.Console.ReadLine();
                Profile.Profile profile = new RPGConsole.Profile.Profile(username, supportProfiles);
                supportProfiles.CreateProfile(profile);
            }
            else
            {
                //skip this part and load the original one!
                SubrightEngine2.EngineStuff.Debug.Log("profile already created!");
            }

            if (cmdMode)
            {
                bool continueSave = false;
                while (!continueSave)
                {
                    Debug.Log("What would you like to do? and also welcome to RPG Console!");
                    Debug.Log("(new/create/cmon) create new save file!");
                    Debug.Log("(continue/old/play) see if there is a file to continue!");
                    Debug.Log("(opensaves/viewsaves/saves) see what files have been loaded!");
                    Debug.Log("or say what file you want to load by file name!");
                    if (SubrightEngine2.Program.debug == true) { Debug.Log("(skip) usually found from enabling debug mode"); }
                    for (int i = 0; i < manager.savefiles.Count; i++)
                    {
                        Debug.Log("(" + i + ") " + manager.savefiles[i].fileName);
                    }
                    string line = System.Console.ReadLine();
                    switch (line)
                    {
                        case "new":
                        case "create":
                        case "cmon":
                            if (manager.SaveFile() == true)
                            {
                                Debug.Log("Able to save the file and create a new one!");
                            }
                            else
                            {
                                Debug.Log("Something went wrong... please try again!");
                                Debug.Log("restarting...");
                                System.Console.Clear();
                            }
                            break;
                        case "continue":
                        case "old":
                        case "play":
                            if (manager.LoadFile(manager.getTopmostFile(), ref gen, ref player))
                            {
                                continueSave = true;
                            }
                            break;
                        case "opensaves":
                        case "viewsaves":
                        case "saves":
                            System.Console.Clear();
                            Debug.Log("use anyword to exit this prompt");
                            if (manager.savefiles != null || manager.savefiles.Count != -1)
                            {
                                for (int i = 0; i < manager.savefiles.Count - 1; i++)
                                {
                                    Debug.Log("(" + i + ") " + manager.savefiles[i].fileName);
                                }
                            }
                            else
                            {
                                Debug.Log("No files found...");
                            }
                            string collectableString = System.Console.ReadLine();
                            System.Console.Clear();
                            break;
                        case "skip":
                            if (SubrightEngine2.Program.debug == true)
                            {
                                continueSave = true;
                            }
                            break;
                        default:
                            bool aSave = false;
                            int lineSave = int.Parse(line);
                            for (int i = 0; i < manager.savefiles.Count; i++)
                            {
                                if (i == lineSave)
                                {
                                    Debug.Log("Sucessfully picked " + line + " now loading!");
                                    aSave = true;
                                    break;
                                }
                            }

                            if (aSave == false)
                            {
                                Debug.Log("Unfortunately that isnt a valid Command!");
                            }
                            else
                            {
                                if (manager.LoadFile(manager.savefiles[lineSave], ref gen, ref player))
                                {
                                    Debug.Log("Yay we have found the file and loaded it!");
                                }
                                else
                                {
                                    Debug.Log("That is an invalid file as it was unable to be loaded?");
                                }
                            }
                            break;
                    }
                }
            }

            //player = supportProfiles.profiles[0].playerInstance;

            if (cmdMode)
            {
                System.Console.Clear();
                SubrightEngine2.EngineStuff.Debug.Log("Welcome to cmdRPG to get started please use the help command!");
                cmdInterpreter(player, gen);
                if (player.health <= 0)
                {
                    Debug.Log("game over restart app to try again!");
                }
            }
            else if (!cmdMode)
            {
                //Player is offset!
                //launch raylib mode
                const int screenWidth = 800;
                const int screenHeight = 600;
                //Raylib.InitWindow(screenWidth, screenHeight, "RPGConsole");

                Camera2D camera = new Camera2D();
                camera.offset = new System.Numerics.Vector2(screenWidth / 2, screenHeight / 2);
                camera.zoom = 1.0f;
                MainScene mainScene = new MainScene();
                MenuTest testmenu = new MenuTest();
                SavesMenu savesMenu = new SavesMenu();
                SubrightEngine2.Program.loader.AddScene(mainScene);
                SubrightEngine2.Program.loader.AddScene(testmenu);
                SubrightEngine2.Program.loader.AddScene(savesMenu);
                SubrightEngine2.Program.loader.LoadScene(mainScene);

                Raylib.SetTargetFPS(30);

                while (!Raylib.WindowShouldClose())
                {
                    Raylib.BeginDrawing();
                    //draw hud etc
                    //Debug.Log("eh");
                    Draw2D(ref camera);
                    if (SubrightEngine2.Program.debug == true) { Raylib.DrawFPS(Raylib.GetMouseX() + 10, Raylib.GetMouseY() + 10); }
                    Raylib.EndDrawing(); 
                }
            }
            //Environment.Exit(0);
        }

        public static CommandRunManager managerRunCommand = new CommandRunManager();

        public static void executeCommand(string line, Player player, Generator gen)
        {
            string[] argsM = line.Split(' ');
            managerRunCommand.FindCommand(argsM);
        }

        public static void cmdInterpreter(Player player, Generator gen)
        {
            bool gameActive = true;
            SubrightEngine2.EngineStuff.Debug.Log("game has started to get used to the commands please type 'help'");
            while (gameActive)
            {
                string line = System.Console.ReadLine();
                executeCommand(line, player, gen);
                if (player.health <= 0)
                {
                    gameActive = false;
                }
            }
        }
    }
}