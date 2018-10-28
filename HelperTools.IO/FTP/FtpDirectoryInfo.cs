using System;
using System.IO;

namespace HelperTools.IO.FTP
{
	public class FtpDirectoryInfo : FileSystemInfo
	{
		public override string Name { get; }
		public override bool Exists { get; }
		public new FtpAttributes Attributes { get; set; }
		public string Owner { get; set; }
		public string Group { get; set; }

		public override void Delete()
		{
			throw new NotImplementedException();
		}
	}

}
