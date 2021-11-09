using RPGConsole.InventoryItems;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;

namespace RPGConsole
{
    public class Inventory
    {
        public List<InventoryItem> items = new List<InventoryItem>();

        public bool itemExists(InventoryItem item)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].name == item.name)
                {
                    return true;
                }
            }
            return false;
        }

        public InventoryItem itemFind(InventoryItem item)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].name == item.name)
                {
                    return items[i];
                }
            }
            return null;
        }

        public void addItem(InventoryItem item)
        {
            if (itemExists(item))
            {
                itemFind(item).itemCount++;
                Debug.Log("Item " + item.name + " has been increased in count in your inventory!");
            }
            else
            {
                items.Add(item);
                Debug.Log("Item " + item.name + " has been added to your inventory!");
            }
        }

        public void removeItem(InventoryItem item)
        {
            if (itemExists(item))
            {
                if (itemFind(item).itemCount > 0)
                {
                    itemFind(item).itemCount--;
                    Debug.Log("one of " + item.name + " has been deducted from your inventory!");
                }
                else
                {
                    for(int i = 0; i < items.Count; i++)
                    {
                        if(items[i].name == item.name)
                        {
                            items.RemoveAt(i);
                        }
                    }

                    Debug.Log("Item " + item.name + " has been removed from your inventory!");
                }
            }
            else
            {
                Debug.Log("Item doesnt exist!");
            }
        }
    }
}
