using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemMetalSword : InventoryItemWeapon
    {
        public InventoryItemMetalSword() : base(5, "Metal Sword", itemMaterial.METAL, "Textures/items/ironsword.png") { }
    }
}
