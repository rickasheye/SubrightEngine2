using Raylib_cs;

namespace SubrightEngine2.UI.Notifications
{
    public class Notification
    {
        public string name;
        public string description;
        public bool shown = false;

        public Notification(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public void RenderNotification()
        {
            Raylib.DrawRectangle(10, 10, Raylib.GetScreenWidth() / 8, Raylib.GetScreenHeight() / 16, Raylib_cs.Color.GRAY);
            Raylib.DrawText(name, 12, 12, 8, Raylib_cs.Color.WHITE);
            Raylib.DrawText(description, 12, 22, 8, Raylib_cs.Color.WHITE);
            Raylib.DrawText("X", Raylib.GetScreenWidth() / 8 + 12, 12, 8, Raylib_cs.Color.RED);
            int mouseX = Raylib.GetMouseX();
            int mouseY = Raylib.GetMouseY();
            if (mouseX > Raylib.GetScreenWidth() / 8 + 12 && mouseY > 12 && mouseX < Raylib.GetScreenWidth() / 8 + 20 &&
                mouseY < 20)
            {
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    //mouse button is down
                    shown = true;
                }
            }
        }

        public static Notification addNotification(string name, string description)
        {
            Notification notif = new Notification(name, description);
            NotificationHandler.notifications.Add(notif);
            return notif;
        }
    }
}