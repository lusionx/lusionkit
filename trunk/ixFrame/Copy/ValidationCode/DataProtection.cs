using System;
using System.Security;
using System.Text;
using System.Runtime.InteropServices;


namespace Portal.Facilities
{
	/// <summary>
	/// Summary description for DataProtection.
	/// </summary>
	public sealed class DataProtection
	{
        /// <summary>
        /// use local machine or user to encrypt and decrypt the data
        /// </summary>
		public enum Store
		{
            /// <summary>
            /// Machine
            /// </summary>
			Machine,

            /// <summary>
            /// User
            /// </summary>
			User
		}

		// const values
		private class Consts 
		{
			// specify an entropy so other DPAPI applications can't see the data
			public readonly static byte[] EntropyData = ASCIIEncoding.ASCII.GetBytes("B0D125B7-967E-4f94-9305-A6F9AF56A19A");
		}

		// static class
		private DataProtection()
		{
		}

		// public methods

        /// <summary>
        /// encrypt the data using DPAPI, returns a base64-encoded encrypted string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="store"></param>
        /// <returns></returns>
		public static string Encrypt(string data, Store store)
		{
			// holds the result string
			string  result = "";

			// blobs used in the CryptProtectData call
			Win32.DATA_BLOB inBlob = new Win32.DATA_BLOB();
			Win32.DATA_BLOB entropyBlob = new Win32.DATA_BLOB();
			Win32.DATA_BLOB outBlob = new Win32.DATA_BLOB();

			try 
			{
				// setup flags passed to the CryptProtectData call
				int flags = Win32.CRYPTPROTECT_UI_FORBIDDEN | 
					(int)((store == Store.Machine) ? Win32.CRYPTPROTECT_LOCAL_MACHINE : 0);

				// setup input blobs, the data to be encrypted and entropy blob
				SetBlobData(ref inBlob, ASCIIEncoding.ASCII.GetBytes(data));
				SetBlobData(ref entropyBlob, Consts.EntropyData);

				// call the DPAPI function, returns true if successful and fills in the outBlob
				if (Win32.CryptProtectData(ref inBlob, "", ref entropyBlob, IntPtr.Zero, IntPtr.Zero, flags, ref outBlob)) 
				{
					byte[] resultBits = GetBlobData(ref outBlob);
					if (resultBits != null) 
						result = Convert.ToBase64String(resultBits);
				}
			}
			catch
			{
				// an error occurred, return an empty string
			}
			finally 
			{
				// clean up
				if (inBlob.pbData.ToInt32() != 0) 
					Marshal.FreeHGlobal(inBlob.pbData);

				if (entropyBlob.pbData.ToInt32() != 0) 
					Marshal.FreeHGlobal(entropyBlob.pbData);
			}

			return result;
		}

        /// <summary>
        /// decrypt the data using DPAPI, data is a base64-encoded encrypted string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="store"></param>
        /// <returns></returns>
		public static string Decrypt( string  data,  Store store) 
		{
			// holds the result string
			string result = "";

			// blobs used in the CryptUnprotectData call
			Win32.DATA_BLOB inBlob = new Win32.DATA_BLOB();
			Win32.DATA_BLOB entropyBlob = new Win32.DATA_BLOB();
			Win32.DATA_BLOB outBlob = new Win32.DATA_BLOB();

			try 
			{
				// setup flags passed to the CryptUnprotectData call
				int flags = Win32.CRYPTPROTECT_UI_FORBIDDEN |
					(int)((store == Store.Machine) ? Win32.CRYPTPROTECT_LOCAL_MACHINE : 0);

				// the CryptUnprotectData works with a byte array, convert string data
				byte[] bits = Convert.FromBase64String(data);

				// setup input blobs, the data to be decrypted and entropy blob
				SetBlobData(ref inBlob, bits);
				SetBlobData(ref entropyBlob, Consts.EntropyData);

				// call the DPAPI function, returns true if successful and fills in the outBlob
				if (Win32.CryptUnprotectData(ref inBlob, null, ref entropyBlob, IntPtr.Zero, IntPtr.Zero, flags, ref outBlob)) 
				{
					byte[] resultBits = GetBlobData(ref outBlob);
					if (resultBits != null) 
						result = ASCIIEncoding.ASCII.GetString(resultBits);
				}
			}
			catch 
			{
				// an error occurred, return an empty string
			}
			finally 
			{
				// clean up
				if (inBlob.pbData.ToInt32() != 0) 
					Marshal.FreeHGlobal(inBlob.pbData);
		
				if (entropyBlob.pbData.ToInt32() != 0) 
					Marshal.FreeHGlobal(entropyBlob.pbData);
			}

			return result;
		}


		// internal methods

		#region Data Protection API

		private class Win32 
		{
			public const int CRYPTPROTECT_UI_FORBIDDEN = 0x1;
			public const int CRYPTPROTECT_LOCAL_MACHINE = 0x4;

			[StructLayout(LayoutKind.Sequential)]
				public struct DATA_BLOB
			{
				public int cbData;
				public IntPtr pbData;
			}

			[DllImport("crypt32", CharSet=CharSet.Auto)]
			public static extern bool CryptProtectData(ref DATA_BLOB pDataIn, string szDataDescr, ref DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);

			[DllImport("crypt32", CharSet=CharSet.Auto)]
			public static extern bool CryptUnprotectData(ref DATA_BLOB pDataIn, StringBuilder szDataDescr, ref DATA_BLOB pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);

			[DllImport("kernel32")] 
			public static extern IntPtr LocalFree(IntPtr hMem);
		}

		#endregion

		// helper method that fills in a DATA_BLOB, copies 
		// data from managed to unmanaged memory
		private static void SetBlobData(ref Win32.DATA_BLOB blob,  byte[] bits) 
		{
			blob.cbData = bits.Length;
			blob.pbData = Marshal.AllocHGlobal(bits.Length);
			Marshal.Copy(bits, 0, blob.pbData, bits.Length);
		}

		// helper method that gets data from a DATA_BLOB, 
		// copies data from unmanaged memory to managed
		private static byte[] GetBlobData(ref Win32.DATA_BLOB blob) 
		{
			// return an empty string if the blob is empty
			if (blob.pbData.ToInt32() == 0) 
				return null;

			// copy information from the blob
			byte[] data = new byte[blob.cbData];
			Marshal.Copy(blob.pbData, data, 0, blob.cbData);
			Win32.LocalFree(blob.pbData);

			return data;
		}
	}

}
