using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using HelperTools.Helpers;

namespace HelperTools.IO.FTP
{

	public static class FtpHelper
	{

		public static string FtpPathPattern
		{
			get
			{
				const string protocol = @"(?<protocol>ftp://)";
				const string domain = @"(?<domain>((([-a-zA-Z0-9]+\.)?[-a-zA-Z0-9]+\.[a-zA-Z]{2,6})))";
				const string ip4 = @"(?<domain>(((\d{1,3}\.){3}\d{1,3})))";
				const string port = @"(:(?<port>((\d+)?)))?";
				const string path = @"(?<path>((([-a-zA-Z\d%_\.~+=;&]+)?(\/))+)?)?";
				const string file = @"(?<file>[-a-zA-Z\d%_\.~+=;&]+)?";

				return $"{protocol}({domain}|{ip4}){port}{path}{file}";
			}
		}


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

		public static List<string> GetFiles(string ftpPath, string ftpUser, string ftpPassword)
		{
			if (NullableHelper.AnyIsNull(ftpPath, ftpUser, ftpPassword))
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
			List<string> files = new List<string>();


			if (responseStream != null)
			{
				StreamReader reader = new StreamReader(responseStream);
				while (reader.Peek() >= 0)
				{
					string fileItems = reader.ReadLine();
					//files.Add(fileItems[fileItems.LastIndex()]);
				}

			}
			return files;
		}


		public static bool GetFile(string ftpServer, string port, string filePattern, string ftpUser, string ftpPassword, out Stream stream, int timeout = 60000)
		{

			stream = null;
			var ftpPath = new FtpPathStructure(ftpServer, port.ParseAs(21));

			string filePath = GetFileNamePathByPattern(ftpPath.ToString(), filePattern, ftpUser, ftpPassword);


			try
			{
				FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(new Uri(filePath));

				//if (ftp == null)
				//	return false;
				// https://msdn.microsoft.com/en-us/library/ms229716(v=vs.110).aspx

				ftp.Timeout = timeout;
				ftp.Method = WebRequestMethods.Ftp.DownloadFile;
				ftp.UseBinary = true;
				ftp.Proxy = null;

				if (string.IsNullOrEmpty(ftpUser) || string.IsNullOrEmpty(ftpPassword))
					throw new InvalidCredentialException();

				ftp.Credentials = new NetworkCredential(ftpUser, ftpPassword);

				try
				{
					//FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

					var webResponse = (FtpWebResponse)ftp.GetResponse();

					//if (response != null)
					//{
					//	var webResponse = (FtpWebResponse)response;
					switch (webResponse.StatusCode)
					{
						case FtpStatusCode.CommandOK: //200
						case FtpStatusCode.DataAlreadyOpen: //125
						case FtpStatusCode.OpeningData: //150
																  //MemoryStream stream = new MemoryStream();
																  //{
							stream = new MemoryStream();
							Stream responseStream2 = webResponse.GetResponseStream();
							responseStream2?.CopyTo(stream);
							return true;
						//}

						default:
							throw new Exception($"FtpStatusCode not implemented: {webResponse.StatusCode}");
					}
					//}

				}
				catch (Exception ex)
				{
					//string status = "FtpWebResponse is null";
					//if(response != null)
					//	status = ((FtpWebResponse)ex.Response).StatusDescription;

					//throw new Exception(status);

				}

				return false;

			}
			catch (Exception ex)
			{
				throw new Exception($"stream == null. Incorrect FileServer? {ex.Message}");

			}
		}


		public static bool IsValidFtpPath(string path)
		{
			return new Regex($"^{FtpPathPattern}$").IsMatch(path);
		}

	}


}
