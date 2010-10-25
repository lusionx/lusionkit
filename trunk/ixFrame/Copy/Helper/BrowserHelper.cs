using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Portal.Facilities
{
	public enum BrowserType
	{
		Ie,
		Ie7,
		Firefox,
		Opera,
		Safari,
		Other
	}
	
	public enum CompressType
	{
		None,
		Deflate,
		Gzip
	}
	
	public sealed class BrowserHelper
	{
		private const string GZIP = "gzip";
		private const string DEFLATE = "deflate";

		public static BrowserType Browser()
		{
			string ua = HttpContext.Current.Request.UserAgent;
			if ( !string.IsNullOrEmpty( ua ) )
			{
				ua = ua.ToLower();
				if ( ua.IndexOf( "msie 7" ) >= 0 )
				{
					return BrowserType.Ie7;
				}
				if ( ua.IndexOf( "msie" ) >= 0 )
				{
					return BrowserType.Ie;
				}
				if ( ua.IndexOf( "opera" ) >= 0 )
				{
					return BrowserType.Opera;
				}
				if ( ua.IndexOf( "firefox" ) >= 0 )
				{
					return BrowserType.Firefox;
				}
				if ( ua.IndexOf( "safari" ) >= 0 )
				{
					return BrowserType.Firefox;
				}
			}
			return BrowserType.Other;
		}

		public static CompressType GetCompressType( HttpContext context )
		{
			CompressType compressType = CompressType.None;
			string acceptEncodingString = context.Request.Headers ["Accept-encoding"];
			if ( !string.IsNullOrEmpty( acceptEncodingString ) )
			{
				acceptEncodingString = acceptEncodingString.ToLower();
				BrowserType browserType = Browser ();
				switch ( browserType )
				{
					case BrowserType.Ie:
						//if ( acceptEncodingString.IndexOf ( DEFLATE ) >= 0 )
						//{
						//    compressType = CompressType.Deflate;
						//}
						break;
					case BrowserType.Ie7:
					case BrowserType.Firefox:
					case BrowserType.Opera:
					case BrowserType.Safari:
					case BrowserType.Other:
						if ( acceptEncodingString.IndexOf ( GZIP ) >= 0 )
						{
							compressType = CompressType.Gzip;
						}
						else if ( acceptEncodingString.IndexOf ( DEFLATE ) >= 0 )
						{
							compressType = CompressType.Deflate;
						}
						break;
					default:
						throw new ArgumentOutOfRangeException ();
				}
			}
			return compressType;
		}

		//public static bool SupportGipCompress()
		//{
		//    string ua = HttpContext.Current.Request.UserAgent;
		//    if ( !string.IsNullOrEmpty ( ua ) )
		//    {
		//        ua = ua.ToLower ();
		//        if ( ua.IndexOf ( "msie 7" ) >= 0 )
		//        {
		//            return true;
		//        }
		//        if ( ua.IndexOf ( "msie" ) >= 0 )
		//        {
		//            return false;
		//        }
		//    }
		//    return true;
		//}

		public static void SetResponseCompressEncodingType( HttpContext context, CompressType compressType )
		{
			switch( compressType )
			{
				case CompressType.None:
					break;
				case CompressType.Deflate:
					context.Response.AppendHeader ( "Content-encoding", DEFLATE );
					break;
				case CompressType.Gzip:
					context.Response.AppendHeader ( "Content-encoding", GZIP );
					break;
				default:
					throw new ArgumentOutOfRangeException( "compressType" );
			}
		}


		//public static bool IsEncodingAccepted( HttpContext context )
		//{
		//    return IsEncodingAccepted ( context, GZIP );
		//}

		//private static bool IsEncodingAccepted( HttpContext context, string encoding )
		//{
		//    NameValueCollection headers = context.Request.Headers;
		//    return headers ["Accept-encoding"] != null && headers ["Accept-encoding"].Contains ( encoding );
		//}

		//private static void SetResponseCompressEncoding( HttpContext context, string encoding )
		//{
		//    context.Response.AppendHeader ( "Content-encoding", encoding );
		//}

		//public static void SetResponseCompressEncoding( HttpContext context )
		//{
		//    SetResponseCompressEncoding ( context, GZIP );
		//}

		public static void CompressBytes( ref byte[] compressBytes, CompressType compressType )
		{
			switch ( compressType )
			{
				case CompressType.None:
					break;
				case CompressType.Deflate:
					compressBytes = StreamHelper.DeflateCompress ( compressBytes );
					break;
				case CompressType.Gzip:
					compressBytes = StreamHelper.Compress ( compressBytes );
					break;
				default:
					throw new ArgumentOutOfRangeException ();
			}
		}
		public static void CompressPage( HttpContext context )
		{
			CompressType compressType = GetCompressType( context );
			switch( compressType )
			{
				case CompressType.None:
					break;
				case CompressType.Deflate:
					context.Response.Filter = new DeflateStream ( context.Response.Filter, CompressionMode.Compress );
					context.Response.AppendHeader ( "Content-encoding", DEFLATE );
					break;
				case CompressType.Gzip:
					context.Response.Filter = new GZipStream ( context.Response.Filter, CompressionMode.Compress );
					context.Response.AppendHeader ( "Content-encoding", GZIP );
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

	}
}
