using System;
using Raylib_cs;
using SubrightEngine2;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.Input;
using SubrightEngine2.UI.Tips;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngine2.UI.Windows
{
    [Serializable]
    public class HireachyWindow : Window
    {
        public HireachyWindow(Vector3 position, Vector3 size) : base(position, size, "Hireachy", Context.TOOLS)
        {
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        /*public List<GameObject> multipleSelected = new List<GameObject>();
        bool multisel = false;*/
        private int lastY = 0;
        
        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (hideRender == false)
                for (var i = 0; i < SubrightEngine2.Program.objects.Count; i++)
                {
                    var newObject = SubrightEngine2.Program.objects[i];
                    DrawText(newObject.name + " - " + newObject.position.ToString + " - " + newObject.size.ToString,
                        position.X + 2, position.Y + 10 + i * 15, 8, Color.WHITE);
                    var positionX = (int) position.X + 2;
                    var positionY = lastY + (int) position.Y + 10 + i * 15;
                    var sizeX = (int) size.X;
                    var sizeY = 10;
                    if (Raylib.GetMouseX() > positionX && Raylib.GetMouseY() > positionY &&
                        Raylib.GetMouseX() < positionX + sizeX && Raylib.GetMouseY() < positionY + sizeY)
                        if (SubContextMenuManager.focusedWindow == this)
                        {
                            TipManager.RenderTip(4);
                            DrawRectangleLines(positionX, positionY, sizeX, sizeY, Color.LIGHTGRAY);
                            if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON, isFocused()))
                                SubrightEngine2.Program.selectedObject = newObject;
                            else if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON, isFocused()))
                                SubrightEngine2.Program.objects.RemoveAt(i);
                        }

                    for (var m = 0; m < newObject.childrenObjects.Count; m++)
                    {
                        var childObject = newObject.childrenObjects[m];
                        if (childObject != null)
                        {
                            var positionYM = positionY + (int) position.Y + 10 + m * 15;
                            DrawText(childObject.name, positionX, positionYM, 8, Color.WHITE);
                        }
                    }
                }
        }
    }
}