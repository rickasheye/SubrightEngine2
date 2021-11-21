using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemLeaf : InventoryItem
    {
        public InventoryItemLeaf() : base("Leaf", itemTYPE.GENERAL, "Textures/items/leaf.png") { }
    }
}
