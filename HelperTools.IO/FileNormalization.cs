using System.Text.RegularExpressions;
using HelperTools.Normalizations;
using HelperTools.Text;

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
			const string drive = @"(?i)(?<drive>([A-Z][:][\\]))";
			const string directory = @"(?<directory>([^\\\/:\*\?""\<\>\|]{1,255}[\\])+)";

			return string.Concat(drive, directory);
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
			string drive = Regex.Replace(value, MaskPattern(), FormatPattern());

			return drive;
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

		public override string Sanitize(string value)
		{
			return !string.IsNullOrEmpty(value) ? value.Sanitize() : null;
		}
	}
}
