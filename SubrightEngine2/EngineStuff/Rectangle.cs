namespace SubrightEngine2.EngineStuff
{
    public class Rectangle
    {
        public float x, y;
        public float width, height;

        public Rectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rectangle(int x, int y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        
        public Rectangle(float x, float y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Rectangle(float x, float y)
        {
            this.x = x;
            this.y = y;
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