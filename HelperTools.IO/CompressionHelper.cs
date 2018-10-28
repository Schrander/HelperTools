using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace HelperTools.IO
{
	public class CompressionHelper
	{
		public static void DecompressToFileWithExtenion(Stream stream, string extractPath, string extension,
			out List<string> extractFile)
		{
			extractFile = new List<string>();

			try
			{
				if (string.IsNullOrWhiteSpace(extension))
				{
					Trace.TraceError($"Method: {nameof(DecompressToFileWithExtenion)}. Extension is missing");
					return;
				}

				if (!CreatePath(extractPath))
					return;

				DriveInfo drive = IOHelper.GetDriveInfo(extractPath);

				if (drive == null)
				{
					Trace.TraceError($"Method: {nameof(DecompressToFileWithExtenion)}. Invalid or missing drive");
					return;
				}

				using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
				{
					var selectedFiles =
						archive.Entries.Where(e => e.FullName.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase)).ToList();

					long availableSize;
					if (!HasAvailableSpace(selectedFiles, drive, out availableSize))
					{
						Trace.TraceError($"Method: {nameof(DecompressToFileWithExtenion)}. Not enough free space on {drive}.");
						return;
					}

					foreach (ZipArchiveEntry entry in selectedFiles)
					{
						string absPath = Path.Combine(extractPath, entry.FullName);

						FileInfo f = new FileInfo(absPath);
						if (f.Exists)
							f.Delete();

						entry.ExtractToFile(absPath);
						extractFile.Add(entry.FullName);
					}
				}
			}
			catch (Exception ex)
			{
				Trace.TraceError($"Method: {nameof(DecompressToFileWithExtenion)}. {ex.Message}");
			}
		}

		public static void DecompressToFile(Stream stream, string extractPath)
		{
			if (string.IsNullOrWhiteSpace(extractPath) || !IOHelper.IsValidPath(extractPath))
			{
				Trace.TraceError($"{nameof(CompressionHelper)}.{nameof(DecompressToFile)}: Missing or invalid extract path.");
				return;
			}

			if (!CreatePath(extractPath))
			{
				Trace.TraceError(
					$"{nameof(CompressionHelper)}.{nameof(DecompressToFile)}: Failed to creath directory {extractPath}.");
				return;
			}

			DriveInfo drive = IOHelper.GetDriveInfo(extractPath);

			if (drive == null)
			{
				Trace.TraceError($"{nameof(CompressionHelper)}.{nameof(DecompressToFile)}: Invalid drive or does not exist.");
				return;
			}

			try
			{
				using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read))
				{
					long availableSize;
					if (!HasAvailableSpace(archive.Entries, drive, out availableSize))
					{
						Trace.TraceError(
							$"{nameof(CompressionHelper)}.{nameof(DecompressToFile)}: Not enough free space ({availableSize}) on drive {drive} to extract files.");
						return;
					}

					foreach (ZipArchiveEntry entry in archive.Entries)
						entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));
				}
			}
			catch (Exception ex)
			{
				Trace.TraceError($"{nameof(CompressionHelper)}.{nameof(DecompressToFileWithExtenion)}. {ex.Message}");
			}
		}

		private static bool HasAvailableSpace(IEnumerable<ZipArchiveEntry> selectedFiles, DriveInfo drive, out long size)
		{
			return IOHelper.HasAvailableSpace(selectedFiles.Select(s => s.Length), drive, out size, 500 << 20);
		}

		private static bool CreatePath(string extractPath)
		{
			try
			{
				if (!IOHelper.IsValidPath(extractPath))
				{
					Trace.TraceError(
						$"{nameof(CompressionHelper)}.{nameof(CreatePath)}. Invalid or missing extract path: {extractPath}");
					return false;
				}

				DirectoryInfo extractPathDir = new DirectoryInfo(extractPath);

				if (!extractPathDir.Exists)
					Directory.CreateDirectory(extractPath); // Create temp dir if not exists

				return extractPathDir.Exists;
			}
			catch (Exception ex)
			{
				Trace.TraceError($"{nameof(CompressionHelper)}.{nameof(CreatePath)}: {ex.Message}");
				return false;
			}
		}


		/// <summary>
		/// Decompresses the specified gzipped stream.
		/// </summary>
		/// <param name="gzippedStream">The gzipped stream.</param>
		/// <param name="unzippedStream">The unzipped stream.</param>
		public static void Decompress(Stream gzippedStream, Stream unzippedStream)
		{
			if (gzippedStream == null)
				throw new ArgumentNullException(nameof(gzippedStream));

			if (unzippedStream == null)
				throw new ArgumentNullException(nameof(unzippedStream));

			using (GZipStream Decompress = new GZipStream(gzippedStream, CompressionMode.Decompress))
			{
				// Copy the decompression stream 
				// into the output file.
				Decompress.CopyTo(unzippedStream);
				unzippedStream.Position = 0;
			}
		}

		/// <summary>
		/// Decompresses the specified gzipped stream.
		/// </summary>
		/// <param name = "gzippedStream" > The gzipped stream.</param>
		/// <returns></returns>
		public static string Decompress(Stream gzippedStream)
		{
			if (gzippedStream == null)
				throw new ArgumentNullException(nameof(gzippedStream));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				Decompress(gzippedStream, memoryStream);

				var reader = new StreamReader(memoryStream);

				return reader.ReadToEnd();
			}
		}

		/// <summary>
		/// Compresses the string.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static string CompressString(string text)
		{
			try
			{
				byte[] buffer = Encoding.UTF8.GetBytes(text);
				var memoryStream = new MemoryStream();
				using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
				{
					gZipStream.Write(buffer, 0, buffer.Length);
				}

				memoryStream.Position = 0;

				var compressedData = new byte[memoryStream.Length];
				memoryStream.Read(compressedData, 0, compressedData.Length);

				var gZipBuffer = new byte[compressedData.Length + 4];
				Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
				Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
				return Convert.ToBase64String(gZipBuffer);
			}
			catch
			{
			}

			return null;
		}

		/// <summary>
		/// Decompresses the string.
		/// </summary>
		/// <param name="compressedText">The compressed text.</param>
		/// <returns></returns>
		public static string DecompressString(string compressedText)
		{
			try
			{
				byte[] gZipBuffer = Convert.FromBase64String(compressedText);
				using (var memoryStream = new MemoryStream())
				{
					int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
					memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

					var buffer = new byte[dataLength];

					memoryStream.Position = 0;
					var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress);

					gZipStream.Read(buffer, 0, buffer.Length);

					return Encoding.UTF8.GetString(buffer);
				}
			}
			catch
			{
				//ignore
			}

			return null;

		}
	}

}