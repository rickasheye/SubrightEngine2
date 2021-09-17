using System;

namespace SCPBreakdown.EngineStuff
{
    public class Random
    {
        public static int Range(int start, int end)
        {
            System.Random rand = new System.Random();
            return rand.Next(start, end);
        }
    }
}