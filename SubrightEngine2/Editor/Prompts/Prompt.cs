using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents._2DComponents;
using Rectangle = Raylib_cs.Rectangle;

namespace SubrightEngine2.Editor.Prompts
{
    public class Prompt : GameObject
    {
        //Seperate prompt asked by the user to load files or do something else. this is usually called by the editor
        public int width = 200;

        public int height = 150;
        public SpriteRenderer render;
        public bool renderTitleBar = true;
        public bool hide = false;

        public override void Start()
        {
            base.Start();
            position = new Vector3(100, 100, 0);
            //get us a rendertexture or a sprite setup to draw in.
            render = new SpriteRenderer();
            render.CreateBlankSprite(width, height, Raylib_cs.Color.White);
            render.spriteContained.ReSizeSprite(new Vector2(width, height));
        }

        //prevent from calling resize function everytime. when we dont need it.
        private int tempstorewidth = 0;

        private int tempstoreheight = 0;

        public override void Draw2D(ref Camera2D cam)
        {
            if (!hide)
            {
                base.Draw2D(ref cam);
                if (renderTitleBar)
                {
                    if (tempstorewidth != width && tempstoreheight != height)
                    {
                        render.spriteContained.ReSizeSprite(new Vector2(width, height));
                        tempstoreheight = height;
                        tempstorewidth = width;
                    }
                    Raylib.DrawRectangle((int)position.X, (int)position.Y, width, height, Raylib_cs.Color.White);
                    //draw a rectangle behind title icons
                    Raylib.DrawRectangle((int)position.X, (int)position.Y - 20, width, 20, Raylib_cs.Color.Gray);
                    //draw window title
                    Raylib.DrawText(name, (int)position.X, (int)position.Y - 22, 20, Raylib_cs.Color.Black);
                    //draw window icons exit etc
                    if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(position.X + width - 20, position.Y, 20, 20)))
                    {
                        Raylib.DrawRectangle((int)position.X + width - 20, (int)position.Y - 20, 20, 20, Raylib_cs.Color.Red);
                        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                        {
                            Destroy(this);
                        }
                    }
                    else
                    {
                        Raylib.DrawRectangle((int)position.X + width - 20, (int)position.Y - 20, 20, 20, Raylib_cs.Color.Black);
                    }
                }
            }
        }

        public override void Update(ref Camera2D cam)
        {
            if (!hide)
            {
                base.Update(ref cam);
            }
        }

        public void ClosePrompt()
        {
            Destroy(this);
        }
    }
}