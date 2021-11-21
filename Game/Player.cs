using RPGConsole.Crafting;
using RPGConsole.Graphical.MenuItems;
using RPGConsole.Graphical.MenuItems.KeyboardOnlyItems;
using RPGConsole.InventoryBlock;
using RPGConsole.InventoryItems;
using SubrightEngine2.EngineStuff;
using System;
using System.Collections.Generic;
using System.Threading;
using Color = SubrightEngine2.EngineStuff.Color;

namespace RPGConsole
{
    public class Player
    {
        public string name;
        public Vector2 position;
        public Inventory inv;
        public int health = 30;
        public InventoryItem equipItem = new Air();
        public int level = 1;

        public bool craftingTable;
        public bool furnace;
        //public Block ontopOfBlock = null;

        public ulong discordid;
        public Player()
        {
            if (Reference.gen != null) { position = new Vector2(Reference.gen.sizeMapX / 2, Reference.gen.sizeMapY / 2); } else { position = new Vector2(0, 0); }
            if (inv == null)
            {
                inv = new Inventory();
            }
            if (name == null)
            {
                name = "untitled player";
            }
            MovePlayer((int)position.X, (int)position.Y);
        }

        public void MovePlayer(int X, int Y)
        {
            bool enemiesNearby = false;
            Vector2 originalPos = new Vector2(position.X, position.Y);
            Vector2 newPos = new Vector2(X, Y);
            if (originalPos.X != newPos.X || originalPos.Y != newPos.Y)
            {
                if (X >= 0 && Y >= 0 && X <= Reference.gen.sizeMapX && Y <= Reference.gen.sizeMapY)
                {
                    if (Reference.gen.returnBlock(newPos.X, newPos.Y) != null)
                    {
                        //Console.WriteLine("You have landed on the block: " + Reference.gen.returnBlock(newPos.X, newPos.Y).name);
                        for (int XPos = -2; XPos < 2; XPos++)
                        {
                            for (int YPos = -2; YPos < 2; YPos++)
                            {
                                Block returnedBlock = Reference.gen.returnBlock(position.X + XPos, position.Y + YPos);
                                if (returnedBlock as Grass != null)
                                {
                                    if (!enemiesNearby)
                                    {
                                        Debug.Log("there maybe enemies nearby..");
                                        enemiesNearby = true;
                                    }
                                }
                            }
                        }
                    }

                    Block getBlock = Reference.gen.returnBlock(newPos.X, newPos.Y);
                    if (getBlock != null)
                    {
                        position = newPos;
                        Debug.Log("moved player from " + originalPos.ToPoint.ToString() + " to " + position.ToPoint.ToString());
                        getBlock.PlayerOnTop(this);
                        getBlock.PlayerOnTop();

                        Block oldBlock = Reference.gen.returnBlock(originalPos.X, originalPos.Y);
                        oldBlock.PlayerOffBlock();
                        oldBlock.PlayerOffBlock(this);
                    }
                    else
                    {
                        Debug.Log("unable to move since that block doesnt eXist!");
                    }
                }
            }
        }

        public InventoryItem inventoryGet(string name)
        {
            return inv.items.Find(t => t.name == name);
        }

        public bool equipInventoryItem(string name)
        {
            InventoryItem item = inventoryGet(name);
            return eqipInventoryItem(item);
        }

        public bool eqipInventoryItem(InventoryItem item)
        {
            equipItem = item;
            Debug.Log("equipped the item: " + item.name);
            return true;
        }

        int levelupTime = 0;

        public void LevelUp()
        {
            if (levelupTime > level * 2)
            {
                //level up
                level++;
                Debug.Log("Congrats player! You have leveled up!");
            }
            else
            {
                levelupTime++;
            }
        }

        public List<CraftingRecipe> recipes = new List<CraftingRecipe>();
        public List<FurnaceRecipe> furnaceRecipes = new List<FurnaceRecipe>();

        void SetupCrafting()
        {
            recipes.Add(new CraftingRecipe(new InventoryItemSticks(1), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemStoneSword(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new Wood(), new Wood(), new CraftingTable(), false));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new Furnace(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemDiamondClump(), new InventoryItemDiamondClump(), new InventoryItemSticks(1), new InventoryItemDiamondSword(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemIronClump(), new InventoryItemIronClump(), new InventoryItemSticks(1), new InventoryItemMetalSword(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new InventoryItemSticks(4), false));
            recipes.Add(new CraftingRecipe(new InventoryItemDiamondClump(), new InventoryItemDiamondClump(), new InventoryItemDiamondClump(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemDiamondPickaxe(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemIronClump(), new InventoryItemIronClump(), new InventoryItemIronClump(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemMetalPickaxe(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemStonePickaxe(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new Wood(), new InventoryItemSticks(1), new InventoryItemSticks(1), new InventoryItemWoodPickaxe(), true));
            recipes.Add(new CraftingRecipe(new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Wood(), new Chest(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemRockFragments(), new InventoryItemRockFragments(), new BlankMachinePart(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemRockFragments(), new InventoryItemSticks(1), new ElectrodeNode(), true));
            recipes.Add(new CraftingRecipe(new InventoryItemElectrode(3), new BlankMachinePart(), new MachinePart_Power(), true));
        }

        void SetupFurnace()
        {
            furnaceRecipes.Add(new FurnaceRecipe(new DiamondOre(), new InventoryItemDiamondClump()));
            furnaceRecipes.Add(new FurnaceRecipe(new IronOre(), new InventoryItemIronClump()));
        }

        public List<CraftingRecipe> recipesCalc()
        {
            List<CraftingRecipe> allowedReceipes = new List<CraftingRecipe>();
            for (int i = 0; i < recipes.Count; i++)
            {
                List<InventoryItem> item = CraftingRecipe.convertToList(recipes[i]);
                bool acceptable = true;
                if (recipes[i].needTable == true && craftingTable == false)
                {
                    acceptable = false;
                }

                if (acceptable == false)
                {
                    for (int z = 0; z < item.Count; z++)
                    {
                        if (inv.itemExists(item[z]) == false)
                        {
                            acceptable = false;
                        }
                        else
                        {
                            if (inv.itemFind(item[z]).itemCount >= item[z].itemCount)
                            {
                                acceptable = true;
                            }
                        }
                    }
                }

                if (acceptable == true)
                {
                    allowedReceipes.Add(recipes[i]);
                }
            }
            return allowedReceipes;
        }

        public bool crafting = false;
        public void InitiateCrafting()
        {
            //Start the crafting!!!
            crafting = true;
            while (crafting)
            {

                //calculate things!
                if (recipes.Count <= 0)
                {
                    SetupCrafting();
                }
                bool unfortunate = true;
                List<CraftingRecipe> allowedReceipes = recipesCalc();
                if (allowedReceipes != null)
                {
                    unfortunate = false;
                }

                //DisplaY a graphical menu
                Reference.loader.currentScene.guiOptions.Clear();
                if (allowedReceipes.Count > 0)
                {
                    Reference.loader.currentScene.guiOptions.Add(new Text("Welcome to crafting use the following menu to craft!", new Vector2(10, 10), 5, Color.BLACK));
                    for (int i = 0; i < allowedReceipes.Count; i++)
                    {
                        CraftingGUIOption guiOption = new CraftingGUIOption(allowedReceipes[i].output.name, new Vector2(10, (10 * i) + 25), new Vector2(50, 50));
                        guiOption.giveItem = allowedReceipes[i].output;
                        guiOption.removingItems = CraftingRecipe.convertToList(allowedReceipes[i]);
                        Reference.loader.currentScene.guiOptions.Add(guiOption);
                    }
                }
                else
                {
                    Console.WriteLine("No recipes to show!");
                    Text text = new Text("No recipes to show use 'C' again to eXit!", new Vector2(10, 10), 5, Color.BLACK);
                    Reference.loader.currentScene.guiOptions.Add(text);
                }

                EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
                container.children.AddRange(Reference.loader.currentScene.guiOptions);
                Reference.loader.currentScene.guiOptions.Add(container);
                crafting = false;
            }
        }

        public void InitiateFurnacing()
        {
            if (furnaceRecipes.Count <= 0)
            {
                SetupFurnace();
            }

            bool furnaceBaking = true;
            if (furnace == false)
            {
                Debug.Log("unable to initate furnace since You are not sitting ontop of a furnace!");
                furnaceBaking = false;
            }
            while (furnaceBaking)
            {
                List<FurnaceRecipe> recipes = new List<FurnaceRecipe>();
                for (int i = 0; i < furnaceRecipes.Count; i++)
                {
                    FurnaceRecipe recipe = furnaceRecipes[i];
                    bool valid = false;
                    if (inv.itemExists(recipe.input))
                    {
                        //allow to provide the output!
                        recipes.Add(recipe);
                    }
                }
                Reference.loader.currentScene.guiOptions.Clear();
                if (recipes.Count > 0)
                {
                    Reference.loader.currentScene.guiOptions.Add(new Text("Welcome to the furnace use the following menu to furnace!", new Vector2(10, 10), 5, Color.BLACK));
                    for (int i = 0; i < recipes.Count; i++)
                    {
                        FurnaceRecipe recipie = recipes[i];
                        FurnaceGUIOption furnaceOption = new FurnaceGUIOption(recipie.input.name + " > " + recipie.output.name, new Vector2(10, (10 * i) + 25), new Vector2(50, 50));
                        furnaceOption.input = recipie.input;
                        furnaceOption.output = recipie.output;
                        Reference.loader.currentScene.guiOptions.Add(furnaceOption);
                    }
                }
                else
                {
                    Console.WriteLine("No furnace recipes to show!");
                    Text teXt = new Text("No recipes to show use 'F' again to exit!", new Vector2(10, 10), 5, Color.BLACK);
                    Reference.loader.currentScene.guiOptions.Add(teXt);
                }
                EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
                container.children.AddRange(Reference.loader.currentScene.guiOptions);
                Reference.loader.currentScene.guiOptions.Add(container);
                furnaceBaking = false;
            }
        }

        public void FurnaceChoose(string furnaceVariable, ref bool furnaceBaking, List<FurnaceRecipe> recipes)
        {
            Debug.Log("give command (exit) to quit out of furnace prompt!");
            switch (furnaceVariable)
            {
                case "quit":
                case "eXit":
                case "goodbYe":
                case "bYe":
                case "stop":
                    furnaceBaking = false;
                    Debug.Log("exited furnacing...");
                    break;
            }
            int finalConvertedInteger = 0;
            bool check = int.TryParse(furnaceVariable, out finalConvertedInteger);
            if (check == true)
            {
                Debug.Log("Cooking...");
                Thread.Sleep(800);
                inv.removeItem(recipes[finalConvertedInteger].input);
                inv.addItem(recipes[finalConvertedInteger].output);
                Debug.Log("Cooked!");
            }
            else
            {
                if (furnaceBaking == true)
                {
                    Debug.Log("Unfortunately that number is incorrect!");
                }
            }
        }

        public Chest chest;

        public void InitiateInventoryGUI()
        {
            Reference.loader.currentScene.guiOptions.Clear();
            //displaY the inventorY
            if (inv.items.Count > 0)
            {
                Reference.loader.currentScene.guiOptions.Add(new Text("INVENTORY", new Vector2(10, 10), 5, Color.BLACK));
                //displaY the actual inventorY with a gui
                for (int i = 0; i < inv.items.Count; i++)
                {
                    if (inv.items[i].itemCount > 0)
                    {
                        string modernName = inv.items[i].name;
                        if (equipItem == inv.items[i])
                        {
                            modernName = modernName + " EQUIPPED!";
                        }
                        InventorYGUIOption invOption = new InventorYGUIOption(modernName + " #" + inv.items[i].itemCount, new Vector2(10, (10 * i) + 25), new Vector2(50, 50), chest);
                        invOption.eqipItem = inv.items[i];
                        Reference.loader.currentScene.guiOptions.Add(invOption);
                    }
                }
            }
            else
            {
                Debug.Log("No inventory items to show!");
                Text teXt = new Text("No items to show use 'I' again to eXit!", new Vector2(10, 10), 5, Color.BLACK);
                Reference.loader.currentScene.guiOptions.Add(teXt);
            }
            EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
            container.children.AddRange(Reference.loader.currentScene.guiOptions);
            Reference.loader.currentScene.guiOptions.Add(container);
        }

        public void InitiateInventoryChest()
        {
            Reference.loader.currentScene.guiOptions.Clear();
            //displaY the inventorY
            if (chest.storeInventoryItems.Count > 0)
            {
                Reference.loader.currentScene.guiOptions.Add(new Text("CHEST", new Vector2(10, 10), 5, Color.BLACK));
                //displaY the actual inventorY with a gui
                for (int i = 0; i < chest.storeInventoryItems.Count; i++)
                {
                    InventorYChestGUIOption invOption = new InventorYChestGUIOption(chest.storeInventoryItems[i].name, new Vector2(10, (10 * i) + 25), new Vector2(50, 50));
                    invOption.eqipItem = chest.storeInventoryItems[i];
                    Reference.loader.currentScene.guiOptions.Add(invOption);
                }
            }
            else
            {
                Debug.Log("No chest items to show!");
                Text teXt = new Text("No items to show in this chest! use the Inventory to put items in this chest!", new Vector2(10, 10), 5, Color.BLACK);
                Reference.loader.currentScene.guiOptions.Add(teXt);
            }
            EmptyContainer container = new EmptyContainer(new Vector2(10, 10), new Vector2(10, 10));
            container.children.AddRange(Reference.loader.currentScene.guiOptions);
            Reference.loader.currentScene.guiOptions.Add(container);
        }
    }

    public class CraftingGUIOption : KeyboardAdjustedButton
    {
        public List<InventoryItem> removingItems;
        public InventoryItem giveItem;

        public CraftingGUIOption(string title, Vector2 pos, Vector2 size) : base(title, size, pos) { }

        public override void Triggerable()
        {
            base.Triggerable();
            foreach (InventoryItem item in removingItems)
            {
                Reference.player.inv.removeItem(item);
            }
            Reference.player.inv.addItem(giveItem);
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.player.InitiateCrafting();
        }
    }

    public class FurnaceGUIOption : KeyboardAdjustedButton
    {
        public InventoryItem input;
        public InventoryItem output;
        public FurnaceGUIOption(string title, Vector2 pos, Vector2 size) : base(title, size, pos) { }

        public override void Triggerable()
        {
            base.Triggerable();
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.loader.currentScene.guiOptions.Add(new Text("Cooking!", new Vector2(10, 10), 5, Color.BLACK));
            Thread.Sleep(800);
            Reference.player.inv.removeItem(input);
            Reference.player.inv.addItem(output);
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.loader.currentScene.guiOptions.Add(new Text("Cooked!", new Vector2(10, 10), 5, Color.BLACK));
            //update the gui
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.player.InitiateFurnacing();
        }
    }

    public class InventorYGUIOption : KeyboardAdjustedButtonOptional
    {
        public InventoryItem eqipItem;
        Chest chestInstance;

        public InventorYGUIOption(string title, Vector2 pos, Vector2 size, Chest chestInstance) : base(title, size, pos) { if (chestInstance == null) { this.chestInstance = chestInstance; justbenormal = true; } }

        public override void Start()
        {
            base.Start();
            if (justbenormal == false)
            {
                //add the children to this object!.
                if (chestInstance != null)
                {
                    InventoryGUIEquip guiEqip = new InventoryGUIEquip("Equip Item", position, size);
                    guiEqip.eqipItem = eqipItem;
                    children.Add(guiEqip);
                    InventoryGUIChestOption chestOption = new InventoryGUIChestOption("Dump this item in a chest!", position, size);
                    chestOption.itemEquippable = eqipItem;
                    chestOption.chestInstance = chestInstance;
                    children.Add(chestOption);
                }
                else
                {
                    //nothing i guess
                    justbenormal = true;
                }
            }
        }

        public override void Triggerable()
        {
            if (justbenormal == true)
            {
                if (eqipItem == null)
                {
                    Console.WriteLine("Unfortunately this item was unavaliable!");
                }
                base.Triggerable();
                Reference.player.equipInventoryItem(eqipItem.name);
                Reference.loader.currentScene.guiOptions.Clear();
                Reference.player.InitiateInventoryGUI();
            }
        }
    }

    public class InventoryGUIEquip : KeyboardAdjustedButton
    {
        public InventoryItem eqipItem;

        public InventoryGUIEquip(string title, Vector2 pos, Vector2 size) : base(title, size, pos) { }

        public override void Triggerable()
        {
            if (eqipItem == null)
            {
                Console.WriteLine("Unfortunately this item was unavaliable!");
            }
            base.Triggerable();
            Reference.player.equipInventoryItem(eqipItem.name);
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.player.InitiateInventoryGUI();
        }
    }

    public class InventoryGUIChestOption : KeyboardAdjustedButton
    {
        public Chest chestInstance;
        public InventoryItem itemEquippable;

        public InventoryGUIChestOption(string teXt, Vector2 pos, Vector2 size) : base(teXt, size, pos) { }

        public override void Triggerable()
        {
            base.Triggerable();
            chestInstance.RemoveChestItem(itemEquippable);
            Reference.player.inv.addItem(itemEquippable);
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.player.InitiateInventoryGUI();
        }
    }

    public class InventorYChestGUIOption : KeyboardAdjustedButton
    {
        public InventoryItem eqipItem;
        public Chest chestInstance;

        public InventorYChestGUIOption(string title, Vector2 pos, Vector2 size) : base(title, size, pos) { }

        public override void Triggerable()
        {
            if (eqipItem == null)
            {
                Console.WriteLine("UnfortunatelY this item was unavaliable!");
            }
            base.Triggerable();
            Reference.player.inv.addItem(eqipItem);
            chestInstance.RemoveChestItem(eqipItem);
            Reference.loader.currentScene.guiOptions.Clear();
            Reference.player.InitiateInventoryChest();
        }
    }
}
