using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class crafting : EmptyCommand
    {
        public crafting():base("Engage crafting mode", "crafting/cc", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Reference.player.InitiateCrafting();
        }
    }
}
