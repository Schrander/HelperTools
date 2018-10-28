using System.Collections.Generic;
using System.IO;

namespace HelperTools.IO
{
	public static class DirectoryInfoHelper
	{
		public static List<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
		{
			List<FileInfo> files = new List<FileInfo>();

			foreach (string extension in extensions)
				files.AddRange(dir.GetFiles("*" + extension));

			return files;
		}

		public static List<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params EnumFileType[] extensions)
		{
			List<FileInfo> files = new List<FileInfo>();

			foreach (EnumFileType extension in extensions)
				files.AddRange(dir.GetFiles("*." + extension));

			return files;
		}
	}
}
