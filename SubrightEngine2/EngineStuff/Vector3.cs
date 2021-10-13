using System;

namespace SubrightEngine2.EngineStuff
{
    [Serializable]
    public struct Vector3
    {
        public float X, Y, Z;

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);

        public Vector3(float _X, float _Y, float _Z)
        {
            X = _X;
            Y = _Y;
            Z = _Z;
        }

        public static Vector3 zero { get; private set; } = new Vector3(0, 0, 0);

        public static Vector3 One { get; private set; } = new Vector3(1, 1, 1);

        public Vector2 ToVector2 => new Vector2((int) Math.Round(X, 0), (int) Math.Round(Y, 0));

        public System.Numerics.Vector3 ToNumericsVector => new System.Numerics.Vector3(X, Y, Z);

        public new string ToString => "{" + X + "," + Y + "," + Z + "}";

        public static Vector3 operator -(Vector3 a)
        {
            return a * -1;
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            var same = false;
            if (a.X == b.X && a.Y == b.Y && a.Z == b.Z) same = true;
            return same;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            var notsame = false;
            if (a.X != b.X && a.Y != b.Y && a.Z != b.Z) notsame = true;
            return notsame;
        }

        // Functions

        public static Vector3 GetFromAngleDegrees(float angle)
        {
            return new Vector3((float) Math.Cos(angle * Mathf.Deg2Rad), (float) Math.Sin(angle * Mathf.Deg2Rad),
                (float) Math.Sin(angle * Mathf.Deg2Rad));
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            var vector = new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
            return (float) Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float p)
        {
            return new Vector3(Mathf.Lerp(a.X, b.X, p), Mathf.Lerp(a.Y, b.Y, p), Mathf.Lerp(a.Z, b.Z, p));
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static Vector3 Normalize(Vector3 a)
        {
            if (a.X == 0 && a.Y == 0) return Zero;
            var distance = (float) Math.Sqrt(a.X * a.X + a.Y * a.Y);
            return new Vector3(a.X / distance, a.Y / distance, a.Z / distance);
        }

        public static float Magnitude(Vector3 a)
        {
            return (float) Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static Vector3 ClampMagnitude(Vector3 a, float l)
        {
            if (Magnitude(a) > l) a = Normalize(a) * l;
            return a;
        }
    }
}