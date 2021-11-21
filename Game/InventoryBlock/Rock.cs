using Raylib_cs;
using RPGConsole.InventoryItems;
using System;

namespace RPGConsole.InventoryBlock
{
    [Serializable]
    public class Rock : Block
    {
        public Rock() : base("Rock", 0, 0, 10, Color.LIGHTGRAY, "Textures/blocks/rock.png", 1) { }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (broken == true)
            {
                if (!giveBlock)
                {
                    player.inv.addItem(new InventoryItemRockFragments());
                }
            }
        }
    }
}
