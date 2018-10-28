using System;
using System.Windows.Forms;

namespace HelperTools.SystemTools
{
	public class MonitorSize
	{
		public int MonitorWidth { get; set; }
		public int MonitorHeight { get; set; }
		public int AppWidth { get; set; }
		public int appHeight { get; set; }

		public MonitorSize()
		{
			try
			{
				var size = SystemInformation.PrimaryMonitorSize;

				MonitorWidth = size.Width;
				MonitorHeight = size.Height;

				appHeight = Convert.ToInt32(MonitorHeight * 0.95m);
				AppWidth = MonitorWidth >= 1280 ? 1200 : Convert.ToInt16(MonitorWidth * 0.95m);
			}
			catch (Exception)
			{
				appHeight = 1024;
				AppWidth = 1200;
			}
		}
	}
}