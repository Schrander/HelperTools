namespace HelperTools.Helpers
{
	public static class BooleanHelper
	{

		public static bool StringToBool(string item)
		{
			if (!string.IsNullOrEmpty(item))
			{
				switch (item)
				{
					case "1":
					case "y":
					case "yes":
					case "ja":
					case "j":
						return true;
					case "0":
					case "n":
					case "no":
					case "nee":
						return false;
					default:
						return false;
				}

			}
			return false;
		}

	}
}