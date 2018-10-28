using static System.Convert;
using static System.Math;

namespace HelperTools.Helpers
{
	public static class GeoHelper
	{
		
		public static string FormatLatitude(decimal value)
		{
			return FormatLatitude(ToDouble(value));
		}

		public static string FormatLatitude(double value)
		{
			var direction = value < 0 ? 'S' : 'N';

			value = Abs(value);

			var degrees = Truncate(value);

			value = (value - degrees) * 60;       //not value = (value - degrees) / 60;

			var minutes = Truncate(value);
			var seconds = (value - minutes) * 60; //not value = (value - degrees) / 60;


			return string.Concat(direction, degrees.ToString("N0"), '°', minutes.ToString("N0"), "'", seconds.ToString("N1"));
		}

		public static string FormatLongitude(decimal value)
		{
			return FormatLongitude(ToDouble(value));
		}

		public static string FormatLongitude(double value)
		{
			var direction = value < 0 ? 'W' : 'E';

			value = Abs(value);

			var degrees = Truncate(value);

			value = (value - degrees) * 60;       //not value = (value - degrees) / 60;

			var minutes = Truncate(value);
			var seconds = (value - minutes) * 60; //not value = (value - degrees) / 60;

			return string.Concat(direction, degrees.ToString("N0"), '°', minutes.ToString("N0"), "'", seconds.ToString("N1"));
		}

		
	}
}