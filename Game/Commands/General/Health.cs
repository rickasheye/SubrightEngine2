using SubrightEngine2.EngineStuff;

namespace RPGConsole.Commands.General
{
    public class Health : EmptyCommand
    {
        public Health() : base("Checks the player health", "health/h/checkhealth/playerhealth", CommandType.NORMAL) { }

        public override void RunCommand(string[] args)
        {
            base.RunCommand(args);
            Debug.Log("Health: " + Reference.player.health);
        }
    }
}
