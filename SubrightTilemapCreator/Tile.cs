using Raylib_cs;
using SubrightEngine2.EngineStuff.BaseComponents;
using System;

namespace SubrightTilemapCreator
{
    [Serializable]
    public class Tile : Component
    {
        public string tile = "";
        [NonSerialized] public Texture2D tileStore;

        public Tile() : base("Tile")
        {
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            //loading character
            if (tileStore.width == 0 && tileStore.height == 0)
            {
            }
        }
    }
}