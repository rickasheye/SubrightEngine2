using System.Collections.Generic;
using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace SubrightEngine2.UI.Notifications
{
    public class NotificationHandler : GameObject
    {
        public NotificationHandler():base(Vector3.zero, Vector3.zero, "NotificationHandler"){}

        public static List<Notification> notifications = new List<Notification>();
        public int index = 0;
        
        public override void Start()
        {
            base.Start();
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw2D(ref cam2);
        }

        private int lastindex = 0;
        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //check if the draw2d is valid ?
            if (notifications != null)
            {
                if (index < notifications.Count && index > 0)
                {
                    if (notifications[index].shown == false)
                    {
                        notifications[index].RenderNotification();
                    }
                    else
                    {
                        if (index + 1 < notifications.Count)
                        {
                            lastindex = index + 1;
                            index++;
                        }
                        else
                        {
                            if (notifications.Count - 1 > lastindex)
                            {
                                index++;
                            }
                        }
                    } 
                }
            }
        }
    }
}