using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public class InventoryItemElectrode : InventoryItem
    {
        public InventoryItemElectrode(int count) : base("Electrode", itemTYPE.GENERAL, count) { }
    }
}
