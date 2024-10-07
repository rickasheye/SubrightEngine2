using System.Collections.Generic;

namespace Game
{
    public class mechaballoon : Balloon
    {
        public mechaballoon(List<Point> points) : base(points)
        {
            health = 10;
            speed = 1;
            cost = 20;
            name = "mechaballoon";
            ReloadTexture("./textures/mechaballoon.png");
        }
    }
}