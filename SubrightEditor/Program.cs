namespace SubrightEditor
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SubrightEngine2.Program.SetExtension(new EditorReference(), false);
            SubrightEngine2.Program.Initialise(args, false, false);
        }
    }
}