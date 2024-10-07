using Raylib_cs;

namespace SubrightEngine2.EngineStuff
{
    public class Extension
    {
        private bool started = false;

        public virtual void Update(ref Camera2D cam2, ref Camera3D cam3)
        {
            if (!started)
            {
                Start();
            }
        }

        public virtual void Start()
        {
            started = true;
        }

        public virtual void Dispose()
        {
        }
    }
}