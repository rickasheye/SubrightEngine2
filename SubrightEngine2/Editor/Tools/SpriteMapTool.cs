using Raylib_cs;
using SubrightEngine2.Editor.Prompts;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;
using System;
using Color = Raylib_cs.Color;

#if DEBUG
namespace SubrightEngine2.Editor.Tools
{
    public class SpriteMapTool : SelectorWindow
    {
        public SpriteMapTool()
        {
            name = "Sprite Map Tool";
        }

        public override void LoadScene()
        {
            base.LoadScene();
            AddPrompt(new PromptFileExplorer());
        }

        private string fileLoad = "";
        private string tempfileload = "";

        public override void UpdateScene(ref Camera2D cam)
        {
            base.UpdateScene(ref cam);
            Raylib.DrawText(name, 10, 10, 20, Color.Black);

            //draw the spritemap recently loaded.
            //maybe pop up the file dialog when avaliable but we dont have a file dialog

            if (fileLoad == "")
            {
                Raylib.DrawText("No file loaded please enter in a file name from the " + Environment.CurrentDirectory + "Assets/Sprites/ folder", 10, 40, 20, Color.Black);
                Raylib.DrawRectangle(10, 60, 500, 20, Color.Black);
                //draw text from keyboard input inside
                Raylib.DrawText("File Name: " + tempfileload, 10, 60, 20, Color.White);
                if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
                {
                    if (tempfileload.Length > 0)
                    {
                        tempfileload = tempfileload.Remove(tempfileload.Length - 1);
                    }
                }
                else if (Raylib.IsKeyPressed(KeyboardKey.Enter))
                {
                    //our file has been entered load it.
                    fileLoad = tempfileload;
                    //load the file into memory to later render it. create a game object :(
                    GameObject newObject = new GameObject("rendermap");
                    newObject.AddComponent(new SpriteRenderer("Assets/Sprites/" + fileLoad));
                    newObject.position = new Vector3(40, 40, 0);
                    newObject.AddComponent(new SpriteMapManipulator());
                }
                else if (Raylib.GetKeyPressed() != 0)
                {
                    tempfileload += (KeyboardKey)Raylib.GetKeyPressed();
                }
            }
        }
    }
}
#endif