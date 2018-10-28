using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HelperTools.IO
{
    public class DirFile
    {
        public DirectoryInfo Directory { get; set; }
        public List<FileInfo> Files = new List<FileInfo>();
    }
    
    public class ImageFileHelper
    {
        public List<string> GetImageFiles()
        {
            var dir = new DirectoryInfo("E:\\Fotos");
            var files = Directory.GetFiles("E:\\Fotos", "*.jpg", SearchOption.AllDirectories);
            return files.ToList();
        }

    }

}
