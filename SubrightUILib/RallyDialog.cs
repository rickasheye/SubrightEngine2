using System.Collections.Generic;
using System.Runtime.InteropServices;
using Raylib_cs;
using Debug = SubrightEngine2.EngineStuff.Debug;

namespace SubrightEngine2.UI
{
    public static class RallyDialog
    {
        //This class looks after the dialog boxes to make sure theres not too many
        //only show one at a time

        public static List<Dialog> dialogs = new List<Dialog>();

        public static int index = 0;
        
        /// <summary>
        /// Usually put in a update method...
        /// </summary>
        public static void RollDialogs(ref Camera2D cam, ref Camera3D cam3)
        {
            if (index > 0 && dialogs != null && index < dialogs.Count)
            {
                if (dialogs[index].hideRender == false)
                {
                    index++;
                    dialogs.Remove(dialogs[index - 1]);
                }
                dialogs[index].Update(ref cam, ref cam3);
            }
        }
        
        public static void CreateDialog(Dialog log)
        {
            if (dialogExists(log))
            {
                Debug.Log("Dialog already exists!");
            }
            else
            {
                //create the dialog...
                dialogs.Add(log);
            }
        }

        public static void RemoveDialog(Dialog log)
        {
            if (!dialogExists(log))
            {
                Debug.Log("Dialog doesnt exist!");
            }
            else
            {
                //remove the dialog...
                dialogs.Remove(log);
            }
        }

        public static bool dialogExists(Dialog dialog)
        {
            for (int i = 0; i < dialogs.Count; i++)
            {
                Dialog log = dialogs[i];
                if (log.name == dialog.name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}