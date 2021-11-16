namespace RPGConsole.Commands.General
{
    public class furnace : EmptyCommand
    {
        public furnace() : base("Enagage furnacing interface", "furnance", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Reference.player.InitiateFurnacing();
        }
    }
}
