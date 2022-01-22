namespace SubrightEngine2.EngineStuff
{
    public class Rectangle
    {
        public int x, y;
        public int width, height;

        public int Left
        {
            get { return this.x; }
        }

        public int Right
        {
            get { return (this.x + this.width); }
        }

        public int Top
        {
            get { return this.y; }
        }

        public int Bottom
        {
            get { return (this.y + this.height); }
        }

        public Rectangle(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rectangle(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}