using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.Scenes;
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
            GameObject Container = GameObject.AssembleObject(new SubrightEngine2.UI.Container(new Vector2(10, 10), new Vector2(Raylib.GetScreenWidth() - 10, Raylib.GetScreenHeight() - 10), new Vector2(10, 10), 10));
            SubrightEngine2.UI.EngineButton button = new SubrightEngine2.UI.EngineButton("New button");
            Container.AddChild(GameObject.AssembleObject(button));
            SubrightEngine2.UI.EngineTextBox textbox = new SubrightEngine2.UI.EngineTextBox("New textbox");
            Container.AddChild(GameObject.AssembleObject(textbox));
            SubrightEngine2.UI.EngineTextButton textbutton = new SubrightEngine2.UI.EngineTextButton("New textbutton", "texbutton sample");
            Container.AddChild(GameObject.AssembleObject(textbutton));
            SubrightEngine2.UI.EngineToggle toggle = new SubrightEngine2.UI.EngineToggle("New toggle");
            Container.AddChild(GameObject.AssembleObject(toggle));
            SubrightEngine2.UI.EngineToggleText textToggle = new SubrightEngine2.UI.EngineToggleText("New text toggle");
            Container.AddChild(GameObject.AssembleObject(textToggle));
            SubrightEngine2.UI.Container con = (SubrightEngine2.UI.Container)Container.GetComponent("UIContainer");
            con.UpdatePositionOfObjects();
        }

        public override void UpdateScene(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.UpdateScene(ref cam2, ref cam3);
        }
    }
}
