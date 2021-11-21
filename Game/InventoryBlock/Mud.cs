using RPGConsole.InventoryItems;
using System;

namespace RPGConsole.InventoryBlock
{
    [Serializable]
    public class Mud : Block
    {
        public Mud() : base("Mud", 0, 5, 2, Color.DARKBROWN, "Textures/blocks/mud.png", 1) { }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (broken)
            {
                if (!giveBlock)
                {
                    player.inv.addItem(new InventoryItemMudClump());
                }
            }
        }
    }
}
