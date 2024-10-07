using Raylib_cs;
using SubrightEngine2.EngineStuff;

namespace Game
{
    public class ShockTower : Tower
    {
        public ShockTower(Vector2 position, SpriteBackground background) : base(position, "Shock Tower", "./textures/shocktower.png", background, 120)
        {
            targetAffect = 1;
            cost = 50;
            atkspeed = 10;
        }

        public ShockTower()
        {
            background = GameObject.FindWithParentType<SpriteBackground>();
            ReloadTexture("./textures/shocktower.png");
            name = "Shock Tower";
            targetAffect = 3;
            cost = 50;
        }

        public override void Draw2D(ref Camera2D cam)
        {
            base.Draw2D(ref cam);
            if (targetBalloon != null)
            {
                Raylib.DrawLine((int)position.X, (int)position.Y, (int)targetBalloon.position.X, (int)targetBalloon.position.Y, Raylib_cs.Color.Blue);
            }
        }
    }
}