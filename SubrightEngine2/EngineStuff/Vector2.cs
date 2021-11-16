using System;
using System.Drawing;

namespace SubrightEngine2.EngineStuff
{
    public struct Vector2
    {
        public float X, Y;

        public static readonly Vector2 zero = new Vector2(0, 0);
        public static readonly Vector2 down = new Vector2(0, -1);
        public static readonly Vector2 left = new Vector2(-1, 0);
        public static readonly Vector2 negativeInfinitY = new Vector2(int.MinValue, int.MinValue);
        public static readonly Vector2 one = new Vector2(1, 1);
        public static readonly Vector2 positiveInfinitY = new Vector2(int.MaxValue, int.MaxValue);
        public static readonly Vector2 right = new Vector2(1, 0);
        public static readonly Vector2 up = new Vector2(0, 1);

        //public Vector3 ToVector3 => new Vector3((int) Math.Round(X, 0), (int) Math.Round(Y, 0), 0);

        public Vector2(float _X, float _Y)
        {
            X = _X;
            Y = _Y;
        }

        public static Vector2 Zero { get; } = new Vector2(0, 0);

        public Point ToPoint => new Point((int)Math.Round(X, 0), (int)Math.Round(Y, 0));

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return a * -1;
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            var same = false;
            if (a.X == b.X && a.Y == b.Y) same = true;
            return same;
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            var notsame = false;
            if (a.X != b.X && a.Y != b.Y) notsame = true;
            return notsame;
        }

        // Functions

        public static Vector2 GetFromAngleDegrees(float angle)
        {
            return new Vector2((float)Math.Cos(angle * Mathf.Deg2Rad), (float)Math.Sin(angle * Mathf.Deg2Rad));
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            var vector = new Vector2(a.X - b.X, a.Y - b.Y);
            return (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float p)
        {
            return new Vector2(Mathf.Lerp(a.X, b.X, p), Mathf.Lerp(a.Y, b.Y, p));
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static Vector2 Normalize(Vector2 a)
        {
            if (a.X == 0 && a.Y == 0) return zero;
            var distance = (float)Math.Sqrt(a.X * a.X + a.Y * a.Y);
            return new Vector2(a.X / distance, a.Y / distance);
        }

        public static float Magnitude(Vector2 a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static float sqrMagnitude(Vector2 a)
        {
            return Magnitude(a) * Magnitude(a);
        }

        public static Vector2 ClampMagnitude(Vector2 a, float l)
        {
            if (Magnitude(a) > l) a = Normalize(a) * l;
            return a;
        }

        public void Set(int Xset, int Yset)
        {
            X = Xset;
            Y = Yset;
        }

        public static string ToString(Vector2 a)
        {
            return "" + a.X + " : " + a.Y;
        }
    }
}