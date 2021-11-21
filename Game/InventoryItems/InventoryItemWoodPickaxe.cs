using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemWoodPickaxe : InventoryItemPickaxe
    {
        public InventoryItemWoodPickaxe() : base(2, "Wood Pickaxe", itemMaterial.WOOD, "Textures/items/woodpickaxe.png")
        {
        }
    }
}
