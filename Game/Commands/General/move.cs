using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Commands.General
{
    public class move : EmptyCommand
    {
        public move():base("Where to move the player?", "right/left/up/down/move", CommandType.NORMAL)
        {

        }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            string direction = args[0].ToLower();
            if (args != null && args.Length > 1)
            {
                if (args[0] != "up" || args[0] != "down" || args[0] != "left" || args[0] != "right")
                {
                    direction = args[1].ToLower();
                }
            }

            Vector2 newPos = new Vector2(Reference.player.position.X, Reference.player.position.Y);
            switch (direction.ToLower())
            {
                case "up":
                    newPos.Y++;
                    break;
                case "down":
                    newPos.Y--;
                    break;
                case "left":
                    newPos.X--;
                    break;
                case "right":
                    newPos.X++;
                    break;
            }

            Reference.player.MovePlayer((int)newPos.X, (int)newPos.Y);
        }
    }
}
