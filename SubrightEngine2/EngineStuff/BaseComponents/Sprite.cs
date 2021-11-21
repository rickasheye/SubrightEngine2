using Raylib_cs;
using SubrightEngine2.EngineStuff.BaseComponents;
using System;
using System.IO;

namespace SubrightEngine2.EngineStuff
{
    public class Sprite : Component
    {
        [NonSerialized] public Texture2D containedSprite;

        public string path = "";

        public void LoadSprite(string loadPath)
        {
            this.path = loadPath;
            StartRender(path);
        }

        public void StartRender(string path)
        {
            if (File.Exists(path))
            {
                Debug.Log(path);
                Image image = Raylib.LoadImage(path);
                if (image.width != 16 && image.height != 16)
                {
                    Raylib.ImageResize(ref image, 16, 16);
                }
                Texture2D storedSprite = Raylib.LoadTextureFromImage(image);
                Raylib.UnloadImage(image);
                containedSprite = storedSprite;
            }
            else
            {
                //Unfortunately this doesnt work
                Debug.LogError("Unfortunately this doesnt work as the file: " + path + " cannot be found!");
            }
        }

        public Sprite(string name, string path) : base(name)
        {
            //run
            LoadSprite(path);
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //Draw image
            if (containedSprite.width != 0 && containedSprite.height != 0)
            {
                Raylib.DrawTexture(containedSprite, (int)connectedObject.position.X, (int)connectedObject.position.Y, Raylib_cs.Color.WHITE);
            }
            else
            {
                StartRender(path);
            }
        }
    }
}