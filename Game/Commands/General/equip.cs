using SubrightEngine2.EngineStuff;

namespace RPGConsole.Commands.General
{
    public class equip : EmptyCommand
    {
        public equip() : base("What item to equip", "equip", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            if (args.Length > 1) { Reference.player.equipInventoryItem(args[1]); } else { Debug.Log("you didnt specify a item name!"); }
        }
    }
}
