using Raylib_cs;
using System;
using System.Collections.Generic;
using Debug = SubrightEngine2.EngineStuff.Debug;

namespace SubrightEngine2.Editor.Prompts
{
    public class PromptFileExplorer : Prompt
    {
        public string directiveaddress = "";
        private List<string> files_ = new List<string>();

        public PromptFileExplorer()
        {
            name = "File Explorer";
        }

        public override void Start()
        {
            //setup the file explorer.
            base.Start();
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //draw the file explorer.
            if (string.IsNullOrEmpty(directiveaddress) || files_.Count <= 0)
            {
                Raylib.DrawText("No address given or no files present in this directory.", (int)position.X, (int)position.Y, 20, Raylib_cs.Color.Black);
            }
            else
            {
                //draw all the files and directories given.
                for (int i = 0; i < files_.Count; i++)
                {
                    //find way to optimize this.
                    Image renderImage = Raylib.LoadImageFromTexture(render.spriteContained.containedSprite);
                    Raylib.ImageDrawText(ref renderImage, files_[i], (int)position.X, (int)position.Y + (i * 20), 20, Raylib_cs.Color.Black);
                    render.spriteContained.containedSprite = Raylib.LoadTextureFromImage(renderImage);
                }
            }
        }

        public void verifyAddress(string givenAddress)
        {
            //verify the address and if it is a file or folder.
            directiveaddress = givenAddress;
            if (string.IsNullOrEmpty(directiveaddress))
            {
                Debug.WriteLine("Address is empty");
                return;
            }
            else
            {
                if (System.IO.Directory.Exists(directiveaddress))
                {
                    //verified our directory exists. now we can list all the files in the directory.
                    string[] files = System.IO.Directory.GetFiles(directiveaddress);
                    foreach (string file in files)
                    {
                        Debug.WriteLine("GOT FILE " + file);
                        files_.Add(file);
                    }
                }
                else
                {
                    Console.WriteLine("Directory does not exist, or possibly a file. Expected a directory.");
                }
            }
        }
    }
}