using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemSticks : InventoryItem
    {
        public InventoryItemSticks(int itemAmount) : base("Sticks", itemTYPE.GENERAL, itemAmount, "Textures/items/sticks.png") { }
    }
}
