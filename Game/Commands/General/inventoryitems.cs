using RPGConsole.InventoryItems;
using SubrightEngine2.EngineStuff;

namespace RPGConsole.Commands.General
{
    public class inventoryitems : EmptyCommand
    {
        public inventoryitems() : base("Check your inventory items avaliable", "ii/inventory/checkinventory/ci/inventoryitems", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Debug.Log("inventory items!");
            for (int i = 0; i < Reference.player.inv.items.Count; i++)
            {
                InventoryItem item = Reference.player.inv.items[i];
                if (item != null)
                {
                    Debug.Log("<" + item.itemCount + "> " + item.name);
                }
            }
        }
    }
}
