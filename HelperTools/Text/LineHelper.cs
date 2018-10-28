using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HelperTools.Text
{
    public static class LineHelper
    {

        public static string FoldLines(this IEnumerable<string> lines, int? max, int offset, string newline = "\r\n")
        {
            List<string> list = new List<string>();
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    list.Add(line);
                    continue;
                }
                string[] splittedLines = line.Split(new[] { newline }, StringSplitOptions.RemoveEmptyEntries);
                list.AddRange(splittedLines);
            }
            return FoldLines(list.ToArray(), max, offset, newline);
        }

        public static string FoldLines(this string value, int max, int? offset, string newline = "\r\n")
        {
            if (string.IsNullOrEmpty(value))
                return null;

            string[] splittedLines = value.Split(new[] { newline }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = new List<string>();
            list.AddRange(splittedLines);
            return FoldLines(list.ToArray(), max, offset, newline);
        }

        public static string FoldLines(this string[] value, int max, int? offset, string newline = "\r\n")
        {
            List<string> lines = new List<string>();
            foreach (string item in value)
            {
                if (string.IsNullOrEmpty(item))
                {
                    lines.Add(item);
                    continue;
                }
                string[] splittedLine = item.Split(new[] { newline }, StringSplitOptions.RemoveEmptyEntries);
                lines.AddRange(splittedLine);
            }

            int sumLength = lines.Sum(s => s.Length);

            if (sumLength == 0)
                return null;

            if (lines.Count == 1 && lines[0].Length < sumLength)
                return lines[0];

            if (offset.HasValue && lines[0].Length > offset && offset < max)
            {
                string firstPart = lines[0].Substring(0, Math.Min(lines[0].Length, offset.Value));
                string secondPart = lines[0].Substring(Math.Min(lines[0].Length, offset.Value));

                lines.RemoveAt(0);
                lines.Insert(0, firstPart);
                lines.Insert(1, string.Format($"\t{newline}{secondPart}"));
            }

            using (var stream = new MemoryStream(sumLength))
            {

                byte[] crlf = Encoding.UTF8.GetBytes(Environment.NewLine); //CRLF
                byte[] crlfs = Encoding.UTF8.GetBytes($"{newline}\t"); //CRLF and SPACE

                int i = 0;
                foreach (string line in lines)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(line);
                    int length = Encoding.UTF8.GetByteCount(line);

                    if (length <= max)
                    {
                        stream.Write(bytes, 0, length);
                        if (lines.Count() > 1 && i == 0)
                            stream.Write(crlfs, 0, crlf.Length);
                    }
                    else
                    {
                        var block = length / max; //calculate block length
                        int remains = length % max; //calculate remaining length
                        int b = 0;
                        while (b < block)
                        {
                            stream.Write(bytes, (b++) * max, max);
                            stream.Write(crlfs, 0, crlfs.Length);
                        }
                        if (remains > 0)
                        {
                            stream.Write(bytes, block * max, remains);
                            //if (lines.Count() > 1 && (i < lines.Count - 1))
                            //  stream.Write(crlf, 0, crlf.Length);
                        }
                    }
                    i++;
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

    }
}
