﻿using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemLavaClump : InventoryItem
    {
        public InventoryItemLavaClump() : base("Lava Clump", itemTYPE.GENERAL, "Textures/items/lavaclump.png") { }
    }
}
