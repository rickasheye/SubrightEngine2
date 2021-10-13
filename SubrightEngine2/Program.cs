using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents;
using SubrightEngine2.EngineStuff.Input;
using SubrightEngine2.EngineStuff.InterpreterCode;
using SubrightEngine2.EngineStuff.LevelLoading;
using Color = Raylib_cs.Color;
using Vector3 = System.Numerics.Vector3;

namespace SubrightEngine2
{
    public static class Program
    {
        public static List<GameObject> objects = new List<GameObject>();
        public static bool debug = false;
        public static GameObject selectedObject;
        public static string levelSaveBlankOpen = Path.Combine(Environment.CurrentDirectory, "levels/level01.scpb");

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

        private static bool initExtension = false;
        public static void Initialise(string[] args)
        {
            //init cycle
            Debug.Log("Start init cycle", LogType.MESSAGE);
            string arguments = string.Join(" ", args);
            if (arguments.Contains("-debug"))
            {
                Debug.Log("Found debug argument launching as debug mode...");
                debug = true;
            }
            //make sure the level exists first of all
            if (!LevelLoader.savedFileByte(levelSaveBlankOpen))
            {
                LevelLoader.WriteLevelByte(levelSaveBlankOpen, ref objects);
                firstRun = true;
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
            if (!firstRun) LevelLoader.LoadLevelByte(levelSaveBlankOpen, ref objects);
            
            for (var i = 0; i < objects.Count; i++)
                //objects
                objects[i].Start();
            
            bool test = false;
            
            if(extension != null && initExtension == false){extension.Start();}
            while (!Raylib.WindowShouldClose())
            {
                ray = Raylib.GetMouseRay(Raylib.GetMousePosition(), cam3);
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLUE);
                if(extension != null){extension.Update(ref cam2, ref cam3);}
                
                //render a console
                if (console)
                {
                    //var myStreamReader = Environment.CommandLine;
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.BLACK);
                    for (int i = Debug.logFile.Count -1; i >= 0; i--)
                    {
                        string logFileCode = Debug.logFile[i];
                        if (logFileCode != "")
                        {
                            //render console output
                            var positionY = (int) 10 + i * 15;
                            Raylib.DrawText(logFileCode, 10, positionY, 10, Color.WHITE);
                        }
                    }
                    //Raylib.DrawText("CONSOLE: " + myStreamReader, 10, 10, 10, Raylib_cs.Color.WHITE);
                }

                //Raylib.DrawText("Hello, world!", 12, 12, 20, Raylib_cs.Color.BLACK);
                for (var i = 0; i < objects.Count; i++) objects[i].Update(ref cam2, ref cam3);

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

                if (bootcount <= 250)
                {
                    Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Color.GRAY);
                    Raylib.DrawTexture(image, Raylib.GetScreenWidth() / 2 - image.width / 2,
                        Raylib.GetScreenHeight() / 2 - image.height / 2, Color.WHITE);
                    //Raylib.DrawRectangle((Raylib.GetScreenWidth() / 2) - (image.width / 2), (Raylib.GetScreenHeight() / 2) - (image.height / 2) + image.height + 10, Raylib_cs.Color.WHITE);
                    if (debug)
                    {
                        Raylib.DrawText("Bootcount: " + bootcount, 10, 10, 8, Color.WHITE);
                        if (Raylib.IsKeyDown(KeyboardKey.KEY_B)) /*skip*/ bootcount = 250;
                    }

                    bootcount++;
                }
                
                Raylib.EndDrawing();
                Raylib.UpdateCamera(ref cam3);
            }
            
            if(extension != null){extension.Dispose();}
            
            LevelLoader.WriteLevelByte(levelSaveBlankOpen, ref objects);
            Raylib.CloseWindow();
        }

        public static void SetWindowTitle(string name)
        {
            Raylib.SetWindowTitle(name);
        }

        public static void SetExtension(Extension ext)
        {
            extension = ext;
            ext.Start();
            Debug.Log("Swapped extension to another! and executed the start method!");
            initExtension = true;
        }

        public static object pointerToObject(IntPtr pMapping, Type type)
        {
            return Marshal.PtrToStructure(pMapping, type);
        }
    }
}