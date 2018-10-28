using System;
using System.Text.RegularExpressions;
using HelperTools.Helpers;

namespace HelperTools.Normalizations
{
	public class NumberNormalization : BaseNormalization
	{
		public static readonly NumberNormalization Instance = new NumberNormalization();

		public override string MaskPattern()
		{
			return "N0";
		}

		public override string ValidationPattern()
		{
			return "N0";
		}

		public override string FormatPattern()
		{
			return null;
		}

		public override string Sanitize(string value)
		{
			throw new NotImplementedException();
		}

		public override string Normalize(string value)
		{
			return !Validate(value) ? null : $"{value.ParseAs<long>():N0}";
		}

		public string Normalize<T>(T? value) where T : struct
		{
			if (value == null)
				return null;

			if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTimeOffset) || typeof(T) == typeof(Guid))
				throw new InvalidCastException();

			if (!Validate(value.ToString()))
				return null;
				
			return $"{value.ParseAs<long>():N0}";
		}


		public override bool Validate(string objectToValidate)
		{
			return string.IsNullOrWhiteSpace(objectToValidate) || objectToValidate.HasOnlyDigits();
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = Sanitize(objectToValidate);
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(sanitized, ValidationPattern(), Options);
		}
	}
}