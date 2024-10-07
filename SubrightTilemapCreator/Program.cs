namespace SubrightTilemapCreator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SubrightEngine2.Program.SetExtension(new ExtensionStart(), false);
            SubrightEngine2.Program.Initialise(args, false, true);
        }
    }
}