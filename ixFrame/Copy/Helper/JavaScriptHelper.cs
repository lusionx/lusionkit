using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Facilities
{
	public static class JavaScriptHelper
	{
		public static string UsingJsLibrary( string scriptString )
		{
			return UsingJsLibrary ( scriptString, false );
		}

		public static string UsingJsLibrary( string scriptString, bool onlyIE )
		{
			if ( onlyIE )
				return "<script type=\"text/javascript\">document.write('<!--[if IE]><script src=\"" + scriptString + "\" type=\"text/javascript\"><' + '/script><![endif]-->');</script>";
			return "<script type=\"text/javascript\">document.write('<script src=\"" + scriptString + "\" type=\"text/javascript\"><' + '/script>');</script>";
		}
	}
}
