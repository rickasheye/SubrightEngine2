using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemDiamondPickaxe : InventoryItemPickaxe
    {
        public InventoryItemDiamondPickaxe() : base(16, "Diamond Pickaxe", itemMaterial.DIAMOND, "Textures/items/diamondpickaxe.png") { }
    }
}
