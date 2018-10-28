using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HelperTools.IO
{
	public static class IOHelper
	{

		public static string DrivePattern => @"(?<drive>[a-zA-Z]{1}:)?";
		public static string DirectoryPattern => @"(?<path>(?:(?:(?:[\w\-%_\.~+=;&]+)?\\)+)?)?";
		public static string FilePattern = @"(?<file>(?<name>[^ \\/:*?""<>|]*[^\\/:*?""<>|]*)\.(?<ext>[a-zA-Z0-9]*))?";
		public static string AbsolutePathPattern => $"{DrivePattern}{DirectoryPattern}{FilePattern}";


		public static bool IsValidPath(string path)
		{
			return !string.IsNullOrWhiteSpace(path) && new Regex(AbsolutePathPattern).IsMatch(path);
		}

		public static bool HasAvailableSpace(long size, DriveInfo drive, out long availableSize, long extraFreeSpace = 500 << 20)
		{
			availableSize = Math.Max(extraFreeSpace, 0);

			if (drive == null)
				return false;

			availableSize += drive.AvailableFreeSpace;

			return availableSize > Math.Max(size, 0);
		}

		public static bool HasAvailableSpace(IEnumerable<long> fileSizes, DriveInfo drive, out long availableSize, long extraFreeSpace = 500 << 20)
		{
			return HasAvailableSpace(fileSizes.Sum(), drive, out availableSize, extraFreeSpace);
		}

		public static bool HasAvailableSpace(IEnumerable<FileInfo> files, DriveInfo drive, out long availableSize, long extraFreeSpace = 500 << 20)
		{
			return HasAvailableSpace(files.Sum(s => s.Length), drive, out availableSize, extraFreeSpace);
		}

		
		public static DriveInfo GetDriveInfo(string path)
		{
			if (string.IsNullOrWhiteSpace(path) || !IsValidPath(path))
			{
				Trace.TraceWarning("path is empty or invalid.");
				return null;
			}

			return DriveInfo.GetDrives().FirstOrDefault(drive => drive.Name.Contains(Path.GetPathRoot(path)));
		}

	}
}
