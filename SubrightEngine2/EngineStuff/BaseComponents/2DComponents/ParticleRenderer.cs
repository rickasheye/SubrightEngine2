using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubrightEngine2.EngineStuff.BaseComponents._2DComponents
{
    public class ParticleRenderer : Component
    {
        int particlesAllowed = 100;
        int particlesAliveS = 100;
        List<Particle> particleSpawn = new List<Particle>();

        public struct Particle
        {
            public float x;
            public float y;

            public Particle(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public ParticleRenderer(string name) : base("Particle Renderer")
        {

        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            const double max = 1000.0;
            System.Random rand = new System.Random();     // Like mentioned, don't provide a seed, .NET already picks a great seed for you
            for (int iParticle = 0; iParticle < particlesAllowed; iParticle++)
            {
                double x = rand.NextDouble() * max;     // Will generate a random number between 0 and max
                double y = rand.NextDouble() * max;
                Raylib.DrawRectangle((int)x, (int)y, 1, 1, Raylib_cs.Color.Red);
            }
        }
    }
}
