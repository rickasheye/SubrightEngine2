using System;
using Raylib_cs;
using SCPBreakdown.EngineStuff;
using Color = SCPBreakdown.EngineStuff.Color;

namespace SubrightEngine2.UI.Windows
{
    [Serializable]
    public class WindowSelectorWindow : Window
    {
        public WindowSelectorWindow() : base(new Vector3(10, 10, 0), new Vector3(100, 100, 0), "Window Selector")
        {
            closeable = false;
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //draw on this base here
            if (!hideRender)
            {
                for (int i = 0; i < SubContextMenuManager.windows.Count; i++)
                {
                    Window m = SubContextMenuManager.windows[i];
                    if (m != null && m.name != name)
                    {
                        int posY = (int) position.Y + 8 + i * 16;
                        int posX = (int) position.X;
                        int sizeX = (int) size.X;
                        int sizeY = (int) 8;
                        DrawText(m.name, posX, posY, 8, Color.WHITE);
                        if (Raylib.GetMouseX() > posX && Raylib.GetMouseX() < posX + sizeX &&
                            Raylib.GetMouseY() > posY &&
                            Raylib.GetMouseY() < posY + sizeY)
                        {
                            //trigger
                            if (SCPBreakdown.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON, isFocused()))
                            {
                                if (m.hideRender)
                                {
                                    m.ownHide = false;
                                    m.hideRender = false;
                                }
                                else
                                {
                                    m.ownHide = true;
                                    m.hideRender = true;
                                }
                            }

                            Raylib.DrawRectangleLines(posX, posY, sizeX, sizeY, Raylib_cs.Color.WHITE);
                        }
                    }
                }
            }
        }
    }
}