using System.IO;

namespace HelperTools.IO
{
	public static class FileInfoExt
	{

		public static string NameWithoutExtension(this FileInfo file)
		{
			return file?.Name.Substring(0, file.Name.Length - file.Extension.Length);
		}

		public static string ReplaceExtension(this FileInfo file, string newExtension)
		{
			if (!string.IsNullOrEmpty(newExtension) && !newExtension.StartsWith("."))
				newExtension = "." + newExtension;

			return file != null ? file.Name.Substring(0, file.Name.Length - file.Extension.Length) + newExtension : null;
		}
	}
}
