using HelperTools.Helpers;
using HelperTools.Text;

namespace HelperTools.PersonalData
{
	public static class PersonNameHelper
	{
		public static string GetAchternaamVoorletters(string voorletters, string tussenvoegsels, string achternaam, string vervolgnaam = null)
		{
			if (NullableHelper.AllAreNull(voorletters, tussenvoegsels, achternaam, vervolgnaam))
				return null;

			return StringHelper.Concat(tussenvoegsels.FirstLetterToUpper(), GetVolledigeAchternaam(achternaam, vervolgnaam))
				+ ", " + InitialsNormalization.Instance.Normalize(voorletters);
		}

		public static string GetVoorlettersAchternaam(string voorletters, string tussenvoegsels, string achternaam, string vervolgnaam = null)
		{
			if (NullableHelper.AllAreNull(voorletters, tussenvoegsels, achternaam, vervolgnaam))
				return null;

			return StringHelper.Concat(InitialsNormalization.Instance.Normalize(voorletters), tussenvoegsels.ToLowerNullable(),
					GetVolledigeAchternaam(achternaam, vervolgnaam));
		}

		public static string GetVolledigeAchternaam(string achternaam, string vervolgnaam)
		{
			string name = achternaam.Sanitize(true);
			name = !string.IsNullOrEmpty(vervolgnaam) ? (!string.IsNullOrEmpty(name) ? name.ToTitleCase() + "-" : null) + vervolgnaam : name.ToTitleCase();
			return SetName(name);
		}

		private static string SetName(string naam)
		{
			return LastNameNormalization.Instance.Normalize(naam);
		}

		public static string GetVolledigeNaam(string voorletters, string tussenvoegsels, string achternaam, string vervolgnaam)
		{
			if (NullableHelper.AllAreNull(voorletters, tussenvoegsels, achternaam, vervolgnaam))
				return null;

			return StringHelper.Concat(InitialsNormalization.Instance.Normalize(voorletters), tussenvoegsels.ToLowerNullable(), GetVolledigeAchternaam(achternaam, vervolgnaam));
		}

		public static string GetVolledigeNaamInformeel(string roepnaam, string tussenvoegsels, string achternaam, string vervolgnaam)
		{
			if (NullableHelper.AllAreNull(roepnaam, tussenvoegsels, achternaam, vervolgnaam))
				return null;

			return StringHelper.Concat(roepnaam, tussenvoegsels.ToLowerNullable(), GetVolledigeAchternaam(achternaam, vervolgnaam));
		}

		public static string GetVolledigeNaamTitels(string aanhef, string voorletters, string tussenvoegsels, string achternaam, string vervolgnaam, string titel, string titelSpec, string roepnaam)
		{
			if (NullableHelper.AllAreNull(voorletters, tussenvoegsels, achternaam, vervolgnaam))
				return null;

			return StringHelper.Concat(aanhef, titel, InitialsNormalization.Instance.Normalize(voorletters),
				tussenvoegsels.ToLowerNullable(), GetVolledigeAchternaam(achternaam, vervolgnaam), titelSpec);
		}

		public static string GetVolledigeRoepnaamTitels(string aanhef, string tussenvoegsels, string achternaam, string vervolgnaam, string titel, string titelSpec, string roepnaam)
		{
			if (NullableHelper.AllAreNull(roepnaam, tussenvoegsels, achternaam, vervolgnaam))
				return null;

			string name = StringHelper.Concat(aanhef, titel, roepnaam.ToTitleCase(), tussenvoegsels.ToLower(), GetVolledigeAchternaam(achternaam, vervolgnaam), titelSpec);
			return name.Sanitize();
		}
	}
}
