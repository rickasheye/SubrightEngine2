using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemWoodSword : InventoryItemWeapon
    {
        public InventoryItemWoodSword() : base(1, "Wood Sword", itemMaterial.WOOD, "Textures/items/woodsword.png")
        {
        }
    }
}
