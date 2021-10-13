namespace SubrightEngine2.EngineStuff
{
    public class Color
    {
        //
        // Summary:
        //     Represents a color that is null.
        public static readonly Color Empty;

        public static readonly Color White = new Color(255, 255, 255);
        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color CatalinaBlue = new Color(43, 51, 95);
        public static readonly Color VividViolet = new Color(126, 32, 144);
        public static readonly Color LightSeaGreen = new Color(25, 149, 156);
        public static readonly Color SolidPink = new Color(139, 72, 82);
        public static readonly Color Mariner = new Color(57, 92, 152);
        public static readonly Color CottonCandy = new Color(169, 193, 255);
        public static readonly Color WhiteSmoke = new Color(238, 238, 238);
        public static readonly Color Ruby = new Color(212, 24, 108);
        public static readonly Color RawSienna = new Color(211, 132, 65);
        public static readonly Color Ranchi = new Color(233, 195, 91);
        public static readonly Color MonteCarlo = new Color(112, 198, 169);
        public static readonly Color JordyBlue = new Color(118, 150, 222);
        public static readonly Color DarkGray = new Color(163, 163, 163);
        public static readonly Color MonaLisa = new Color(255, 151, 152);
        public static readonly Color DesertSand = new Color(237, 199, 176);

        //raylib ported colors
        public static Color LIGHTGRAY = new Color(200, 200, 200, 255);
        public static Color GRAY = new Color(130, 130, 130, 255);
        public static Color DARKGRAY = new Color(80, 80, 80, 255);
        public static Color YELLOW = new Color(253, 249, 0, 255);
        public static Color GOLD = new Color(255, 203, 0, 255);
        public static Color ORANGE = new Color(255, 161, 0, 255);
        public static Color PINK = new Color(255, 109, 194, 255);
        public static Color RED = new Color(230, 41, 55, 255);
        public static Color MAROON = new Color(190, 33, 55, 255);
        public static Color GREEN = new Color(0, 228, 48, 255);
        public static Color LIME = new Color(0, 158, 47, 255);
        public static Color DARKGREEN = new Color(0, 117, 44, 255);
        public static Color SKYBLUE = new Color(102, 191, 255, 255);
        public static Color BLUE = new Color(0, 121, 241, 255);
        public static Color DARKBLUE = new Color(0, 82, 172, 255);
        public static Color PURPLE = new Color(200, 122, 255, 255);
        public static Color VIOLET = new Color(135, 60, 190, 255);
        public static Color DARKPURPLE = new Color(112, 31, 126, 255);
        public static Color BEIGE = new Color(211, 176, 131, 255);
        public static Color BROWN = new Color(127, 106, 79, 255);
        public static Color DARKBROWN = new Color(76, 63, 47, 255);
        public static Color WHITE = new Color(255, 255, 255, 255);
        public static Color BLACK = new Color(0, 0, 0, 255);
        public static Color BLANK = new Color(0, 0, 0, 0);
        public static Color MAGENTA = new Color(255, 0, 255, 255);
        public static Color RAYWHITE = new Color(245, 245, 245, 255);

        /// <summary> Creates a new Color from rgb. </summary>
        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color(int r, int g, int b, int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary> Red component. </summary>
        public int R { get; set; }

        /// <summary> Green component. </summary>
        public int G { get; set; }

        /// <summary> Blue component. </summary>
        public int B { get; set; }

        // <summary> Alpha component. </summary>
        public int A { get; set; }

        public void SetColor(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}