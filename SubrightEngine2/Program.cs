using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.InterpreterCode;
using SubrightEngine2.EngineStuff.Scenes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using Color = Raylib_cs.Color;
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

        public static SubrightEngine2.EngineStuff.Color foregroundColor = EngineStuff.Color.LIGHTGRAY;
        public static SubrightEngine2.EngineStuff.Color backgroundColor = EngineStuff.Color.GRAY;
        public static SubrightEngine2.EngineStuff.Color textColor = EngineStuff.Color.White;
        static bool finishedDownloading = false;

        //temp fix
        static string[] args;
        static bool saveFile = false;
        static bool overrideStart = false;

        public static void Initialise(string[] args, bool saveFile, bool overrideStart)
        {
            gameStart = overrideStart;
            //init cycle
            Debug.Log("Start init cycle");
            //probably should make sure raylib is installed correctly before loading aswell... sometime i guess.
            string raylibDest = Path.Combine(Environment.CurrentDirectory, "raylib.dll");
            if (!File.Exists(raylibDest))
            {
                //Download raylib into that folder extract it to the dll and shed the other files.
                Debug.Log("Downloading raylib zip file");
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri("https://github.com/raysan5/raylib/releases/download/3.7.0/raylib-3.7.0_win64_msvc16.zip"), "raylib-3.7.0_win64_msvc16.zip");
                client.DownloadProgressChanged += delegate { Debug.Log("Downloading..."); };
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(completedraylibdownload);
                Program.args = args;
                Program.saveFile = saveFile;
                Program.overrideStart = overrideStart;
                string line = Console.ReadLine();
                //halt
            }
            else
            {
                Debug.Log("raylib found! no need to download!");
                finishedDownloading = true;
            }

            if(loader == null)
            {
                loader = new SceneLoader();
                Debug.Log("Loaded scene loader");
            }

            if(loader.currentScene == null)
            {
                //load a blank scene.
                loader.AddScene(new BlankScene());
                loader.LoadScene("BlankScene");
                Debug.Log("Loaded blank scene");
            }

            string arguments = string.Join(" ", args);
            if (arguments.Contains("-debug"))
            {
                Debug.Log("Found debug argument launching as debug mode...");
                debug = true;
            }

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
            cam3.position = new Vector3(0.0f, 10.0f, 10.0f); // Camera position
            cam3.target = new Vector3(0.0f, 0.0f, 0.0f); // Camera looking at point
            cam3.up = new Vector3(0.0f, 1.0f, 0.0f); // Camera up vector (rotation towards target)
            cam3.fovy = 45.0f; // Camera field-of-view Y
            cam3.projection = CameraProjection.CAMERA_PERSPECTIVE; // Camera mode type

            //objects.AddRange(windows);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
            Raylib.SetConfigFlags(ConfigFlags.FLAG_FULLSCREEN_MODE);

            //Initialise Icon
            var image = Raylib.LoadTexture(Path.Combine(Directory.GetCurrentDirectory(), "textures/titlescreen.png"));

            Ray ray;

            //load the level
            if (saveFile == true) { if (!firstRun) LevelLoader.LoadLevelByte(); }

            for (var i = 0; i < loader.currentScene.GameObjects.Count; i++)
                //objects
                loader.currentScene.GameObjects[i].Start();

            bool test = false;

            while (!Raylib.WindowShouldClose())
            {
                ray = Raylib.GetMouseRay(Raylib.GetMousePosition(), cam3);
                Raylib.BeginDrawing();
                //Raylib.ClearBackground(Color.BLUE);
                if (extension != null) { extension.Update(ref cam2, ref cam3); }

                //render a console
                if (console)
                {
                    //var myStreamReader = Environment.CommandLine;
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.BLACK);
                    for (int i = Debug.logFile.Count - 1; i >= 0; i--)
                    {
                        string logFileCode = Debug.logFile[i];
                        if (logFileCode != "")
                        {
                            //render console output
                            var positionY = (int)10 + i * 15;
                            Raylib.DrawText(logFileCode, 10, positionY, 10, Color.WHITE);
                        }
                    }
                    //Raylib.DrawText("CONSOLE: " + myStreamReader, 10, 10, 10, Raylib_cs.Color.WHITE);
                }

                //Raylib.DrawText("Hello, world!", 12, 12, 20, Raylib_cs.Color.BLACK);
                for (var i = 0; i < loader.currentScene.GameObjects.Count; i++) loader.currentScene.GameObjects[i].Update(ref cam2, ref cam3);

                Raylib.BeginMode3D(cam3);
                if (selectedObject != null)
                {
                    //draw arrows around here
                    //Raylib.DrawModel(arrowModel, selectedObject.position, 4, Raylib_cs.Color.RED);
                    //use the arrow keys instead
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                        //left
                        switch (snapmovement)
                        {
                            case false:
                                selectedObject.position.X--;
                                break;
                            case true:
                                selectedObject.position.X -= 1;
                                break;
                        }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                        //right
                        switch (snapmovement)
                        {
                            case false:
                                selectedObject.position.X++;
                                break;
                            case true:
                                selectedObject.position.X += 1;
                                break;
                        }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                        //up
                        switch (snapmovement)
                        {
                            case false:
                                selectedObject.position.Y++;
                                break;
                            case true:
                                selectedObject.position.Y += 1;
                                break;
                        }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                        //down
                        switch (snapmovement)
                        {
                            case false:
                                selectedObject.position.Y--;
                                break;
                            case true:
                                selectedObject.position.Y -= 1;
                                break;
                        }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_Q))
                        //back
                        switch (snapmovement)
                        {
                            case false:
                                selectedObject.position.Z--;
                                break;
                            case true:
                                selectedObject.position.Z -= 1;
                                break;
                        }

                    if (Raylib.IsKeyDown(KeyboardKey.KEY_E))
                        //forward
                        switch (snapmovement)
                        {
                            case false:
                                selectedObject.position.Z++;
                                break;
                            case true:
                                selectedObject.position.Z += 1;
                                break;
                        }
                }

                //Raylib.DrawCube(new Vector3(0, 0, 0), 1, 1, 1, Raylib_cs.Color.BLACK);
                Raylib.EndMode3D();

                if (debug)
                    if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                        Raylib.DrawText("" + Raylib.GetMouseX() + ": " + Raylib.GetMouseY(), Raylib.GetMouseX(),
                            Raylib.GetMouseY(), 12, Color.BLACK);
                Raylib.DrawFPS(10, 10);
                //Draw icon

                if (bootcount <= 100)
                {
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), backgroundColor.ToRaylibColor);
                    if (image.width != 0 && image.height != 0)
                    {
                        Raylib.DrawTexture(image, Raylib.GetScreenWidth() / 2 - image.width / 2,
                    Raylib.GetScreenHeight() / 2 - image.height / 2, Color.WHITE);
                    }
                    else
                    {
                        Raylib.DrawText("You are missing the cool splash screen texture :( Loading Subright Engine 2's Assets hold tight... next time add the texture! (PS its supposed to be under 'textures/titlescreen.png').", 10, 10, 10, Color.BLACK);
                    }
                    //Raylib.DrawRectangle((Raylib.GetScreenWidth() / 2) - (image.width / 2), (Raylib.GetScreenHeight() / 2) - (image.height / 2) + image.height + 10, Raylib_cs.Color.WHITE);
                    if (debug)
                    {
                        Raylib.DrawText("Bootcount: " + bootcount, 10, 10, 8, textColor.ToRaylibColor);
                        if (Raylib.IsKeyDown(KeyboardKey.KEY_B)) /*skip*/ bootcount = 250;
                    }

                    if (Raylib.GetKeyPressed() != 0)
                    {
                        Debug.Log("" + bootcount);
                    }

                    bootcount++;
                }

                Raylib.EndDrawing();
                Raylib.UpdateCamera(ref cam3);
            }

            if (extension != null) { extension.Dispose(); }

            if (saveFile == false) { LevelLoader.WriteLevelByte(); }
            Raylib.CloseWindow();
        }

        private static void completedraylibdownload(object sender, AsyncCompletedEventArgs e)
        {
            Debug.Log("Finished downloading extracting files.");
            ZipFile.ExtractToDirectory("raylib-3.7.0_win64_msvc16.zip", Environment.CurrentDirectory);
            var directoryFiles = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "raylib-3.7.0_win64_msvc16"), "*.dll", SearchOption.AllDirectories);
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
                File.Copy(foundFile, Path.Combine(Environment.CurrentDirectory, "raylib.dll"));
                RecursiveDelete(new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "raylib-3.7.0_win64_msvc16")));
                File.Delete(Path.Combine(Environment.CurrentDirectory, "raylib-3.7.0_win64_msvc16.zip"));
                Debug.Log("Copied file and cleaned up! sucesfully!");
            }
            else
            {
                Debug.Log("Process was unable to find file quitting!!");
                Environment.Exit(0);
            }
            Debug.Log("Finished process reloading...");
            Initialise(args, saveFile, overrideStart);
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
        }

        public static object pointerToObject(IntPtr pMapping, Type type)
        {
            return Marshal.PtrToStructure(pMapping, type);
        }
    }
}