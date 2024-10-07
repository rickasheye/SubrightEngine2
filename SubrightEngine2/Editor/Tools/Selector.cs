using Raylib_cs;
using SubrightEngine2.EngineStuff.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Xml;

#if DEBUG
namespace SubrightEngine2.Editor.Tools
{
    public class Selector : Scene
    {
        public struct GuideEntry
        {
            public SelectorWindow window;
            public string overrideName;
            public string description;
        }

        public List<GuideEntry> guides = new List<GuideEntry>();

        public Selector()
        {
            name = "Selector Guide / Engine Tools";
            loadRenderIfNotPriority = true;
        }

        //load the selector as a menu when requested.
        public override void LoadScene()
        {
            //change the name of the scene
            base.LoadScene();
            List<SelectorWindow> windows = new List<SelectorWindow>();
            windows = GetDerivedClasses<SelectorWindow>();
            foreach (SelectorWindow window in windows)
            {
                if (window.name != "Undefined")
                {
                    GuideEntry entry = new GuideEntry();
                    entry.window = window;
                    guides.Add(entry);
                }
            }
        }

        public int selectable = 0;
        public bool mouseHandOver = false;

        public override void UpdateScene(ref Camera2D cam)
        {
            base.UpdateScene(ref cam);
            if (notPriority == false)
            {
                //render the selector guide.
                Raylib.ClearBackground(Color.White);
                Raylib.DrawText("Selector Guide / Engine Tools. No Reference loaded.", 10, 10, 20, Color.Black);
                if (guides == null)
                {
                    Raylib.DrawText("No guides found!", 10, 10, 20, Color.Red);
                }
                else
                {
                    int mouseX = Raylib.GetMouseX();
                    int mouseY = Raylib.GetMouseY();
                    //draw the guides
                    for (int i = 0; i < guides.Count; i++)
                    {
                        string windowTitle = guides[i].overrideName == "" ? guides[i].overrideName : guides[i].window.name;
                        Raylib.DrawText(windowTitle, 10, 25 + i * 20, 20, Color.Black);

                        if (mouseHandOver == true)
                        {
                            //when mouse is over the title text draw the description
                            if (mouseX > 10 && mouseX < 10 + windowTitle.Length * 20 && mouseY > 25 + i * 20 && mouseY < 25 + i * 20 + 20)
                            {
                                Raylib.DrawText(">", 0, 25 + i * 20, 20, Color.Black);
                                Raylib.DrawText(guides[i].description, 10, 25 + i * 20 + 20, 20, Color.Black);
                                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                                {
                                    Program.LoadScene(guides[i].window);
                                }
                            }

                            if(Program.debug == true)
                            {
                                //draw the mouse over box over
                                Raylib.DrawRectangleLines(10, 25 + i * 20, windowTitle.Length * 20, 20, Color.Black);
                            }
                        }
                        else
                        {
                            //if selectable draw an arrow behind it
                            if (i == selectable)
                            {
                                Raylib.DrawText(">", 0, 25 + i * 20, 20, Color.Black);
                                //draw the description on the lower left hand corner of the window
                                int windowHeight = Raylib.GetScreenHeight() - 25;
                                Raylib.DrawText(guides[i].description, 10, windowHeight, 20, Color.Black);
                            }
                        }
                    }

                    //articulate the guide with up and down arrow keys and when need to use load the guide into the scene loader
                    if (Raylib.IsKeyPressed(KeyboardKey.Up))
                    {
                        selectable--;
                        if (selectable < 0)
                        {
                            selectable = guides.Count - 1;
                        }
                        mouseHandOver = false;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.Down))
                    {
                        selectable++;
                        if (selectable > guides.Count - 1)
                        {
                            selectable = 0;
                        }
                        mouseHandOver = false;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        Program.LoadScene(guides[selectable].window);
                        mouseHandOver = false;
                    }

                    //if click or movement of the mouse is detected handover to mouse
                    if (Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsMouseButtonPressed(MouseButton.Right) || Raylib.IsMouseButtonPressed(MouseButton.Middle) || Raylib.IsMouseButtonPressed(MouseButton.Back) || Raylib.IsMouseButtonPressed(MouseButton.Forward) || Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        mouseHandOver = true;
                    }
                }
            }
            else
            {
                //render a hint?
                Raylib.DrawText("Press TAB to return to the main menu.", 0, 10, 20, Color.Black);
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Tab))
            {
                Program.loader.LoadScene("Selector Guide / Engine Tools");
            }
            //Console.WriteLine("selector guide updated");
        }

        public static List<SelectorWindow> GetDerivedClasses<TBase>() where TBase : SelectorWindow
        {
            Type baseType = typeof(TBase);
            Assembly assembly = Assembly.GetExecutingAssembly(); // Replace with the appropriate assembly if needed

            List<SelectorWindow> derivedInstances = assembly.GetTypes().Where(type => type.IsClass && type.Name != "SelectorWindow" && !type.IsAbstract && baseType.IsAssignableFrom(type)).Select(type => (SelectorWindow)Activator.CreateInstance(type)).ToList();

            return derivedInstances;
        }
    }
}
#endif