using Raylib_cs;
using SubrightEngine2.EngineStuff.Input;
using System.Collections.Generic;

namespace SubrightEngine2.Editor.Prompts
{
    public class PromptColourSelector : Prompt
    {
        private int offset = 1;
        private bool openedSubGreen = false;
        private bool openedSubRed = false;
        public int selColour = 0;
        public int selColourG = 0;
        public int selColourR = 0;
        private int goffset = 1;
        private int roffset = 1;
        private List<Raylib_cs.Color> colors = new List<Color>();

        public PromptColourSelector()
        {
            name = "Colour Selector";
            //generate colours
            for (int b = 0; b < 256; b++)
            {
                colors.Add(new Color(0, 0, b, 255));
            }
            //loaded the colours
            renderTitleBar = false;
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //draw final colour under the blue selection as text
            Raylib.DrawText("Selected Colour: R:" + selColourR + " G: " + selColourG + " B: " + selColour, 10, 10, 20, Color.Black);

            //draw all the colours in the scene as gameobjects according to scroll wheel
            if (Raylib.GetMouseX() > 10 && Raylib.GetMouseX() < 30 && openedSubGreen == false)
            {
                float scrollwheel = Input.GetMouseWheel();
                if (offset >= 0 && offset < colors.Count) { offset += (int)scrollwheel; }
                else if (offset <= 0) { offset = 0; }
                else if (offset >= colors.Count) { offset = colors.Count - 1; }
            }

            //draw offset number
            if (Program.debug == true) { Raylib.DrawText("Offset: " + offset, 10, 30, 20, Color.Black); }

            //draw the colours
            for (int i = 0; i < Raylib.GetRenderHeight() / (30); i++)
            {
                int index = (i + offset);
                if (index < colors.Count && index > 0)
                {
                    Color chosenColor = colors[index];
                    Raylib.DrawRectangle(10, 50 + (i * 20), 20, 20, chosenColor);
                    //if mouse over the color draw it bigger and if clicked
                    if (Raylib.GetMouseX() > 10 && Raylib.GetMouseX() < 30 && Raylib.GetMouseY() > 50 + (i * 20) && Raylib.GetMouseY() < 70 + (i * 20))
                    {
                        Raylib.DrawRectangle(10, 50 + (i * 20), 40, 40, chosenColor);
                        //draw a hint
                        if (Input.GetMouseButtonDown(MouseButton.Left))
                        {
                            selColour = chosenColor.B;
                            openedSubGreen = true;
                        }
                    }
                    //if opened green render green colour next to it with that blue;
                    if (openedSubGreen)
                    {
                        if (chosenColor.B == selColour)
                        {
                            //individual green colour wheel offset just to not confuse ourselves. probably couldve used the original one but oh well
                            if (openedSubRed == false)
                            {
                                float mouseGreenOffset = Input.GetMouseWheel();
                                if (goffset >= 0 && goffset < colors.Count) { goffset += (int)mouseGreenOffset; }
                                else if (goffset <= 0) { goffset = 0; }
                                else if (goffset >= colors.Count) { goffset = colors.Count - 1; }
                            }

                            for (int g = 0; g < 30; g++)
                            {
                                int gIndex = (g + goffset);
                                //Draw the color to the goffset
                                if (gIndex < 255 && gIndex > 0)
                                {
                                    Color greenColor = new Color(0, gIndex, chosenColor.B, 255);
                                    Raylib.DrawRectangle(20 + (g * 20), 50 + (i * 20), 20, 20, greenColor);
                                    //draw a hint when mouse over
                                    if (openedSubRed == false)
                                    {
                                        //dont do anything till off the blue colour
                                        if (Raylib.GetMouseX() > 10 && Raylib.GetMouseX() < 30 && Raylib.GetMouseY() > 50 + (i * 20) && Raylib.GetMouseY() < 70 + (i * 20))
                                        {
                                            Raylib.DrawText("Scroll to select", 55, 50 + (i * 20), 20, Color.Black);
                                        }
                                        else
                                        {
                                            if (Raylib.GetMouseX() > 20 + (g * 20) && Raylib.GetMouseX() < 40 + (g * 20) && Raylib.GetMouseY() > 50 + (i * 20) && Raylib.GetMouseY() < 70 + (i * 20))
                                            {
                                                Raylib.DrawRectangle(20 + (g * 20), 50 + (i * 20), 40, 40, new Color(0, g, chosenColor.B, 255));
                                                if (Input.GetMouseButtonDown(MouseButton.Left))
                                                {
                                                    selColourG = greenColor.G;
                                                    openedSubRed = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //render the Red colours
                                        if (greenColor.G == selColourG)
                                        {
                                            if (roffset >= 0 && roffset < colors.Count) { roffset += (int)Input.GetMouseWheel(); }
                                            else if (roffset <= 0) { roffset = 0; }
                                            else if (roffset >= colors.Count) { roffset = colors.Count - 1; }
                                            for (int r = 0; r < 30; r++)
                                            {
                                                int rIndex = (r + roffset);
                                                if (rIndex < 255 && rIndex > 0)
                                                {
                                                    Color redColor = new Color(rIndex, selColourG, chosenColor.B, 255);
                                                    Raylib.DrawRectangle(20 + (g * 20), 50 + (i * 20) + (r * 20), 20, 20, redColor);
                                                    //make sure out of the green before making a selection
                                                    if (Raylib.GetMouseX() > 20 + (g * 20) && Raylib.GetMouseX() < 40 + (g * 20) && Raylib.GetMouseY() > 50 + (i * 20) && Raylib.GetMouseY() < 70 + (i * 20))
                                                    {
                                                        Raylib.DrawText("Scroll to select", 55, 50 + (i * 20), 20, Color.Black);
                                                    }
                                                    else
                                                    {
                                                        if (Raylib.GetMouseX() > 20 + (g * 20) && Raylib.GetMouseX() < 40 + (g * 20) && Raylib.GetMouseY() > 50 + (i * 20) + (r * 20) && Raylib.GetMouseY() < 70 + (i * 20) + (r * 20))
                                                        {
                                                            Raylib.DrawRectangle(20 + (g * 20), 50 + (i * 20) + (r * 20), 40, 40, new Color(rIndex, selColourG, chosenColor.B, 255));
                                                            if (Input.GetMouseButtonDown(MouseButton.Left))
                                                            {
                                                                selColourR = redColor.R;
                                                                //close this prompt?
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Reset()
        {
            selColour = 0;
            selColourG = 0;
            selColourR = 0;
            openedSubGreen = false;
            openedSubRed = false;
            goffset = 1;
            roffset = 1;
            offset = 1;
        }
    }
}