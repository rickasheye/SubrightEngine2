using System;
using System.Collections.Generic;
using Raylib_cs;

namespace SCPBreakdown.EngineStuff.BaseComponents
{
    [Serializable]
    public class BoxCollider : Collider
    {

        public BoxCollider(Vector3 offsetPosition, Vector3 offsetSize) : base(offsetPosition, offsetSize, "Box Collider")
        {
        }

        public override bool intersect(Vector3 amin, Vector3 amax, Vector3 bmin, Vector3 bmax)
        {
            return amin.X <= bmax.X && amax.X >= bmin.X && amin.Y <= bmax.Y && amax.Y >= bmin.Y && amin.Z <= bmax.Z &&
                   amax.Z >= bmin.Z;
        }
    }
}