using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.UI.Tips;
using SubrightEngine2.UI.Windows;
using System;
using Color = SubrightEngine2.EngineStuff.Color;

namespace SubrightEngine2.UI
{
    [Serializable]
    public enum Context
    {
        SETTINGS,
        FILE,
        TOOLS
    }

    [Serializable]
    public class Window : GameObject
    {
        //classed as a window
        public Context context;
        public bool hideRender;

        private bool lockedWindow;

        public bool ownHide;
        public bool resizeable = true;
        public bool closeable = true;

        public Image framebuffer;

        public Window(Vector3 position, Vector3 size, string name) : base(position, size, name)
        {
        }

        public Window(Vector3 position, Vector3 size, string name, Context cxt) : base(position, size, name)
        {
            context = cxt;
        }

        public Window(Vector3 position, Vector3 size, string name, Context cxt, bool resizeable) : base(position, size,
            name)
        {
            this.resizeable = resizeable;
        }

        /// <summary>
        /// When the window starts.
        /// </summary>
        public override void Start()
        {
            base.Start();
            framebuffer = new Image();
        }

        /// <summary>
        /// Update method to update the width and height of the window.
        /// </summary>
        /// <param name="cam2"></param>
        /// <param name="cam3"></param>
        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            framebuffer.width = (int)size.X;
            framebuffer.height = (int)size.Y;
            Draw2D(ref cam2);
        }

        /// <summary>
        /// Drawing Window method in 2D
        /// </summary>
        /// <param name="cam"></param>
        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (hideRender == false)
            {
                //Update this Window
                //if(framebuffer.width != 0 && framebuffer.height != 0){Raylib.DrawTexture(Raylib.LoadTextureFromImage(framebuffer), (int)position.X, (int)position.Y, Raylib_cs.Color.WHITE);}
                DrawRectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y, Program.backgroundColor);
                DrawRectangle((int)position.X, (int)position.Y, (int)size.X, 8, Program.foregroundColor);
                DrawText(name, (int)position.X, (int)position.Y, 8, Program.textColor);

                if (Raylib.GetMouseX() > (int)position.X && Raylib.GetMouseX() < (int)position.X + (int)size.X &&
                    Raylib.GetMouseY() > (int)position.Y && Raylib.GetMouseY() < (int)position.Y + (int)size.Y)
                {
                    if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                        if (SubContextMenuManager.lockedWindows == false)
                            if (SubContextMenuManager.focusedWindow != this)
                            {
                                SubContextMenuManager.focusedWindow = this;
                                //SubContextMenuManager.windows.Insert(0, this);
                            }

                    TipManager.RenderTip(0);
                }

                if (SubContextMenuManager.focusedWindow == this)
                    if (Raylib.GetMouseX() > (int)position.X && Raylib.GetMouseX() < position.X + size.X &&
                        Raylib.GetMouseY() > position.Y && Raylib.GetMouseY() < position.Y + 8)
                        if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                        {
                            lockedWindow = true;
                            SubContextMenuManager.lockedWindows = true;
                        }

                if (position.X + size.X > Raylib.GetScreenWidth() - 1)
                    position.X--;
                else if (position.Y + size.Y > Raylib.GetScreenHeight() - 1)
                    position.Y--;
                else if (position.X < 1)
                    position.X++;
                else if (position.Y < 1) position.Y++;

                if (lockedWindow)
                    if (position.X + size.X < Raylib.GetScreenWidth() &&
                        position.Y + size.Y < Raylib.GetScreenHeight() && position.X > 0 && position.Y > 0)
                    {
                        position.X = Raylib.GetMouseX();
                        position.Y = Raylib.GetMouseY();
                        if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonUp(MouseButton.MOUSE_LEFT_BUTTON))
                        {
                            lockedWindow = false;
                            SubContextMenuManager.lockedWindows = false;
                        }
                    }

                if (SubContextMenuManager.focusedWindow == this)
                {
                    if (closeable == true)
                    {
                        Raylib.DrawText("X", (int)position.X - 10, (int)position.Y, 8, Raylib_cs.Color.RED);
                        if (Raylib.GetMouseX() > (int)position.X - 10 && Raylib.GetMouseX() < (int)position.X - 2 &&
                            Raylib.GetMouseY() > position.Y && Raylib.GetMouseY() < position.Y + 8)
                        {
                            TipManager.RenderTip(1);
                            if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
                            {
                                ownHide = true;
                                hideRender = true;
                            }
                        }
                    }
                }

                if (resizeable)
                    if (SubContextMenuManager.focusedWindow == this)
                    {
                        DrawRectangle((int)position.X + (int)size.X - 8, (int)position.Y + (int)size.Y - 8, 8, 8,
                            Program.foregroundColor);
                        if (Raylib.GetMouseX() > (int)position.X + (int)size.X - 8 &&
                            Raylib.GetMouseY() > (int)position.Y + (int)size.Y - 8 &&
                            Raylib.GetMouseY() <= (int)position.Y + size.Y &&
                            Raylib.GetMouseX() < (int)position.X + (int)size.X)
                            //Drag side ways
                            if (size.X >= 45 && size.Y >= 45)
                                if (SubrightEngine2.EngineStuff.Input.Input.GetMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON, isFocused()))
                                {
                                    size.X = position.X - Raylib.GetMouseX();
                                    size.Y = position.Y - Raylib.GetMouseY();
                                }
                    }

                DrawRectangleLines(position.X, position.Y, size.X, size.Y, Color.LIGHTGRAY);
            }
            else
            {
            }
        }

        /// <summary>
        /// Check if this window is focused.
        /// </summary>
        /// <returns>if this window is focused.</returns>
        public bool isFocused()
        {
            if (SubContextMenuManager.focusedWindow == this)
            {
                return false;
            }

            return true;
        }
    }
}