using System;

namespace HelperTools.Helpers
{
	public static class GeoHelper
	{
		
		public static string FormatLatitude(decimal Value)
		{
			return FormatLatitude(Convert.ToDouble(Value));
		}

		public static string FormatLatitude(double Value)
		{
			var direction = Value < 0 ? 'S' : 'N';

			Value = System.Math.Abs(Value);

			var degrees = System.Math.Truncate(Value);

			Value = (Value - degrees) * 60;       //not Value = (Value - degrees) / 60;

			var minutes = System.Math.Truncate(Value);
			var seconds = (Value - minutes) * 60; //not Value = (Value - degrees) / 60;
															  //...

			return string.Concat(direction, degrees.ToString("N0"), '°', minutes.ToString("N0"), "'", seconds.ToString("N1"));
		}

		public static string FormatLongitude(decimal Value)
		{
			return FormatLongitude(Convert.ToDouble(Value));
		}

		public static string FormatLongitude(double Value)
		{
			var direction = Value < 0 ? 'W' : 'E';

			Value = System.Math.Abs(Value);

			var degrees = System.Math.Truncate(Value);

			Value = (Value - degrees) * 60;       //not Value = (Value - degrees) / 60;

			var minutes = System.Math.Truncate(Value);
			var seconds = (Value - minutes) * 60; //not Value = (Value - degrees) / 60;
															  //...

			return string.Concat(direction, degrees.ToString("N0"), '°', minutes.ToString("N0"), "'", seconds.ToString("N1"));
		}

		
	}
}