using System;
using System.Collections.Generic;
using System.Text;
using RPGConsole.InventoryItems;
using SubrightEngine2.EngineStuff;

namespace RPGConsole.InventoryBlock
{
    public class Chest : Block
    {

        public List<RPGConsole.InventoryItems.InventoryItem> storeInventoryItems = new List<RPGConsole.InventoryItems.InventoryItem>();

        public Chest():base("Chest", 0, 0, 2, Color.BROWN, "Textures/blocks/chest.png", 1) { }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            //Initiate chest view!
            player.chest = this;
            player.InitiateInventoryChest();
        }

        public override void PlayerOffBlock(Player player)
        {
            base.PlayerOffBlock(player);
            player.chest = null;
            Reference.loader.currentScene.guiOptions.Clear();
        }

        public virtual void AddChestItem(InventoryItem item)
        {
            if(ChestItemExists(item) == false)
            {
                storeInventoryItems.Add(item);
            }
            else
            {
                Debug.Log("Unable to add item to chest! as it already exists!");
            }
        }

        public virtual void RemoveChestItem(InventoryItem item)
        {
            if(ChestItemExists(item) == true)
            {
                storeInventoryItems.Remove(item);
            }
            else
            {
                Debug.Log("This item does not exist!");
            }
        }

        public virtual bool ChestItemExists(InventoryItem item)
        {
            foreach(InventoryItem items in storeInventoryItems)
            {
                if(items.name == item.name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
