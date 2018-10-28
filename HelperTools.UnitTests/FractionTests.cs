using System.Collections.Generic;
using HelperTools.Linq.Extensions;
using HelperTools.MathExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class FractionTests
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }


		[TestMethod]
		public void FractionTest()
		{
			var oneHalf = new Fraction { Numerator = 1, Denominator = 2 };
			var oneThird = new Fraction { Numerator = 1, Denominator = 3 };
			var twoThird = new Fraction { Numerator = 2, Denominator = 3 };
			var gg = new Fraction { Numerator = 11, Denominator = 3 };
			var seven35th = new Fraction { Numerator = 7, Denominator = 35 };
			
		
			var sevenSixth = oneHalf + twoThird;
			var oneSixth = twoThird - oneHalf;
			var minusOneSixth = oneHalf - twoThird;
			var threeSixth = oneHalf * twoThird;

			var threeHalf = oneHalf / oneThird;
			var anderhalf = threeHalf.ToWholeFraction();


			var oneFifth = seven35th.Simplify();

		}

		[TestMethod]
		public void FractionLCMTest()
		{
			int[] ints = { 3, 4, 5, 6 };
			var lcm = FractionHelper.LCM(ints);
			Assert.AreEqual(60, lcm);

			int[] ints2 = { 3, 4 };
			var lcm2 = FractionHelper.LCM(ints2);
			Assert.AreEqual(12, lcm2);

			int[] ints3 = { 5, 7 };
			var lcm3 = FractionHelper.LCM(ints3);
			Assert.AreEqual(35, lcm3);

		}

		[TestMethod]
		public void FractionSumTest()
		{
			var one3rd = new Fraction { Numerator = 1, Denominator = 3 };
			var one4th = new Fraction { Numerator = 1, Denominator = 4 };
			var one5th = new Fraction { Numerator = 1, Denominator = 5 };
			var one6th = new Fraction { Numerator = 1, Denominator = 6 };

			List<Fraction> fractions = new List<Fraction> { one3rd, one4th, one5th, one6th };

			var sum = fractions.Sum();

		}


		[TestMethod]
		public void FractionConverterTest()
		{
			//var onefifth = FractionConverter.Convert(0.2m);
			//var oneThird = FractionConverter.Convert(0.333333m);
			var twoThird = FractionHelper.Convert(0.666666m);
			var oneEighth = FractionHelper.Convert(0.125m);
		}

		[TestMethod]
		public void FractionCommonDenominatorTest()
		{
			var one3rd = new Fraction { Numerator = 1, Denominator = 3 };
			var one4th = new Fraction { Numerator = 1, Denominator = 4 };
			var one5th = new Fraction { Numerator = 1, Denominator = 5 };
			var one6th = new Fraction { Numerator = 1, Denominator = 6 };


			List<Fraction> fractions = new List<Fraction> { one3rd, one4th, one5th, one6th };
			var list = fractions.ToCommonDenominator();
		}
	}
}
