using System.Text.RegularExpressions;
using HelperTools.Helpers;

namespace HelperTools.IO.FTP
{

	public class FtpPathStructure : IPathStructure
	{
		public string Protocol { get; set; }
		public string Domain { get; set; }
		public string Path { get; set; }
		public string[] Directories => Path.Split('/');
		public int? Port { get; set; }
		public string FullPath => ToString();
		public string File { get; set; }
		public string Extension { get; set; }

		public string Root => Port > 0 ? $"{Protocol}{Domain}:{Port}/" : $"{Protocol}{Domain}{Path}{File}/";

		public override string ToString()
		{
			return Port > 0 ? $"{Protocol}{Domain}:{Port}{Path}{File}" : $"{Protocol}{Domain}{Path}{File}";
		}

		public FtpPathStructure()
		{
		}

		public FtpPathStructure(string server, int port)
		{
			var r = new Regex(FtpHelper.FtpPathPattern);

			Protocol = r.ReplaceCustomGroup(server, nameof(Protocol));
			Domain = r.ReplaceCustomGroup(server, nameof(Domain));
			Path = r.ReplaceCustomGroup(server, nameof(Path));
			Port = port;
			File = r.ReplaceCustomGroup(server, nameof(File));
		}

		public FtpPathStructure(string path)
		{
			var r = new Regex(FtpHelper.FtpPathPattern);

			Protocol = r.ReplaceCustomGroup(path, nameof(Protocol));
			Domain = r.ReplaceCustomGroup(path, nameof(Domain));
			Path = r.ReplaceCustomGroup(path, nameof(Path));
			Port = r.ReplaceCustomGroup(path, nameof(Port)).ParseAs(21);
			File = r.ReplaceCustomGroup(path, nameof(File));
		}

	}
}
