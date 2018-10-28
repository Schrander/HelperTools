using HelperTools.Extensions;
using HelperTools.Helpers;
using HelperTools.Normalizations;
using HelperTools.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HelperTools.PersonalData
{
	/// <summary>
	/// Valideert, normaliseert of formateert een string naar woonplaats.
	/// </summary>
	public class CityNormalization : BaseNormalization
	{
		public static readonly CityNormalization Instance = new CityNormalization();

		private readonly List<string> _plaatsenMetEenY = new List<string>();

		public override string MaskPattern()
		{
			return RegexPatternHelper.GetAlfaMask(50);
		}

		public override string ValidationPattern()
		{
			return RegexPatternHelper.GetAlfaMask(50);
		}

		public override string FormatPattern()
		{
			return null;
		}

		public override RegexOptions Options => base.Options | RegexOptions.IgnoreCase;

		public List<string> PlaatsenMetEenY
		{
			get
			{
				if (!_plaatsenMetEenY.Any())
				{
					_plaatsenMetEenY.Add("tynaarlo");
					_plaatsenMetEenY.Add("yde");
					_plaatsenMetEenY.Add("garyp");
					_plaatsenMetEenY.Add("gytsjerk");
					_plaatsenMetEenY.Add("hurdegaryp");
					_plaatsenMetEenY.Add("lytsewierrum");
					_plaatsenMetEenY.Add("ryptsjerk");
					_plaatsenMetEenY.Add("ysbrechtum");
					_plaatsenMetEenY.Add("ypecolsga");
					_plaatsenMetEenY.Add("tytsjerk");
					_plaatsenMetEenY.Add("eys");
					_plaatsenMetEenY.Add("heythuysen");
					_plaatsenMetEenY.Add("ysselsteyn");
					_plaatsenMetEenY.Add("luyksgestel");
					_plaatsenMetEenY.Add("yerseke");
					_plaatsenMetEenY.Add("hippolytushoef");
					_plaatsenMetEenY.Add("lelystad");
				}
				return _plaatsenMetEenY;
			}

		}

		//public override void Normalize(ref string value)
		//{
		//	value = Normalize(value);
		//}

		public override string Normalize(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = Sanitize(value);

			if (!Validate(value)) 
				return value;

			value = value.ToPascalCase();

			// Plaatsen zoals 's-Gravenhage en 's-Hertogenbosch
			MatchCollection matches = Regex.Matches(value, @"(?n)(^([' ])?|[ ][']?)[s][ '-][ '-]?", Options);
			foreach (Match m in matches)
				value = value.Replace(m.Value, "'s-").Trim();

			// Plaatsen zoals 't Harde
			matches = Regex.Matches(value, @"(?n)(?<city>(([' ])?|[ ][']?)[st][ '-]([\w]+))", Options);
			foreach (Match m in matches)
				value = value.Replace(m.Value, "'t ").Trim();

			return value;

			//nothing happened.
		}

		public override bool Validate(string objectToValidate)
		{
			return !string.IsNullOrEmpty(objectToValidate) && Regex.IsMatch(Sanitize(objectToValidate), ValidationPattern());
		}

		public override bool Validate(string objectToValidate, out string sanitized)
		{
			sanitized = objectToValidate.Sanitize();
			return !string.IsNullOrEmpty(objectToValidate) && Regex.IsMatch(Sanitize(objectToValidate), ValidationPattern());
		}

		public override string Sanitize(string value)
		{
			return value.Sanitize();
		}
	}
}
