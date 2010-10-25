using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;

namespace Portal.Facilities
{
	public static class StreamHelper
	{
		
		public static string GetStringFromStream( Stream stream )
		{
			return GetStringFromStream( stream, System.Text.Encoding.Default );
		}

		public static string GetStringFromStream( Stream stream, System.Text.Encoding encoding )
		{
			stream.Position = 0;
			using ( StreamReader streamReader = new StreamReader( stream, encoding ) )
			{
				return streamReader.ReadToEnd();
			}
		}

		public static MemoryStream GetMemoryStreamFromByteArray( byte [] data )
		{
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write( data, 0, data.Length );
			return memoryStream;
		}

		public static MemoryStream GetMemoryStreamFromStream( Stream stream )
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] bs = new byte[256];
			for ( int j = stream.Read( bs, 0, ( int ) bs.Length ); j > 0; j = stream.Read( bs, 0, ( int ) bs.Length ) )
			{
				memoryStream.Write( bs, 0, j );
			}
			//stream.Close();
			memoryStream.Position = ( long )0;
			return memoryStream;
		}

		public static Collection<byte []> GetUnZipBytes( string zipBase64String )
		{
			Collection<byte []> unZipBytes = new Collection<byte []> ();
			Byte [] zipBytes = Convert.FromBase64String ( zipBase64String );
			using ( MemoryStream ms = new MemoryStream ( zipBytes ) )
			{
				using ( ZipInputStream zipStream = new ZipInputStream ( ms ) )
				{
					ZipEntry zipEntry = null;
					try
					{
						zipEntry = zipStream.GetNextEntry ();
					}
					catch
					{
						//²»ÊÇZip°ü
						return unZipBytes;
					}
					while ( zipEntry != null )
					{
						using ( MemoryStream unZipStream = StreamHelper.GetMemoryStreamFromStream ( zipStream ) )
						{
							byte [] bytes = unZipStream.ToArray ();
							unZipBytes.Add ( bytes );
						}
						zipEntry = zipStream.GetNextEntry ();
					}
				}
			}
			return unZipBytes;
		}

		public static byte [] Compress( string content )
		{
			byte [] bytes = System.Text.Encoding.UTF8.GetBytes ( content );
			MemoryStream ms = new MemoryStream ();
			Stream zipStream = new GZipStream ( ms, CompressionMode.Compress );
			zipStream.Write ( bytes, 0, bytes.Length );
			zipStream.Close ();
			byte [] compressedData = ( byte [] ) ms.ToArray ();
			ms.Close ();
			return compressedData;
		}

		public static byte [] DeflateCompress( string content )
		{
			byte [] bytes = System.Text.Encoding.UTF8.GetBytes ( content );
			MemoryStream ms = new MemoryStream ();
			Stream zipStream = new DeflateStream ( ms, CompressionMode.Compress );
			zipStream.Write ( bytes, 0, bytes.Length );
			zipStream.Close ();
			byte [] compressedData = ( byte [] ) ms.ToArray ();
			ms.Close ();
			return compressedData;
		}


		public static byte [] Compress( byte [] unzippedBytes )
		{
			MemoryStream ms = new MemoryStream ();
			Stream zipStream = new GZipStream ( ms, CompressionMode.Compress );
			zipStream.Write ( unzippedBytes, 0, unzippedBytes.Length );
			zipStream.Close ();
			byte [] compressedData = ( byte [] ) ms.ToArray ();
			ms.Close ();
			return compressedData;
		}

		public static byte [] DeflateCompress( byte [] unzippedBytes )
		{
			MemoryStream ms = new MemoryStream ();
			Stream zipStream = new DeflateStream ( ms, CompressionMode.Compress );
			zipStream.Write ( unzippedBytes, 0, unzippedBytes.Length );
			zipStream.Close ();
			byte [] compressedData = ( byte [] ) ms.ToArray ();
			ms.Close ();
			return compressedData;
		}


		public static string DeCompress( byte [] compressData )
		{
			StringBuilder resultBulder = new StringBuilder ( compressData.Length );
			int totalLength = 0;
			byte [] writeData = new byte [4096];
			MemoryStream ms = new MemoryStream ( compressData );
			Stream zipStream = new GZipStream ( ms, CompressionMode.Decompress );
			try
			{
				while ( true )
				{
					int size = zipStream.Read ( writeData, 0, writeData.Length );
					if ( size > 0 )
					{
						totalLength += size;
						resultBulder.Append ( System.Text.Encoding.UTF8.GetString ( writeData, 0, size ) );
					}
					else
					{
						break;
					}
				}
			}
			catch ( Exception e )
			{
				ExceptionService.Current.Handle ( e );
			}
			zipStream.Close ();
			ms.Close ();
			return resultBulder.ToString ();
		}
	}
}
