using System.Text.RegularExpressions;
using HelperTools.Normalizations;
using HelperTools.Text;

namespace HelperTools.PersonalData
{
	/// <summary>
	/// Valideert, normaliseert of formateert een string naar geldig postcode.
	/// </summary>
	public class ZipcodeNormalization : BaseNormalization
	{
		public static readonly ZipcodeNormalization Instance = new ZipcodeNormalization();

		public override string MaskPattern()
		{
			return MaskPattern(151);
		}

		public override string ValidationPattern()
		{
			return ValidationPattern(151);
		}

		public override string FormatPattern()
		{
			return FormatPattern(151);
		}

		public override string Normalize(string value)
		{
			return Normalize(value, 151);
		}

		/// <summary>
		/// Formateert een string naar een postcode.
		/// Example: '3125aa' >> '3125 AA'
		/// </summary>
		public string Normalize(string value, int landID)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			value = Sanitize(value);
			return Validate(value) ? Regex.Replace(value, ValidationPattern(), FormatPattern()) : value;
		}

		/// <summary>
		/// Valideert of een string een geldige postcode is.
		/// </summary>
		public override bool Validate(string objectToValidate)
		{
			if (string.IsNullOrWhiteSpace(objectToValidate))
				return false;
			objectToValidate = objectToValidate.Sanitize();
			return Regex.IsMatch(objectToValidate, MaskPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{		
			sanitized = objectToValidate.Sanitize();
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(objectToValidate, MaskPattern());
		}

		/// <summary>
		/// Valideert of een string een geldige postcode is.
		/// </summary>
		public bool Validate(string objectToValidate, int landID)
		{
			return string.IsNullOrWhiteSpace(objectToValidate) || Regex.IsMatch(objectToValidate, MaskPattern(landID));
		}

		public string FormatPattern(int landID)
		{
			switch (landID)
			{
				case 151: //Nederland
					return "${number} ${letter}";
				case 21: //Belgie
					return "${number}";
				case 57: //Duitsland
					return "${number}";
				case 232: //VK
					return "${number} ${number2}";
				case 70: //Frankrijk
					return "${number}";
				case 102: //Italie
					return "${number}";
				case 204: //Spanje
					return "${number}";
				case 178: //Polen
					return "${number1}-${number2}";
				default:
					return @"[A-Za-z0-9]{1,10}";
			}
		}

		public string ValidationPattern(int? landID)
		{
			if (!landID.HasValue)
				return @"^[A-Za-z0-9]{1,10}$";

			switch (landID.Value)
			{
				case 151: //Nederland
					return @"^(?<number>[1-9]{1}[0-9]{3}) ?(?<letter>\p{Lu}{2})$";
				case 21: //Belgie
					return @"^(?<number>[1-9]{1}[0-9]{3})$";
				case 57: //Duitsland
					return @"^(?<number>[1-9]{1}[0-9]{4})$";
				case 232: //VK
					return
						@"^(?<number>(GIR 0AA))|(((?<number>([A-Z-[QVX]][0-9][0-9]?))|((?<number>([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?))|((?<number>([A-Z-[QVX]][0-9][A-HJKSTUW]))|(?<number>([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY]))))) (?<number2>[0-9][A-Z-[CIKMOV]]{2}))$";
				case 70: //Frankrijk
					return @"^(?<number>[0-9]{5})$";
				case 102: //Italie
					return @"^(?<number>[0-9]{5})$";
				case 204: //Spanje
					return @"^(?<number>([0-9][1-9]|[1-4][0-9]|[5][012])[0-9]{3})$";
				case 178: //Polen
					return @"^(?<number1>[0-9]{2})-(?<number2>[0-9]{3})$";
				default:
					return @"^[A-Za-z0-9]{1,10}$";
			}
		}

		private string MaskPattern(int? landID)
		{

			if (!landID.HasValue)
				return @"[A-Za-z0-9]{1,10}";

			switch (landID.Value)
			{
				case 151: //Nederland
					return @"([1-9]{1}[0-9]{3} ?\p{Lu}{2})";
				case 21: //Belgie
					return @"[1-9]{1}[0-9]{3}";
				case 57: //Duitsland
					return @"[1-9]{1}[0-9]{4}";
				case 232: //VK
					return @"(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})";
				case 70: //Frankrijk
					return @"(F-)?((2[A|B])|[0-9]{2})[0-9]{3}";
				case 102: //Italie
					return @"[0-9]{5}";
				case 204: //Spanje
					return @"([0-9][1-9]|[1-4][0-9]|[5][012])[0-9]{3}";
				case 178: //Polen
					return @"[0-9]{2}-[0-9]{3}";
				default:
					return @"[A-Za-z0-9]{1,10}";
			}
		}

		public override string Sanitize(string value)
		{
			return string.IsNullOrEmpty(value) ? null : value.Sanitize();
		}
	}
}
