using System;

namespace HelperTools.IO.FTP
{

	[Flags]
	public enum FtpAttributes
	{
		None = 0,
		Directory = 1,
		OwnerRead = 2,
		OwnerWrite = 4,
		OwnerExecute = 8,
		GroupRead = 16,
		GroupWrite = 32,
		GroupExecute = 64,
		PublicRead = 128,
		PublicWrite = 256,
		PublicExectute = 512,
	}
}
