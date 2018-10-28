using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelperTools.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelperTools.UnitTests
{
	[TestClass]
	public class LinqTest
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

		private class Employee
		{
			public string Name { get; set; }
			public string Department { get; set; }
			public string Function { get; set; }
			public decimal Salary { get; set; }
		}


		[TestMethod]
		public void PivotTest()
		{
			StringBuilder s = new StringBuilder();
			var l = new List<Employee>() {
				new Employee() { Name = "Fons", Department = "R&D", Function = "Trainer", Salary = 2000 },
				new Employee() { Name = "Jim", Department = "R&D", Function = "Trainer", Salary = 3000 },
				new Employee() { Name = "Ellen", Department = "Dev", Function = "Developer", Salary = 4000 },
				new Employee() { Name = "Mike", Department = "Dev", Function = "Consultant", Salary = 5000 },
				new Employee() { Name = "Jack", Department = "R&D", Function = "Developer", Salary = 6000 },
				new Employee() { Name = "Demy", Department = "Dev", Function = "Consultant", Salary = 2000 }};

			var result1 = l.Pivot(emp => emp.Department, emp2 => emp2.Function, lst => lst.Sum(emp => emp.Salary));

			foreach (var row in result1)
			{
				s.AppendLine(row.Key);
				foreach (var column in row.Value)
				{
					s.AppendLine("  " + column.Key + "\t" + column.Value);

				}
			}

			s.AppendLine("----");

			var result2 = l.Pivot(emp => emp.Function, emp2 => emp2.Department, lst => EnumerableExtensions.Count(lst));

			foreach (var row in result2)
			{
				s.AppendLine(row.Key);
				foreach (var column in row.Value)
				{
					s.AppendLine("  " + column.Key + "\t" + column.Value);

				}
			}

			s.AppendLine("----");


		}
		


	}

}
