using System;
using System.Collections;
using System.Runtime.InteropServices; 
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Portal.Facilities
{
	public class RequestHelper
	{
		public static WebHeaderCollection GetRequestHead( string requestUrl )
		{
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(requestUrl);
            webRequest.Method = "GET";
            webRequest.Credentials = CredentialCache.DefaultCredentials;
            using ( WebResponse webResponse = webRequest.GetResponse() )
            {
                return webResponse.Headers;
            }
		}

        public static MemoryStream GetRequest(string requestUrl)
        {
            return GetRequest(requestUrl, "");
        }

		public static MemoryStream GetRequest( string requestUrl, string data )
		{
			HttpWebRequest webRequest = ( HttpWebRequest ) WebRequest.Create( requestUrl );
			webRequest.Method = "GET";
			webRequest.Credentials = CredentialCache.DefaultCredentials;
			return GetResponseStream( ref webRequest );
		}

		public static MemoryStream PostRequest( string requestUrl, string data )
		{
			return PostRequest( requestUrl, data, Encoding.UTF8 );
		}

		public static MemoryStream PostRequest( string requestUrl, string data, Encoding encoding )
		{
			HttpWebRequest webRequest = ( HttpWebRequest ) WebRequest.Create( requestUrl );
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			webRequest.Timeout = 1000;
			WriteRequestStream( ref webRequest, data, encoding );
			return GetResponseStream( ref webRequest );
		}

		public static MemoryStream SoapRequest( string requestUrl, string data, string soapAction )
		{
			return SoapRequest( requestUrl, data, soapAction, Encoding.UTF8 );
		}

		public static MemoryStream SoapRequest( string requestUrl, string data, string soapAction, Encoding encoding )
		{
			HttpWebRequest webRequest = ( HttpWebRequest ) WebRequest.Create( requestUrl );
			webRequest.ContentType = "text/xml; charset=utf-8";
			webRequest.Method = "POST";
			webRequest.Headers.Add( "SOAPAction", soapAction );
			WriteRequestStream( ref webRequest, data, encoding );
			return GetResponseStream( ref webRequest );
		}

		private static void WriteRequestStream( ref HttpWebRequest webRequest, string data )
		{
			WriteRequestStream( ref webRequest, data, Encoding.Default );
		}

		private static void WriteRequestStream( ref HttpWebRequest webRequest, string data, Encoding encoding )
		{
			using ( Stream requestStream = webRequest.GetRequestStream() )
			{
				requestStream.Write( encoding.GetBytes( data ), 0, data.Length );
				requestStream.Close();
			}
		}

		private static MemoryStream GetResponseStream( ref HttpWebRequest webRequest )
		{
			using ( WebResponse webResponse = webRequest.GetResponse() ) 
			{
				Stream stream =  webResponse.GetResponseStream();
				MemoryStream memoryStream = new MemoryStream();
				byte[] bs = new byte[256];
				for ( int j = stream.Read( bs, 0, ( int ) bs.Length ); j > 0; j = stream.Read( bs, 0, ( int ) bs.Length ) )
				{
					memoryStream.Write( bs, 0, j );
				}
				stream.Close();
				memoryStream.Position = ( long )0;
				return memoryStream;
			}
		}

		public static string GetResponseStream1( ref HttpWebRequest webRequest )
		{
			using ( WebResponse webResponse = webRequest.GetResponse() ) 
			{
				using ( StreamReader responseStream = new StreamReader( webResponse.GetResponseStream(), System.Text.Encoding.GetEncoding( "gb2312" ) ) )
				{
					return responseStream.ReadToEnd();
				}
			}
		}

	}
}
