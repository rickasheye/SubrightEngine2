using Raylib_cs;
using System;
using System.IO;

namespace SubrightEngine2.EngineStuff.BaseComponents._2DComponents
{
    public class SpriteRenderer : Component
    {
        public string spritepath = "";
        public Sprite spriteContained = null;
        public Color tint = Color.White;
        public Vector2 offset;
        public float rot;

        public SpriteRenderer(string name) : base("Sprite Renderer")
        {
            this.name = name;
            this.spritepath = "";
        }

        public SpriteRenderer() : base("Sprite Renderer")
        {
            this.name = "Untitled Sprite Renderer";
        }

        public void CreateBlankSprite(int width, int height, Raylib_cs.Color color)
        {
            spriteContained = new Sprite("Blank Sprite", "");
            //use raylib to create a new sprite to use from the width and height arguments it can be blank
            spriteContained.containedSprite = Raylib.LoadTextureFromImage(Raylib.GenImageColor(width, height, color));
        }

        public SpriteRenderer(string name, string path) : base("Sprite Renderer")
        {
            this.spritepath = path;
            if (spritepath != null && spritepath != "")
            {
                if (File.Exists(spritepath))
                {
                    //try to load that sprite
                    spriteContained = new Sprite(name, spritepath);
                }
                else
                {
                    Debug.LogError("Unfortunately this doesnt work as the file: " + spritepath + " cannot be found!");
                    connectedObject.RemoveComponent(this);
                }
            }
        }

        private bool spriteWarning = false;

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (spritepath != "" && spritepath != null)
            {
                if (connectedObject != null)
                {
                    if (spriteContained.containedSprite.Width != 0)
                    {
                        if (enabled == true)
                        {
                            Raylib.DrawTextureEx(spriteContained.containedSprite, new System.Numerics.Vector2((int)offset.X + (int)connectedObject.position.X, (int)offset.Y + (int)connectedObject.position.Y), rot, 1, tint.ToRaylibColor);
                        }
                        else
                        {
                            //even though this might not even execute we want to unload the sprite.
                            spriteContained.UnloadSprite();
                        }
                    }
                    else
                    {
                        if (enabled == true)
                        {
                            //sprite may not be loaded properly
                            Debug.Log("sprite may have not loaded correctly reloading..");
                            spriteContained.path = spritepath;
                            spriteContained.LoadSprite(name, spriteContained.path);
                            spriteWarning = false;
                        }
                        else
                        {
                            if (spriteWarning == false)
                            {
                                Debug.LogWarning("Sprite is not loaded and the component is disabled!");
                                spriteWarning = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Raylib.DrawText("NO SPRITE LOADED", 10, 10, 20, Raylib_cs.Color.White);
            }
        }

        public void RotateTowardsVector2(Vector2 pos)
        {
            Vector2 direction = pos - connectedObject.position.ToVector2;
            float angle = (float)Math.Atan2(direction.Y, direction.X);
            rot = angle * (180 / (float)Math.PI);
        }
    }
}