
using System.IO;
using System.Web.Helpers;

namespace HelperTools.Web
{
    public static class WebImageHelper
    {
        public static void CreateThumb(string sourcePath, string destPath, int width, int height)
        {

            FileInfo sourceFile = new FileInfo(sourcePath);
            FileInfo destFile = new FileInfo(destPath);

            if (destFile.Exists)
            {
                new WebImage(destPath).Write();
                return;
            }

            if (!sourceFile.Exists)
                return;

            new WebImage(sourcePath).Resize(width, height, false, true).Crop(1, 1).Save(destPath);
            new WebImage(destPath).Write();
        }

        public static void GetThumb(string sourcePath, string destPath, int width, int height)
        {
            new WebImage(sourcePath).Resize(width, height, false, true).Crop(1, 1).Write();
        }
    }
}
