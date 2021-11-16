using RPGConsole.InventoryBlock;
using SubrightEngine2.EngineStuff;

namespace RPGConsole.Commands.General
{
    public class ontop : EmptyCommand
    {
        public ontop() : base("What block the player is ontop?", "ontop/ot", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Block useableblock = Reference.gen.returnBlock(Reference.player.position.X, Reference.player.position.Y);
            if (useableblock != null)
            {
                Debug.Log("the player is currently sitting on block " + useableblock.name);
            }
            else
            {
                Debug.Log("somehow the player isnt sitting on anyblock!");
            }
        }
    }
}
