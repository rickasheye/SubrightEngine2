using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace SubrightEngine2.EngineStuff.LevelLoading
{
    public class LevelLoader
    {
        //load all sorts of stuff!.
        public static void LoadLevel(string path, ref List<GameObject> objects)
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

                    for (var p = 0; p < Program.objects.Count; p++)
                    {
                        var l = Program.objects[p];
                        if (m.name == l.name)
                        {
                            //remove
                            corrupted = true;
                            allObjects.RemoveAt(i);
                        }
                    }
                }

                if (corrupted) Debug.Log("Save file seems to be corrupted, but readable.", LogType.ERROR);

                objects.AddRange(allObjects);
                Debug.Log("Sucessfully loaded all content", LogType.MESSAGE);
            }
            else
            {
                Debug.Log("Unfortunately that level doesnt exist!", LogType.ERROR);
            }
        }

        public static void WriteLevelByte(string path, ref List<GameObject> elements)
        {
            //run or create a byte array out of the elements
            var directory = path.Replace(Path.GetFileName(path), "");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            if (File.Exists(path))
            {
                var binary = new BinaryFormatter();
                using (var fs = File.Create(path))
                {
                    binary.Serialize(fs, elements);
                }
                Debug.Log("Sucessfully saved file in " + path);
            }
            else
            {
                File.Create(path);
                Debug.Log("Created a file to make sure that it exists!", LogType.WARNING);
            }
        }

        public static void LoadLevelByte(string path, ref List<GameObject> objects)
        {
            var directory = path.Replace(Path.GetFileName(path), "");
            if (!Directory.Exists(directory))
                //need to write the original first
                Debug.Log("Need to write the file and directory first", LogType.ERROR);

            if (File.Exists(path))
            {
                if (File.ReadAllText(path) != "")
                {
                    var binary = new BinaryFormatter();
                    using (var fs = File.OpenRead(path))
                    {
                        if (Program.debug) Debug.Log(fs.ToString(), LogType.MESSAGE);
                        var gameObjects = (List<GameObject>) binary.Deserialize(fs);
                        objects.Clear();
                        objects.AddRange(gameObjects);
                        fs.Dispose();
                    }
                }
                else
                {
                    if(Program.debug){Debug.Log("File seems to be empty! on load!");}
                }
            }
            else
            {
                WriteLevelByte(path, ref objects);
            }
        }

        public static void WriteLevel(string path, ref List<GameObject> objects)
        {
            var directory = path.Replace(Path.GetFileName(path), "");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            for (var i = 0; i < objects.Count; i++)
            for (var m = 0; m < objects[i].components.Count; m++)
                objects[i].components[m].connectedObject = null;

            if (File.Exists(path))
            {
                var jsonScript = JsonConvert.SerializeObject(objects);
                File.WriteAllText(path, jsonScript);
                Debug.Log("Sucessfully written scene to disk!", LogType.MESSAGE);
            }
            else
            {
                File.Create(path);
                Debug.Log("Created a file to make sure that it exists!", LogType.WARNING);
            }
        }

        public static bool savedFile(string path)
        {
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
                    Debug.Log("Save file is empty, but saved", LogType.WARNING);
                    return true;
                }
            }

            return false;
        }

        public static bool savedFileByte(string path)
        {
            return File.Exists(path);
        }
    }
}