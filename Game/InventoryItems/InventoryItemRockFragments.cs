using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemRockFragments : InventoryItem
    {
        public InventoryItemRockFragments() : base("Rock Fragments", itemTYPE.GENERAL, "Textures/items/rockfragments.png") { }
    }
}
