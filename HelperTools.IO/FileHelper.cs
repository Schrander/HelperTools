using System;
using System.IO;

namespace HelperTools.IO
{
	public static class FileHelper
	{
		public static string GetTempFileName(EnumDocumentType docType)
		{
			return GetTempFileName(docType, Guid.NewGuid().ToString());
		}

		public static string GetTempFileName(EnumDocumentType docType, string fileName)
		{
			string extension = docType.GetDescription();
			string fullPath = Path.GetTempPath() + $"{fileName}.{extension}";

			if (!File.Exists(fullPath)) 
				return fullPath;
			try
			{
				File.Delete(fullPath);
			}
			catch
			{
				// Dan maar even een nieuwe filename aanmaken met een unieke naam...
				fileName = $"{fileName}_{Guid.NewGuid()}";
				fullPath = Path.GetTempPath() + $"{fileName}.{extension}";
			}
			return fullPath;
		}

		public static void Delete(string file)
		{
			if (string.IsNullOrEmpty(file) || !File.Exists(file))
				return;
			try
			{
				FileInfo fileInfo = new FileInfo(file);
				fileInfo.Delete();
			}
			catch (Exception ex)
			{
				// ignored
			}
		}

		public static string UrlFile(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return null;
			return "file://" + filename.Replace("\\", "/");
		}

		public static string ToFileSize(long size)
		{
			if (size < 1024) { return (size).ToString("F0") + " bytes"; }
			if (size < Math.Pow(1024, 2)) { return (size / 1024).ToString("F0") + "KB"; }
			if (size < Math.Pow(1024, 3)) { return (size / Math.Pow(1024, 2)).ToString("F0") + "MB"; }
			if (size < Math.Pow(1024, 4)) { return (size / Math.Pow(1024, 3)).ToString("F0") + "GB"; }
			if (size < Math.Pow(1024, 5)) { return (size / Math.Pow(1024, 4)).ToString("F0") + "TB"; }
			if (size < Math.Pow(1024, 6)) { return (size / Math.Pow(1024, 5)).ToString("F0") + "PB"; }
			return (size / Math.Pow(1024, 6)).ToString("F0") + "EB";
		}

	}
}
