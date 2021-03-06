using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents;
using SubrightEngine2.UI;
using SubrightEngine2.UI.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngineEditor.Windows
{
    [Serializable]
    public class ModelsWindow : Window
    {
        private int indexBegin;
        private int indexEnd = 8;

        public ModelsWindow(Vector3 position, Vector3 size) : base(position, size, "Asset Explorer", Context.FILE)
        {
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (!hideRender)
            {
                //Load all of the modelsd
                var modelsPath = Path.Combine(Directory.GetCurrentDirectory(), "models/");
                if (!Directory.Exists(modelsPath)) Directory.CreateDirectory(modelsPath);
                var directoryFiles = Directory.GetFiles(modelsPath, "*.obj", SearchOption.AllDirectories);

                if (SubrightEngine2.EngineStuff.Input.Input.GetMouseWheel(isFocused()) > 0.1f)
                    //mouse is moving up
                    indexEnd++;
                else if (SubrightEngine2.EngineStuff.Input.Input.GetMouseWheel(isFocused()) < -0.1f) indexEnd--;
                if (indexEnd >= directoryFiles.Length)
                    indexEnd = directoryFiles.Length;
                else if (indexEnd <= 8) indexEnd = 8;

                indexBegin = indexEnd - 8;
                var titles = new List<string>();
                for (var i = indexBegin; i < indexEnd; i++) titles.Add(directoryFiles[i]);

                for (var i = 0; i < titles.Count; i++)
                {
                    DrawText(titles[i], position.X + 2, position.Y + 10 + i * 15, 8, Color.WHITE);
                    if (Raylib.GetMouseX() > (int)position.X + 2 && Raylib.GetMouseX() < (int)position.X + size.X &&
                        Raylib.GetMouseY() <= (int)position.Y + 18 + i * 15 &&
                        Raylib.GetMouseY() >= (int)position.Y + 10 + i * 15)
                        //put a selection field around the marked
                        if (SubContextMenuManager.focusedWindow == this)
                        {
                            DrawRectangleLines(position.X + 2, position.Y + 10 + i * 15, position.X + size.X, 10,
                                Color.LIGHTGRAY);
                            if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON, isFocused()))
                            {
                                bool exists = gameObjectExists("Asset: " + titles[i]);

                                //select or render a object onto the scene!
                                string nameThis = "Asset: " + titles[i];
                                var gameObject = new GameObject(new Vector3(0, 0, 0), new Vector3(1, 1, 1),
                                    returnSimmilaritiesModify(nameThis));
                                var render = new ModelRender(titles[i]);
                                gameObject.AddComponent(render);
                                SubrightEngine2.Program.loader.currentScene.GameObjects.Add(gameObject);
                            }
                        }
                }

                titles.Clear();

                if (directoryFiles.Length == 0)
                    DrawText("No Files Visible", position.X + 2, position.Y + 10, 8, Color.WHITE);
            }
        }

        public bool gameObjectExists(string name)
        {
            for (var i = 0; i < SubrightEngine2.Program.loader.currentScene.GameObjects.Count; i++)
                if (SubrightEngine2.Program.loader.currentScene.GameObjects[i].name == name)
                    return true;
            return false;
        }

        public string returnSimmilaritiesModify(string name)
        {
            int count = 0;
            string thisname = "";
            for (int i = 0; i < SubrightEngine2.Program.loader.currentScene.GameObjects.Count; i++)
            {
                GameObject gameObject = SubrightEngine2.Program.loader.currentScene.GameObjects[i];
                if (gameObject.name.Contains(name))
                {
                    count++;
                }
            }

            for (int m = 0; m < count; m++)
            {
                thisname = name + "(COPY)";
            }

            return thisname;
        }
    }
}