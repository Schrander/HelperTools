//using System.Windows.Media;
//using Color = System.Drawing.Color;

//namespace U4.SEB.Utils.Helpers.Extensions
//{
//    public static class ColorConverters
//    {
//        public static Color GetDrawingColor(this System.Windows.Media.Color mediaColor)
//        {
//            Color drawingColor = Color.FromArgb(mediaColor.A, mediaColor.R, mediaColor.G, mediaColor.B);
//            return drawingColor;
//        }

//        public static Color GetDrawingColor(this SolidColorBrush mediaBrush)
//        {
//            Color drawingColor = Color.FromArgb(mediaBrush.Color.A, mediaBrush.Color.R, mediaBrush.Color.G, mediaBrush.Color.B);
//            return drawingColor;
//        }

//        public static System.Windows.Media.Color GetMediaColor(this Color drawingColor)
//        {
//            System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
//            return mediaColor;
//        }

//        public static SolidColorBrush GetMediaBrush(this Color drawingColor)
//        {
//            SolidColorBrush mediaBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B));
//            return mediaBrush;
//        }
//    }
//}
