//***************************************************************
// �ļ���:		VerifyCode.cs
// ��Ȩ:		Copyright @ 2007 Mtime
// ������:		cuigh  2007-8-14
// ����ʵ��:	
// �޸���:		
// ����:		��֤��
// ��ע:		
//***************************************************************
using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections.Generic;

namespace Portal.Facilities
{
    public static class VerifyCode
	{
        // ��ȫ���޸����ǰΪ�˲�����ǰ����֤��ʵ�����ͻ,����һ����ͬ��CacheKey
        private const string VerifyKey = "_VerifyKey_";

        public static string CreateVerifyCode()
        {
            Dictionary<string, string> dict = HttpContext.Current.Session[VerifyKey] as Dictionary<string, string>;
            if (dict == null)
            {
                dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                HttpContext.Current.Session[VerifyKey] = dict;
            }

            string code = new Random().Next(0, 10000).ToString("0000");
            string codeID = Guid.NewGuid().ToString();
            dict.Add(codeID, code);

            return codeID;
        }

        public static string GetVerifyCode(string codeID)
        {
            string code;
            Dictionary<string, string> dict = HttpContext.Current.Session[VerifyKey] as Dictionary<string, string>;
            if (dict == null) code = string.Empty;
            else if (!dict.TryGetValue(codeID, out code)) code = string.Empty;

            return code;
        }

        public static void RemoveVerifyCode(string codeID)
        {
            Dictionary<string, string> dict = HttpContext.Current.Session[VerifyKey] as Dictionary<string, string>;
            if (dict != null) dict.Remove(codeID);
        }

        /// <summary>
        /// ��֤�����ע�����Ƿ���ȷ
        /// </summary>
        public static bool Validate(string codeID, string code, ref string errHtml)
        {
            string errorStr = string.Empty;
            bool isValid = true;
            if (string.IsNullOrEmpty(code))
            {
                errorStr = "��������֤��";
                isValid = false;
            }
            else
            {
                string vc = GetVerifyCode(codeID);
                if (string.IsNullOrEmpty(vc))
                {
                    errorStr = "�Բ�����֤����ʧЧ,��ˢ��ҳ��";
                    isValid = false;
                }
                else if (vc != code)
                {
                    errorStr = "��֤����������,��������д";
                    isValid = false;
                }
            }

            if (errHtml != string.Empty) errHtml += "<br>";
            errHtml += errorStr;
            return isValid;
        }

        public static string GetImageUrl(string code)
        {
            string codeEncrypt = HttpUtility.UrlEncode(DataProtection.Encrypt(code, DataProtection.Store.User));
            string ValidateURL = (System.Configuration.ConfigurationManager.AppSettings["ValidateURL"] == null ?
            string.Empty : System.Configuration.ConfigurationManager.AppSettings["ValidateURL"].ToString());

            return string.Format(ValidateURL, codeEncrypt);
        }

	}
}
