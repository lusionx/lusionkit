using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Portal.Facilities
{
	public class ExceptionService
	{
		private static ExceptionService current = null;
		private static object lockObject = new object ();
		private readonly string exceptionLogPath = System.Configuration.ConfigurationManager.AppSettings[ "ExceptionLogPath" ];
		
		private ExceptionService()
		{
		}

		public static ExceptionService Current
		{
			get
			{
				//if ( current == null )
				//{
				//    lock ( lockObject )
				//    {
				//        if ( current == null )
				//        {
				//            current = new ExceptionService ();
				//        }
				//    }
				//}
				if ( current == null )
				{
					System.Threading.Interlocked.CompareExchange ( ref current,
																  new ExceptionService (), null );
				}
				return current;
			}
		}

		public void Handle( Exception e )
		{
			//������ΪĿ¼
			string logFile = exceptionLogPath + DateTime.Now.ToString( "yyyy-MM-dd" ) + "\\";
			if ( !System.IO.Directory.Exists( logFile ) )
				System.IO.Directory.CreateDirectory( logFile );
			string ExceptionName = e.GetType().Name;
			logFile = logFile + "\\" + ExceptionName;
			string FilePath = logFile + DateTime.Now.ToString( "yyyyMMdd" ) + ".log";
			StringBuilder sb = new StringBuilder();
			sb.Append( "---------------------Header-----------------" );
			sb.Append( Environment.NewLine );
			sb.Append( "<" + ExceptionName + ">\n" );
			sb.Append( Environment.NewLine );
			sb.Append( "<LogDateTime>" + DateTime.Now.ToString() + "</LogDateTime>\n" );
			sb.Append( Environment.NewLine );
			sb.Append( "<Message>" + HttpUtility.HtmlEncode( e.Message ) + "</Message>\n" );
			sb.Append( Environment.NewLine );
			if ( e.Source != null )
			{
				sb.Append ( "<Source>" + e.Source + "</Source>\n" );
				sb.Append ( Environment.NewLine );
			}
			if ( e.StackTrace != null )
			{
				sb.Append ( "<StackTrace>" + e.StackTrace + "</StackTrace>\n" );
				sb.Append ( Environment.NewLine );
			}
			if ( e.InnerException != null )
			{
				sb.Append( "<InnerException>" + HttpUtility.HtmlEncode( e.InnerException.ToString() ) + "</InnerException>\n" );
				sb.Append( Environment.NewLine );
			}
			sb.Append( "<TargetSite>" + e.TargetSite + "</TargetSite>\n" );
			sb.Append( Environment.NewLine );
			HttpContext context = HttpContext.Current;
			if ( context != null )
			{
				sb.Append( "<RawUrl>" );
				sb.Append( context.Request.RawUrl == null
				           	? string.Empty : HttpUtility.HtmlEncode( context.Request.RawUrl.ToString() ) );
				sb.Append( "</RawUrl>" );
				sb.Append( Environment.NewLine );
				sb.Append( "<QueryString>" );
				sb.Append( context.Request.QueryString == null
				           	? string.Empty : HttpUtility.HtmlEncode( context.Request.QueryString.ToString() ) );
				sb.Append( "</QueryString>" );
				sb.Append( Environment.NewLine );
				sb.Append( "<Form>" );
				sb.Append( context.Request.Form == null ? string.Empty : context.Request.Form.ToString() );
				sb.Append( "</Form>" );
				sb.Append( Environment.NewLine );
				sb.Append( "<UrlReferrer>" );
				sb.Append( context.Request.UrlReferrer == null
				           	? string.Empty : HttpUtility.HtmlEncode( context.Request.UrlReferrer.ToString() ) );
				sb.Append( "</UrlReferrer>" );
				sb.Append( Environment.NewLine );
			}
			sb.Append( "</" + ExceptionName + ">\n" );
			sb.Append( Environment.NewLine );
			sb.Append( "---------------------Footer-----------------" );
			sb.Append( Environment.NewLine );
			Write( FilePath, sb.ToString() );
		}

        public static string GetExceptionInfo(Exception e)
        {

            string ExceptionName = e.GetType().Name;
            StringBuilder sb = new StringBuilder();
            sb.Append("---------------------Header-----------------");
            sb.Append(Environment.NewLine);
            sb.Append("<" + ExceptionName + ">\n");
            sb.Append(Environment.NewLine);
            sb.Append("<LogDateTime>" + DateTime.Now.ToString() + "</LogDateTime>\n");
            sb.Append(Environment.NewLine);
            sb.Append("<Message>" + HttpUtility.HtmlEncode(e.Message) + "</Message>\n");
            sb.Append(Environment.NewLine);
            if (e.Source != null)
            {
                sb.Append("<Source>" + e.Source + "</Source>\n");
                sb.Append(Environment.NewLine);
            }
            if (e.StackTrace != null)
            {
                sb.Append("<StackTrace>" + e.StackTrace + "</StackTrace>\n");
                sb.Append(Environment.NewLine);
            }
            if (e.InnerException != null)
            {
                sb.Append("<InnerException>" + HttpUtility.HtmlEncode(e.InnerException.ToString()) + "</InnerException>\n");
                sb.Append(Environment.NewLine);
            }
            sb.Append("<TargetSite>" + e.TargetSite + "</TargetSite>\n");
            sb.Append(Environment.NewLine);
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                sb.Append("<RawUrl>");
                sb.Append(context.Request.RawUrl == null
                            ? string.Empty : HttpUtility.HtmlEncode(context.Request.RawUrl.ToString()));
                sb.Append("</RawUrl>");
                sb.Append(Environment.NewLine);
                sb.Append("<QueryString>");
                sb.Append(context.Request.QueryString == null
                            ? string.Empty : HttpUtility.HtmlEncode(context.Request.QueryString.ToString()));
                sb.Append("</QueryString>");
                sb.Append(Environment.NewLine);
                sb.Append("<Form>");
                sb.Append(context.Request.Form == null ? string.Empty : context.Request.Form.ToString());
                sb.Append("</Form>");
                sb.Append(Environment.NewLine);
                sb.Append("<UrlReferrer>");
                sb.Append(context.Request.UrlReferrer == null
                            ? string.Empty : HttpUtility.HtmlEncode(context.Request.UrlReferrer.ToString()));
                sb.Append("</UrlReferrer>");
                sb.Append(Environment.NewLine);
            }
            sb.Append("</" + ExceptionName + ">\n");
            sb.Append(Environment.NewLine);
            sb.Append("---------------------Footer-----------------");
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

		#region д��Ϣ
		
		/// <summary>
		/// д�쳣��־
		/// </summary>
		/// <param name="filePath">�ļ�·��</param>
		/// <param name="Message">��־��Ϣ</param>
		private static void Write(string filePath, string Message)
		{
			try
			{
				CheckDir( ref filePath );
				if ( filePath != "" )
				{
					using ( StreamWriter sw = new StreamWriter( filePath, true, System.Text.Encoding.Default ) )
					{
						//ȷ���̰߳�ȫ
						TextWriter tw = TextWriter.Synchronized( sw );
						tw.Write( Message );
						tw.Close();
					}
				}
			}
			catch
			{
			}
		}

		#endregion

		#region �ж�Ŀ¼�Ƿ���ڲ�����
		
        /// <summary>
		/// ���һ��Ŀ¼�ǲ��Ǵ��ڣ���������ھʹ���
		/// </summary>
		/// <param name="filePath"></param>
		private static void CheckDir( ref string filePath )
        {
        	if ( filePath != "" )
        	{
        		filePath = filePath.Replace( "/", "\\" );
        		string strFileDir = filePath.Substring( 0, filePath.LastIndexOf( "\\" ) );
        		//���Ŀ¼�Ƿ���ڣ������ڴ�����Ŀ¼
        		if ( ! Directory.Exists( strFileDir ) )
        		{
        			Directory.CreateDirectory( strFileDir );
        		}
        	}
        	else
        		return;
        }

		#endregion
	}
}
