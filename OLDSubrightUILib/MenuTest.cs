using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.Scenes;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole.Graphical.ScenesAvaliable
{
    public class MenuTest : SceneUI
    {
        public MenuTest() : base("Menu Test") { }
        Text text;
        Button buttonTest;
        TextBox textBoxTest;
        ToggleBox toggle;
        KeyboardAdjustedButton kab;
        EmptyContainer container;

        public override void LoadScene()
        {
            base.LoadScene();
            text = new Text("Menu Test", new Vector2(10, 10), 40, Color.BLACK);
            buttonTest = new Button("Menu Test", new Vector2(10, 60), new Vector2(40, 40), 30, Color.GRAY, Color.DARKGRAY, Color.BLACK);
            textBoxTest = new TextBox("", new Vector2(10, 110), new Vector2(40, 40), 30, Color.GRAY, Color.BLACK);
            toggle = new ToggleBox("yes", new Vector2(10, 160), new Vector2(20, 20));
            container = new EmptyContainer(new Vector2(10, 10), new Vector2(Raylib.GetScreenWidth() - 10, Raylib.GetScreenHeight() - 10));
            kab = new KeyboardAdjustedButton("Keyboard Adjusted Button", new Vector2(80, 20), new Vector2(10, 190));
            guiOptions.Add(text);
            guiOptions.Add(buttonTest);
            guiOptions.Add(textBoxTest);
            guiOptions.Add(toggle);
            guiOptions.Add(kab);
            container.children.AddRange(guiOptions);
            guiOptions.Add(container);
        }

        public override void UpdateScene(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.UpdateScene(ref cam2, ref cam3);
        }
    }
}
