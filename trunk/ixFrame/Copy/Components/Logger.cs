using System;
using System.Collections.Generic;
using System.Text;
using Portal.Facilities.Helper;

namespace Portal.Facilities
{
	/// <summary>
	/// Log4netºÊ»›
	/// </summary>
	public sealed class Logger
	{
		private static Logger current = null;
		private static object lockObject = new object ();
		private static readonly string LOG_PATH = SafeConvert.ToString( System.Configuration.ConfigurationManager.AppSettings ["ExceptionLogPath"] );
		private const string LOGFILE_PREFIX = "Log_";
		private const string LOG_FILENAME = "Log";
		private const string ERROR_FILENAME = "Error";
		private const string DEBUG_FILENAME = "Debug";
		
		enum LogType
		{
			Log,
			Error,
			Debug
		}
		
		private Logger()
		{
		}

		public static Logger Current
		{
			get
			{
				if ( current == null )
				{
					lock ( lockObject )
					{
						if ( current == null )
						{
							current = new Logger ();
						}
					}
				}
				return current;
			}
		}

		private void Write( LogType logType, string logString )
		{
			Write ( logType, string.Empty, logString );
		}
		
		private void Write( LogType logType, string logFileName, string logString )
		{
			string logFile = LOG_PATH + DateTime.Now.ToString( "yyyy-MM-dd" ) + "\\";
			if ( !System.IO.Directory.Exists ( logFile ) )
				System.IO.Directory.CreateDirectory ( logFile );
			logFile += LOGFILE_PREFIX;
			if ( string.IsNullOrEmpty ( logFileName ) )
			{
				switch ( logType )
				{
					case LogType.Log:
						logFile += LOG_FILENAME;
						break;
					case LogType.Error:
						logFile += ERROR_FILENAME;
						break;
					case LogType.Debug:
						logFile += DEBUG_FILENAME;
						break;
					default:
						throw new ArgumentOutOfRangeException ( "logType" );
				}
			}
			else
			{
				logFile += logFileName;
			}
			string filePath = logFile + "_" + DateTime.Now.ToString ( "yyyyMMdd" ) + ".log";
			FileHelper.Log ( filePath, logString );
		}

		public bool IsDebugEnabled
		{
			get
			{
#if DEBUG
				return true;
#else
				return false;
#endif
			}
		}
		
		public bool IsErrorEnabled
		{
			get
			{
#if DEBUG
				return true;
#else
				return false;
#endif
			}
		}
		
		public bool IsInfoEnabled
		{
			get
			{
#if DEBUG
				return true;
#else
				return false;
#endif
			}
		}

		public bool IsWarnEnabled
		{
			get
			{
#if DEBUG
				return true;
#else
				return false;
#endif
			}
		}
		public void Log( string logFileName, string message )
		{
			Write ( LogType.Log, logFileName, message );
		}

		public void Log( string message )
		{
			Write ( LogType.Log, message );
		}

		public void Error( Exception e )
		{
			Error ( e.Message, e );
		}
		
		public void Error( string errorString )
		{
			Error ( errorString, new Exception ( errorString ) );
		}
		
		public void Error( string errorString, Exception e )
		{
			Write ( LogType.Error, errorString );
			ExceptionService.Current.Handle ( e );
		}		
		public void Info( string infoString )
		{
			Write ( LogType.Log, infoString );
		}

		public void Debug( string debugString )
		{
			Debug ( debugString, new Exception ( debugString ) );
		}
		
		public void DebugFormat( string debugString, params object [] parms )
		{
			Debug ( string.Format( debugString, parms ), new Exception ( debugString ) );
		}

		public void Debug( string debugString, Exception e )
		{
			Write ( LogType.Debug, debugString );
			ExceptionService.Current.Handle ( e );
		}

		public void Warn( string debugString )
		{
			Debug ( debugString, new Exception ( debugString ) );
		}
		
		public void WarnFormat( string debugString, params object [] parms )
		{
			Debug ( string.Format ( debugString, parms ), new Exception ( debugString ) );
		}

	}
}
