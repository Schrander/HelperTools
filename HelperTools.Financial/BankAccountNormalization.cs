using System;
using System.Text.RegularExpressions;
using HelperTools.Helpers;
using HelperTools.Normalizations;
using HelperTools.Text;

namespace HelperTools.Financial
{

	// Bankrekening normalisation is niet meer benodig
	// Eerdaags gaan de banken over op een IBAN.

	public class BankAccountNormalization : BaseNormalization
	{
		public static readonly BankAccountNormalization Instance = new BankAccountNormalization();

		public override string MaskPattern()
		{
			//return @"[nlNL]{2}[0-9]{2} [a-zA-Z]{4} [0-9]{10}";
			return RegexPatternHelper.GetAlfanumeriekMask(20);
		}

		public override string ValidationPattern()
		{
			string pattern = @"(?<country>([NL]{2}))(?<controle>([0-9]{2}))( )?(?<bank>([A-Z]{4}))( )?(?<number>([0-9]{10}))|(?<number>([0-9]{20}))";

			return string.Concat(InlineExplicitCapture, pattern);
		}

		public override string FormatPattern()
		{
			return "${country}${controle} ${bank} ${number}";
		}

		public override string Normalize(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			if (value.HasOnlyDigits())
				return Regex.Replace(Sanitize(value), ValidationPattern(), "${number}", RegexOptions.IgnoreCase);

			return Regex.Replace(Sanitize(value), ValidationPattern(), FormatPattern(), RegexOptions.IgnoreCase);
		}

		public override bool Validate(string objectToValidate)
		{
			if (string.IsNullOrEmpty(objectToValidate))
				return false;
			try
			{
				string bank = Regex.Replace(objectToValidate, ValidationPattern(), "${bank}", RegexOptions.IgnoreCase);
				string nummer = Regex.Replace(objectToValidate, ValidationPattern(), "${number}", RegexOptions.IgnoreCase);
				int controle = Convert.ToInt32(Regex.Replace(objectToValidate, ValidationPattern(), "${controle}", RegexOptions.IgnoreCase));

				bool isMatch = Regex.IsMatch(objectToValidate, ValidationPattern());
				if (isMatch)
				{
					string numbers;
					LettersToNumberConverter(bank, out numbers);

					int modulo = Modulo(Modulo(Modulo(numbers, null).ToString(), nummer).ToString(), "232100");
					int controleNummer = 98 - modulo;
					return controle.Equals(controleNummer);
				}

			}
			catch
			{
				// ignored
			}

			return false;
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			throw new NotImplementedException();
		}

		public static int Modulo(string nummer1, string nummer2)
		{
			return Convert.ToInt32(Convert.ToUInt64(nummer1 + nummer2) % 97);
		}

		public static bool LettersToNumberConverter(string value, out string numbers)
		{
			numbers = null;
			if (string.IsNullOrWhiteSpace(value) || value.Length != 4)
				return false;

			value = value.ToUpper();

			char[] chars = value.ToCharArray();

			foreach (char c in chars)
			{
				int i = Convert.ToInt32(c) - 55;
				numbers += i.ToString();
			}
			return true;
		}

		public override string Sanitize(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = value.Sanitize();

			return value.ToUpper();
		}
	}
}
