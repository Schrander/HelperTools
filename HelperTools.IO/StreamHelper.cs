using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HelperTools.IO
{
    public static class StreamHelper
    {


        public static Stream ToStream(this string value)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(value);
                    writer.Flush();
                    stream.Position = 0;
                    return stream;
                }
            }
        }

        public static byte[] ToByteStream<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            using (MemoryStream stream = new MemoryStream())
            {
                var serializer = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Persistence));
                serializer.Serialize(stream, value);
                stream.Position = 0;
                return stream.GetBuffer();
            }
        }

        public static T Parse<T>(byte[] value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            using (MemoryStream stream = new MemoryStream(value))
            {
                BinaryFormatter serializer = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Persistence));
                return (T)serializer.Deserialize(stream);
            }
        }

    }
}
