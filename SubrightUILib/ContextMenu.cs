using System;
using System.Collections.Generic;
using Raylib_cs;
using SCPBreakdown.EngineStuff;
using Color = SCPBreakdown.EngineStuff.Color;

namespace SubrightEngine2.UI
{
    [Serializable]
    public class ContextMenu : GameObject
    {
        public bool hideRender = false;
        private readonly List<ContextItem> items = new List<ContextItem>();

        public ContextMenu(List<ContextItem> itemsOverride) : base(Vector3.zero, Vector3.zero, "Context Menu")
        {
            if (itemsOverride != null) items.AddRange(itemsOverride);
            SetupMenu();
        }

        public void SetupMenu()
        {
            var minMaxX = 0;
            //Check if something is avaliable if not idk. 
            for (var i = 0; i < items.Count; i++)
            {
                var window = items[i];
                if (window.name.Length * 8 > minMaxX) minMaxX = window.name.Length * 8;
            }
            size.X = minMaxX;
            size.Y = items.Count * 16 + 16;
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            DrawContext();
        }

        public void DrawContext()
        {
            if (hideRender == false)
            {
                DrawRectangle(position.X, position.Y, size.X, size.Y, Color.GRAY);
                DrawRectangleLines(position.X, position.Y, size.X, size.Y, Color.LIGHTGRAY);

                if (items.Count > 0)
                    for (var i = 0; i < items.Count; i++)
                    {
                        var wind = items[i];
                        DrawText(wind.name, position.X + 5, position.Y + 8 + i * 16, 8, Color.WHITE);
                        if (Raylib.GetMouseX() >= position.X + 5 && Raylib.GetMouseX() <= position.X + size.X &&
                            Raylib.GetMouseY() >= position.Y + 8 + i * 16 &&
                            Raylib.GetMouseY() <= position.Y + 8 + i * 16 + 8)
                        {
                            DrawRectangleLines(position.X + 2, position.Y + 8 + i * 16 - 2, size.X - 5, 12,
                                Color.LIGHTGRAY);
                            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) wind.Execute();
                        }
                    }
                else
                    Debug.Log("Huh no items here? for this context menu?");
            }
        }

        public bool itemExists(string name)
        {
            for (var i = 0; i < items.Count; i++)
                if (items[i].name == name)
                    //choose this one
                    return true;
            return false;
        }

        public void addItem(ContextItem item)
        {
            if (itemExists(item.name))
            {
                if (SCPBreakdown.Program.debug) Debug.Log("This context item already exists!");
            }
            else
            {
                items.Add(item);
            }
        }

        public void removeItem(ContextItem item)
        {
            if (itemExists(item.name))
            {
                items.Remove(item);
            }
            else
            {
                if (SCPBreakdown.Program.debug) Debug.Log("This item doesnt exist!");
            }
        }
    }
}