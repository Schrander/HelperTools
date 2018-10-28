using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class EnumTest
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }


		[Flags]
		public enum Numbers
		{
			One = 1,
			Two = 2,
			Four = 4,
			Eight = 8,
			[global::System.ComponentModel.Description("Sixteen")]
			Sixteen = 16,
		}

		public enum NumbersDifferent
		{
			Eight = 8,
			Ten = 10,
			Sixteen = 16,
			Eightteen = 18,
			Forty = 40
		}


		public enum Fruit
		{
			Apple = 1,
			Banana,
			Lemon,
			Grape,
			Mango,
			Pear,
			Tangerine,
			Orange,
		}

		[TestMethod]
		public void EnumFlagTest()
		{
			Numbers thirteen = Numbers.One | Numbers.Four | Numbers.Eight;

			Assert.AreEqual(13, (int)thirteen);

			bool isFour = true;
			bool isEight = true;
			bool isTwo = false;

			Numbers r = new Numbers();
			if (isFour)
				r |= Numbers.Four;
			if (isEight)
				r |= Numbers.Eight;
			if (isTwo)
				r |= Numbers.Two;

			Assert.AreEqual(12, (int)r);
		}

		[TestMethod]
		public void GetFlaggedValues()
		{
			var number = Numbers.Four | Numbers.One;
			var values = default(Numbers).GetFlaggedValues(number);

			var v = number.GetFlaggedValues();

			// False; No Flagged Enum
			var fruitbowl = Fruit.Apple | Fruit.Orange;
			var fruits = default(Fruit).GetFlaggedValues(fruitbowl);

		}

		[TestMethod]
		public void EnumConvertByValueTest()
		{
			var eight = Numbers.Eight.ConvertByValue<NumbersDifferent>();
			var eightteen = NumbersDifferent.Eightteen.ConvertByValue<Numbers>();
			var thirty = NumbersDifferent.Forty.ConvertByValue<Numbers>();

		}

		[TestMethod]
		public void EnumAddRemoveTest()
		{
			var number = Numbers.Four | Numbers.One;
			var seven = number.Add(Numbers.Two);
			var six = seven.Remove(Numbers.One);
		}




	}

}
