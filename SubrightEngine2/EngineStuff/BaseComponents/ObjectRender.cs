using Raylib_cs;
using System;

namespace SubrightEngine2.EngineStuff.BaseComponents
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

        public override void Draw3D(ref Camera3D cam)
        {
            base.Draw3D(ref cam);
            switch (typeObject)
            {
                case ObjectType.CUBE:
                    Raylib.DrawCube(connectedObject.position.ToNumericsVector, connectedObject.size.X,
                        connectedObject.size.Y, connectedObject.size.Z, Raylib_cs.Color.White);
                    break;

                case ObjectType.CYLINDER:
                    Raylib.DrawCylinder(connectedObject.position.ToNumericsVector, connectedObject.size.X,
                        connectedObject.size.Y, connectedObject.size.Z, 18, Raylib_cs.Color.White);
                    break;

                case ObjectType.SPHERE:
                    Raylib.DrawSphere(connectedObject.position.ToNumericsVector,
                        connectedObject.size.X * connectedObject.size.Y * connectedObject.size.Z,
                        Raylib_cs.Color.White);
                    break;

                case ObjectType.SQUAREBASEDPYRAMID:
                    //Raylib.DrawTriangle3D(connectedObject.position, connectedObject.size, )
                    break;
            }
        }
    }
}