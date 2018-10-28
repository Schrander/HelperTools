using System.Text.RegularExpressions;
using HelperTools.Helpers;

namespace HelperTools.FTP
{

	public class FtpPathStructure : IPathStructure
	{

		private string _path;

		public string Protocol { get; set; }
		public string Domain { get; set; }

		public string Path
		{
			get => _path.Coalesce("/");
			set => _path = value;
		}

		public string[] Directories => Path.Split('/');
		public int? Port { get; set; }
		public string AbsolutePath => ToString();
		public string File { get; set; }
		public string Extension { get; set; }

		public string Root => Port > 0 ? $"{Protocol}{Domain}{Port}:/" : $"{Protocol}{Domain}/";

		public override string ToString()
		{
			return Port > 0 ? FtpHelper.Combine($"{Protocol}{Domain}{Port}:", Path, File) : FtpHelper.Combine($"{Protocol}{Domain}", Path, File);
		}

		public FtpPathStructure()
		{
		}

		public FtpPathStructure(string server, int port)
		{
			var r = new Regex(FtpHelper.FtpPattern);
			Protocol = r.ReplaceCustomGroup(server, nameof(Protocol));
			Domain = r.ReplaceCustomGroup(server, nameof(Domain));
			Path = r.ReplaceCustomGroup(server, nameof(Path));
			Port = port;
			File = r.ReplaceCustomGroup(server, nameof(File));
		}


		public FtpPathStructure(string server, int port, string file)
		{
			var r = new Regex(FtpHelper.FtpPattern);

			Protocol = r.ReplaceCustomGroup(server, nameof(Protocol));
			Domain = r.ReplaceCustomGroup(server, nameof(Domain));
			Path = r.ReplaceCustomGroup(server, nameof(Path));
			Port = port;
			File = file;
		}

		public FtpPathStructure(string server, int port, string path, string file)
		{
			var r = new Regex(FtpHelper.FtpPattern);

			Protocol = r.ReplaceCustomGroup(server, nameof(Protocol));
			Domain = r.ReplaceCustomGroup(server, nameof(Domain));
			Path = new Regex(FtpHelper.FtpPathPattern).Replace(path ?? "/", RegexHelper.CustomGroupFormat(nameof(Path)));
			Port = port;
			File = !string.IsNullOrWhiteSpace(file)
				? new Regex(FtpHelper.FtpFilePattern).Replace(file, RegexHelper.CustomGroupFormat(nameof(File)))
				: null;
		}

		public FtpPathStructure(string path)
		{
			var r = new Regex(FtpHelper.FtpPattern);

			Protocol = r.ReplaceCustomGroup(path, nameof(Protocol));
			Domain = r.ReplaceCustomGroup(path, nameof(Domain));
			Path = r.ReplaceCustomGroup(path, nameof(Path));
			Port = r.ReplaceCustomGroup(path, nameof(Port)).ParseAs(21);
			File = r.ReplaceCustomGroup(path, nameof(File));
		}
	}
}