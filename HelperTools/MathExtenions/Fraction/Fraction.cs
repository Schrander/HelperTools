namespace HelperTools.MathExtensions
{

	public class Fraction
	{
		public int Numerator { get; set; }
		public int Denominator { get; set; }

		public static Fraction operator +(Fraction first, Fraction second)
		{
			return new Fraction {
				Numerator = first.Numerator * second.Denominator + second.Numerator * first.Denominator,
				Denominator = first.Denominator * second.Denominator
			};	
		}

		public static Fraction operator -(Fraction first, Fraction second)
		{
			return new Fraction {
				Numerator = first.Numerator * second.Denominator - second.Numerator * first.Denominator,
				Denominator = first.Denominator * second.Denominator
			};	
		}

		public static Fraction operator *(Fraction first, Fraction second)
		{
			return new Fraction {
				Numerator = first.Numerator * second.Numerator,
				Denominator = first.Denominator * second.Denominator
			};
		}

		public static Fraction operator /(Fraction first, Fraction second)
		{
			Fraction s = new Fraction { Numerator = second.Denominator, Denominator = second.Numerator };
			return first * s;
		}

		public override string ToString()
		{
			string output = $"{Numerator}/{Denominator}";

			if (Numerator > 1)
			{
				Fraction simplified = new Fraction {Numerator = Numerator, Denominator = Denominator}.Simplify();

				if (simplified.Numerator != Numerator && simplified.Denominator != Denominator)
					output += $"=> {simplified.Numerator}/{simplified.Denominator}";

				if (simplified.Numerator >= simplified.Denominator)
					output += $"=> {simplified.ToWholeFraction()}";
			}

			return output;
		}
	}


}