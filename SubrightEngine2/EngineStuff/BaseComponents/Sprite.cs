using System.IO;
using Raylib_cs;
using SubrightEngine2.EngineStuff.BaseComponents;

namespace SubrightEngine2.EngineStuff
{
    public class Sprite : Component
    {
        public Texture2D containedSprite;

        public void LoadSprite(string loadPath)
        {
            if (File.Exists(loadPath))
            {
                containedSprite = Raylib.LoadTexture(loadPath);
            }
            else
            {
                Debug.Log("This path doesnt exist!");
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
                Raylib.DrawTexture(containedSprite, (int) connectedObject.position.X, (int) connectedObject.position.Y,
                    Raylib_cs.Color.WHITE);
            }
        }
    }
}