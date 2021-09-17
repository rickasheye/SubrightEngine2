using System;
using System.IO;
using Raylib_cs;

namespace SCPBreakdown.EngineStuff.BaseComponents
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
                Debug.Log("Unfortunately this doesnt work as the file: " + path + " cannot be found!", LogType.MESSAGE);
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            if (model.meshCount <= 0)
            {
                Debug.Log("reviving model " + path);
                StartRender(path);
            }

            Draw3D(ref cam3);
        }

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            if (model.meshCount > 0)
                Raylib.DrawModel(model, connectedObject.position.ToNumericsVector,
                    connectedObject.size.X * connectedObject.size.Y * connectedObject.size.Z, Raylib_cs.Color.WHITE);
        }
    }
}