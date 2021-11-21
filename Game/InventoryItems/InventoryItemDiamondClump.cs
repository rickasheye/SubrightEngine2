using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemDiamondClump : InventoryItem
    {
        public InventoryItemDiamondClump() : base("Diamond Ore Clump", itemTYPE.GENERAL, "Textures/items/diamondclump.png") { }
    }
}
