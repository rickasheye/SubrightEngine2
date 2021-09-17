using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SCPBreakdown.EngineStuff.LevelLoading
{
    public class PrefabLoader
    {
        public static void LoadPrefab(Prefab pref)
        {
            pref.loadPrefab(ref Program.objects);
        }

        //load all of the text prefabs!
        public static Prefab CreatePrefab(List<GameObject> prefabs, string name)
        {
            //create a prefab from the objects.
            var prefabNew = new Prefab();
            prefabNew.name = name;
            prefabNew.gameObjects.AddRange(prefabs);
            //convert to text.
            var jsonConv = JsonConvert.SerializeObject(prefabNew);
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "prefabs/");
            var filename = Path.Combine(directory, name);
            if (!Directory.Exists(directory))
                //directory doesnt exist so create
                Directory.CreateDirectory(directory);

            if (!File.Exists(filename)) File.Create(filename);

            File.WriteAllText(filename, jsonConv);
            return prefabNew;
        }

        public static List<Prefab> LoadPrefabs()
        {
            var prefabs = new List<Prefab>();
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "prefabs/");
            if (!Directory.Exists(directory))
                //directory doesnt exist so create
                Directory.CreateDirectory(directory);

            for (var i = 0; i < Directory.GetFiles(directory).Length; i++)
            {
                var filename = Path.Combine(directory, Directory.GetFiles(directory)[i]);
                var prefabsB = JsonConvert.DeserializeObject<List<Prefab>>(File.ReadAllText(filename));
                prefabs.AddRange(prefabsB);
            }

            return prefabs;
        }
    }
}