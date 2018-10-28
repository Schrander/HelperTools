namespace HelperTools
{

	public interface IPathStructure
	{
		string Path { get; set; }
		string[] Directories { get; }
		string Root { get; }
		string AbsolutePath { get; }
	}

}
