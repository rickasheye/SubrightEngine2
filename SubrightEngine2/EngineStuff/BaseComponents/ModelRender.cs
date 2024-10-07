using Raylib_cs;
using System;
using System.IO;

namespace SubrightEngine2.EngineStuff.BaseComponents
{
    [Serializable]
    public class ModelRender : Component
    {
        [field: NonSerialized] public Model model;

        public string path = "";

        public ModelRender(string pathRender) : base("Model Render")
        {
            path = pathRender;
            StartRender(pathRender);
        }

        public void StartRender(string path)
        {
            if (File.Exists(path))
                model = Raylib.LoadModel(path);
            else
                //Unfortunately this doesnt work
                Debug.LogError("Unfortunately this doesnt work as the file: " + path + " cannot be found!");
        }

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            if (model.MeshCount <= 0)
            {
                Debug.Log("reviving model " + path);
                StartRender(path);
            }
            if (model.MeshCount > 0)
                Raylib.DrawModel(model, connectedObject.position.ToNumericsVector,
                    connectedObject.size.X * connectedObject.size.Y * connectedObject.size.Z, Raylib_cs.Color.White);
        }
    }
}