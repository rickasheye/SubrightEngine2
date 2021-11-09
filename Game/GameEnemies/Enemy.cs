using Raylib_cs;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using RPGConsole.InventoryItems;
using SubrightEngine2.EngineStuff;
using System;
using System.Threading;
using Color = SubrightEngine2.EngineStuff.Color;
using Random = SubrightEngine2.EngineStuff.Random;

namespace RPGConsole.GameEnemies
{
    public class Enemy : GameObject
    {
        public int health = 20;
        public bool playerStuck;

        public Enemy(string name, Vector2 position, Player player) : base(new Vector3(position.X, position.Y, 0), Vector3.zero, name)
        {
            if (Reference.cmdMode) { StartSequence(player); } else
            {
                StartGUISequence();
            }
        }

        public void StartSequence(Player player)
        {
            //Sequence start
            bool playerReady = true;
            Debug.Log(name + " has decided to attack!");
            while (playerReady == true)
            {
                //stop the player
                string lines = Console.ReadLine();
                string[] args = lines.Split(" ");
                switch (args[0])
                {
                    case "equipment":
                    case "equip":
                    case "e":
                        //equip an item from a inventory!
                        if (args.Length > 1)
                        {
                            bool itemEquipped = player.equipInventoryItem(args[1]);
                            if (itemEquipped == false)
                            {
                                Debug.Log("Item was not equipped continuing");
                            }
                        }
                        else
                        {
                            Debug.Log("args are incorrect please try again");
                        }
                        break;
                    case "fight":
                    case "engage":
                        if (player.equipItem.type == itemTYPE.WEAPON)
                        {
                            //add this onto the pressure.
                            InventoryItemWeapon weapon = (InventoryItemWeapon)player.equipItem;
                            if (weapon == null)
                            {
                                Debug.Log("Reference error: unable to convert to weapon!!!");
                            }
                            else
                            {
                                int randdmg = Random.Range(weapon.damageLevel);
                                health -= randdmg;
                                Debug.Log("Your weapon called " + weapon.name + " has delt " + randdmg + " to the enemy!");
                                weapon.UpgradeWeapon(player);
                            }
                        }
                        break;
                    case "run":
                        int randomValue = Random.Range(10);
                        if (randomValue == 5)
                        {
                            //run
                            Debug.Log("you have sucessfully ran from the enemy");
                            Thread.Sleep(300);
                            if (playerReady == true)
                            {
                                playerReady = false;
                                Reference.loader.currentScene.guiOptions.Clear();
                            }
                            else
                            {
                                Debug.Log("thats unfortunate? the player is stuck but the boolean has been set to false!");
                            }
                        }
                        else
                        {
                            //unfortunately you have not escaped!
                            Debug.Log("unfortunately you have not escaped!");
                        }
                        break;
                    case "help":
                        Debug.Log("HELP COMMANDS!");
                        Debug.Log("( equipment/equip/e <itemname> ) to equip an item during battle! watch out as soon as you execute a command fully the enemy has a stab at you!");
                        Debug.Log("( fight/engage ) to fight or engage on the enemy!");
                        break;
                    default:
                        Debug.Log("that is not a valid command! please execute 'help' for more information!");
                        break;
                }
                //take a break and let the enemy fight the player
                int randValue = Random.Range(player.level * 2);
                player.health -= randValue;
                Debug.Log("enemy has delt " + randValue + " damage");
                Debug.Log("your health is now at " + player.health);

                //break the process if player health is too low!
                if (player.health <= 0)
                {
                    playerReady = false;
                }
            }
        }

        public void StartGUISequence()
        {
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.loader.currentScene.guiOptions.Add(new Text("An enemy has decided to attack you!", new Vector2(10, 10), 20, Color.BLACK));
            Reference.loader.currentScene.guiOptions.Add(new EnemyGUIButton(this, "Attack the " + name, new Vector2(20, 20), new Vector2(10, 40)));
            Reference.loader.currentScene.guiOptions.Add(new EnemyRunGUIButton(this, "Run from the " + name, new Vector2(20, 20), new Vector2(10, 70)));
            EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
            container.children.AddRange(Reference.loader.currentScene.guiOptions);
            Reference.loader.currentScene.guiOptions.Add(container);
        }
    }

    public class EnemyGUIButton : KeyboardAdjustedButton
    {
        public Enemy enemyConcern;

        public EnemyGUIButton(Enemy enemyConcern, string name, Vector2 size, Vector2 position):base(name, size, position)
        {
            this.enemyConcern = enemyConcern;
        }

        public override void Triggerable()
        {
            base.Triggerable();
            if (Reference.player.equipItem.type == itemTYPE.WEAPON)
            {
                //add this onto the pressure.
                InventoryItemWeapon weapon = (InventoryItemWeapon)Reference.player.equipItem;
                if (weapon == null)
                {
                    Debug.Log("Reference error: unable to convert to weapon!!!");
                }
                else
                {
                    int randdmg = Random.Range(weapon.damageLevel);
                    enemyConcern.health -= randdmg;
                    Debug.Log("Your weapon called " + weapon.name + " has delt " + randdmg + " to the enemy!");
                    weapon.UpgradeWeapon(Reference.player);
                }
            }
        }
    }

    public class EnemyRunGUIButton : KeyboardAdjustedButton
    {
        public Enemy enemyConcern;

        public EnemyRunGUIButton(Enemy enemyConcern, string name, Vector2 size, Vector2 position):base(name, size, position) { this.enemyConcern = enemyConcern; }

        public override void Triggerable()
        {
            base.Triggerable();
            int randomValue = Random.Range(10);
            if(randomValue == 5)
            {
                //run
                Debug.Log("you have sucessfully ran from the enemy");
                Reference.loader.currentScene.guiOptions.Add(new Text("You have sucessfully escaped from the enemy!", new Vector2(10, 10), 20, Color.BLACK));
                Thread.Sleep(300);
                if (enemyConcern.playerStuck == true)
                {
                    enemyConcern.playerStuck = false;
                    Reference.loader.currentScene.guiOptions.Clear();
                }
                else
                {
                    Debug.Log("thats unfortunate? the player is stuck but the boolean has been set to false!");
                }
            }
            else
            {
                //unfortunately you have not escaped!
                Debug.Log("The player has unfortunately not escaped!");
                Reference.loader.currentScene.guiOptions.Clear();
                Reference.loader.currentScene.guiOptions.Add(new Text("Unfortunately you have not escaped from the monster!", new Vector2(10, 10), 20, Color.BLACK));
                Thread.Sleep(300);
                enemyConcern.StartGUISequence();
            }
        }
    }
}
