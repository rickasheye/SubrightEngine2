using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.Scenes;
using SubrightEngine2.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightUITest
{
    public class SceneUITEST : Scene
    {
        //Scene 01 UI Test

        public SceneUITEST() : base("SceneUITest") { }

        public override void LoadScene()
        {
            base.LoadScene();
            GameObject Container = GameObject.AssembleObject(new SubrightEngine2.UI.Container(new Vector2(10, 10), new Vector2(Raylib.GetScreenWidth() - 20, Raylib.GetScreenHeight() - 20), new Vector2(100, 100), 10));
            SubrightEngine2.UI.EngineButton button = new SubrightEngine2.UI.EngineButton("New button");
            button.size = new Vector2(100, 100);
            button.position = new Vector2(10, 10);
            Container.AddChild(GameObject.AssembleObject(button));
            SubrightEngine2.UI.EngineTextBox textbox = new SubrightEngine2.UI.EngineTextBox("New textbox");
            textbox.size = new Vector2(100, 100);
            textbox.position = new Vector2(120, 120);
            Container.AddChild(GameObject.AssembleObject(textbox));
            SubrightEngine2.UI.EngineTextButton textbutton = new SubrightEngine2.UI.EngineTextButton("New textbutton", "texbutton sample");
            textbutton.size = new Vector2(100, 100);
            textbutton.position = new Vector2(220, 220);
            Container.AddChild(GameObject.AssembleObject(textbutton));
            SubrightEngine2.UI.EngineToggle toggle = new SubrightEngine2.UI.EngineToggle("New toggle");
            toggle.size = new Vector2(100, 100);
            toggle.position = new Vector2(320, 320);
            Container.AddChild(GameObject.AssembleObject(toggle));
            SubrightEngine2.UI.EngineToggleText textToggle = new SubrightEngine2.UI.EngineToggleText("New text toggle");
            textToggle.size = new Vector2(100, 100);
            textToggle.position = new Vector2(420, 420);
            Container.AddChild(GameObject.AssembleObject(textToggle));
            SubrightEngine2.UI.Container con = (SubrightEngine2.UI.Container)Container.GetComponent("UIContainer");
            //con.UpdatePositionOfObjects();
            GameObjects.Add(Container);
            //Debug.Log("" + GameObjects.Count);
        }

        public override void UpdateScene(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.UpdateScene(ref cam2, ref cam3);

            //maybe pop some debug info?
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                if (SubrightEngine2.Program.debug)
                {
                    SubrightEngine2.Program.debug = false;
                }
                else
                {
                    SubrightEngine2.Program.debug = true;
                }
                Debug.Log("" + SubrightEngine2.Program.debug);
            }
        }
    }
}
