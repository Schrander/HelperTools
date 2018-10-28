namespace HelperTools.PersonalData
{
	public static class GenderHelper
	{
		public static string HeOrShe(this Gender? gender)
		{
			if (!gender.HasValue) 
				return "zijn/haar";

			switch (gender.Value)
			{
				case Gender.Man:
					return "zijn";

				case Gender.Female:
					return "haar";
				default:
					return "zijn/haar";
			}
		}

		//http://taaladvies.net/taal/advies/tekst/95/
		public static string Aanhef(this Gender? gender)
		{
			return Aanhef(gender, false);
		}

		public static string Aanhef(this Gender? gender, bool isAfk)
		{
			if (gender.HasValue)
			{
				switch (gender.Value)
				{
					case Gender.Man:
						return isAfk ? "Dhr." : "heer";

					case Gender.Female:
						return isAfk ? "Mevr." : "mevrouw";
				}
			}
			return "heer/mevrouw";
		}
	}
}
