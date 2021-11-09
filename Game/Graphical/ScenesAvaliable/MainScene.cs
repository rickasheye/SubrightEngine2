using Raylib_cs;
using RPGConsole.InventoryBlock;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole.Graphical.ScenesAvaliable
{
    public class MainScene : Scene
    {
        public Texture2D overlay;

        public MainScene() : base("Main Game Scene")
        {
            //LoadTextures(true);
        }

        public bool gameActive = true;
        private bool craftingMode = false;
        private bool furnacingMode = false;
        private bool inventoryMode = false;
        public bool freezePlayer = false;

        public override void UpdateScene(Camera2D cam)
        {
            Raylib.SetExitKey(KeyboardKey.KEY_Q);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                Reference.loader.LoadScene(Reference.loader.getScene("Main Menu"));
            }
            if (Reference.player != null) { cam.target = new System.Numerics.Vector2(Reference.player.position.X * 64, Reference.player.position.Y * 64); }
            Raylib.BeginMode2D(cam);
            //draw the map!
            System.Numerics.Vector2 screen = new System.Numerics.Vector2(Raylib.GetScreenData().width, Raylib.GetScreenData().height);
            System.Numerics.Vector2 translation = Raylib.GetScreenToWorld2D(screen, cam);
            System.Numerics.Vector2 oppTrans = Raylib.GetScreenToWorld2D(System.Numerics.Vector2.Zero, cam);
            foreach (Block block in Reference.gen.blockMap)
            {
                if (block != null)
                {
                    if ((block.position.X * 64) <= translation.X && (block.position.Y * 64) <= translation.Y && (block.position.X * 64) >= oppTrans.X - 50 && (block.position.Y * 64) >= oppTrans.Y - 20)
                    {
                        if (block.texture == string.Empty)
                        {
                            Color color = Color.WHITE;
                            switch (block.color)
                            {
                                case Block.Color.BROWN:
                                    color = new Color(127, 106, 79, 255);
                                    break;

                                case Block.Color.DARKBROWN:
                                    color = Color.DARKBROWN;
                                    break;

                                case Block.Color.GRAY:
                                    color = Color.GRAY;
                                    break;

                                case Block.Color.GREEN:
                                    color = Color.GREEN;
                                    break;

                                case Block.Color.LIGHTBLUE:
                                    color = Color.SKYBLUE;
                                    break;

                                case Block.Color.LIGHTBROWN:
                                    color = new Color(205, 133, 63, 255);
                                    break;

                                case Block.Color.LIGHTGRAY:
                                    color = Color.LIGHTGRAY;
                                    break;

                                case Block.Color.LIGHTGREEN:
                                    color = new Color(124, 252, 0, 255);
                                    break;

                                case Block.Color.ORANGE:
                                    color = Color.ORANGE;
                                    break;

                                case Block.Color.PASTEBROWN:
                                    color = Color.RAYWHITE;
                                    break;

                                case Block.Color.WHITESKINCOLOR:
                                    color = Color.BEIGE;
                                    break;
                                case Block.Color.SILVER:
                                    color = Color.DARKBLUE;
                                    break;
                                case Block.Color.BLACK:
                                    color = Color.BLACK;
                                    break;
                                default:
                                    //if a color gets added down the track we want to add one here
                                    if (Reference.debugMode) { Console.WriteLine("Color is invalid!"); }
                                    break;
                            }
                            Raylib.DrawRectangle(((int)block.position.X * 64), ((int)block.position.Y * 64), 64, 64, color.ToRaylibColor);
                            //Raylib.DrawText(block.name + block.perlinvalue, (int)block.position.x * 64, (int)block.position.y * 64, 10, Color.BLACK);
                        }
                        else
                        {
                            //draw the texture attached!
                            Raylib.DrawTexture(block.textureInit(loaderAsset), (int)block.position.X * 64, (int)block.position.Y * 64, Color.WHITE.ToRaylibColor);
                        }
                        if(block.broken == true)
                        {
                            Raylib.DrawTexture(loaderAsset.textureLoad("Textures/overlays/brokenoverlay.png"), (int)block.position.X * 64, (int)block.position.Y * 64, Color.WHITE.ToRaylibColor);
                        }
                        block.UpdateBlock();
                    } 
                }
            }
            //Console.WriteLine("there are " + blocks + " in view!");
            //draw the players ontop
            //draw the individual player
            if (Reference.player != null)
            {
                Vector2 newPosition = new Vector2(Reference.player.position.X, Reference.player.position.Y);
                if (!freezePlayer)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                    {
                        newPosition.Y--;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_S))
                    {
                        newPosition.Y++;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
                    {
                        newPosition.X--;
                    }
                    else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                    {
                        newPosition.X++;
                    } 
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_C))
                {
                    if (craftingMode == false)
                    {
                        Reference.player.InitiateCrafting();
                        freezePlayer = true;
                        craftingMode = true;
                        furnacingMode = false;
                    }
                    else
                    {
                        Reference.loader.currentScene.guiOptions.Clear();
                        freezePlayer = false;
                        craftingMode = false;
                    }
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_F))
                {
                    if (furnacingMode == false)
                    {
                        Reference.player.InitiateFurnacing();
                        freezePlayer = true;
                        furnacingMode = true;
                        craftingMode = false;
                        inventoryMode = false;
                    }
                    else
                    {
                        Reference.loader.currentScene.guiOptions.Clear();
                        freezePlayer = false;
                        furnacingMode = false;
                    }
                }

                if (Raylib.IsKeyPressed(KeyboardKey.KEY_I))
                {
                    if (inventoryMode == false)
                    {
                        Reference.player.InitiateInventoryGUI();
                        freezePlayer = true;
                        inventoryMode = true;
                        craftingMode = false;
                        furnacingMode = false;
                    }
                    else
                    {
                        Reference.loader.currentScene.guiOptions.Clear();
                        freezePlayer = false;
                        inventoryMode = false;
                    }
                }

                if (!freezePlayer)
                {
                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        if (inventoryMode == false && furnacingMode == false && craftingMode == false)
                        {
                            //place item into the slot where the player is!
                            if (Reference.gen.returnBlock(Reference.player.position.X, Reference.player.position.Y).broken)
                            {
                                if (Reference.player.equipItem as Air == null)
                                {
                                    if (Reference.player.equipItem.typeMaterial == InventoryItems.entityType.BLOCK)
                                    {
                                        Reference.gen.setBlock((int)Reference.player.position.X, (int)Reference.player.position.Y, Reference.player.equipItem as Block);
                                        Reference.player.inv.removeItem(Reference.player.equipItem);
                                        if (Reference.player.equipItem.itemCount <= 0 || Reference.player.equipItem == null)
                                        {
                                            Reference.player.equipItem = new Air(); 
                                        }
                                    }
                                    else
                                    {
                                        if (Reference.debugMode == true) { Console.WriteLine("Not a block to place!"); }
                                    } 
                                }
                            }
                            else
                            {
                                if (Reference.debugMode == true) { Console.WriteLine("This block is not broken so unable to place!"); }
                            }
                            //LoadTextures(false);
                            //Reference.gen.manualUpdate();
                        }
                    }

                    if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                    {
                        //mine the block
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
                                    if (Reference.debugMode == true) { Console.WriteLine("the block under you is already been broken!"); }
                                }
                            }
                            else
                            {
                                if (Reference.debugMode == true) { Console.WriteLine("this block cannot be mined!"); }
                            }
                        }
                        else
                        {
                            if (Reference.debugMode == true) { Console.WriteLine("for some reason that didnt work as the block doesnt exist!"); }
                        }
                        //Reference.gen.manualUpdate();
                    }
                    Reference.player.MovePlayer((int)newPosition.X, (int)newPosition.Y); 
                }
                Raylib.DrawRectangle(((int)Reference.player.position.X * 64) + 9, ((int)Reference.player.position.Y * 64) + 9, 45, 45, Color.WHITE.ToRaylibColor);
            }
            Raylib.EndMode2D();
            if (gameActive)
            {
                for (int i = 0; i < Reference.player.health; i++)
                {
                    Raylib.DrawRectangle((10 * i) + 10, 10, 10, 10, Color.RED.ToRaylibColor);
                    if (i >= Reference.player.health - 1)
                    {
                        Raylib.DrawText("Health " + Reference.player.health, (10 * i) + 25, 10, 10, Color.RED.ToRaylibColor);
                    }
                }

                if (Reference.player.equipItem.name != "Air")
                {
                    if (Reference.player.equipItem.textureInit(loaderAsset).width == 0)
                    {
                        Reference.player.equipItem.textureInit(loaderAsset);
                        Console.WriteLine(Reference.player.equipItem.texture);
                    }
                    Raylib.DrawTexture(Reference.player.equipItem.textureInit(loaderAsset), (10 * Reference.player.health) + 85, 10, Color.WHITE.ToRaylibColor);
                    Raylib.DrawText(Reference.player.equipItem.name, (10 * Reference.player.health) + 90 + 64, 10, 10, Color.WHITE.ToRaylibColor);
                }

                if (Reference.player.health <= 0)
                {
                    gameActive = false;
                }
            }
            else
            {
                Reference.player = null;
                Raylib.DrawText("Game Over", Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 30, Raylib_cs.Color.BLACK);
                Thread.Sleep(300);
                Console.WriteLine("game over!");
                Reference.loader.LoadScene(Reference.loader.getScene("Main Menu"));
            }
            Raylib.DrawRectangle(10, 5, 1 + loaderAsset.texturesCached.Count, 5, Color.BLACK.ToRaylibColor);
            base.UpdateScene(cam);
        }
    }
}