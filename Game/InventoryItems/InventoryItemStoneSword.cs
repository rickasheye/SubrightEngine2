using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemStoneSword : InventoryItemWeapon
    {
        public InventoryItemStoneSword() : base(1, "Stone Sword", itemMaterial.STONE, "Textures/items/stonesword.png") { }
    }
}
