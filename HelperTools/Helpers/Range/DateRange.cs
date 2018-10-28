using System;

namespace HelperTools.Helpers.DateTimeHelpers
{
	public class DateRange : IRange<DateTime>
	{
		public DateRange(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
		}

		public DateTime Start { get; private set; }
		public DateTime End { get; private set; }

		public bool Includes(DateTime value)
		{
			return (Start <= value) && (value <= End);
		}

		public bool Includes(IRange<DateTime> range)
		{
			return (Start <= range.Start) && (range.End <= End);
		}

		public int Substract => End.Subtract(Start).Days;

		public int Nights => Substract;

		public int Days => Substract + 1;

		public override string ToString()
		{
			return $"{Start.ToShortDateString()} - {End.ToShortDateString()}";
		}
	}

	public class CharRange : IRange<char>
	{
		public char Start { get; }
		public char End { get; }

		public CharRange(char start, char end)
		{
			Start = start;
			End = end;
		}

		public bool Includes(char value)
		{
			return (Start <= value) && (value <= End);
		}

		public bool Includes(IRange<char> range)
		{
			return (Start <= range.Start) && (range.End <= End);
		}
	}
}
