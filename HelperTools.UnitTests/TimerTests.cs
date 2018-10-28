using System;
using System.Text.RegularExpressions;
using HelperTools.Normalizations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class TimerTests
	{

		public TestContext TestContext { get; set; }

		[TestMethod]
		public void TimerTest()
		{

			Regex r = new Regex(TimerNormalization.Instance.MaskPattern());

			Assert.IsTrue(r.IsMatch("1h24:12.033"));
			Assert.IsTrue(r.IsMatch("24:12.033"));
			Assert.IsTrue(r.IsMatch("12.033"));
			Assert.IsTrue(r.IsMatch("2:24"));

			TimeSpan t = TimerNormalization.Instance.NormalizeTimeSpan("1h24:12.033");
			TimeSpan t2 = TimerNormalization.Instance.NormalizeTimeSpan("4:12.033");
			TimeSpan t3 = TimerNormalization.Instance.NormalizeTimeSpan("4:05");
			TimeSpan t4 = TimerNormalization.Instance.NormalizeTimeSpan("12.033");
		}
	}

}
