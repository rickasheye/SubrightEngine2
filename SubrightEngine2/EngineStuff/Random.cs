namespace SubrightEngine2.EngineStuff
{
    public class Random
    {
        public static int Range(int start, int end)
        {
            System.Random rand = new System.Random();
            return rand.Next(start, end);
        }

        public static int Range(int end)
        {
            return Range(0, end);
        }
    }
}