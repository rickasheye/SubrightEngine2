using RPGConsole.InventoryItems;
using SubrightEngine2.EngineStuff;
using System;
using System.Threading;
using Random = SubrightEngine2.EngineStuff.Random;

namespace RPGConsole.InventoryBlock
{
    [Serializable]
    public class Lava : Block
    {
        public Lava() : base("Lava", 2, 0, -1, Color.ORANGE, "Textures/blocks/lava.png", 1) { }

        public override void PlayerOnTop(Player player)
        {
            base.PlayerOnTop(player);
            //Take stuff from the player!
            int tempTimer = 0;
            while (player.position.X == position.X && player.position.Y == position.Y)
            {
                //take health from the player!
                player.health -= toxicity;
                Debug.Log("you are sitting on lava it has depleted your health by " + toxicity);

                if (player.health <= 0)
                {
                    break;
                }
                int randomChance = Random.Range(2);
                if (randomChance == 1)
                {
                    player.MovePlayer((int)player.position.X + 1, (int)player.position.Y);
                    Debug.Log("luckily you have survived and made it out of the lava!");
                }
            }
        }

        public override void MineBlock(Player player)
        {
            base.MineBlock(player);
            if (!giveBlock)
            {
                player.inv.addItem(new InventoryItemLavaClump());
            }
        }

        public override void UpdateBlockThroughMiningandPlacing()
        {
            base.UpdateBlockThroughMiningandPlacing();
            //producing obsidian or modifying blocks on there
            Generator generator = Reference.gen;
            bool connectedToWater = false;
            Vector2 newPosition = new Vector2(0, 0);
            if (generator.returnBlock(position.X + 1, position.Y) == generator.getBlock("Water"))
            {
                newPosition = new Vector2((int)position.X + 1, position.Y);
                connectedToWater = true;
            }
            else if (generator.returnBlock(position.X, position.Y + 1) == generator.getBlock("Water"))
            {
                newPosition = new Vector2(position.X, position.Y + 1);
                connectedToWater = true;
            }
            else if (generator.returnBlock(position.X - 1, position.Y) == generator.getBlock("Water"))
            {
                newPosition = new Vector2(position.X - 1, position.Y);
                connectedToWater = true;
            }
            else if (generator.returnBlock(position.X, position.Y - 1) == generator.getBlock("Water"))
            {
                newPosition = new Vector2(position.X, position.Y - 1);
                connectedToWater = true;
            }

            if (connectedToWater == true)
            {
                generator.setBlock((int)newPosition.X, (int)newPosition.Y, new Obsidian());
                generator.setBlock((int)position.X, (int)position.Y, new Rock());
            }
        }
    }
}
