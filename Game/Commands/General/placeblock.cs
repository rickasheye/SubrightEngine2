using RPGConsole.InventoryBlock;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class placeblock : EmptyCommand
    {
        public placeblock():base("Place the block equipped in place in where the player is", "pb/placeblock/place", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Block blockPlaced = Reference.player.equipItem as Block;
            if (blockPlaced != null)
            {
                blockPlaced.broken = false;
                blockPlaced.strength = blockPlaced.originalStrength;
                Reference.gen.setBlock((int)Reference.player.position.X, (int)Reference.player.position.Y, blockPlaced);
                Reference.player.inv.removeItem(blockPlaced);
                Debug.Log("The block " + blockPlaced.name + " was placed at " + Reference.player.position.ToString());
            }
            else
            {
                Debug.Log("the item equipped is not able to be placed!");
            }
        }
    }
}
