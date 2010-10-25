using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Portal.Facilities.Unused;
namespace Portal.Facilities
{
    /// <summary>
    /// 验证字符串是否合法
    /// </summary>
    public class ValidateUtil
    {
        /// <summary>构造函数</summary>
        public ValidateUtil()
        {

        }
        ///   <summary> 
        ///   防注入字符串检查 
        ///   </summary> 
        ///   <param   name= "str "> 待检查的字符串 </param> 
        ///   <returns> </returns> 
        public   static   bool   StringCheck(string   str) 
        { 
                if   (str.Trim()   ==   " "   ||   str   ==   null) 
                { 
                        return   true; 
                } 
                else 
                { 
                        Regex   re   =   new   Regex(@"\s "); 
                        str   =   re.Replace(str.Replace( "%20 ",   "   "),   "   "); 
                        string   pattern   =  @"select   |insert   |delete   from   |count\(|drop   table|update   |truncate   |asc\(|mid\(|char\(|xp_cmdshell|exec   master|net   localgroup   administrators|:|net   user|or"; 
                        if   (Regex.IsMatch(str,   pattern)) 
                        { 
                                return   false; 
                        } 
                        else 
                        { 
                                return   true; 
                        } 
                } 
        }

        /// <summary>是否空</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isBlank(string strInput)
        {
            if (strInput == null || strInput.Trim() == "")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>是否数字</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isNumeric(string strInput)
        {

            char[] ca = strInput.ToCharArray();
            bool found = true;
            for (int i = 0; i < ca.Length; i++)
            {
                if ((ca[i] < '0' || ca[i] > '9') && ca[i] != '.')
                {

                    found = false;
                    break;

                };

            };
            return found;

        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="values"></param>
        /// <param name="returnvalue"></param>
        /// <returns></returns>
        public static int CheckNumeric(string values, int returnvalue)
        {
            try
            {
                return Convert.ToInt32(HttpContext.Current.Request.QueryString[values]);
            }
            catch (Exception)
            {

                return returnvalue;
            }
        }
        /// <summary>是否日期</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isDate(string strInput)
        {
            string datestr = strInput;
            string year, month, day;
            string[] c ={ "/", "-", "." };
            string cs = "";
            for (int i = 0; i < c.Length; i++)
            {
                if (datestr.IndexOf(c[i]) > 0)
                {
                    cs = c[i];
                    break;
                }

            };

            if (cs != "")
            {
                year = datestr.Substring(0, datestr.IndexOf(cs));
                if (year.Length != 4) { return false; };
                datestr = datestr.Substring(datestr.IndexOf(cs) + 1);

                month = datestr.Substring(0, datestr.IndexOf(cs));
                if ((month.Length != 2) || (Convert.ToInt16(month) > 12))
                { return false; };
                datestr = datestr.Substring(datestr.IndexOf(cs) + 1);

                day = datestr;
                if ((day.Length != 2) || (Convert.ToInt16(day) > 31)) { return false; };

                return checkDatePart(year, month, day);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 检查年月日是否合法
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        private static bool checkDatePart(string year, string month, string day)
        {
            int iyear = Convert.ToInt16(year);
            int imonth = Convert.ToInt16(month);
            int iday = Convert.ToInt16(day);
            if (iyear > 2099 || iyear < 1900) { return false; }
            if (imonth > 12 || imonth < 1) { return false; }
            if (iday > DateUtil.GetDaysOfMonth(iyear, imonth) || iday < 1) { return false; };
            return true;


        }


        /// <summary>是否Null</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isNull(object strInput)
        {
            return true;
        }

        /// <summary>是否为Double</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isDouble(string strInput)
        {
            return true;
        }

        /// <summary>是否为Int</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isInt(string strInput)
        {
            return true;
        }

        /// <summary>是否为合法的电话号码</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isTel(string strInput)
        {
            //电话号码规则在Concief中配置[CurrentPhoneRule]
            if (isZhengZe(@"(^(\d{2,4}[-_－—]?)?\d{3,8}([-_－—]?\d{3,8})?([-_－—]?\d{1,7})?$)|(^0?1[35]\d{9}$)", strInput))
            {
                return true;
            }

            return false;
        }


        /// <summary>是否为合法的中国邮政编码</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isZip(string strInput)
        {
            //邮政编码规则在Concief中配置[CurrentZipRule]
            if (isZhengZe(@"^[1-9]\d{5}(?!\d)", strInput))
            {
                return true;
            }

            return false;
        }

        /// <summary>是否为合法的电子邮件</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isEmail(string strInput)
        {
            if (isZhengZe(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$", strInput))
            {
                return true;
            }
            return true;
        }


        /// <summary>
        /// 是否为生日
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isbirthday(string strInput)
        {
            //邮政编码规则在Concief中配置[CurrentZipRule]
            if (isZhengZe(@"^19\d\d-[0-1]\d-[0-3]\d+$", strInput) || isZhengZe(@"^20\d\d-[0-1]\d-[0-3]\d+$", strInput))
            {
                return true;
            }

            return false;
        }



        /// <summary>
        /// 判断是否为非负整数（正整数 + 0）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Is_positive_number_integer_and_zero(string str)
        {
            string regextext = @"^\d+$";
            Regex regex = new Regex(regextext, RegexOptions.None);
            return regex.IsMatch(str.Trim());
        }



        /// <summary>
        /// 使用正则表达式判断
        /// </summary>
        /// <param name="zhengze_str">正则表达式</param>
        /// <param name="str">要判断的字符串</param>
        /// <returns></returns>
        public static bool isZhengZe(string zhengze_str, string str)
        {
            string regextext = zhengze_str;
            Regex regex = new Regex(regextext, RegexOptions.None);
            return regex.IsMatch(str.Trim());
        }

    }
}
