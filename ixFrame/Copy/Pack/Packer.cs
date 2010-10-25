using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Portal.Facilities
{
	public static class Packer
	{
		static Regex jsComment1Regex = new Regex ( "/\\*[^*]*\\*+([^/][^*]*\\*+)*/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline );
		//static Regex jsComment1Regex = new Regex ( "/\\*.*?\\*/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline );
		static Regex jsComment2Regex = new Regex ( "//.*$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline );
		static string jsPageCommentBeginRegex = "<!--";
		static string jsPageCommentEndRegex = "//-->";
		public static string PackPageJs( string pageJsString )
		{
			//return pageJsString;
			string packJsString = jsComment1Regex.Replace ( pageJsString, "" );
			StringBuilder packBuilder = new StringBuilder ( pageJsString.Length );
			string [] packJsStrings = packJsString.Split ( Environment.NewLine.ToCharArray () );
			for ( int i = 0, count = packJsStrings.Length; i < count; i++ )
			{
				string packString = packJsStrings [i].Trim ();
				if ( packString.Length > 0 && packString != jsPageCommentBeginRegex && packString != jsPageCommentEndRegex )
				{
					packString = packString.Replace ( "http://", "бс" );
					packString = jsComment2Regex.Replace ( packString, "" );
					packString = packString.Replace ( "бс", "http://" );
					if ( packString.Trim ().Length > 0 )
					{
						packBuilder.Append ( packString.Trim () );
						packBuilder.Append ( Environment.NewLine );
					}
				}
			}
			return packBuilder.ToString ();
		}

		public static string PackFileJs( string jsString )
		{
			//string packJsString = jsComment1Regex.Replace( jsString, "" );
			//StringBuilder packBuilder = new StringBuilder ( jsString.Length );
			//string[] packJsStrings = packJsString.Split( Environment.NewLine.ToCharArray() );
			//for ( int i = 0, count = packJsStrings.Length; i < count; i++ )
			//{
			//    string packString = packJsStrings[ i ].Trim();
			//    if ( packString.Length > 0 && packString != jsPageCommentBeginRegex && packString != jsPageCommentEndRegex )
			//    {
			//        packString = jsComment2Regex.Replace ( packString, "" );
			//        if ( packString.Trim( ).Length > 0 )
			//        {
			//            packBuilder.Append ( packString.Trim () );
			//            //packBuilder.Append ( Environment.NewLine );
			//        }
			//    }
			//}
			//return packBuilder.ToString();
			//return jsString;
			return new JavaScriptMinifier().Pack( jsString );
			//ECMAScriptPacker p = new ECMAScriptPacker ( ECMAScriptPacker.PackerEncoding.Normal, true, false );
			//return p.Pack ( jsString );
		}
		
		// remove comments
		static Regex cssCommentRegex = new Regex ( "/\\*[^*]*\\*+([^/][^*]*\\*+)*/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline );

		public static string PackFileCss( string cssString )
		{
			string packCssString = cssCommentRegex.Replace ( cssString, "" );
			StringBuilder packBuilder = new StringBuilder ( packCssString.Length );
			string [] packCssStrings = packCssString.Split ( Environment.NewLine.ToCharArray () );
			for ( int i = 0, count = packCssStrings.Length; i < count; i++ )
			{
				string packString = packCssStrings [i].Trim ();
				if ( packString.Length > 0 )
				{
					packBuilder.Append ( packString.Trim () );
					//packBuilder.Append ( Environment.NewLine );
				}
			}
			return packBuilder.ToString ();
		}
	}
}
