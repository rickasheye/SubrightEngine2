using System;

namespace SubrightEngine2.EngineStuff
{
    public class Vector4
    {
        //why is this even here?
        public Vector4 negativeInfinity { get; private set; } = new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        public Vector4 one = new Vector4(1, 1, 1, 1);

        public Vector4 postiveInfinity { get; private set; } =
            new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

        public Vector4 zero { get; private set; } = new Vector4(0, 0, 0, 0);

        public float w, x, y, z;
        public float Magnitude { get; private set; } = 0;
        public float sqrMagnitude { get; private set; } = 0;

        public Vector4(float w, float x, float y, float z)
        {
            Set(w, x, y, z);
            Magnitude = x * x + y * y + z * z + w * w;
            sqrMagnitude = Mathf.Sqrt(Magnitude);
        }

        public Vector4(Vector3 value, float w)
        {
            this.x = value.X;
            this.y = value.Y;
            this.z = value.Z;
            this.w = w;
        }

        public bool Equals(Vector4 vector)
        {
            if (vector == this)
            {
                return true;
            }

            return false;
        }

        public Vector4 Set(float w, float x, float y, float z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
            return this;
        }

        public new string ToString()
        {
            return "{" + w + ", " + x + "," + y + "," + z + "}";
        }

        public static Vector4 Transform(Vector2 value, Matrix matrix)
        {
            Vector4 result;
            Transform(ref value, ref matrix, out result);
            return result;
        }

        public static Vector4 Transform(Vector2 value, Quaternion rotation)
        {
            Vector4 result;
            Transform(ref value, ref rotation, out result);
            return result;
        }

        public static Vector4 Transform(Vector3 value, Matrix matrix)
        {
            Vector4 result;
            Transform(ref value, ref matrix, out result);
            return result;
        }

        public static Vector4 Transform(Vector3 value, Quaternion rotation)
        {
            Vector4 result;
            Transform(ref value, ref rotation, out result);
            return result;
        }

        public static Vector4 Transform(Vector4 value, Matrix matrix)
        {
            Transform(ref value, ref matrix, out value);
            return value;
        }

        public static Vector4 Transform(Vector4 value, Quaternion rotation)
        {
            Vector4 result;
            Transform(ref value, ref rotation, out result);
            return result;
        }

        //just for now instanitate a zeroed Vector4
        public static void Transform(ref Vector2 value, ref Matrix matrix, out Vector4 result)
        {
            result = new Vector4(0, 0, 0, 0);
            result.x = (value.X * matrix.M11) + (value.Y * matrix.M21) + matrix.M41;
            result.y = (value.X * matrix.M12) + (value.Y * matrix.M22) + matrix.M42;
            result.z = (value.X * matrix.M13) + (value.Y * matrix.M23) + matrix.M43;
            result.w = (value.X * matrix.M14) + (value.Y * matrix.M24) + matrix.M44;
        }

        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector3 value, ref Matrix matrix, out Vector4 result)
        {
            result = new Vector4(0, 0, 0, 0);
            result.x = (value.X * matrix.M11) + (value.Y * matrix.M21) + (value.Z * matrix.M31) + matrix.M41;
            result.y = (value.X * matrix.M12) + (value.Y * matrix.M22) + (value.Z * matrix.M32) + matrix.M42;
            result.z = (value.X * matrix.M13) + (value.Y * matrix.M23) + (value.Z * matrix.M33) + matrix.M43;
            result.w = (value.X * matrix.M14) + (value.Y * matrix.M24) + (value.Z * matrix.M34) + matrix.M44;
        }

        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector4 value, ref Matrix matrix, out Vector4 result)
        {
            result = new Vector4(0, 0, 0, 0);
            var x = (value.x * matrix.M11) + (value.y * matrix.M21) + (value.z * matrix.M31) + (value.w * matrix.M41);
            var y = (value.x * matrix.M12) + (value.y * matrix.M22) + (value.z * matrix.M32) + (value.w * matrix.M42);
            var z = (value.x * matrix.M13) + (value.y * matrix.M23) + (value.z * matrix.M33) + (value.w * matrix.M43);
            var w = (value.x * matrix.M14) + (value.y * matrix.M24) + (value.z * matrix.M34) + (value.w * matrix.M44);
            result.x = x;
            result.y = y;
            result.z = z;
            result.w = w;
        }

        public static void Transform(ref Vector4 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static void Transform
        (
            Vector4[] sourceArray,
            int sourceIndex,
            ref Matrix matrix,
            Vector4[] destinationArray,
            int destinationIndex,
            int length
        )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (var i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Transform(value, matrix);
            }
        }

        public static void Transform(
            Vector4[] sourceArray,
            int sourceIndex,
            ref Quaternion rotation,
            Vector4[] destinationArray,
            int destinationIndex,
            int length
            )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (var i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Transform(value, rotation);
            }
        }

        public static void Transform(Vector4[] sourceArray, ref Matrix matrix, Vector4[] destinationArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var value = sourceArray[i];
                destinationArray[i] = Transform(value, matrix);
            }
        }

        public static void Transform(Vector4[] sourceArray, ref Quaternion rotation, Vector4[] destinationArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var value = sourceArray[i];
                destinationArray[i] = Transform(value, rotation);
            }
        }
    }
}