using Raylib_cs;
using System;

namespace SubrightEngine2.EngineStuff.BaseComponents
{
    [Serializable]
    public class PhysicsBody : Component
    {
        public PhysicsBody() : base("Physics Body")
        {
            if (connectedObject.GetComponent("BoxCollider") == null)
                if (Program.debug)
                    Debug.Log("It seems that there is no BoxCollider on this object!");
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            connectedObject.position.Y--;
            //somehow connect the collision with something???
        }
    }
}