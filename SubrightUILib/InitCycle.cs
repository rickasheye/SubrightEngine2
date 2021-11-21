using Raylib_cs;
using SubrightEngine2.EngineStuff.Input;
using SubrightEngine2.UI.Notifications;
using SubrightEngine2.UI.Windows;
using System;
using System.IO;
using Debug = SubrightEngine2.EngineStuff.Debug;

namespace SubrightEngine2.UI
{
    public class InitCycle
    {
        public static NotificationHandler handler;
        public bool console = false;
        public bool initWindowMan = false;

        /// <summary>
        /// Check if the focused window exists if not return false.
        /// </summary>
        /// <returns>returns if the focused window is not null or not in boolean fashion.</returns>
        public static bool windowFocused()
        {
            if (SubContextMenuManager.focusedWindow != null)
            {
                return true;
            }

            return false;
        }

        public InitCycle(bool loadWindowSystem)
        {
            this.initWindowMan = loadWindowSystem;
        }

        private Toolbar bar;
        private SubContextMenuManager cxtMan;
        
        /// <summary>
        /// Initialise the UI Library to actually help or display GUI graphics on the screen.
        /// </summary>
        public void Start()
        {
            if (initWindowMan == true)
            {
                cxtMan = new SubContextMenuManager();
                Program.loader.currentScene.GameObjects.Add(cxtMan);

                bar = new Toolbar();

                if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "lock")))
                {
                    //MessageBox.CreateMessageBox("Welcome to Subright Engine 2, if its your first time \n then read the Tips when hovering over items.");
                    Notification.addNotification("Welcome!",
                        "Welcome to Subright Engine 2 to get started hover over anything to get a tip");
                    if (!Program.debug) { Debug.Log("File wasnt present showing welcome box!"); }
                    if (!Program.debug) { File.Create(Path.Combine(Environment.CurrentDirectory, "lock")); }
                }

                handler = new NotificationHandler(); 
            }
        }

        private bool test = false;
        public static bool hideWindows;

        /// <summary>
        /// Updating every UI Element on the screen needs to be intiialised by calling Start() first
        /// </summary>
        /// <param name="cam2"></param>
        /// <param name="cam3"></param>
        public void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            if (initWindowMan)
            {
                if (Program.debug)
                {
                    if (test == false)
                    {
                        Notification.addNotification("TEST", "TESTING BOX DEFINED HERE!!!");
                        test = true;
                    }
                }
                RallyDialog.RollDialogs(ref cam2, ref cam3);

                cxtMan.Update(ref cam2, ref cam3);
                bar.Draw2D(ref cam2);
                handler.Update(ref cam2, ref cam3);

                if (Input.GetButtonPressed(KeyboardKey.KEY_C, !windowFocused()))
                {
                    if (console)
                    {
                        console = false;
                    }
                    else
                    {
                        console = true;
                    }
                }

                for (var i = 0; i < SubContextMenuManager.windows.Count; i++)
                    if (SubContextMenuManager.windows[i].ownHide == false)
                        if (SubContextMenuManager.windows[i].hideRender == !hideWindows)
                            SubContextMenuManager.windows[i].hideRender = hideWindows;

                if (Program.debug)
                    Raylib.DrawText(
                        "Game Object instances: " + Program.loader.currentScene.GameObjects.Count + " Window instances: " +
                        SubContextMenuManager.windows.Count, 10, 10, 10, Program.textColor.ToRaylibColor); 
            }
        }

        /// <summary>
        /// Dispose the windows.
        /// </summary>
        public void Dispose()
        {
            if (initWindowMan)
            {
                cxtMan.DisposeWindows(); 
            }
        }
    }
}