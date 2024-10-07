using Raylib_cs;
using System;
using System.IO;

namespace SubrightEngine2.EngineStuff.BaseComponents._2DComponents
{
    public class Sprite
    {
        public string path = "";
        public string name = "Untitled Sprite";
        public Texture2D containedSprite = new Texture2D();
        public Raylib_cs.Color color;
        public bool spriteUnloaded = false;

        public void LoadSprite(string name, string loadPath)
        {
            this.path = loadPath;
            this.name = name;
            if (loadPath != null && loadPath != "")
            {
                //try to load that sprite
                containedSprite = StartRender(loadPath);
                if (containedSprite.Width > 0)
                {
                    spriteUnloaded = false;
                }
                Debug.Log("Found and using sprite: " + name + " at path: " + loadPath);
            }
            else
            {
                Debug.Log("Unable to load sprite as the path is null or empty!");
            }
        }

        public void UnloadSprite()
        {
            Raylib.UnloadTexture(containedSprite);
            spriteUnloaded = true;
            Debug.Log("Unloaded sprite: " + name);
        }

        public Sprite(string name, string loadpath)
        {
            LoadSprite(name, loadpath);
        }

        public Texture2D StartRender(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    Debug.Log(path);
                    Image image = Raylib.LoadImage(path);
                    Texture2D storedSprite = Raylib.LoadTextureFromImage(image);
                    Raylib.UnloadImage(image);
                    Raylib.SetTextureFilter(storedSprite, TextureFilter.Point);
                    return storedSprite;
                }
                else
                {
                    //Unfortunately this doesnt work
                    Debug.LogError("Unfortunately this doesnt work as the file: " + path + " cannot be found!");
                    return new Texture2D();
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Unfortunately this doesnt work as the file: " + path + " cannot be found!");
                Debug.LogError(e.Message);
                return new Texture2D();
            }
        }

        public void ReSizeSprite(Vector2 newSize)
        {
            try
            {
                //grab the sprite and resize.
                Image image = Raylib.LoadImageFromTexture(containedSprite);
                Raylib.SetTextureFilter(Raylib.LoadTextureFromImage(image), TextureFilter.Point);
                Raylib.ImageResizeNN(ref image, (int)newSize.X, (int)newSize.Y);
                containedSprite = Raylib.LoadTextureFromImage(image);
                Raylib.UnloadImage(image);
                Raylib.SetTextureFilter(containedSprite, TextureFilter.Point);
                Debug.Log(name + " has been resized to: " + newSize.X + "x" + newSize.Y);
            }
            catch (Exception e)
            {
                Debug.LogError("Unable to resize sprite: " + name + " to: " + newSize.X + "x" + newSize.Y);
                Debug.LogError(e.Message);
            }
        }

        public void SetHighlightColor(Raylib_cs.Color color)
        {
            //sets the highlight color of the sprite
            try
            {
                //get the image
                Image image = Raylib.LoadImageFromTexture(containedSprite);
                //get each color of the image
                Raylib.ImageColorTint(ref image, color);
                containedSprite = Raylib.LoadTextureFromImage(image);
                Raylib.UnloadImage(image);
                this.color = color;
            }
            catch (Exception e)
            {
                Debug.LogError("Unable to set highlight color of sprite: " + name + " to: " + color.ToString());
                Debug.LogError(e.Message);
            }
        }
    }
}