using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HelperTools.IO
{
    public static class CloneHelper
    {
        
        public static T DeepClone<T>(T value) 
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter serializer = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                serializer.Serialize(stream, value);
                stream.Position = 0;
                return (T)serializer.Deserialize(stream);
            }
        }
    }


}
