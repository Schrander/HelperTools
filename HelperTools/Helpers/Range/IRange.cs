namespace HelperTools.Helpers
{

	public interface IRange<T> where T : struct
	{
		T Start { get; }
		T End { get; }
		bool Includes(T value);
		bool Includes(IRange<T> range);
	}

}
