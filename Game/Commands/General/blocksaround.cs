using RPGConsole.InventoryBlock;
using RPGConsole.Saving;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class blocksaround : EmptyCommand
    {
        public blocksaround() : base("Checking the blocks around the plaYer...", "blocksaround/fa/ba/findblocks/blockaround/findblock", CommandType.NORMAL)
        {

        }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            //get the blocks around the plaYer
            if (Reference.player != null)
            {
                Debug.Log("Getting blocks in a 2 block radius");
                string positionValues = "";
                for (int Y = ((int)Reference.player.position.Y - 2); Y < Reference.player.position.Y + 2; Y++)
                {
                    for (int X = ((int)Reference.player.position.X - 2); X < Reference.player.position.X + 2; X++)
                    {
                        Block block = Reference.gen.returnBlock(X, Y);
                        if (block != null)
                        {
                            if ((int)Reference.player.position.X == X && (int)Reference.player.position.Y == Y)
                            {
                                string alternateplayername = "";
                                alternateplayername = Reference.player.name;
                                positionValues = positionValues + " " + Reference.player.name;
                            }
                            else
                            {
                                if (block.broken == false)
                                {
                                    positionValues = positionValues + " " + block.name;
                                }
                                else
                                {
                                    positionValues = positionValues + " <" + block.name + ">";
                                }
                            }
                        }
                        else
                        {
                            positionValues = positionValues + " NULL";
                        }
                    }
                    positionValues = positionValues + "\n";
                }
                Debug.Log(positionValues);
            }
        }
    }
}
