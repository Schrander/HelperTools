using System.ComponentModel;

namespace HelperTools.IO
{
    public enum EnumFileType
    {
        [Description("Word")]
        DOC,

        [Description("Excel")]
        XLS,

        [Description("JPEG")]
        JPG,

        [Description("PNG")]
        PNG,

        [Description("GIF")]
        GIF,

        [Description("PDF")]
        PDF,
    }

    public enum ImageFileType
    {

        [Description("JPEG")]
        JPG,

        [Description("PNG")]
        PNG,

        [Description("GIF")]
        GIF,

    }
}