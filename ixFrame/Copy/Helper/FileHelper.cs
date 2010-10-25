using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace Portal.Facilities
{
	/// <summary>
	/// 文件操作类
	/// </summary>
	public static class FileHelper
	{
		public static void Log( string filePhysicalPath, string text )
		{
			StringBuilder stringBuilder = new StringBuilder ();
			stringBuilder.Append ( "///---Begin(" );
			stringBuilder.Append ( System.DateTime.Now.ToString ( "yyyy-MM-dd HH:mm:ss" ) );
			stringBuilder.Append ( ")---///" );
			stringBuilder.Append ( Environment.NewLine );
			stringBuilder.Append ( text );
			stringBuilder.Append ( Environment.NewLine );
			stringBuilder.Append ( "///----End----///" );
			Write ( filePhysicalPath, stringBuilder.ToString () );
		}

		public static void Write( string filePhysicalPath, string text )
		{
			CheckDirectory ( filePhysicalPath );
			try
			{
				System.IO.FileStream fileStream;
				if ( !System.IO.File.Exists ( filePhysicalPath ) )
				{
					fileStream = new System.IO.FileStream ( filePhysicalPath, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite );
				}
				else
				{
					fileStream = new System.IO.FileStream ( filePhysicalPath, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite );
				}
				System.IO.StreamWriter streamWriter = new System.IO.StreamWriter ( fileStream, System.Text.Encoding.Default );
				streamWriter.Write ( text );
				streamWriter.WriteLine ();
				streamWriter.Close ();
				fileStream.Close ();
			}
			catch ( Exception exception )
			{
				ExceptionService.Current.Handle( exception );
			}
		}


		public static void Unzip( Stream srcStream, Stream destStream )
		{
			using ( GZipStream input = new GZipStream ( srcStream, CompressionMode.Decompress ) )
			{
				byte [] bytes = new byte [4096];
				int n;
				while ( ( n = input.Read ( bytes, 0, bytes.Length ) ) != 0 )
				{
					destStream.Write ( bytes, 0, n );
				}
			}
		}

		public static string GetFileExtension( string fileName )
		{
			if ( string.IsNullOrEmpty ( fileName ) )
				return string.Empty;
			return fileName.Substring ( fileName.LastIndexOf ( "." ) + 1 );
		}

		public static string GetFileNameNoExtension( string fileName )
		{
			if ( string.IsNullOrEmpty ( fileName ) )
				return string.Empty;
			return fileName.Substring ( 0, fileName.LastIndexOf ( "." ) );
		}

        /// <summary>
        /// 保存文本文件内容
        /// </summary>
        /// <param name="content">要保存的内容</param>
        /// <param name="filePath">文件路径</param>
        public static void SaveZipFileText( string content, string filePath )
        {
        	byte[] bytes = StreamHelper.Compress( content );
        	SaveFileBinary( bytes, filePath );
			Logger.Current.Log ( "Compress", filePath );
        }

		public static void SaveDeflateCompressFileText( string content, string filePath )
		{
			byte [] bytes = StreamHelper.DeflateCompress( content );
			SaveFileBinary ( bytes, filePath );
			Logger.Current.Log ( "Compress", filePath );
		}


		#region 保存文本文件内容
		/// <summary>
		/// 保存文本文件内容
		/// </summary>
		/// <param name="content">要保存的内容</param>
		/// <param name="filePath">文件路径</param>
		public static void SaveFileText( string content, string filePath )
		{
			CheckDirectory ( filePath );
			using ( StreamWriter sw = File.CreateText ( filePath ) )
			{
				using( TextWriter tw = TextWriter.Synchronized ( sw ) )
				{
					tw.Write ( content );
					tw.Close ();
				}
			}
		}

		public static void CheckDirectory( string filePath )
		{
			string path = Path.GetDirectoryName ( filePath );
			if ( !Directory.Exists ( path ) )
			{
				Directory.CreateDirectory ( path );
			}
		}

		#endregion

		#region 取得文本文件内容
		/// <summary>
		/// 取得文本文件内容
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <returns>文本文件内容</returns>
		public static string GetFileText( string filePath )
		{
			using ( StreamReader streamReader = File.OpenText ( filePath ) )
			{
				return streamReader.ReadToEnd ();
			}
		}
		#endregion


		#region 保存二进制文件内容
		/// <summary>
		/// 保存二进制文件内容
		/// </summary>
		/// <param name="content">要保存的内容</param>
		/// <param name="filePath">文件路径</param>
		public static void SaveFileBinary( byte [] content, string filePath )
		{
			CheckDirectory( filePath );
			using ( FileStream fs = new FileStream ( filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite ) )
			{
				using( BinaryWriter bw = new BinaryWriter ( fs ) )
				{
					bw.Write ( content );
					//for ( int i = 0, count = content.Length; i < count; i++ )
					//{
					//    bw.Write ( content [i] );
					//}
				}
			}
		}

		public static void SaveFileBinary( byte [] content1, byte [] content2, string filePath )
		{
			CheckDirectory ( filePath );
			using ( FileStream fs = new FileStream ( filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite ) )
			{
				using ( BinaryWriter bw = new BinaryWriter ( fs ) )
				{
					bw.Write ( content1 );
					bw.Write ( content2 );
					//for ( int i = 0, count = content.Length; i < count; i++ )
					//{
					//    bw.Write ( content [i] );
					//}
				}
			}
		}

		#endregion

		#region 读取二进制文件内容
		/// <summary>
		/// 读取二进制文件内容
		/// </summary>
		/// <param name="filePath">文件路径</param>
		public static byte [] GetFileBinary( string filePath )
		{
			byte[] content = null;
			if ( File.Exists( filePath ) )
			{
				using ( FileStream fs = new FileStream ( filePath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
				{
					using ( BinaryReader br = new BinaryReader ( fs ) )
					{
						content = br.ReadBytes ( Convert.ToInt32 ( fs.Length ) );
					}

				}
			}
			return content;
		}
		#endregion

		public static void Delete( string fileName )
		{
			int retries = 10;
			int msecsBetweenRetries = 100;
			while ( retries > 0 )
			{
				try
				{
					File.Delete ( fileName );
				}
				catch
				{
					retries--;
					Thread.Sleep ( msecsBetweenRetries );
					continue;
				}
				break;
			}
			if ( retries == 0 )
			{
				ExceptionService.Current.Handle ( new Exception ( string.Format ( "Error deleting {0}, perhaps it is in use by another process?", fileName ) ) );
			}
		}

	}
}
