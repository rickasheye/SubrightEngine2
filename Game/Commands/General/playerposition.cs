using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class playerposition : EmptyCommand
    {
        public playerposition():base("Where the player is on the map.", "playerposition/pp/playerpos", CommandType.NORMAL) {  }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Debug.Log("Player is at: " + Reference.player.position.ToString());
        }
    }
}
