using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HelperTools.Extensions;
using HelperTools.Helpers;

namespace HelperTools.IO.FTP
{

	public class FtpFileInfo : FileSystemInfo
	{
		public override string Name { get; }
		public override bool Exists { get; }
		public new FtpAttributes Attributes { get; set; }
		public DateTime UpdateDate { get; set; }
		public string Owner { get; set; }
		public string Group { get; set; }
		public int FileSize { get; set; }

		public FtpFileInfo(string value)
		{
			string[] splitted = value.Split(' ');
			Attributes = SetAttributes(splitted[0]);
			Owner = splitted[2];
			Group = splitted[3];
			FileSize = splitted[4].ParseAs<int>();
			Name = splitted[splitted.LastIndex()];
		}

		public FtpAttributes SetAttributes(string value)
		{
			FtpAttributes? attributes = null;
			Regex r = new Regex(@"^[d\-][r\-][w\-][x\-][r\-][w\-][x\-][r\-][w\-][x\-]$");

			if (!r.IsMatch(value))
				return FtpAttributes.None;

			char[] chars = value.ToCharArray();
			var attr = default(FtpAttributes).ToElementsCollection().Where(w => (int)w > 0).ToArray();

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



		public override void Delete()
		{
			throw new NotImplementedException();
		}
	}

}
