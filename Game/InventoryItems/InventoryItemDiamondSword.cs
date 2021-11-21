using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemDiamondSword : InventoryItemWeapon
    {
        public InventoryItemDiamondSword() : base(10, "Diamond Sword", itemMaterial.DIAMOND, "Textures/items/diamondsword.png") { }
    }
}
