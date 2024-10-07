using Raylib_cs;
using System;
using System.Collections.Generic;
using System.IO;

namespace SubrightEngine2.EngineStuff.Scenes
{
    //Appears to be obselete i do not remember writing this lmao.
    [Serializable]
    public class Asset
    {
        public string name;
        public string path;

        [NonSerialized]
        public Texture2D texture;

        public Asset(string name, string path)
        {
            this.name = name;
            this.path = path;
            ReloadTexture();
        }

        public void ReloadTexture()
        {
            Image image = Raylib.LoadImage(path);
            Raylib.ImageResizeNN(ref image, 64, 64);
            Texture2D texture = Raylib.LoadTextureFromImage(image);
            Raylib.UnloadImage(image);
            this.texture = texture;
        }
    }

    [Serializable]
    public class AssetLoader
    {
        public List<Asset> texturesCached = new List<Asset>();

        public void UnloadAll()
        {
            foreach (Asset texture in texturesCached)
            {
                if (texture.name != string.Empty)
                {
                    Raylib.UnloadTexture(texture.texture);
                }
            }
            texturesCached.Clear();
        }

        //used to load all of the assets into game but since we have no assets yet nothing to load.
        public Texture2D textureLoad(string path)
        {
            try
            {
                if (!TextureExist(path))
                {
                    TextureCreate(Path.GetFileName(path), path);
                    return textureLoad(path);
                }
                else
                {
                    return getTexture(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e);
                return new Texture2D();
            }
        }

        public Asset TextureCreate(string name, string path)
        {
            if (!TextureExist(path))
            {
                Asset newTexure = new Asset(name, path);
                texturesCached.Add(newTexure);
                Debug.Log("Created a new texture by the name of: " + name);
                return newTexure;
            }
            else
            {
                Debug.Log("Texture already exists by the name of: " + name + "so unable to create a new storedTexture!");
                return null;
            }
        }

        public Texture2D getTexture(string path)
        {
            foreach (Asset texturem in texturesCached)
            {
                if (texturem.path == path)
                {
                    return texturem.texture;
                }
            }
            return new Texture2D();
        }

        public void TextureRemove(string path)
        {
            if (TextureExist(path))
            {
                for (int i = 0; i < texturesCached.Count; i++)
                {
                    if (path == texturesCached[i].path)
                    {
                        texturesCached.RemoveAt(i);
                    }
                }
            }
            else
            {
                Debug.Log("Texture doesnt exist to be removed by this protocol?");
            }
        }

        public bool TextureExist(string path)
        {
            foreach (Asset text in texturesCached)
            {
                if (text.path == path)
                {
                    return true;
                }
            }
            return false;
        }
    }
}