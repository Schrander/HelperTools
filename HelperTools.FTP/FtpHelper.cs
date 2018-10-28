using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using HelperTools.Helpers;

namespace HelperTools.FTP
{

	public static class FtpHelper
	{

		public static string FtpProtocolPattern => @"(?<protocol>ftp://)";
		public static string FtpDomainPattern => @"(?<domain>((?:[\w\-]+\.)?[\w\-]+\.[a-zA-Z]{2,6}))";
		public static string FtpIP4Pattern => @"(?<domain>(((?:\d{1,3}\.){3}\d{1,3})))";
		public static string FtpPortPattern => @"(:(?<port>(?:\d+)?))?";
		public static string FtpPathPattern => @"(?<path>(?:(?:(?:[\w\-%_\.~+=;&]+)?\/)+)?)?";
		public static string FtpFilePattern => @"(?<file>(?<name>[^ \\/:*?""<>|]*[^\\/:*?""<>|]*)\.(?<ext>[a-zA-Z0-9]*))?$";
		public static string FtpPattern => $"{FtpProtocolPattern}({FtpDomainPattern}|{FtpIP4Pattern}){FtpPortPattern}{FtpPathPattern}{FtpFilePattern}";

		public static byte[] ReadFully(Stream input)
		{
			byte[] buffer = new byte[64 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		public static FtpAttributes BuildAttributes(string value)
		{
			FtpAttributes? attributes = null;
			Regex r = new Regex(@"^[d\-]([r\-][w\-][x\-]){3}$");

			Regex r2 = new Regex(@"(?<path>((\/)+)?)?");

			if (!r.IsMatch(value))
				return FtpAttributes.None;

			char[] chars = value.ToCharArray();
			var attr = default(FtpAttributes).ToElementsCollection().Where(w => w > 0).ToArray();

			for (int i = 0; i < 10; i++)
			{
				if (chars[i] != '-')
				{
					if (!attributes.HasValue)
						attributes = attr[i];
					else
						attributes |= attr[i];
				}
			}
			if (!attributes.HasValue)
				attributes = FtpAttributes.None;

			return attributes.Value;
		}


		public static string Combine(params string[] items)
		{
			if (items.Count() > 2)
				return null;

			string p = Path.Combine(items);
			return p.Replace("\\", "/");
		}

		/// <summary>		
		/// https://msdn.microsoft.com/en-us/library/ms229716(v=vs.110).aspx
		/// </summary>
		/// <returns></returns>
		public static string GetFileNamePathByPattern(string ftpPath, string filePattern, string ftpUser, string ftpPassword)
		{
			if (NullableHelper.AnyIsNull(ftpPath, filePattern, ftpUser, ftpPassword))
				return null;

			FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(new Uri(ftpPath));
			ftp.Timeout = 60000;
			ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
			ftp.UseBinary = true;
			ftp.Proxy = null;

			if (string.IsNullOrEmpty(ftpUser) || string.IsNullOrEmpty(ftpPassword))
				throw new InvalidCredentialException();

			ftp.Credentials = new NetworkCredential(ftpUser, ftpPassword);
			FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();


			Stream responseStream = response.GetResponseStream();
			StreamReader reader = new StreamReader(responseStream);

			List<string> files = new List<string>();
			while (reader.Peek() >= 0)
			{
				string line = reader.ReadLine();
				//string[] fileItems = line.CollapseWhiteSpaces().Split(' ');
				files.Add(line);
			}

			string ftpFile = null;
			foreach (var item in files.OrderByDescending(o => o))
			{
				if (new Regex(filePattern).IsMatch(item))
				{
					ftpFile = item;
					break;
				}
			}

			return $"{ftpPath}/{ftpFile}";

		}


		// https://msdn.microsoft.com/en-us/library/ms229716(v=vs.110).aspx
		public static FtpWebRequest GetFtpWebRequest(string path, string ftpUser, string ftpPassword, FtpMethod method, int timeout = 60000)
		{
			FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(new Uri(path));

			ftp.Timeout = timeout;
			ftp.Method = method.GetDescription();
			ftp.UseBinary = true;
			ftp.Proxy = null;

			if (string.IsNullOrEmpty(ftpUser) || string.IsNullOrEmpty(ftpPassword))
				throw new Exception("Missing credentials.");

			ftp.Credentials = new NetworkCredential(ftpUser, ftpPassword);

			return ftp;
		}


		public static List<FtpFileInfo> GetFiles(string ftpPath, string ftpUser, string ftpPassword)
		{
			List<FtpFileInfo> files = new List<FtpFileInfo>();
			try
			{
				FtpWebRequest ftp = GetFtpWebRequest(ftpPath, ftpUser, ftpPassword, FtpMethod.ListDirectoryDetails);

				if (ftp == null)
					throw new Exception($"Failed to connect to FTP {ftpPath}.");

				FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
				Stream responseStream = response.GetResponseStream();

				if (responseStream != null)
				{
					StreamReader reader = new StreamReader(responseStream);
					while (reader.Peek() >= 0)
					{
						files.Add(new FtpFileInfo(reader.ReadLine()));
					}
				}
				return files;
			}
			catch (Exception ex)
			{
				throw new Exception($"{nameof(FtpHelper)}.{nameof(GetFiles)}: {ex.Message}");
			}
		}

		public static bool RenameFile(string path, string newFileName, string ftpUser, string ftpPassword, int timeout = 60000)
		{
			if (!IsValidFtpPath(path))
				return false;
			try
			{
				FtpWebRequest ftp = GetFtpWebRequest(path, ftpUser, ftpPassword, FtpMethod.Rename, timeout);

				if (ftp == null)
					throw new Exception($"Failed to connect to FTP {path}.");

				ftp.RenameTo = newFileName;
				ftp.GetResponse();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"{nameof(FtpHelper)}.{nameof(RenameFile)}: {ex.Message}");
			}
		}

		public static bool DownloadFile(string ftpServer, string port, string fileName, string ftpUser, string ftpPassword, out Stream stream, int timeout = 60000)
		{
			if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(ftpServer))
				throw new Exception("Missing filename or ftpServer.");

			return DownloadFile(new FtpPathStructure(ftpServer, port.ParseAs(21), fileName).ToString(), ftpUser, ftpPassword, out stream, timeout);
		}

		public static bool DownloadFile(string fileName, string ftpUser, string ftpPassword, out Stream stream, int timeout = 60000)
		{
			stream = null;

			if (string.IsNullOrWhiteSpace(fileName) || !IsValidFtpPath(fileName))
				return false;
			try
			{
				FtpWebRequest ftp = GetFtpWebRequest(fileName, ftpUser, ftpPassword, FtpMethod.DownloadFile, timeout);

				if (ftp == null)
					throw new Exception($"Failed to connect to FTP {fileName}.");

				var webResponse = (FtpWebResponse)ftp.GetResponse();

				switch (webResponse.StatusCode)
				{
					case FtpStatusCode.CommandOK: //200
					case FtpStatusCode.DataAlreadyOpen: //125
					case FtpStatusCode.OpeningData: //150
						stream = new MemoryStream();
						Stream responseStream2 = webResponse.GetResponseStream();
						responseStream2?.CopyTo(stream);
						return true;

					default:
						throw new Exception($"FtpStatusCode not implemented: {webResponse.StatusCode}");
				}

			}
			catch (Exception ex)
			{
				throw new Exception($"{nameof(FtpHelper)}.{nameof(DownloadFile)}: {ex.Message}");
			}
		}


		public static bool IsValidFtpPath(string path)
		{
			return new Regex($"^{FtpPattern}$").IsMatch(path);
		}

	}

}
