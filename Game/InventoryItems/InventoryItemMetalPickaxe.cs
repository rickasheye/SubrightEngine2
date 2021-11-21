using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemMetalPickaxe : InventoryItemPickaxe
    {
        public InventoryItemMetalPickaxe() : base(8, "Metal Pickaxe", itemMaterial.METAL, "Textures/items/ironpickaxe.png")
        {
        }
    }
}
