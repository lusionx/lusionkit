using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Portal.Facilities;


namespace Portal.Facilities
{
    public static class VCode
    {
        /// <summary>
        /// 产生一个验证码
        /// </summary>
        public static void GenerateVcode()
        {
            //char[] vchar = { 'A', 'B', 'C', 'E', 'F', 'G', 'H', '9', 'K', 'M', 'N', 'P', 'Q', '2', 'S', 'T', 'U', 'V', '5', 'X', 'Y', 'Z', 'D', 'R', '3', '4', 'W', '6', '7', '8', 'J' };
			char [] vchar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            Randoms randoms = new Randoms();
			StringBuilder vstring = new StringBuilder( 5 );
            for (int i = 0; i < 4; i++)
            {
                vstring.Append( vchar[randoms.Next(0, 9)].ToString() );
            }
            HttpContext.Current.Session[QueryStringCons.ValidateCode] = vstring;
        }

        #region 验证码
        /// <summary>
        /// 验证输入的注册码是否正确
        /// </summary>
        public static bool ValidVcode(string vcode, ref string errHtml)
        {
            string errorStr = string.Empty;
            bool isValid = true;
            if (string.IsNullOrEmpty(vcode))
            {
                errorStr = "请输入验证码";
                isValid = false;
            }
            else
            {
                if (HttpContext.Current.Session[QueryStringCons.ValidateCode] != null)
                {
                    if (HttpContext.Current.Session[QueryStringCons.ValidateCode].ToString().ToUpper() != vcode.ToUpper())
                    {
                        {
                            errorStr = "验证码输入有误,请重新填写";
                            isValid = false;
                        }
                    }
                }
                else
                {
                    errorStr = "对不起，验证码已失效,请刷新页面";
                    isValid = false;
                }
            }
            if (errHtml != string.Empty)
            {
                errHtml += "<br>";
            }
            errHtml += errorStr;
            return isValid;
        }
        #endregion
        #region 获取验证码路径
        /// <summary>
        /// 获取验证码路径
        /// </summary>
        /// <returns></returns>
        public static string GetVcodeUrl(string prehttpurl)
        {

            GenerateVcode();

            return prehttpurl + "ValidateCode.ashx?code=" +
                    HttpUtility.UrlEncode(DataProtection.Encrypt(HttpContext.Current.Session[QueryStringCons.ValidateCode].ToString(),
                                                                DataProtection.Store.User));

        }
        #endregion
    }
}
