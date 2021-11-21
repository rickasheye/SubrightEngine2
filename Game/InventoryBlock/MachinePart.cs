using Raylib_cs;
using RPGConsole.Graphical;
using RPGConsole.Graphical.ScenesAvaliable;
using SubrightEngine2.EngineStuff.Scenes;
using System;

namespace RPGConsole.InventoryBlock
{
    [Serializable]
    public class BlankMachinePart : Block
    {
        public string middleMachinePart = "Textures/blocks/machine_parts/blank_machine_part.png";
        public string rightMachinePart = "Textures/blocks/machine_parts/blank_machine_part_right.png";
        public string leftMachinePart = "Textures/blocks/machine_parts/blank_machine_part_left.png";
        public string upMachinePart = "Textures/blocks/machine_parts/blank_machine_part_up.png";
        public string downMachinePart = "Textures/blocks/machine_parts/blank_machine_part_down.png";

        public string others = "";
        public AssetLoader loaderAsset;

        public bool disabled = false;

        public int powerLevel = 0;

        public BlankMachinePart(string MachineBlockName, string middleMachinePart, string rightMachinePart, string leftMachinePart, string downMachinePart) : base(MachineBlockName, 0, 0, 5, Color.GRAY, "Textures/blocks/machine_parts/blank_machine_part.png", 1)
        {
            name = MachineBlockName;
            this.middleMachinePart = middleMachinePart;
            this.rightMachinePart = rightMachinePart;
            this.leftMachinePart = leftMachinePart;
            this.downMachinePart = downMachinePart;
            GeneralStartup();
        }

        public BlankMachinePart(string MachineBlockName) : base(MachineBlockName, 0, 0, 5, Color.GRAY, 1)
        {
            name = MachineBlockName;
        }

        public BlankMachinePart() : base("Blank Machine Part", 0, 0, 5, Color.GRAY, 1)
        {

        }

        public void GeneralStartup()
        {
            if (loaderAsset == null)
            {
                MainScene sceneMain = Reference.loader.currentScene as MainScene;
                if (sceneMain != null)
                {
                    loaderAsset = sceneMain.loaderAsset;
                }
            }
            UpdateBlock();
        }

        public void UpdateMachineBlock()
        {
            others = "";
            if (Reference.gen.returnBlock(position.X + 1, position.Y) is BlankMachinePart)
            {
                //we want to actuallY modifY this!
                others += " right";
                BlankMachinePart part = Reference.gen.returnBlock(position.X + 1, position.Y) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
            else if (Reference.gen.returnBlock(position.X - 1, position.Y) is BlankMachinePart)
            {
                others += " left";
                BlankMachinePart part = Reference.gen.returnBlock(position.X - 1, position.Y) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
            else if (Reference.gen.returnBlock(position.X, position.Y + 1) is BlankMachinePart)
            {
                others += " down";
                BlankMachinePart part = Reference.gen.returnBlock(position.X, position.Y + 1) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
            else if (Reference.gen.returnBlock(position.X, position.Y - 1) is BlankMachinePart)
            {
                others += " up";
                BlankMachinePart part = Reference.gen.returnBlock(position.X, position.Y - 1) as BlankMachinePart;
                DemeritPowerLevel(part);
            }
        }

        public void DemeritPowerLevel(BlankMachinePart machinePart)
        {
            if (machinePart != null)
            {
                if (machinePart.powerLevel > 0)
                {
                    machinePart.powerLevel = powerLevel - 1;
                }
            }
        }

        public override void UpdateBlock()
        {
            base.UpdateBlock();
            string[] splitString = others.Split(' ');
            for (int i = 0; i < splitString.Length; i++)
            {
                string splitStringChosen = splitString[i];
                switch (splitStringChosen.ToLower())
                {
                    case "right":
                        Raylib.DrawTexture(loaderAsset.textureLoad(rightMachinePart), (int)position.X, (int)position.Y, Raylib_cs.Color.WHITE);
                        break;
                    case "left":
                        Raylib.DrawTexture(loaderAsset.textureLoad(leftMachinePart), (int)position.X, (int)position.Y, Raylib_cs.Color.WHITE);
                        break;
                    case "down":
                        Raylib.DrawTexture(loaderAsset.textureLoad(downMachinePart), (int)position.X, (int)position.Y, Raylib_cs.Color.WHITE);
                        break;
                    case "up":
                        Raylib.DrawTexture(loaderAsset.textureLoad(upMachinePart), (int)position.X, (int)position.Y, Raylib_cs.Color.WHITE);
                        break;
                }
            }

            if (disabled == true)
            {
                Raylib.DrawTexture(loaderAsset.textureLoad("TeXtures/overlays/cross.png"), (int)position.X, (int)position.Y, Raylib_cs.Color.WHITE);
            }
        }
    }
}
