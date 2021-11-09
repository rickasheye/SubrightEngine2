using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using RPGConsole.InventoryBlock;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Random = SubrightEngine2.EngineStuff.Random;

namespace RPGConsole.Saving
{
    public class SaveFileManager
    {
        public List<CombinedSaveFile> savefiles = new List<CombinedSaveFile>();

        public SaveFileManager()
        {
            //save the files
            if (Reference.debugMode == true) { Debug.Log("Save File Manager loaded!"); }
            if(!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "saves/")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "saves/"));
            }

            foreach (string m in Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "saves/")))
            {
                if (File.Exists(m))
                {
                    //then is file
                    CombinedSaveFile saveFile = new CombinedSaveFile();
                    saveFile.LoadFile(m);
                    foreach (Block moo in saveFile.generator)
                    {
                        if (moo == null || moo.name == "")
                        {
                            saveFile.generator.Remove(moo);
                        }
                    }
                }
            }
        }

        public bool SaveFiles(CombinedSaveFile saveFile)
        {
            if (Reference.debugMode)
            {
                Debug.Log("Saving file...");
            }

            if (Reference.debugMode) { Debug.Log("Saving individual player profile..."); }
            if (FileExists(saveFile))
            {
                return LoadFile(saveFile, ref Reference.gen, ref Reference.player);
            }
            else
            {
                saveFile.fileName = "saveFile" + Random.Range(5000) + ".json";
                saveFile.SaveFile(Path.Combine(Environment.CurrentDirectory, "saves/" + saveFile.fileName), Reference.player, Reference.gen);
                Debug.Log("Saved file successfully");
                return true;
            }
        }

        public bool SaveFile()
        {
            return SaveFiles(new CombinedSaveFile());
        }

        public CombinedSaveFile getTopmostFile()
        {
            return savefiles[0]; 
        }

        public void DeleteFile(CombinedSaveFile saveFile)
        {
            if (!FileExists(saveFile))
            {
                Debug.Log("Unfortunately this save file doesnt exist?");
            }
            else
            {
                string path = Path.Combine(Environment.CurrentDirectory, "saves/saveFile" + saveFile.fileName);
                if (File.Exists(path))
                {
                    Debug.Log("OMG this actually file exists? yay! now lets delete it " + path);
                    File.Delete(path);
                }
                else
                {
                    Debug.Log("This file doesnt exist great! " + path);
                }
                //actually do it my way which is taxing on cpu cycles but just do it
                for(int i = 0; i < savefiles.Count; i++)
                {
                    if(savefiles[i] == saveFile)
                    {
                        savefiles.RemoveAt(i);
                    }
                }
                Debug.Log("Finished the removal cycle now this is deleted!");
                //actually remove the file located for it or well atleast above this part!
            }
        }

        public bool LoadFile(CombinedSaveFile file, ref Generator genInstance, ref Player playerInstance)
        {
            if (FileExists(file))
            {
                Console.WriteLine(file.fileName);
                string path = Path.Combine(Environment.CurrentDirectory, "saves/" + file.fileName);
                if (File.Exists(path))
                {
                    Reference.gen.blockMap = file.generator;
                    Reference.player = file.playerFile;
                    Debug.Log("Sucessfully loaded save file!");
                    return true;
                    //bc under here is unacessable!
                }
                else
                {
                    Debug.Log("Unfortunately this file failed to exist? so recreating?");
                    SaveFiles(file);
                    return LoadFile(file, ref genInstance, ref playerInstance);
                }
                //unreachable code here
                return false;
            }
            else
            {
                Debug.Log("Unfortunately this file doesnt exist in the directory? but creating a new one anyway!");
                SaveFiles(file);
                return LoadFile(file, ref genInstance, ref playerInstance);
            }
        }

        public bool FileExists(CombinedSaveFile saveFile)
        {
            if (savefiles.Count > 0)
            {
                for (int i = 0; i < savefiles.Count - 1; i++)
                {
                    if (savefiles[i] == saveFile)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
    }
}
