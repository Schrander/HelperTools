using HelperTools.Normalizations;
using HelperTools.Text;
using System.Text.RegularExpressions;

namespace HelperTools.IO
{

	/// <summary>
	/// Valideert, normaliseert of formateert een file
	/// </summary>
	public class FileNormalization : BaseNormalization
	{
		public static readonly FileNormalization Instance = new FileNormalization();

		public override string MaskPattern()
		{
			const string drive = @"(?<drive>([a-zA-Z][:][\\]))";
			const string directory = @"(?<directory>([^\\\/:\*\?""\<\>\|]{1,255}[\\])+)";

			return $"{drive}{directory}";
		}

		// http://www.regexlib.com/REDetails.aspx?regexp_id=90	
		public override string ValidationPattern()
		{
			return MaskPattern();
		}

		public override string FormatPattern()
		{
			return "${drive}${directory}";
		}

		public override string Normalize(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			Sanitize(value);

			if (!Validate(value)) return value;
			string drive = Regex.Replace(value, @"(?<drive>([A-Z][:][\\]))", "${drive}", Options);
			string directory = Regex.Replace(value, @"(?<directory> ([^\\\/:\*\?""\<\>\|]{1,255}[\\])+)", "${directory}", Options);

			return $"{drive}{directory}";
		}

		/// <summary>
		/// Valideert of een string voldoet aan een url.
		/// </summary>
		/// <param name="objectToValidate"></param>
		/// <returns></returns>
		public override bool Validate(string objectToValidate)
		{
			return !string.IsNullOrEmpty(objectToValidate) && Regex.IsMatch(objectToValidate, ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = Sanitize(objectToValidate);
			return !string.IsNullOrWhiteSpace(objectToValidate) && Regex.IsMatch(sanitized, ValidationPattern(), Options);
		}

		public override string Sanitize(string value)
		{
			return !string.IsNullOrEmpty(value) ? value.Sanitize() : null;
		}
	}
}
