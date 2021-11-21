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
    }
}