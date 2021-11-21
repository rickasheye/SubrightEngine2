using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemDirtClump : InventoryItem
    {
        public InventoryItemDirtClump() : base("Dirt Clump", itemTYPE.GENERAL, "Textures/items/dirtclump.png") { }
    }
}
