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

        public override void Draw3D(ref Camera3D cam3)
        {
            base.Draw3D(ref cam3);
            connectedObject.position.Y--;
            //somehow connect the collision with something???
        }
    }
}