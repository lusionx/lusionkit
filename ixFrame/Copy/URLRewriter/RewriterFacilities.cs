using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


namespace Portal.Facilities
{
    /// <summary>
    /// Provides utility helper methods for the rewriting HttpModule and HttpHandler.
    /// </summary>
    /// <remarks>This class is marked as internal, meaning only classes in the same assembly will be
    /// able to access its methods.</remarks>
    public class RewriterUtils
    {
        #region RewriteUrl
        /// <summary>
        /// Rewrite's a URL using <b>HttpContext.RewriteUrl()</b>.
        /// </summary>
        /// <param name="context">The HttpContext object to rewrite the URL to.</param>
        /// <param name="sendToUrl">The URL to rewrite to.</param>
        public static void RewriteUrl(HttpContext context, string sendToUrl)
        {
            string x;
            RewriteUrl(context, sendToUrl, out x);
        }

        /// <summary>
        /// Rewrite's a URL using <b>HttpContext.RewriteUrl()</b>.
        /// </summary>
        /// <param name="context">The HttpContext object to rewrite the URL to.</param>
        /// <param name="sendToUrl">The URL to rewrite to.</param>
        /// <param name="sendToUrlLessQString">Returns the value of sendToUrl stripped of the querystring.</param>
        /// <param name="filePath">Returns the physical file path to the requested page.</param>
        public static void RewriteUrl(HttpContext context, string sendToUrl, out string sendToUrlLessQString)
        {
#if DEBUG

            // For IIS 7, IIS 7 can not get RawUrl directly, so we have to save it first.
            HttpContext.Current.Items.Add("X-Rewrite-Url", HttpContext.Current.Request.RawUrl);

#endif

            // see if we need to add any extra querystring information
            if (context.Request.QueryString.Count > 0)
            {
                if (sendToUrl.IndexOf('?') != -1)
                    sendToUrl += "&" + context.Request.QueryString.ToString();
                else
                    sendToUrl += "?" + context.Request.QueryString.ToString();
            }

            // first strip the querystring, if any
            string queryString = String.Empty;
            sendToUrlLessQString = sendToUrl;
            if (sendToUrl.IndexOf('?') > 0)
            {
                sendToUrlLessQString = sendToUrl.Substring(0, sendToUrl.IndexOf('?'));
                queryString = sendToUrl.Substring(sendToUrl.IndexOf('?') + 1);
            }

            // grab the file's physical path
            //filePath = string.Empty;
            //filePath = context.Server.MapPath(sendToUrlLessQString);
            //
            if (string.IsNullOrEmpty(queryString))
                queryString = string.Empty;
            if (string.IsNullOrEmpty(sendToUrlLessQString))
                queryString = string.Empty;
            // rewrite the path...
            try
            {
                context.RewritePath(sendToUrlLessQString, String.Empty, queryString);
            }
            catch
            {
                context.RewritePath(sendToUrlLessQString + "?" + queryString);
            }
            // NOTE!  The above RewritePath() overload is only supported in the .NET Framework 1.1
            // If you are using .NET Framework 1.0, use the below form instead:
            // context.RewritePath(sendToUrl);
        }
        #endregion

        /// <summary>
        /// Converts a URL into one that is usable on the requesting client.
        /// </summary>
        /// <remarks>Converts ~ to the requesting application path.  Mimics the behavior of the 
        /// <b>Control.ResolveUrl()</b> method, which is often used by control developers.</remarks>
        /// <param name="appPath">The application path.</param>
        /// <param name="url">The URL, which might contain ~.</param>
        /// <returns>A resolved URL.  If the input parameter <b>url</b> contains ~, it is replaced with the
        /// value of the <b>appPath</b> parameter.</returns>
        public static string ResolveUrl(string appPath, string url)
        {
            if (url.Length == 0 || url[0] != '~')
                return url;		// there is no ~ in the first character position, just return the url
            else
            {
                if (url.Length == 1)
                    return appPath;  // there is just the ~ in the URL, return the appPath
                if (url[1] == '/' || url[1] == '\\')
                {
                    // url looks like ~/ or ~\
                    if (appPath.Length > 1)
                        return appPath + "/" + url.Substring(2);
                    else
                        return "/" + url.Substring(2);
                }
                else
                {
                    // url looks like ~something
                    if (appPath.Length > 1)
                        return appPath + "/" + url.Substring(1);
                    else
                        return appPath + url.Substring(1);
                }
            }
        }
    }
}
