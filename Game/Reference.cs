using Raylib_cs;
using RPGConsole.Commands;
using RPGConsole.Graphical;
using RPGConsole.Graphical.ScenesAvaliable;
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

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_Q))
            {
                Debug.Log("" + loader.currentScene.GameObjects.Count);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            if (SubrightEngine2.Program.debug == true) { Debug.Log("unloaded all textures!"); }

            string settingsPath = Path.Combine(Environment.CurrentDirectory, "system.json");
            settings.SaveSettings(settingsPath);
        }

        public static Generator gen;

        public static Player player;
        public static SystemSettings settings;

        public static ulong masteriddiscord = 745584917253193790;
        public static bool answermaster = false;
        //public static List<storedTexture> storedTextures = new List<storedTexture>();

        public static SceneLoaderUI loader;

        public static DateTime timeStart;

        static void StartGame()
        {
            timeStart = Process.GetCurrentProcess().StartTime;
            SubrightEngine2.EngineStuff.Debug.Log("Started time at: " + timeStart.ToString());
            //get the discord bot ready if enabled!
            string settingsPath = Path.Combine(Environment.CurrentDirectory, "system.json");
            settings = SystemSettings.LoadSystemSettings(settings, settingsPath);

            if(loader == null)
            {
                loader = new SceneLoaderUI();
            }

            string dirpath = Path.Combine(Environment.CurrentDirectory, "saves/");
            if (Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }

            if (SubrightEngine2.Program.debug == true)
            {
                Debug.Log("Hello World... debug mode enabled!");
            }

            if (gen == null)
            {
                gen = new Generator(512, 512);
            }

            player = new Player();

            //launch raylib mode
            const int screenWidth = 800;
            const int screenHeight = 600;
            //Raylib.InitWindow(screenWidth, screenHeight, "RPGConsole");

            Camera2D camera = new Camera2D();
            camera.offset = new System.Numerics.Vector2(screenWidth / 2, screenHeight / 2);
            camera.zoom = 1.0f;
            MainScene mainScene = new MainScene();
            loader.AddScene(mainScene);
            loader.LoadScene(mainScene);

            Raylib.SetTargetFPS(30);
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