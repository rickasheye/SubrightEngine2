using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using RPGConsole.Saving;
using SubrightEngine2.EngineStuff;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole.Graphical.ScenesAvaliable
{
    public class SavesMenu : Scene
    {
        Text text;
        EmptyContainer container;

        public SavesMenu() : base("Saves Menu")
        {

        }

        public override void LoadScene()
        {
            base.LoadScene();
            text = new Text("Saves Menu", new Vector2(10, 10), 40, Color.BLACK);
            container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
            guiOptions.Add(text);
            int saveFilesCount = Reference.manager.savefiles.Count;
            if (saveFilesCount > 0)
            {
                for (int i = 0; i < saveFilesCount; i++)
                {
                    SaveButton button = new SaveButton(Reference.manager.savefiles[i].fileName, new Vector2(10, (10 * i) + 60), new Vector2(50, 50), Reference.manager.savefiles[i]);
                    guiOptions.Add(button);
                }
            }
            else
            {
                Text newText = new Text("Unable to load all avaliable items!", new Vector2(10, 60), 1, Color.BLACK);
                guiOptions.Add(newText);
            }
            container.children.AddRange(guiOptions);
            guiOptions.Add(container);
        }

        public override void UpdateScene(Camera2D cam)
        {
            base.UpdateScene(cam);
        }
    }

    public class SaveButton : KeyboardAdjustedButton
    {
        public CombinedSaveFile saveFileSaved;

        public SaveButton(string text, Vector2 position, Vector2 size, CombinedSaveFile saveFile) : base(text, size, position) { this.saveFileSaved = saveFile; }

        public override void Triggerable()
        {
            base.Triggerable();
            if (saveFileSaved != null)
            {
                Reference.manager.LoadFile(saveFileSaved, ref Reference.gen, ref Reference.player);
            }
        }

        public override void DrawObject()
        {
            base.DrawObject();
            if (saveFileSaved == null)
            {
                //we want to draw that it is disabled!
                Raylib.DrawText("Disabled!", (int)position.X - 5, (int)position.Y - 5, ((int)size.X / (int)size.Y) * 2, Color.RED.ToRaylibColor);
            }
        }
    }
}
