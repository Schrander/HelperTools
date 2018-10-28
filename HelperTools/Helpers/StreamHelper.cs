using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace HelperTools.Helpers
{
	public sealed class StreamHelper
	{
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
			catch { }

			return "";
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
			catch { }

			return "";
		}
	}
}
