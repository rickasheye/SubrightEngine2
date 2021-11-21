using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemStonePickaxe : InventoryItemPickaxe
    {
        public InventoryItemStonePickaxe() : base(4, "Stone Pickaxe", itemMaterial.STONE, "Textures/items/stonepickaxe.png")
        {
        }
    }
}
