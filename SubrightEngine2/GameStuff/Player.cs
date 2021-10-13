using System;
using System.Collections.Generic;
using Raylib_cs;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.BaseComponents;
using Color = Raylib_cs.Color;

namespace SubrightEngine2.GameStuff
{
    [Serializable]
    public class Player : Component
    {
        public List<Floor> floors = new List<Floor>();
        private bool gravity;
        public int speed = 2;

        public Player() : base("Player")
        {
        }

        public override void Start()
        {
            base.Start();
            if (connectedObject != null) connectedObject.size = new Vector3(1, 2, 1);
            var collider = new BoxCollider(connectedObject.position, connectedObject.size);
            connectedObject.AddComponent(collider);
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            if (connectedObject != null)
            {

                if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    if (gravity == false)
                        gravity = true;
                    else
                        gravity = false;
                }

                cam3.position = new System.Numerics.Vector3(connectedObject.position.X, connectedObject.position.Y + 10,
                    connectedObject.position.Z + 10);
                cam3.target = connectedObject.position.ToNumericsVector;
                //draw or use the input here
                if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
                    //left
                    connectedObject.position.X -= speed * Raylib.GetFrameTime();

                if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
                    //right
                    connectedObject.position.X -= -(speed * Raylib.GetFrameTime());

                if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
                    //up
                    connectedObject.position.Z -= -(speed * Raylib.GetFrameTime());

                if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
                    //down
                    connectedObject.position.Z -= speed * Raylib.GetFrameTime();
                
                if (gravity) connectedObject.position.Y -= speed * Raylib.GetFrameTime();
            }

            Draw3D(ref cam3);
        }

        public List<Floor> getFloors()
        {
            var floorObjects = new List<Floor>();
            for (var i = 0; i < Program.objects.Count; i++)
            {
                var m = Program.objects[i];
                if (m.GetComponent("Floor") != null)
                {
                    var mCom = (Floor) m.GetComponent("Floor");
                    floorObjects.Add(mCom);
                }
            }

            return floorObjects;
        }

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            if (connectedObject != null)
                Raylib.DrawCube(connectedObject.position.ToNumericsVector, (int) connectedObject.size.X,
                    (int) connectedObject.size.Y, (int) connectedObject.size.Z, Color.BLACK);
        }
    }
}