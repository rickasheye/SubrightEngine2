using System;
using Raylib_cs;

namespace SCPBreakdown.EngineStuff.BaseComponents
{
    [Serializable]
    public enum ObjectType
    {
        CUBE,
        SPHERE,
        CYLINDER,
        SQUAREBASEDPYRAMID
    }

    [Serializable]
    public class ObjectRender : Component
    {
        private ObjectType typeObject;

        public ObjectRender(ObjectType ObjectType) : base("Object Render")
        {
            typeObject = ObjectType;
        }

        public override void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            base.Update(ref cam2, ref cam3);
            Draw3D(ref cam3);
        }

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            switch (typeObject)
            {
                case ObjectType.CUBE:
                    Raylib.DrawCube(connectedObject.position.ToNumericsVector, connectedObject.size.X,
                        connectedObject.size.Y, connectedObject.size.Z, Raylib_cs.Color.WHITE);
                    break;
                case ObjectType.CYLINDER:
                    Raylib.DrawCylinder(connectedObject.position.ToNumericsVector, connectedObject.size.X,
                        connectedObject.size.Y, connectedObject.size.Z, 18, Raylib_cs.Color.WHITE);
                    break;
                case ObjectType.SPHERE:
                    Raylib.DrawSphere(connectedObject.position.ToNumericsVector,
                        connectedObject.size.X * connectedObject.size.Y * connectedObject.size.Z,
                        Raylib_cs.Color.WHITE);
                    break;
                case ObjectType.SQUAREBASEDPYRAMID:
                    //Raylib.DrawTriangle3D(connectedObject.position, connectedObject.size, )
                    break;
            }
        }
    }
}