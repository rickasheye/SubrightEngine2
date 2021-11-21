using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemMudClump : InventoryItem
    {
        public InventoryItemMudClump() : base("Mud Clump", itemTYPE.GENERAL, "Textures/items/mudclump.png") { }
    }
}
