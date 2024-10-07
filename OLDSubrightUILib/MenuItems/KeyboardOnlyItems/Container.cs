using SubrightEngine2.EngineStuff;
using System;

namespace RPGConsole.Graphical.MenuItems.KeyboardOnlyItems
{
    public class Container : GUIOption
    {
        //container class
        public enum Alignment
        {
            LEFT, RIGHT, UP, DOWN, MIDDLE
        }

        public Alignment alignment;

        public Container(Alignment alignment, Vector2 size, Vector2 position) : base(size, position)
        {
            this.alignment = alignment;
        }

        public void ResetScaling()
        {
            //scale well?
            Vector2 Lastposition = new Vector2(0, 0);
            Vector2 Lastsize = new Vector2(0, 0);
            foreach (GUIOption option in children)
            {
                if (Lastposition != new Vector2(0, 0))
                {
                    Lastposition = option.position;
                    Lastsize = option.size;
                    switch (alignment)
                    {
                        case Alignment.DOWN:
                            option.position = new Vector2(position.X + (size.X / 2), position.Y + size.Y);
                            break;

                        case Alignment.LEFT:
                            option.position = new Vector2(position.X, position.Y + (size.Y / 2));
                            break;

                        case Alignment.MIDDLE:
                            option.position = new Vector2(position.X + (size.X / 2), position.Y + (size.Y / 2));
                            break;

                        case Alignment.RIGHT:
                            option.position = new Vector2(position.X + size.X, position.Y + (size.Y / 2));
                            break;

                        case Alignment.UP:
                            option.position = new Vector2(position.X + (size.X / 2), position.Y);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Unfortunately that element is at 0, 0 and not able to be sized!");
                }
            }
        }
    }
}