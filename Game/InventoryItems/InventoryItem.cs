using Raylib_cs;
using RPGConsole.Graphical;
using SubrightEngine2.EngineStuff;
using SubrightEngine2.EngineStuff.Scenes;
using System;

namespace RPGConsole.InventoryItems
{
    [Serializable]
    public enum itemTYPE
    {
        WEAPON, TOOL, GENERAL
    }

    [Serializable]
    public enum entityType
    {
        BLOCK, ITEM
    }

    [Serializable]
    public class InventoryItem : GameObject
    {
        public string name;
        public itemTYPE type;
        public entityType typeMaterial = entityType.ITEM;
        public int itemCount = 1;

        public string texture = "Textures/blocks/air.png";

        public Texture2D textureInit(AssetLoader loader)
        {
            return loader.textureLoad(texture);
        }

        public InventoryItem(string name, itemTYPE type) : base(name)
        {
            this.name = name;
            this.type = type;
        }

        public InventoryItem(string name, itemTYPE type, string texturePath) : base(name)
        {
            this.name = name;
            this.type = type;
            this.texture = texturePath;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount) : base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount, string texturePath) : base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
            this.texture = texturePath;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount, entityType typeEntity) : base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
            this.typeMaterial = typeEntity;
        }

        public InventoryItem(string name, itemTYPE type, int itemCount, entityType typeEntity, string texturePath) : base(name)
        {
            this.name = name;
            this.type = type;
            this.itemCount = itemCount;
            this.typeMaterial = typeEntity;
            texture = texturePath;
        }

        public InventoryItem()
        {
            if (SubrightEngine2.Program.debug) { Debug.Log("ITEM IS EMPTY and but called: " + name); }
        }
    }
}
