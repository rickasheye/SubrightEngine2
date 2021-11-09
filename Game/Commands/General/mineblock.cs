using RPGConsole.InventoryBlock;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class mineblock : EmptyCommand
    {
        public mineblock():base("Mine the block under the player", "mb/break/breakblock/mineblock", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            //mine the block under the player!
            Block mineableBlock = Reference.gen.returnBlock(Reference.player.position.X, Reference.player.position.Y);
            if (mineableBlock != null)
            {
                if (mineableBlock.strength != -1)
                {
                    if (mineableBlock.broken == false)
                    {
                        mineableBlock.MineBlock(Reference.player);
                    }
                    else
                    {
                        Debug.Log("the block under you is already been broken!");
                    }
                }
                else
                {
                    Debug.Log("this block cannot be mined!");
                }
            }
            else
            {
                Debug.Log("for some reason that didnt work as the block doesnt exist!");
            }
        }
    }
}
