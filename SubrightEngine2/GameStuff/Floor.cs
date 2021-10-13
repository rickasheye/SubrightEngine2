using System;
using Raylib_cs;
using SubrightEngine2.EngineStuff.BaseComponents;

namespace SubrightEngine2.GameStuff
{
    [Serializable]
    public class Floor : Component
    {
        public Floor() : base("Floor")
        {
        }

        public override void Start()
        {
            base.Start();
            var collider = new BoxCollider(connectedObject.position, connectedObject.size);
            connectedObject.AddComponent(collider);
        }

        //Component for floor
        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            //render or classify the model!
            Raylib.DrawCube(connectedObject.position.ToNumericsVector, 1, 0.1f, 1, Color.GRAY);
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw3D(ref cam3);
        }
    }
}