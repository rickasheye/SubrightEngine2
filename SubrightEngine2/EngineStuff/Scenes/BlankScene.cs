using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightEngine2.EngineStuff.Scenes
{
    [Serializable]
    public class BlankScene : Scene
    {
        //used to load so first scene isnt "undefined" to depart from the base class "Scene"
        public BlankScene() : base("BlankScene") { }
    }
}
