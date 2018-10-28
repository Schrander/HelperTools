using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace HelperTools.Helpers
{
	public static class ImageHelper {

		public static Image ResizeImage(Image orgImage, int? width, int? height) {
			double scaleHeight = height.HasValue ? orgImage.Height / (double)height.Value : 1;
			double scaleWidth = width.HasValue ? orgImage.Width / (double)width.Value : 1;
			double scale = MathExt.Max<double>(scaleHeight, scaleWidth);

			if (scale < 1)
				return orgImage;


			int newHeight = GetValue(height, (int)(orgImage.Height / scale));
			int newWidth = GetValue(width, (int)(orgImage.Width / scale));

			Image iThumb = new Bitmap(newWidth, newHeight);
			using (Graphics g = Graphics.FromImage(iThumb)) {
				g.DrawImage(orgImage,
							new Rectangle(0, 0, newWidth, newHeight),
							new Rectangle(0, 0, orgImage.Width, orgImage.Height),
							GraphicsUnit.Pixel);
			}
			return iThumb;
		}

		public static int GetValue(int? value, int scaledValue) {
			if (!value.HasValue) 
				return scaledValue;

			if (scaledValue < value.Value)
				return scaledValue;

			if (scaledValue > value.Value)
				return value.Value;

			return scaledValue;
		}


		public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage) {
			// BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

			using (MemoryStream outStream = new MemoryStream()) {
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create(bitmapImage));
				enc.Save(outStream);
				Bitmap bitmap = new Bitmap(outStream);

				return new Bitmap(bitmap);
			}
		}

	}
}
