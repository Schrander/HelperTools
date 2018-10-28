using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HelperTools.Helpers;

namespace HelperTools.MathExtensions
{
	public static class FractionHelper
	{
		public static Fraction Simplify(this Fraction fraction)
		{
			bool hasNegativeNumerator = fraction.Numerator < 0;
			bool hasNegativeDenominator = fraction.Denominator < 0;

			int[] numbers = { Math.Abs(fraction.Numerator), fraction.Denominator };
			int gcd = GCD(numbers);
			for (int i = 0; i < numbers.Length; i++)
				numbers[i] /= gcd;

			return new Fraction { Numerator = numbers[0] * (hasNegativeNumerator ? -1 : 1), Denominator = numbers[1] * (hasNegativeDenominator ? -1 : 1) };
		}

		public static string ToWholeFraction(this Fraction fraction)
		{
			Fraction simplified = fraction.Simplify();

			if (simplified.Numerator >= simplified.Denominator)
			{
				double d = simplified.Numerator / (double)simplified.Denominator;
				var whole = Math.Truncate(d);
				var mod = simplified.Numerator % simplified.Denominator;
				return $"{whole}'{mod}/{simplified.Denominator}";
			}

			return $"{simplified.Numerator}/{simplified.Denominator}";
		}

		public static int GCD(this Fraction fraction)
		{
			while (fraction.Denominator > 0)
			{
				int rem = fraction.Numerator % fraction.Denominator;
				fraction.Numerator = fraction.Denominator;
				fraction.Denominator = rem;
			}
			return fraction.Numerator;
		}

		public static int GCD(int a, int b)
		{
			while (b > 0)
			{
				int rem = a % b;
				a = b;
				b = rem;
			}
			return a;
		}

		public static int GCD(IEnumerable<int> args)
		{
			return args.Aggregate((gcd, arg) => GCD(new Fraction { Numerator = arg, Denominator = gcd }));
		}

		//private static uint GreatestCommonDivisor(uint valA, uint valB)
		//{
		//	// return 0 if both values are 0 (no GSD)
		//	if (valA == 0 && valB == 0)
		//		return 0;

		//	// return value b if only a == 0
		//	if (valA == 0 && valB != 0)
		//		return valB;

		//	// return value a if only b == 0
		//	if (valA != 0 && valB == 0)
		//		return valA;


		//	uint first = valA;
		//	uint second = valB;

		//	while (first != second)
		//	{
		//		if (first > second)
		//			first = first - second;
		//		else
		//			second = second - first;
		//	}

		//	return first;
		//}

		private static int GCF(int a, int b)
		{
			while (b != 0)
			{
				int temp = b;
				b = a % b;
				a = temp;
			}
			return a;
		}

		public static int LCM(int a, int b)
		{
			return (a / GCF(a, b)) * b;
		}

		public static int LCM(IEnumerable<int> args)
		{
			return args.Aggregate((a, b) => LCM(a, b));
		}

		public static int FindLCM(this IEnumerable<Fraction> fractions)
		{
			var denominators = fractions.Select(s => s.Denominator);
			return LCM(denominators);
		}

		public static Fraction ToCommonDenominator(this Fraction fraction, int lcm)
		{
			return new Fraction { Numerator = (lcm / fraction.Denominator) * fraction.Numerator, Denominator = lcm };
		}

		public static List<Fraction> ToCommonDenominator(this IEnumerable<Fraction> fractions)
		{
			var list = fractions.ToList();
			var lcm = list.FindLCM();
			return list.Select(s => s.ToCommonDenominator(lcm)).ToList();
		}

		public static Fraction Convert(decimal value)
		{
			decimal whole = Math.Truncate(value);
			decimal fraction = value - whole;

			int numerator = 1;
			int denomenator = 1;
			int gcd = 1;
			int denomenator2 = 0;

			if (fraction > 0m)
			{
				string strFraction = fraction.ToString(CultureInfo.InvariantCulture).Remove(0, 2);
				int intFractLength = strFraction.Length;
				denomenator2 = (int)Math.Pow(10, intFractLength);
				numerator = strFraction.ParseAs<int>();
				gcd = GCD(numerator, denomenator2);

				numerator = numerator / gcd;
				denomenator = denomenator2 / gcd;
			}

			int w;
			Fraction f = !ZeroCount(denomenator2, numerator, out w)
				? new Fraction { Numerator = gcd, Denominator = w }
				: new Fraction { Numerator = numerator, Denominator = denomenator };

			return f;
		}

		private static bool ZeroCount(int denomenator, int numerator, out int whole)
		{
			var n = denomenator / (double)numerator;
			whole = Math.Truncate(n).ParseAs<int>();
			double fraction = n - whole;
			string strFraction = fraction.ToString(CultureInfo.InvariantCulture).Remove(0, 2);

			bool v = false;
			var s = strFraction.Substring(0, Math.Min(5, strFraction.Length - 1));

			return s.ToCharArray().All(a => a == 0);
		}

	}

}