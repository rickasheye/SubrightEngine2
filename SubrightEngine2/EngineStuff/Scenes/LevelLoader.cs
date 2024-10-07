using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SubrightEngine2.EngineStuff.Scenes
{
    public class LevelLoader
    {
        //load all sorts of stuff!.
        public static void LoadLevel(string path)
        {
            if (Program.saveFile)
            {
                //use this to load a level
                if (File.Exists(path))
                {
                    var jsonScript = File.ReadAllText(path);
                    var allObjects = JsonConvert.DeserializeObject<List<GameObject>>(jsonScript);
                    //objects.Clear();
                    //remove the blacklisted objects
                    var corrupted = false;
                    for (var i = 0; i < allObjects.Count; i++)
                    {
                        var m = allObjects[i];
                        //run the components through setup tooo
                        for (var o = 0; o < m.components.Count; o++)
                        {
                            var comModel = m.components[o];
                            if (comModel.connectedObject == null) comModel.connectedObject = m;
                        }

                        for (var p = 0; p < Program.loader.currentScene.GameObjects.Count; p++)
                        {
                            var l = Program.loader.currentScene.GameObjects[p];
                            if (m.name == l.name)
                            {
                                //remove
                                corrupted = true;
                                allObjects.RemoveAt(i);
                            }
                        }
                    }

                    if (corrupted) Debug.LogError("Save file seems to be corrupted, but readable.");

                    Program.loader.currentScene.GameObjects.AddRange(allObjects);
                    Debug.Log("Sucessfully loaded all content");
                }
                else
                {
                    Debug.LogError("Unfortunately that level doesnt exist!");
                }
            }
            else
            {
                //unable to save save files is disabled
                Debug.Log("Unable to load file as save files are disabled!");
            }
        }

        public static void WriteLevelByte()
        {
            //run or create a byte array out of the elements
            if (Program.saveFile)
            {
                string path = Path.Combine(Environment.CurrentDirectory, "scenes/", Program.loader.currentScene.name + ".sbrs");
                var directory = path.Replace(Path.GetFileName(path), "");
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                if (File.Exists(path))
                {
                    var binary = new BinaryFormatter();
                    using (var fs = File.Create(path))
                    {
                        binary.Serialize(fs, Program.loader.currentScene.GameObjects);
                    }
                    Debug.Log("Sucessfully saved file in " + path);
                }
                else
                {
                    File.Create(path);
                    Debug.LogWarning("Created a file to make sure that it exists!");
                }
            }
            else
            {
                //unable to save save files is disabled
                Debug.Log("Unable to save file as save files are disabled!");
            }
        }

        public static void LoadLevelByte()
        {
            if (Program.saveFile)
            {
                string path = Path.Combine(Environment.CurrentDirectory, "scenes/", Program.loader.currentScene.name + ".sbrs");
                var directory = path.Replace(Path.GetFileName(path), "");
                if (!Directory.Exists(directory))
                    //need to write the original first
                    Debug.LogError("Need to write the file and directory first");

                if (File.Exists(path))
                {
                    if (File.ReadAllText(path) != "")
                    {
                        var binary = new BinaryFormatter();
                        using (var fs = File.OpenRead(path))
                        {
                            Debug.Log(fs.ToString(), false);
                            var gameObjects = (List<GameObject>)binary.Deserialize(fs);
                            Program.loader.currentScene.GameObjects.Clear();
                            Program.loader.currentScene.GameObjects.AddRange(gameObjects);
                            fs.Dispose();
                        }
                    }
                    else
                    {
                        if (Program.debug) { Debug.Log("File seems to be empty! on load!"); }
                    }
                }
                else
                {
                    WriteLevelByte();
                }
            }
            else
            {
                //unable to save save files is disabled
                Debug.Log("Unable to load file as save files are disabled!");
            }
        }

        public static void WriteLevel()
        {
            if (Program.saveFile)
            {
                string path = Path.Combine(Environment.CurrentDirectory, "scenes/", Program.loader.currentScene.name + ".sbrs");
                var directory = path.Replace(Path.GetFileName(path), "");
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                for (var i = 0; i < Program.loader.currentScene.GameObjects.Count; i++)
                    for (var m = 0; m < Program.loader.currentScene.GameObjects[i].components.Count; m++)
                        Program.loader.currentScene.GameObjects[i].components[m].connectedObject = null;

                if (File.Exists(path))
                {
                    var jsonScript = JsonConvert.SerializeObject(Program.loader.currentScene.GameObjects);
                    File.WriteAllText(path, jsonScript);
                    Debug.Log("Sucessfully written scene to disk!");
                }
                else
                {
                    File.Create(path);
                    Debug.LogWarning("Created a file to make sure that it exists!");
                }
            }
            else
            {
                //unable to save save files is disabled
                Debug.Log("Unable to save file as save files are disabled!");
            }
        }

        public static bool savedFile()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "scenes/", Program.loader.currentScene.name + ".sbrs");
            if (File.Exists(path))
            {
                //check if its a proper save?
                var jsonScript = File.ReadAllText(path);
                if (jsonScript != string.Empty)
                {
                    var objectsRead = JsonConvert.DeserializeObject<List<GameObject>>(jsonScript);
                    if (objectsRead != null)
                        //not blank so readable
                        return true;
                }

                if (jsonScript == "[]")
                {
                    //then json file is not empty but empty on objects
                    Debug.LogWarning("Save file is empty, but saved");
                    return true;
                }
            }

            return false;
        }

        public static bool savedFileByte()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "scenes/", Program.loader.currentScene.name + ".sbrs");
            return File.Exists(path);
        }
    }
}