using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace Portal.Facilities
{
	public class SerializeHelper
	{
		#region Binary Serializers

		public static MemoryStream SerializeBinary( object request )
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream ();
			binaryFormatter.Serialize ( memoryStream, request );
			return memoryStream;
		}

		public static object DeserializeBinary( MemoryStream memoryStream )
		{
			memoryStream.Position = 0;
			BinaryFormatter binaryFormatter = new BinaryFormatter ();
			object newObj = binaryFormatter.Deserialize ( memoryStream );
			memoryStream.Close ();
			return newObj;
		}

		#endregion

		#region XML Serializers

		public static MemoryStream SerializeXml( object request )
		{
			SoapFormatter soapFormatter = new SoapFormatter ();
			MemoryStream memoryStream = new System.IO.MemoryStream ();
			soapFormatter.Serialize ( memoryStream, request );
			return memoryStream;
		}

		public static object DeserializeXml( MemoryStream memoryStream )
		{
			memoryStream.Position = 0;
			SoapFormatter soapFormatter = new SoapFormatter ();
			object newObj = soapFormatter.Deserialize ( memoryStream );
			memoryStream.Close ();
			return newObj;
		}

		#endregion

	}
}
