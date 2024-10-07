using Raylib_cs;
using SubrightEngine2.Editor.Tools;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.InterpreterCode;
using SubrightEngine2.EngineStuff.Scenes;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Color = Raylib_cs.Color;
using Debug = SubrightEngine2.EngineStuff.Debug;
using Vector3 = System.Numerics.Vector3;

namespace SubrightEngine2
{
    public static class Program
    {
        public static bool debug = false;
        public static GameObject selectedObject;

        public static Model arrowModel;
        public static bool console = false;

        public static int bootcount;
        public static bool snapmovement;

        public static bool gameStart = false;
        public static bool firstRun;

        public static Interpreter interpret;

        public static bool showCollision = true;
        public static bool ranonce = false;

        public static Extension extension;
        public static SceneLoader loader;

        private static bool initExtension = false;

        public static SubrightEngine2.EngineStuff.Color foregroundColor = EngineStuff.Color.LightSeaGreen;
        public static SubrightEngine2.EngineStuff.Color backgroundColor = EngineStuff.Color.GREEN;
        public static SubrightEngine2.EngineStuff.Color textColor = EngineStuff.Color.Black;
        private static bool finishedDownloading = false;

        //temp fix
        private static string[] args;

        public static bool saveFile = false;
        private static bool overrideStart = false;
        private static bool loadedSceneLoader = false;
        public static bool showsplash = false;

        public static void Main(string[] args)
        {
            SubrightEngine2.Program.Initialise(args, false, false);
        }

        public static async Task downloadRaylib()
        {
            try
            {
                string raylibDest = Path.Combine(Environment.CurrentDirectory, "raylib.dll");
                //search for all the files in the current directory if they contain "raylib.dll" if so check it has -5.0 attached to it before the .dll
                string[] files = Directory.GetFiles(Environment.CurrentDirectory, "*.dll", SearchOption.AllDirectories);
                for(int i = 0; i < files.Length; i++)
                {
                    //CHECKING THE FILES
                    string file = files[i];
                    if (file.Contains("raylib"))
                    {
                        if (file.Contains("5.0"))
                        {
                            Debug.Log("Found raylib.dll and version is 5.0! no need to download!");
                            finishedDownloading = true;
                            return;
                        }
                        else
                        {
                            //not 5.0 so delete it.
                            Debug.Log("Found raylib.dll but version is not 5.0! deleting and redownloading...");
                            File.Delete(Path.Combine(Environment.CurrentDirectory, "raylib.dll"));
                            break;
                        }
                    }
                }

                //probably should make sure raylib is installed correctly before loading aswell... sometime i guess.
                if (!File.Exists(raylibDest))
                {
                    //Download raylib into that folder extract it to the dll and shed the other files.
                    Debug.Log("Downloading raylib zip file");
                    WebClient client = new WebClient();
                    var tcs = new TaskCompletionSource<bool>();
                    client.DownloadFileAsync(new Uri("https://github.com/raysan5/raylib/releases/download/5.0/raylib-5.0_win64_msvc16.zip"), "raylib-5.0_win64_msvc16.zip");
                    client.DownloadProgressChanged += delegate { Debug.Log("Downloading..."); };
                    int code = 0;
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(delegate
                    {
                        Debug.Log("Finished downloading extracting files.");
                        ZipFile.ExtractToDirectory("raylib-5.0_win64_msvc16.zip", Environment.CurrentDirectory);
                        var directoryFiles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "raylib-5.0_win64_msvc16"), "*.dll", SearchOption.AllDirectories);
                        //find the raylib.dll
                        Debug.Log("Locating raylib dll");
                        string foundFile = "undefined";
                        for (int i = 0; i < directoryFiles.Length; i++)
                        {
                            if (directoryFiles[i].Contains("raylib.dll"))
                            {
                                foundFile = directoryFiles[i];
                                break;
                            }
                        }
                        Debug.Log("Located file moving");
                        if (File.Exists(foundFile))
                        {
                            File.Copy(foundFile, Path.Combine(Environment.CurrentDirectory, "raylib-5.0.dll"));
                            RecursiveDelete(new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "raylib-5.0_win64_msvc16")));
                            File.Delete(Path.Combine(Environment.CurrentDirectory, "raylib-5.0_win64_msvc16.zip"));
                            Debug.Log("Copied file and cleaned up! sucesfully!");
                            code = 200;
                        }
                        else
                        {
                            Debug.Log("Process was unable to find file quitting!!");
                            Environment.Exit(0);
                        }
                        Debug.Log("Finished process reloading...");
                        tcs.SetResult(true);
                    });
                    Program.args = args;
                    Program.saveFile = saveFile;
                    //halt
                    await tcs.Task;
                    client.Disposed += delegate
                    {
                        Debug.Log("Finished downloading raylib!");
                        finishedDownloading = true;
                        Initialise(args, saveFile, overrideStart);
                    };
                }
                else
                {
                    Debug.Log("raylib found! no need to download!");
                    finishedDownloading = true;
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Unable to download raylib! " + e.Message);
                return;
            }
        }

        public static bool createMessage = false;

        //when the engine starts on its own.
        public static bool engineStartAlone = false;

        public static void Initialise(string[] args, bool saveFile, bool overrideStart)
        {
            gameStart = overrideStart;
            //init cycle
            Debug.Log("Start init cycle");
            var taskrun = Task.Run(downloadRaylib);
            while (taskrun.IsCompleted == false)
            {
                //wait for raylib to download
                //somehow inform the user if not using console? eventually.
                if (createMessage == false)
                {
                    Debug.Log("Downloading raylib! please wait...");
                    createMessage = true;
                }
            }

            if (loader == null)
            {
                loader = new SceneLoader();
                loadedSceneLoader = true;
                Debug.Log("Loaded scene loader");
            }

            if (loader.currentScene == null)
            {
                //load a blank scene.
                loader.AddScene(new BlankScene());
                loader.LoadScene("BlankScene");
                Debug.Log("Loaded blank scene");
            }

#if DEBUG
            debug = true;
#endif

            //make sure the level exists first of all
            if (saveFile == true)
            {
                if (!LevelLoader.savedFileByte())
                {
                    LevelLoader.WriteLevelByte();
                    firstRun = true;
                }
            }

            Debug.Log("Loading extensions");
            //LoadExtensions.LoadExtension(Path.Combine(Environment.CurrentDirectory, "extensions/"), arguments);
            Debug.Log("Finished loading.");
            //loading interpreter
            Debug.Log("Interpreter load");
            interpret = new Interpreter();
            Debug.Log("Finished Interpreter load");

            Raylib.InitWindow(1280, 720, "Subright Engine 2");
            Raylib.SetTargetFPS(30);

            if (extension != null) { extension.Start(); }
            var cam2 = new Camera2D();

            var cam3 = new Camera3D();
            cam3.Position = new Vector3(0.0f, 10.0f, 10.0f); // Camera position
            cam3.Target = new Vector3(0.0f, 0.0f, 0.0f); // Camera looking at point
            cam3.Up = new Vector3(0.0f, 1.0f, 0.0f); // Camera up vector (rotation towards target)
            cam3.Projection = CameraProjection.Perspective; // Camera mode type

            //objects.AddRange(windows);
            Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
            Raylib.SetConfigFlags(ConfigFlags.FullscreenMode);

            //Initialise Icon
            Debug.Log("loading icon");
            var imageGen = Raylib.LoadImage(Path.Combine(Directory.GetCurrentDirectory(), "textures/titlescreen.png"));
            Raylib.ImageFormat(ref imageGen, PixelFormat.UncompressedR8G8B8A8);
            Raylib.ImageResizeNN(ref imageGen, imageGen.Width * 6, imageGen.Height * 6);
            var image = Raylib.LoadTextureFromImage(imageGen);

            Raylib_cs.Ray ray;

            //load the level
            if (saveFile == true) { if (!firstRun) LevelLoader.LoadLevelByte(); }

            for (var i = 0; i < loader.currentScene.GameObjects.Count; i++)
                //objects
                loader.currentScene.GameObjects[i].Start();

            while (!Raylib.WindowShouldClose())
            {
                ray = Raylib.GetMouseRay(Raylib.GetMousePosition(), cam3);
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);
                //showsplash = false;
                if (!showsplash)
                {
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), backgroundColor.ToRaylibColor);
                    if (image.Width != 0 && image.Height != 0)
                    {
                        Raylib.DrawTexture(image, Raylib.GetScreenWidth() / 2 - image.Width / 2,
                        Raylib.GetScreenHeight() / 2 - image.Height / 2, Color.White);
                    }
                    else
                    {
                        Raylib.DrawText("You are missing the cool splash screen texture :( Loading Subright Engine 2's Assets hold tight... next time add the texture! (PS its supposed to be under 'textures/titlescreen.png').", 10, 10, 10, Color.Black);
                    }
                    Debug.Log("Finished with splash screen");
                    showsplash = true;
                }
                //Raylib.ClearBackground(Color.BLUE);
                if (extension != null) { extension.Update(ref cam2, ref cam3); }
                if (loader.currentScene != null) { loader.UpdateScene(ref cam2, ref cam3); }
                //render a console
                if (console)
                {
                    //var myStreamReader = Environment.CommandLine;
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.Black);
                    for (int i = Debug.logFile.Count - 1; i >= 0; i--)
                    {
                        string logFileCode = Debug.logFile[i];
                        if (logFileCode != "")
                        {
                            //render console output
                            var positionY = (int)10 + i * 15;
                            Raylib.DrawText(logFileCode, 10, positionY, 10, Color.White);
                        }
                    }
                    //Raylib.DrawText("CONSOLE: " + myStreamReader, 10, 10, 10, Raylib_cs.Color.White);
                }
#if DEBUG
                if (extension == null && engineStartAlone == false) { Debug.Log("Extension is null! engine must be running alone"); /*run the engine editor*/ LoadScene(new Selector()); engineStartAlone = true; }
#else
                if (engineStartAlone == false) { Debug.Log("engine is running in release mode, editor would not be compiled with system unable to launch editor."); engineStartAlone = true; }
#endif
                //Raylib.DrawText("Hello, world!", 12, 12, 20, Raylib_cs.Color.Black);

                if (debug)
                    if (Raylib.IsMouseButtonDown(MouseButton.Left))
                        Raylib.DrawText("" + Raylib.GetMouseX() + ": " + Raylib.GetMouseY(), Raylib.GetMouseX(),
                            Raylib.GetMouseY(), 12, Color.Black);
                Raylib.DrawFPS(10, 10);
                //Draw icon
                Raylib.EndDrawing();
                Raylib.UpdateCamera(ref cam3, CameraMode.Free);
            }

            if (extension != null) { extension.Dispose(); }

            if (saveFile == true) { LevelLoader.WriteLevelByte(); }
            else
            {
                Debug.Log("Unable to save level as save files are disabled!");
            }
            Raylib.CloseWindow();
        }

        public static void RecursiveDelete(DirectoryInfo baseDir)
        {
            if (!baseDir.Exists)
                return;

            foreach (var dir in baseDir.EnumerateDirectories())
            {
                RecursiveDelete(dir);
            }
            baseDir.Delete(true);
        }

        public static void SetWindowTitle(string name)
        {
            Raylib.SetWindowTitle(name);
        }

        public static void SetExtension(Extension ext, bool start)
        {
            extension = ext;
            if (start) { ext.Start(); }
            Debug.Log("Swapped extension to another! and executed the start method!");
            engineStartAlone = false;
        }

        public static void LoadScene(Scene loadingScene)
        {
            if (loader != null)
            {
                Debug.Log("Loading scene or awaiting sceneloader to start... the scene");
                loader.AddScene(loadingScene);
                loader.CleanScene();
                loader.LoadScene(loadingScene);
            }
            else
            {
                //Sceneloader doesnt exist
                Debug.Log("Sceneloader doesnt exist!");
                while (loadedSceneLoader)
                {
                    //when scene loader actually inits please load this scene
                    LoadScene(loadingScene);
                }
            }
        }

        public static object pointerToObject(IntPtr pMapping, Type type)
        {
            return Marshal.PtrToStructure(pMapping, type);
        }
    }
}