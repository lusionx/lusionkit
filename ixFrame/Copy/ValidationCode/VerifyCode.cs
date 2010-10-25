//***************************************************************
// 文件名:		VerifyCode.cs
// 版权:		Copyright @ 2007 Mtime
// 创建人:		cuigh  2007-8-14
// 代码实现:	
// 修改人:		
// 描述:		验证码
// 备注:		
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
        // 在全部修改完成前为了不与以前的验证码实现相冲突,暂用一个不同的CacheKey
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
        /// 验证输入的注册码是否正确
        /// </summary>
        public static bool Validate(string codeID, string code, ref string errHtml)
        {
            string errorStr = string.Empty;
            bool isValid = true;
            if (string.IsNullOrEmpty(code))
            {
                errorStr = "请输入验证码";
                isValid = false;
            }
            else
            {
                string vc = GetVerifyCode(codeID);
                if (string.IsNullOrEmpty(vc))
                {
                    errorStr = "对不起，验证码已失效,请刷新页面";
                    isValid = false;
                }
                else if (vc != code)
                {
                    errorStr = "验证码输入有误,请重新填写";
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
