using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemWaterClump : InventoryItem
    {
        public InventoryItemWaterClump() : base("Water Clump", itemTYPE.GENERAL, "Textures/items/waterclump.png") { }
    }
}
