using System.ComponentModel;

namespace HelperTools.FTP
{

	public enum FtpMethod
	{

		[Description("APPE")]
		AppendFile,

		[Description("DELE")]
		DeleteFile,

		[Description("RETR")]
		DownloadFile,

		[Description("MDTM")]
		GetDateTimeStamp,

		[Description("SIZE")]
		GetFileSize,

		[Description("NLST")]
		ListDirectory,

		[Description("LIST")]
		ListDirectoryDetails,

		[Description("MKD")]
		MakeDirectory,

		[Description("PWD")]
		PrintWorkingDirectory,

		[Description("RMD")]
		RemoveDirectory,

		[Description("RENAME")]
		Rename,

		[Description("STOR")]
		UploadFile,

		[Description("STOU")]
		UploadFileWithUniqueName,
	}
}
