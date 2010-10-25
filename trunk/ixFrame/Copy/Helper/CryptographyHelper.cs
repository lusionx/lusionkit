using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Portal.Facilities
{
	public class CryptographyHelper
	{
		public static string DesEncrypt( string strText, string strEncrKey )
		{
			byte [] byKey = null;
			byte [] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
			byKey = System.Text.Encoding.UTF8.GetBytes ( strEncrKey.Substring ( 0, 8 ) );
			DESCryptoServiceProvider des = new DESCryptoServiceProvider ();
			byte [] inputByteArray = Encoding.UTF8.GetBytes ( strText );
			MemoryStream ms = new MemoryStream ();
			CryptoStream cs = new CryptoStream ( ms, des.CreateEncryptor ( byKey, IV ), CryptoStreamMode.Write );
			cs.Write ( inputByteArray, 0, inputByteArray.Length );
			cs.FlushFinalBlock ();
			return Convert.ToBase64String ( ms.ToArray () );


		}
		/// <summary> 
		/// Decrypt string 
		/// Attention:key must be 8 bits 
		/// </summary> 
		/// <param name="strText">Decrypt string</param> 
		/// <param name="sDecrKey">key</param> 
		/// <returns>output string</returns> 
		public static string DesDecrypt( string strText, string sDecrKey )
		{
			byte [] byKey = null;
			byte [] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
			byte [] inputByteArray = new Byte [strText.Length];
			byKey = System.Text.Encoding.UTF8.GetBytes ( sDecrKey.Substring ( 0, 8 ) );
			DESCryptoServiceProvider des = new DESCryptoServiceProvider ();
			inputByteArray = Convert.FromBase64String ( strText );
			MemoryStream ms = new MemoryStream ();
			CryptoStream cs = new CryptoStream ( ms, des.CreateDecryptor ( byKey, IV ), CryptoStreamMode.Write );
			cs.Write ( inputByteArray, 0, inputByteArray.Length );
			cs.FlushFinalBlock ();
			System.Text.Encoding encoding = new System.Text.UTF8Encoding ();
			return encoding.GetString ( ms.ToArray () );
		}
		
		/// <summary> 
		/// MD5 Encrypt 
		/// </summary> 
		/// <param name="strText">text</param> 
		/// <returns>md5 Encrypt string</returns> 
		public static string MD5Encrypt(string strText) 
		{ 
			MD5 md5 = new MD5CryptoServiceProvider(); 
			byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));    
			return System.Text.Encoding.Default.GetString(result); 
		} 
	}
}
