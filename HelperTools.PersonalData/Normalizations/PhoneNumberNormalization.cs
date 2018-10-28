using HelperTools.Normalizations;
using HelperTools.Text;
using System.Text.RegularExpressions;

namespace HelperTools.PersonalData
{
	/// <summary>
	/// Valideert, normaliseert of formateert een string naar telefoonnummer.
	/// Locaal, nationaal of internationaal.
	/// </summary>
	public class PhoneNumberNormalization : BaseNormalization
	{
		public static readonly PhoneNumberNormalization Instance = new PhoneNumberNormalization();

		// Stadnetnummers : 010, 020, 072 etc
		// De leading zero is weggelaten
		private const string StadNetnummer = @"(1[035]|2[0346]|3[03568]|4[0356]|5[0358]|(7|8)[0-9])";

		// Dorpnetnummers : 0226, 0229, 0579
		// De leading zero is weggelaten;
		private const string DorpNetnummer = @"(1[^035][0-9]|2[^0346][0-9]|3[^03568][0-9]|4[^0356][0-9]|5[^0358][0-9])";

		public override string MaskPattern()
		{
			// Stadnummer: 020-123 45 67
			const string nlNationaalStad = "0" + StadNetnummer + @"\-?[0-9]{3}( )?[0-9]{2}( )?[0-9]{2}";

			// Dorpnummer: 0226-12 34 56
			const string nlNationaalDorp = "0" + DorpNetnummer + @"\-?[0-9]{2}( )?[0-9]{2}( )?[0-9]{2}";

			// Mobiel 06-1234 56 78
			const string nlNationaalMobiel = @"06\-?[0-9]{4}( )?[0-9]{2}( )?[0-9]{2}";

			// Informatie nummers 0800-1234
			const string nlNationaalInfo = @"0(800|90[069])\-?[0-9]{4,7}";

			// Nederlands telefoonnummer nationale notatie
			string nlNationaal = $@"({nlNationaalStad}|{nlNationaalDorp}|{nlNationaalMobiel}|{nlNationaalInfo})";

			// Stadnummer: +31 20 1234567
			const string nlInternationaalStad = @"\+31" + StadNetnummer + @"[0-9]{7}";

			// dorpnummer: +31 226 123456
			const string nlInternationaalDorp = @"\+31" + DorpNetnummer + @"[0-9]{6}";

			// Mobiel: +31 6 12345678
			const string nlInternationaalMobiel = @"\+31[0-9]6[0-9]{8}";

			// Informatie nummers +31 800 1234
			const string nlInternationaalInfo = @"\+31 (800|90[069]) \d{4,7}";
			// Nederlands telefoonnummer internationale notatie
			string nlInternationaal = $@"({nlInternationaalStad}|{nlInternationaalDorp}|{nlInternationaalMobiel}|{nlInternationaalInfo})";

			// Verenigde Staten, Canada en Oceanië.
			const string america = @"\+1-[0-9]{3}-[0-9\- ]{7,9}";
			const string russia = @"\+7 [0-9]{3} [0-9]{3} [0-9]{4}";

			//Wereld. Nederland, Rusland en Amerika zijn weggelaten.
			const string world =
				@"\+((2[07]|3[023469]|4[^2]|5[1-8]|6[0-6]|8[1246]|9[0123458])|(2[^07][0-9]|3[578][0-9]|4[2][013]|5[09][0-9]|6([78][0-9]|9[012])|8(00|5[0-9]|7[1-4]|8[06])|9[679][0-9])) [0-9 \-]{7,15}";

			//Samenstelling van de bovenstaande onderdelen.
			string pattern = $@"({nlNationaal}|{nlInternationaal}|{america}|{russia}|{world})";

			return pattern;

		}

		public override string FormatPattern()
		{
			return "${netnumber}-${abonnee}${abonnee1} ${abonnee2} ${abonnee3}";
		}

		public string FormatPattern(bool landcode)
		{
			return landcode ? "${landcode} ${netnumber} ${abonnee}${abonnee1}${abonnee2}${abonnee3}" : FormatPattern();
		}

		public override string ValidationPattern()
		{
			const string nlLandcode = @"(?<landcode>\+31)";

			// Kort netnummer. zoals 010, 020, 072 etc.
			const string nlNationaalStad = @"(?<netnumber>0" + StadNetnummer + @")(?<abonnee1>\d{3})(?<abonnee2>\d{2})(?<abonnee3>\d{2})";

			// Lang netnummer. zoals 010, 020, 072 etc.
			const string nlNationaalDorp = @"(?<netnumber>0" + DorpNetnummer + @")(?<abonnee1>\d{2})(?<abonnee2>\d{2})(?<abonnee3>\d{2})";

			// Mobiel
			const string nlNationaalMobiel = @"(?<netnumber>06)(?<abonnee1>\d{4})(?<abonnee2>\d{2})(?<abonnee3>\d{2})";

			// Informatie nummers
			const string nlNationaalInfo = @"(?<netnumber>0(800|90[069]))(?<abonnee>\d{4,7})";

			// Nederlands telefoonnummer nationale notatie
			string nlNationaal = $@"({nlNationaalStad}|{nlNationaalDorp}|{nlNationaalMobiel}|{nlNationaalInfo})";

			// Kort netnummer. zoals (0)10, (0)20, (0)72 etc.
			const string nlInternationaalStad = nlLandcode + @"(?<netnumber>" + StadNetnummer + @")(?<abonnee>\d{7})";

			// Lang netnummer. zoals 0226 etc.
			const string nlInternationaalDorp = nlLandcode + @"(?<netnumber>" + DorpNetnummer + @")(?<abonnee>\d{6})";

			// Mobiel
			const string nlInternationaalMobiel = nlLandcode + @"(?<netnumber>6)(?<abonnee>\d{8})";

			// Informatie nummers
			const string nlInternationaalInfo = nlLandcode + @"(?<netnumber>(800|90[069]))(?<abonnee>\d{4,7})";

			// Nederlands telefoonnummer internationale notatie
			string nlInternationaal = $@"({nlInternationaalStad}|{nlInternationaalDorp}|{nlInternationaalMobiel}|{nlInternationaalInfo})";


			// Verenigde Staten, Canada en Oceanië.
			const string america = @"((?<landcode>\+1)(?<abonnee>\d{10,12}))";
			const string russia = @"((?<landcode>\+7)(?<abonnee1>\d{3})(?<abonnee2>\d{3})(?<abonnee3>\d{4}))";

			//Wereld. Nederland, Rusland en Amerika is weggelaten.
			const string world = @"(?<landcode>\+(1(-\d{3})?|7|(2[07]|3[023469]|4[^2]|5[1-8]|6[0-6]|8[1246]|9[0123458])|(2[^07]\d{1}|3[578]\d{1}|4[2][013]|5[09]\d{1}|6([78]\d|9[012])|8(00|5\d|7[1-4]|8[06])|9[679]\d)))(?<abonnee>[0-9 \-]{7,15})";

			//Samenstelling van de bovenstaande onderdelen.
			string pattern = $@"^({nlNationaal}|{nlInternationaal}|{america}|{russia}|{world})$";

			return pattern;
		}

		/// <summary>
		/// Formateert een string naar een telefoonnummer
		/// </summary>
		public override string Normalize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;
			value = Sanitize(value);
			if (Validate(value))
			{
				string landcode = Regex.Replace(value, ValidationPattern(), "${landcode}");
				value = Regex.Replace(value, ValidationPattern(), FormatPattern(!string.IsNullOrEmpty(landcode)));
			}
			return !string.IsNullOrEmpty(value) ? value.Trim() : value;
		}

		/// <summary>
		/// Valideert een string of het een geldig telefoonnummer is.
		/// </summary>
		public override bool Validate(string objectToValidate)
		{
			objectToValidate = Sanitize(objectToValidate);
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(objectToValidate, ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = Sanitize(objectToValidate);
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(objectToValidate, ValidationPattern());
		}

		public override string Sanitize(string value)
		{
			return string.IsNullOrWhiteSpace(value) ? null : value.Sanitize(new[] { " ", ".", "-", "/" });
		}
	}

}

