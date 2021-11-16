using Newtonsoft.Json;
using RPGConsole.InventoryBlock;
using SubrightEngine2.EngineStuff;
using System.Collections.Generic;
using System.IO;

namespace RPGConsole.Saving
{
    public class CombinedSaveFile
    {
        public List<Block> generator = new List<Block>();
        public List<Player> playerFiles = new List<Player>();
        public Player playerFile;

        public string fileName = "";

        public CombinedSaveFile(string path, Player playerSave, Generator genSave)
        {
            SaveFile(path, playerSave, genSave);
        }

        public CombinedSaveFile()
        {
            //nothing here...
            if (SubrightEngine2.Program.debug) { Debug.Log("There is nothing here on this save file just yet..."); }
        }

        public void SaveFile(string path, Player playSave, Generator genSave)
        {
            //save in a path simmilar to "/saves"
            if (File.Exists(path))
            {
                if (playSave != null) { this.playerFile = playSave; }
                else
                {
                    if (SubrightEngine2.Program.debug) { Debug.Log("Unfortunately the player save option for this 'combinedsavefile' is disabled!"); }
                }
                foreach (Block moo in genSave.blockMap)
                {
                    if (moo == null || moo.name == "")
                    {
                        genSave.blockMap.Remove(moo);
                    }
                }
                this.generator = genSave.blockMap;
                string json = JsonConvert.SerializeObject(this);
                File.WriteAllText(path, json);
                Debug.Log("yes save file is saved!");
            }
            else
            {
                Debug.Log("This file does not exist! " + path);
                string moddedPath = path.Replace(Path.GetFileName(path), "");
                if (Directory.Exists(moddedPath))
                {
                    Debug.Log("Directory exists?");
                    File.Create(path);
                }
                else
                {
                    Debug.Log("Created new saves directory...");
                    Directory.CreateDirectory(moddedPath);
                }
                SaveFile(path, playSave, genSave);
            }
        }

        public void LoadFile(string path)
        {
            if (File.Exists(path))
            {
                string jsonCode = File.ReadAllText(path);
                CombinedSaveFile json = JsonConvert.DeserializeObject<CombinedSaveFile>(jsonCode);
                generator = json.generator;
                if (json.playerFile != null) { playerFile = json.playerFile; }
                else { if (SubrightEngine2.Program.debug) { Debug.Log("Unfortunately this savefile mode for the player is disabled!"); } }
                Debug.Log("Sucessfully read raw file!");
            }
            else
            {
                Debug.Log("This file unfortunately doesnt exist!");
            }
        }
    }
}
