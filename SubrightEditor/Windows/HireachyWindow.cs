using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI;
using SubrightEngine2.UI.Tips;
using SubrightEngine2.UI.Windows;
using System;
using System.Collections.Generic;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngineEditor.Windows
{
    [Serializable]
    public class HireachyWindow : Window
    {
        public HireachyWindow(Vector3 position, Vector3 size) : base(position, size, "Hireachy", Context.TOOLS)
        {
        }

        private struct HireachyObject
        {
            public GameObject gameObject;
            public int index;
        }

        /*public List<GameObject> multipleSelected = new List<GameObject>();
        bool multisel = false;*/
        private int lastY = 0;

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (hideRender == false)
            {
                List<HireachyObject> createdObjects = new List<HireachyObject>();
                for (var i = 0; i < SubrightEngine2.Program.loader.currentScene.GameObjects.Count; i++)
                {
                    var newObject = SubrightEngine2.Program.loader.currentScene.GameObjects[i];
                    if (!newObject.hideableFromHireachy)
                    {
                        createdObjects.Add(new HireachyObject()
                        {
                            gameObject = newObject,
                            index = i
                        });
                    }
                }

                for (var i = 0; i < createdObjects.Count; i++)
                {
                    var newObject = createdObjects[i].gameObject;
                    DrawText(newObject.name + " - " + newObject.position.ToString + " - " + newObject.size.ToString,
                        position.X + 2, position.Y + 10 + i * 15, 8, Color.White);
                    var positionX = (int)position.X + 2;
                    var positionY = lastY + (int)position.Y + 10 + i * 15;
                    var sizeX = (int)size.X;
                    var sizeY = 10;
                    if (Raylib.GetMouseX() > positionX && Raylib.GetMouseY() > positionY &&
                        Raylib.GetMouseX() < positionX + sizeX && Raylib.GetMouseY() < positionY + sizeY)
                        if (SubContextMenuManager.focusedWindow == this)
                        {
                            TipManager.RenderTip(4);
                            DrawRectangleLines(positionX, positionY, sizeX, sizeY, Color.LIGHTGRAY);
                            if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.Left, isFocused()))
                                SubrightEngine2.Program.selectedObject = newObject;
                            else if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.Right, isFocused()))
                                SubrightEngine2.Program.loader.currentScene.GameObjects.RemoveAt(i);
                        }

                    for (var m = 0; m < newObject.childrenObjects.Count; m++)
                    {
                        var childObject = newObject.childrenObjects[m];
                        if (childObject != null)
                        {
                            var positionYM = positionY + (int)position.Y + 10 + m * 15;
                            DrawText(childObject.name, positionX, positionYM, 8, Color.White);
                        }
                    }
                }
            }
        }
    }
}