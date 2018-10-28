using HelperTools.Web;

namespace HelperTools.IO
{

	public class UrlPathStructure : IPathStructure
	{
		public string Protocol { get; set; }
		public string Domain { get; set; }
		public string Path { get; set; }
		public string[] Directories => Path.Split('/');
		public string Root => string.Concat(Protocol, Domain, Port.HasValue ? $":{Port.Value}/" : "/") ;
		public int? Port { get; set; }
		public string File { get; set; }
		public string Extension { get; set; }
		public string AbsolutePath => ToString();


		public override string ToString()
		{
			return string.Concat(Protocol, Domain, Port.HasValue ? $":{Port.Value}/" : "/", Path); 
		}

		public UrlPathStructure()
		{
		}

		public UrlPathStructure(string server, int port)
		{
			var n = UrlNormalization.Instance;

			Protocol = n.Protocol(server);
			Domain = n.Domain(server); 
			Path = n.Location(server);
			Port = n.Port(server) ?? port;
			File = n.File(server);
		}

		public UrlPathStructure(string path)
		{
			var n = UrlNormalization.Instance;

			Protocol = n.Protocol(path);
			Domain = n.Domain(path);
			Path = n.Location(path);
			Port = n.Port(path) ;
			File = n.File(path);
	
		}
	}

}
