using RPGConsole.InventoryItems;
using System;

namespace RPGConsole.InventoryBlock
{
    [Serializable]
    public class IronOre : Block
    {
        public IronOre() : base("Iron Ore", 0, 0, 20, Color.WHITESKINCOLOR, "Textures/blocks/ironore.png", 1) { giveBlock = true; }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (broken)
            {
                if (!giveBlock)
                {
                    player.inv.addItem(new InventoryItemIronClump());
                }
            }
        }
    }
}
