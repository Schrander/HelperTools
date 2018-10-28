using System;

namespace HelperTools.Web
{
    public static class ImageExt
	{

		public static void CalculateResolution(int orgWidth, int orgHeight, double scale, out int oWidth, out int oHeight)
		{
			oWidth = (int)(orgWidth / scale);
			oHeight = (int)(orgHeight / scale);
		}

		public static void CalculateResolution(int orgWidth, int orgHeight, int newWidth, int newHeight, out int oWidth, out int oHeight, out double scale)
		{

			double scaleH = (orgHeight == 0 ? 1 : Convert.ToDouble(orgHeight) / Convert.ToDouble(newHeight));
			double scaleW = (orgWidth == 0 ? 1 : Convert.ToDouble(orgWidth) / Convert.ToDouble(newWidth));
			scale = Math.Max(scaleH, scaleW);

			oWidth = scale < 1 ? orgWidth : (int)(orgWidth / scale);
			oHeight = scale < 1 ? orgHeight : (int)(orgHeight / scale);
		}

		public static Padding CalculateMargin(int imgWidth, int imgHeight, int maxWidth, int maxHeight)
		{
			int floor = MathExt.Floor<double, int>(MathExt.Max<double>(maxWidth - imgWidth, 0) / 2d);
			int ceiling = MathExt.Ceiling<double, int>(MathExt.Max<double>(maxWidth - imgWidth, 0) / 2d);

			Padding margin = new Padding { Left = floor, Right = ceiling, Bottom = floor, Top = ceiling };

			return margin;
		}

		public struct Padding
		{
			public int Left { get; set; }
			public int Right { get; set; }
			public int Top { get; set; }
			public int Bottom { get; set; }
		}
	}
}