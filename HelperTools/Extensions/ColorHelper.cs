//using DrawingColor = System.Drawing.Color;
//using MediaColor = System.Windows.Media.Color;

//namespace U4.SEB.Utils.Helpers.Extensions
//{
//    public class ColorHelper
//    {
//        public class Hsv
//        {
//            public Hsv(double hue, double saturation, double value)
//            {
//                Hue = hue;
//                Saturation = saturation;
//                Value = value;
//            }

//            public double Hue { get; set; }
//            public double Saturation { get; set; }
//            public double Value { get; set; }
//        }

//        public class Hsl
//        {
//            public Hsl(double hue, double saturation, double lightness)
//            {
//                Hue = hue;
//                Saturation = saturation;
//                Lightness = lightness;
//            }

//            public double Hue { get; set; }
//            public double Saturation { get; set; }
//            public double Lightness { get; set; }
//        }

//        public class Rgb
//        {
//            public Rgb(byte alpha, byte red, byte green, byte blue)
//            {
//                Alpha = alpha;
//                Blue = blue;
//                Red = red;
//                Green = green;
//            }

//            public byte Alpha { get; set; }
//            public byte Red { get; set; }
//            public byte Green { get; set; }
//            public byte Blue { get; set; }
//        }

//        public ColorHelper(double hue, double saturation, double lightness, double value, byte alpha, byte red, byte green, byte blue)
//        {
//            HsvColor = new Hsv(hue, saturation, value);
//            HslColor = new Hsl(hue, saturation, lightness);
//            RgbColor = new Rgb(alpha, red, green, blue);
//        }

//        public Hsv HsvColor { get; set; }
//        public Hsl HslColor { get; set; }
//        public Rgb RgbColor { get; set; }

//    }
//}
