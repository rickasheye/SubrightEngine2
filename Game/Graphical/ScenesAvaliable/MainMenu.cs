using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole.Graphical.ScenesAvaliable
{
    public class MainMenu : Scene
    {
        public MainMenu():base("Main Menu") {
        }

        Text text;
        ButtonPlay playbutton;
        ButtonCreateNewSave newSave;
        ButtonQuit quitbutton;
        EmptyContainer container;
        public Dictionary<float[,], int> textureIndexes = new Dictionary<float[,], int>();
        Model startingModel = new Model();
        Camera3D ThreeDCam = new Camera3D();

        public override void LoadScene()
        {
            base.LoadScene();
            text = new Text("RPGConsole", new Vector2(10, 10), 40, Color.BLACK);
            playbutton = new ButtonPlay();
            newSave = new ButtonCreateNewSave();
            quitbutton = new ButtonQuit();
            container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
            guiOptions.Add(text);
            guiOptions.Add(playbutton);
            guiOptions.Add(newSave);
            guiOptions.Add(quitbutton);
            container.children.AddRange(guiOptions);
            guiOptions.Add(container);
            ThreeDCam.position = new Vector3(14, 14, 16).ToNumericsVector;
            ThreeDCam.target = new Vector3(0, 0, 0).ToNumericsVector;
            ThreeDCam.up = new Vector3(0, 1, 0).ToNumericsVector;
            ThreeDCam.fovy = 45;
            Raylib.SetCameraMode(ThreeDCam, CameraMode.CAMERA_ORBITAL);

            startingModel = Raylib.LoadModel("Models/mainMenuItem.obj");
            unsafe
            {
                Material* materials = (Material*)startingModel.materials.ToPointer();
                MaterialMap* maps = (MaterialMap*)materials[0].maps.ToPointer();
                Raylib.SetMaterialTexture(ref materials[0], 0, Raylib.LoadTexture("Textures/models/menuitem.png"));
                //maps[(int)Convert.GetTypeCode(MaterialMapType.MAP_ALBEDO)].texture = Raylib.LoadTexture("Textures/models/menuitem.png");
            }
        }

        public override void UpdateScene(Camera2D cam)
        {
            base.UpdateScene(cam);
            Raylib.BeginMode3D(ThreeDCam);
            //Raylib.DrawCube(new Vector3(0, 0, 0), 3, 3, 3, Color.WHITE);
            Raylib.DrawModel(startingModel, new Vector3(0, 0, 0).ToNumericsVector, 0.1f, Color.WHITE.ToRaylibColor);
            Raylib.EndMode3D();
            Raylib.UpdateCamera(ref ThreeDCam);
        }
    }

    public class ButtonPlay : KeyboardAdjustedButton
    {
        //Continue on the last save!!!
        public ButtonPlay():base("Continue", new Vector2(40, 80), new Vector2(10, 60)) {
            if(Reference.manager.savefiles.Count <= 0)
            {
                name = "Load new save file!";
            }
        }

        public override void Triggerable()
        {
            base.Triggerable();
            if (Reference.manager.savefiles.Count > 0)
            {
                if (Reference.manager.LoadFile(Reference.manager.getTopmostFile(), ref Reference.gen, ref Reference.player))
                {
                    Debug.Log("Sucessfully loaded the save file!");
                }
                else
                {
                    Debug.Log("Unfortunately unable to load save file!");
                    if (Reference.manager.SaveFile())
                    {
                        Debug.Log("Successfully saved your file!");
                        if (Reference.manager.LoadFile(Reference.manager.savefiles[Reference.manager.savefiles.Count - 1], ref Reference.gen, ref Reference.player))
                        {
                            Debug.Log("Sucessfully loaded your file!");
                        }
                        else
                        {
                            Debug.Log("Unfortunately your file was unable to be loaded!");
                        }
                    }
                    else
                    {
                        Debug.Log("Unfortunately your file has not saved correctly!");
                    }
                }
            }
            else
            {
                //create a new file!@
                Reference.manager.SaveFile();
                Triggerable();
            }
            Reference.loader.LoadScene(Reference.loader.getScene("Main Game Scene"));
        }
    }

    public class ButtonCreateNewSave : KeyboardAdjustedButton
    {
        //Create a new save and play that!
        public ButtonCreateNewSave() : base("Create new save! and play!", new Vector2(40, 80), new Vector2(10, 120)) {
            if (Reference.manager.savefiles.Count <= 0)
            {
                disabled = true;
            }
        }

        public override void Triggerable()
        {
            base.Triggerable();
            if (Reference.manager.SaveFile())
            {
                Debug.Log("Successfully saved your file!");
                if(Reference.manager.LoadFile(Reference.manager.savefiles[Reference.manager.savefiles.Count - 1], ref Reference.gen, ref Reference.player))
                {
                    Debug.Log("Sucessfully loaded your file!");
                }
                else
                {
                    Debug.Log("Unfortunately your file was unable to be loaded!");
                }
            }
            else
            {
                Debug.Log("Unfortunately your file has not saved correctly!");
            }
        }
    }

    public class ButtonQuit : KeyboardAdjustedButton
    {
        public ButtonQuit() : base("Quit", new Vector2(40, 80), new Vector2(10, 180)) { }

        public override void Triggerable()
        {
            base.Triggerable();
            Environment.Exit(0);
        }

        public override void DrawObject()
        {
            base.DrawObject();
        }
    }
}
