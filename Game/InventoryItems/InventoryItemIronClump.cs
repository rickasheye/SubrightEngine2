using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemIronClump : InventoryItem
    {
        public InventoryItemIronClump() : base("Iron Ore Clump", itemTYPE.GENERAL, "Textures/items/ironclump.png") { }
    }
}
