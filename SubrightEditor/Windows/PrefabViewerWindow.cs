using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents;
using SubrightEngine2.EngineStuff.LevelLoading;
using SubrightEngine2.GameStuff;
using SubrightEngine2.UI;
using SubrightEngine2.UI.Windows;
using System;
using System.Collections.Generic;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngineEditor.Windows
{
    [Serializable]
    public class PrefabViewerWindow : Window
    {
        //List<Prefab> prefabs = new List<Prefab>();
        public static List<Prefab> prefabs = new List<Prefab>();
        private bool ran;

        public PrefabViewerWindow(Vector3 position, Vector3 size) : base(position, size, "Prefab Viewer", Context.TOOLS)
        {
        }

        public override void Start()
        {
            base.Start();
            if (ran == false)
            {
                AddPrefab(new Prefab(new Player(), new Vector3(0, 0, 0), new Vector3(1, 2, 1), "Player"));
                AddPrefab(new Prefab(new Floor(), new Vector3(0, 0, 0), new Vector3(1, 1, 1), "Floor"));
                AddPrefab(new Prefab(new ObjectRender(ObjectType.CUBE), new Vector3(0, 0, 0), Vector3.One, "Cube"));
                AddPrefab(new Prefab(new ObjectRender(ObjectType.CYLINDER), new Vector3(0, 0, 0), Vector3.One,
                    "Cylinder"));
                AddPrefab(new Prefab(new ObjectRender(ObjectType.SPHERE), new Vector3(0, 0, 0), Vector3.One, "Sphere"));
                AddPrefab(new Prefab(new ObjectRender(ObjectType.SQUAREBASEDPYRAMID), new Vector3(0, 0, 0), Vector3.One,
                    "Square Based Pyramid"));
                //AddPrefab(new Prefab(new EngineTextBox("Engine Text Box")));
                prefabs.AddRange(PrefabLoader.LoadPrefabs());
                ran = true;
            }
        }

        public void AddPrefab(Prefab pref)
        {
            if (prefabExists(pref.name))
            {
                //check really
                Debug.Log("Check if the prefab already exists as it does seem it already exists!");
            }
            else
            {
                prefabs.Add(pref);
                Debug.Log("Added new prefab to prefab viewer!");
            }
        }

        public bool prefabExists(string name)
        {
            for (var i = 0; i < prefabs.Count; i++)
                if (prefabs[i].name == name)
                    return true;
            return false;
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
            Draw3D(ref cam3);
        }

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            //nothing just yet
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (hideRender == false)
            {
                //show information and use it to spawn prefabs!
                if (prefabs.Count > 0)
                {
                    Prefab prefToSpawn = null;
                    for (var i = 0; i < prefabs.Count; i++)
                    {
                        var pref = prefabs[i];
                        DrawText(pref.name, position.X + 2, position.Y + 10 + i * 15, 8, Color.WHITE);
                        var positionX = (int)position.X + 2;
                        var positionY = (int)position.Y + 10 + i * 15;
                        var sizeX = (int)size.X;
                        var sizeY = 10;
                        if (Raylib.GetMouseX() > positionX && Raylib.GetMouseY() > positionY &&
                            Raylib.GetMouseX() < positionX + sizeX && Raylib.GetMouseY() < positionY + sizeY)
                            if (SubContextMenuManager.focusedWindow == this)
                            {
                                DrawRectangleLines(positionX, positionY, sizeX, sizeY, Color.LIGHTGRAY);
                                if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON, isFocused())) prefToSpawn = pref;
                            }
                    }

                    if (prefToSpawn != null)
                    {
                        if (!doesGameObjectExist(prefToSpawn.name))
                        {
                            SubrightEngine2.Program.objects.AddRange(prefToSpawn.gameObjects);
                        }
                    }
                }
                else
                {
                    DrawText("Unfortunately there is no prefabs loaded, or found in on file.", position.X + 2,
                        position.Y + 10, 8, Color.WHITE);
                }
            }
        }

        public bool doesGameObjectExist(string name)
        {
            for (var i = 0; i < SubrightEngine2.Program.objects.Count; i++)
                if (SubrightEngine2.Program.objects[i].name == name)
                    return true;
            return false;
        }
    }
}