using SubrightEngine2.EngineStuff.Scenes;

namespace Game
{
    public class CreateGameScene : Scene
    {
        public CreateGameScene() : base("GameScene")
        {
        }

        //since we dont have a game scene or editor we need to create one.
        public override void LoadScene()
        {
            base.LoadScene();
            //create the scene.
            //create the background.
            SpriteBackground background = new SpriteBackground();
            AddGameObject(background);
        }
    }
}