using HelperTools.IO;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HelperTools.XML
{
    public static class SerializerHelper
	{
		public static string SerializeXml<T>(T value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));
			
			var xmlserializer = new XmlSerializer(typeof(T));
			using (var stringWriter = new StringWriter())
			{ 
				using (var writer = XmlWriter.Create(stringWriter))
				{
					xmlserializer.Serialize(writer, value);
					return stringWriter.ToString();
				}
			}
		}

        //public static T ParseXML<T>(string value)
        //{
        //	if (string.IsNullOrWhiteSpace(value))
        //		throw new ArgumentNullException(nameof(value));

        //	var xmlserializer = new XmlSerializer(typeof(T));

        //	using (var stringReader = new StringReader(value))
        //	{
        //		using (var reader = XmlReader.Create(stringReader))
        //		{
        //			if (xmlserializer.CanDeserialize(reader))
        //			{
        //				return (T)xmlserializer.Deserialize(reader);
        //			}
        //		    var ex = new Exception("Cannot Deserialize object");
        //		    ex.Data.Add("value", value);
        //		    ex.Data.Add("T.FullName", typeof(T).FullName);
	    //		    throw ex;
	    //		}
	    //	}
	    //}

	    public static T ParseXML<T>(string value) where T : class
	    {
	        if (value == null)
	            throw new ArgumentNullException(nameof(value));

            var stream = value.Trim().ToStream();
	        var reader = XmlReader.Create(stream, new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document });
	        return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
	    }

        public static byte[] SerializeBinaryBytes<T>(T value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

		    return StreamHelper.ToByteStream(value);
		}

		public static T ParseXML<T>(byte[] value)
		{
		    return StreamHelper.Parse<T>(value);
		}

	}
}
