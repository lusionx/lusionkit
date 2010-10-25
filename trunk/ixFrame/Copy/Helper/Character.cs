using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
namespace Portal.Facilities
{
    public class Character
    {
        #region Generate random value
        /// <summary>
        /// Generate random value 
        /// </summary> 
        /// <param name="digit">generate length</param>
        /// <returns></returns>
        public string GenerateCheckCode(int digit)
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < digit; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            return checkCode;
        }
        #endregion

        #region 生成随机数字
        public static string RandomNumber(int RanLength)
        {
            string RanNum = string.Empty;
            char[] Nums = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            Random r = new Random();
            for (int i = 0; i < RanLength; i++)
            {
                RanNum += Nums[r.Next(0, Nums.Length)].ToString();
            }

            return RanNum;
        }
        #endregion

        #region = IsNumeric =
        /// <summary>
        /// 返回 Boolean 值指明表达式的值是否为数字。

        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  IsNumeric(expression)
        /// </remarks>
        /// <param name="Str">参数可以是任意表达式。</param>
        /// <returns>表达式的值是否为数字</returns>
        public static bool IsNumeric(string Str)
        {
            if (string.IsNullOrEmpty(Str) == true)
            {
                return false;
            }

            int result = 0;

            return (int.TryParse(Str, out result));
        }
        #endregion

        #region 获得字符串实际长度（包括中文字符）

        public static int StringLength(string Str)
        {
            byte[] strArray = System.Text.Encoding.Default.GetBytes(Str);
            int res = strArray.Length;
            return res;
        }
        #endregion

        #region = Left =
        /// <summary>
        /// 返回指定数目的从字符串的左边算起的字符。

        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Left(string, length)
        /// </remarks>
        /// <param name="s">字符串表达式，其最左边的字符被返回。如果 string 参数中包含 Null，则返回 Null。</param>
        /// <param name="n">
        /// 数值表达式，指明要返回的字符数目。如果是 0，返回零长度字符串 ("")；如果大于或等于 string 参数中的字符总数，

        /// 则返回整个字符串。

        /// </param>
        /// <returns>返回指定数目的字符串</returns>
        public static string Left(string s, int n)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            if (n <= 0 || n > s.Length) return s;

            return s.Substring(0, n);
        }
        #endregion

        #region = Right =
        /// <summary>
        /// 从字符串右边返回指定数目的字符。

        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Right(string, length)
        /// </remarks>
        /// <param name="s">字符串表达式，其最右边的字符被返回。如果 string 参数中包含 Null，则返回 Null。</param>
        /// <param name="n">
        /// 数值表达式，指明要返回的字符数目。如果为 0，返回零长度字符串；
        /// 如果此数大于或等于 string 参数中的所有字符数目，则返回整个字符串。

        /// </param>
        /// <returns>返回指定数目的字符串</returns>
        public static string Right(string s, int n)
        {
            if (s == "" || s == null)
            {
                return null;
            }

            if (n <= 0 || n > s.Length) return s;

            return s.Substring(s.Length - n);
        }
        #endregion

        #region = Html2Text =
        /// <summary>
        /// HTML2s the text.
        /// </summary>
        /// <param name="strHTML">The STR HTML.</param>
        /// <returns></returns>
        public static string Html2Text(string strHTML)
        {
            strHTML = Regex.Replace(strHTML, @"<br.+>", "\n");
            strHTML = Regex.Replace(strHTML, @"&\s+;", string.Empty);
            //过滤HTML
            strHTML = RemoveHtml(strHTML);

            return strHTML;
        }

        #endregion

        #region = HtmlEncode =
        /// <summary>
        /// 将特殊字元转成 HTML 格式。

        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  HtmlEncode(string)
        /// 本函式将特殊字元转成 HTML 的字串格式 ( &....; )。最常用到的场合可能就是处理客户留言的留言版了。

        /// & (和) 转成 &amp; 
        /// " (双引号) 转成 &quot; 
        /// < (小于) 转成 &lt; 
        /// > (大于) 转成 &gt; 
        /// 此函式只转换上面的特殊字元，并不会全部转换成 HTML 所定的 ASCII 转换。 
        /// </remarks>
        /// <param name="str">字符串表达式，所有 string 的字元都转成 HTML 的特殊字集字串。如果 string 参数中包含 Null，则返回 ""</param>
        /// <returns>字串</returns>
        public static string HtmlEncode(string str)
        {
            if (str == null)
            {
                return string.Empty;
            }

            string chrChang;

            str = HttpUtility.HtmlEncode(str);

            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("&", "&amp;");

            chrChang = ((char)13).ToString();
            str = str.Replace(chrChang, "<br/>");

            chrChang = ((char)34).ToString();
            str = str.Replace(chrChang, "&quot;");

            chrChang = ((char)32).ToString();
            str = str.Replace(chrChang, "&nbsp;");

            chrChang = ((char)10).ToString();
            str = str.Replace(chrChang, string.Empty);

            return str;
        }
        #endregion

        #region = 获取网页内容 =
        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <remarks>
        /// e.g.:  
        /// GetWebPage(URL)
        /// </remarks>
        /// <param name="sUrl">一个标准的URL</param>
        /// <param name="compress">是否压缩</param>
        /// <returns>返回该网页的HTML代码</returns>
        public static string GetWebPage(string sUrl)
        {
            return GetWebPage(sUrl, true);
        }

        public static string GetWebPage(string sUrl, bool compress)
        {
            Encoding enc = Encoding.GetEncoding("GB2312");
            return GetWebPage(sUrl, compress, enc);
        }

        public static string GetWebPage(string sUrl, bool compress, Encoding enc)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sUrl);
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; Q312461; .NET CLR 1.0.3705)";

            HttpWebResponse res;

            try
            {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch
            {
                return "Error";
            }

            if (res.StatusCode == HttpStatusCode.OK)
            {
                StreamReader sr = new StreamReader(res.GetResponseStream(), enc);
                string sHtml = sr.ReadToEnd();
                sr.Close();
                res.Close();

                if (compress == true)
                {
                    sHtml = CompressHTML(sHtml);
                }

                return sHtml;
            }
            else
            {
                return "Error";
            }
        }
        #endregion

        #region = 压缩HTML =
        public static string CompressHTML(string sHtml)
        {
            sHtml = Regex.Replace(sHtml, @"<!--\S*?-->", "");

            sHtml = Regex.Replace(sHtml, @"", "");

            sHtml = Regex.Replace(sHtml, " {2,}", " ");
            sHtml = Regex.Replace(sHtml, @"\s{2,}", "\n");
            sHtml = Regex.Replace(sHtml, @">\s+?<", "><");

            return sHtml;
        }
        #endregion

        #region = 提取HTML控件的指定属性 =
        /// <summary>
        /// Pickups the HTML control attribute.
        /// </summary>
        /// <param name="HTML">The HTML.</param>
        /// <param name="TagName">Name of the tag.</param>
        /// <param name="AttributeName">Name of the attribute.</param>
        /// <param name="RemoteUri">The remote URI.</param>
        /// <returns></returns>
        public static string[] PickupHTMLControlAttribute(string HTML, string TagName, string AttributeName, Uri RemoteUri)
        {
            Regex re = new Regex("<" + TagName + " .*?" + AttributeName + "=(?<" + AttributeName + ">.+?) .*?>", RegexOptions.IgnoreCase);

            MatchCollection matches = re.Matches(HTML);

            string[] string_array = new string[matches.Count];

            int i = 0;
            foreach (Match match in matches)
            {
                string word = match.Groups["src"].Value.Replace("\"", "").ToLower();

                if (word.StartsWith("http") == true)
                {

                }
                else if (word.StartsWith("/") == true) //root 处理
                {
                    word = RemoteUri.ToString().Replace(RemoteUri.AbsolutePath, string.Empty) + word;
                }
                else
                {
                }

                string_array[i] = word;
                i++;
            }

            return string_array;
        }
        #endregion

        #region = GB2Big5 =
        /// <summary>
        /// 简体转换为繁体
        /// </summary>
        /// <remarks>
        /// e.g.:  
        /// GB2Big5(gb2312)
        /// </remarks>
        /// <param name="sUrl">一个简体的字符串</param>
        /// <returns>返回相应的繁体字符串</returns>
        public static string GB2Big5(string gb2312)
        {
            return Microsoft.VisualBasic.Strings.StrConv(gb2312, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, System.Globalization.CultureInfo.CurrentCulture.LCID);
        }
        #endregion

        #region = 字符截断 =
        /// <summary>
        /// Truncates the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string Truncate(string text, int length)
        {
            return Truncate(text, length, "...");
        }

        public static string Truncate(string text, int length, string Truncation)
        {
            StringBuilder str = new StringBuilder();

            if (length < 1 || string.IsNullOrEmpty(text) == true)
            {
                return text;
            }

            char[] TextDir = RemoveHtml(text).ToCharArray();
            char[] CharDir = text.ToCharArray(); //字符表


            int index = 0; //字符表的定位
            int count = 0; //目前的位置

            foreach (char c in TextDir)
            {
                while (c != CharDir[index])
                {
                    str.Append(CharDir[index]);
                    index++;
                    if (CharDir.Length < index) //防止数组溢出
                    {
                        break;
                    }
                }
                count += FindCharWidth(c);

                if (count / 12 > length)
                {
                    str.Append(Truncation);
                    break;
                }
                str.Append(c);

                index++;
                if (CharDir.Length < index) //防止数组溢出
                {
                    break;
                }
            }

            return str.ToString();
        }

        /// <summary>
        /// 截取指定长度中英文字符串方法(Truncate的截取有Bug,如"教 师 家 教 考试见效　不收... ")
        /// </summary>
        /// <param name="stringToSub">字符串</param>
        /// <param name="length">所要截取的长度</param>
        /// <returns></returns>
        public static string GetSubString(string stringToSub, int length)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = stringToSub.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            bool isCut = false;
            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength > length)
                {
                    isCut = true;
                    break;
                }
            }
            return sb.ToString();
        }

        private static int FindCharWidth(char c)
        {
            int[] charcode = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 224, 225, 232, 233, 234, 236, 237, 242, 243, 249, 250, 252, 257, 275, 283, 299, 324, 328, 333, 363, 462, 464, 466, 468, 470, 472, 474, 476, 593, 609, 913, 914, 915, 916, 917, 918, 919, 920, 921, 922, 923, 925, 926, 927, 928, 929, 931, 932, 933, 934, 935, 936, 937, 945, 946, 947, 948, 949, 950, 951, 952, 953, 954, 955, 956, 957, 958, 959, 960, 961, 963, 964, 965, 966, 967, 968, 969, 1025, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 1050, 1051, 1053, 1054, 1055, 1056, 1057, 1058, 1059, 1060, 1061, 1062, 1063, 1066, 1067, 1068, 1069, 1071, 1072, 1073, 1074, 1075, 1076, 1077, 1078, 1079, 1080, 1081, 1082, 1083, 1084, 1085, 1086, 1087, 1088, 1089, 1090, 1091, 1092, 1093, 1094, 1095, 1096, 1097, 1098, 1099, 1100, 1101, 1102, 1103, 1105, 8364, 59335, 59336, 59393, 59394, 59395, 59396, 59397 };
            int[] charlangth = new int[] { 8, 8, 8, 8, 8, 8, 8, 8, 0, 0, 0, 0, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 0, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 5, 5, 5, 5, 5, 3, 3, 8, 8, 8, 8, 8, 5, 5, 5, 3, 8, 8, 8, 8, 5, 3, 8, 8, 8, 8, 8, 8, 8, 8, 9, 8, 7, 8, 7, 8, 9, 9, 8, 9, 9, 9, 8, 9, 9, 7, 7, 8, 8, 8, 9, 9, 9, 8, 8, 5, 8, 5, 5, 8, 8, 3, 8, 8, 8, 8, 5, 8, 8, 8, 8, 5, 8, 7, 8, 8, 8, 7, 9, 7, 8, 7, 8, 7, 11, 8, 9, 9, 8, 8, 9, 9, 9, 7, 8, 8, 9, 9, 9, 9, 8, 8, 10, 7, 8, 8, 5, 8, 8, 5, 8, 5, 8, 5, 8, 8, 8, 8, 7, 8, 8, 8, 7, 5, 5, 7, 8, 5, 8, 8, 9, 9, 8, 8, 8, 5, 9, 8, 5, 8, 8, 8, 5, 7, 7, 7, 7 };

            int asc = (int)c;

            for (int i = 0; i < charcode.Length; i++)
            {
                if (asc == charcode[i])
                {
                    return charlangth[i];
                }
            }

            return 12;
        }
        #endregion

        #region = UBB代码处理函数 =
        /// <summary>
        /// UBB代码处理函数
        /// </summary>
        /// <param name="sDetail">输入字符串</param>
        /// <returns>输出字符串</returns>
        public static string UBBToHTML(string sDetail)
        {
            Regex r;
            Match m;
            #region 处理空格
            r = new Regex("(<.+) (.+>)", RegexOptions.IgnoreCase);

            do
            {
                m = r.Match(sDetail);
                sDetail = r.Replace(sDetail, "$1&nbspII;$2");
            } while (m.Success == true);


            sDetail = sDetail.Replace(" ", "&nbsp;");
            sDetail = sDetail.Replace("&nbspII;", " ");

            //sDetail = sDetail.Replace(" ", "&nbsp;");
            #endregion
            #region 处理单引号

            //sDetail = sDetail.Replace("'", "’");
            #endregion
            #region 处理双引号

            //sDetail = sDetail.Replace("\"", "&quot;");
            #endregion
            #region html标记符

            //sDetail = sDetail.Replace("<", "&lt;");
            //sDetail = sDetail.Replace(">", "&gt;");

            #endregion
            #region 处理换行
            //处理换行，在每个新行的前面添加两个全角空格

            r = new Regex(@"(\r\n((&nbsp;)|　)+)(?<正文>\S+)", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<BR>　　" + m.Groups["正文"].ToString());
            }
            //处理换行，在每个新行的前面添加两个全角空格

            sDetail = sDetail.Replace("\r\n", "<BR>");
            #endregion
            #region 处[b][/b]标记
            sDetail = Regex.Replace(sDetail, @"\[b\]", "<b>", RegexOptions.IgnoreCase);
            sDetail = Regex.Replace(sDetail, @"\[\/b\]", "</b>", RegexOptions.IgnoreCase);

            //r = new Regex(@"(\[b\])([ \S\t]*?)(\[\/b\])", RegexOptions.IgnoreCase);
            //for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            //{
            //    sDetail = sDetail.Replace(m.Groups[0].ToString(), "<B>" + m.Groups[2].ToString() + "</B>");
            //}
            #endregion
            #region 处[i][/i]标记
            r = new Regex(@"(\[i\])([ \S\t]*?)(\[\/i\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<I>" + m.Groups[2].ToString() + "</I>");
            }
            #endregion
            #region 处[u][/u]标记
            r = new Regex(@"(\[U\])([ \S\t]*?)(\[\/U\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<U>" + m.Groups[2].ToString() + "</U>");
            }
            #endregion
            #region 处[p][/p]标记
            //处[p][/p]标记
            r = new Regex(@"((\r\n)*\[p\])(.*?)((\r\n)*\[\/p\])", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<P class=\"pstyle\">" + m.Groups[3].ToString() + "</P>");
            }
            #endregion
            #region 处[sup][/sup]标记
            //处[sup][/sup]标记
            r = new Regex(@"(\[sup\])([ \S\t]*?)(\[\/sup\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<SUP>" + m.Groups[2].ToString() + "</SUP>");
            }
            #endregion
            #region 处[sub][/sub]标记
            //处[sub][/sub]标记
            r = new Regex(@"(\[sub\])([ \S\t]*?)(\[\/sub\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<SUB>" + m.Groups[2].ToString() + "</SUB>");
            }
            #endregion
            #region 处[url][/url]标记
            //处[url][/url]标记
            r = new Regex(@"(\[url\])([ \S\t]*?)(\[\/url\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<A href=\"" + m.Groups[2].ToString() + "\" target=\"_blank\">" +
                 m.Groups[2].ToString() + "</A>");
            }
            #endregion
            #region 处[url=xxx][/url]标记
            //处[url=xxx][/url]标记
            r = new Regex(@"(\[url=([ \S\t]+)\])([ \S\t]*?)(\[\/url\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<A href=\"" + m.Groups[2].ToString() + "\" target=\"_blank\">" +
                 m.Groups[3].ToString() + "</A>");
            }
            #endregion
            #region 处[email][/email]标记
            //处[email][/email]标记
            r = new Regex(@"(\[email\])([ \S\t]*?)(\[\/email\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<A href=\"mailto:" + m.Groups[2].ToString() + "\" target=\"_blank\">" +
                 m.Groups[2].ToString() + "</A>");
            }
            #endregion
            #region 处[email=xxx][/email]标记
            //处[email=xxx][/email]标记
            r = new Regex(@"(\[email=([ \S\t]+)\])([ \S\t]*?)(\[\/email\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<A href=\"mailto:" + m.Groups[2].ToString() + "\" target=\"_blank\">" +
                 m.Groups[3].ToString() + "</A>");
            }
            #endregion
            #region 处[size=x][/size]标记
            //处[size=x][/size]标记
            sDetail = Regex.Replace(sDetail, @"\[size=([1-7])\]", "<font size=\"$1\">");
            sDetail = Regex.Replace(sDetail, @"\[\/size\]", "</font>");

            //r = new Regex(@"(\[size=([1-7])\])([ \S\t]*?)(\[\/size\])", RegexOptions.IgnoreCase);
            //for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            //{
            //    sDetail = sDetail.Replace(m.Groups[0].ToString(),
            //     "<font size=" + m.Groups[2].ToString() + ">" +
            //     m.Groups[3].ToString() + "</font>");
            //}
            #endregion
            #region 处[color=x][/color]标记
            //处[color=x][/color]标记
            sDetail = Regex.Replace(sDetail, @"\[color=([\S]+?)\]", "<font color=\"$1\">");
            sDetail = Regex.Replace(sDetail, @"\[\/color\]", "</font>");
            //r = new Regex(@"(\[color=([\S]+)\])([\S\s]*?)(\[\/color\])", RegexOptions.IgnoreCase);
            //for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            //{
            //    sDetail = sDetail.Replace(m.Groups[0].ToString(),
            //     "<font color=" + m.Groups[2].ToString() + ">" +
            //     m.Groups[3].ToString() + "</font>");
            //}
            #endregion
            #region 处[font=x][/font]标记
            //处[font=x][/font]标记
            r = new Regex(@"(\[font=([\S]+)\])([ \S\t]*?)(\[\/font\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<font face=" + m.Groups[2].ToString() + ">" +
                 m.Groups[3].ToString() + "</font>");
            }
            #endregion
            #region 处理图片链接
            //处理图片链接
            r = new Regex("\\[picture\\](\\d+?)\\[\\/picture\\]", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<A href=\"ShowImage.aspx?Type=ALL&Action=forumImage&ImageID=" + m.Groups[1].ToString() +
                 "\" target=\"_blank\"><IMG border=0 Title=\"点击打开新窗口查看\" src=\"ShowImage.aspx?Action=forumImage&ImageID=" + m.Groups[1].ToString() +
                 "\"></A>");
            }
            #endregion
            #region 处理[align=x][/align]
            //处理[align=x][/align]
            r = new Regex(@"(\[align=([\S]+)\])([ \S\t]*?)(\[\/align\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<P align=" + m.Groups[2].ToString() + ">" +
                 m.Groups[3].ToString() + "</P>");
            }
            #endregion
            #region 处[H=x][/H]标记
            //处[H=x][/H]标记
            r = new Regex(@"(\[H=([1-6])\])([ \S\t]*?)(\[\/H\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<H" + m.Groups[2].ToString() + ">" +
                 m.Groups[3].ToString() + "</H" + m.Groups[2].ToString() + ">");
            }
            #endregion
            #region 处理[list=x][*][/list]
            //处理[list=x][*][/list]
            r = new Regex(@"(\[list(=(A|a|I|i| ))?\]([ \S\t]*)\r\n)((\[\*\]([ \S\t]*\r\n))*?)(\[\/list\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                string strLI = m.Groups[5].ToString();
                Regex rLI = new Regex(@"\[\*\]([ \S\t]*\r\n?)", RegexOptions.IgnoreCase);
                Match mLI;
                for (mLI = rLI.Match(strLI); mLI.Success; mLI = mLI.NextMatch())
                {
                    strLI = strLI.Replace(mLI.Groups[0].ToString(), "<LI>" + mLI.Groups[1]);
                }
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<UL TYPE=\"" + m.Groups[3].ToString() + "\"><B>" + m.Groups[4].ToString() + "</B>" +
                 strLI + "</UL>");
            }

            #endregion
            #region 处[SHADOW=x][/SHADOW]标记
            //处[SHADOW=x][/SHADOW]标记
            r = new Regex(@"(\[SHADOW=)(\d*?),(\w*?),(\d*?)\]([\S\t]*?)(\[\/SHADOW\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<TABLE WIDTH=" + m.Groups[2].ToString() + "  STYLE=FILTER:SHADOW(COLOR=" + m.Groups[3].ToString() + ", STRENGTH=" + m.Groups[4].ToString() + ")>" +
                 m.Groups[5].ToString() + "</TABLE>");
            }
            #endregion
            #region 处[glow=x][/glow]标记
            //处[glow=x][/glow]标记
            r = new Regex(@"(\[glow=)(\d*?),(#*\w*?),(\d*?)\]\[([\S\t]*?)\](\[\/glow\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<TABLE WIDTH=" + m.Groups[2].ToString() + "  STYLE=FILTER:GLOW(COLOR=" + m.Groups[3].ToString() + ", STRENGTH=" + m.Groups[4].ToString() + ")>" +
                 m.Groups[5].ToString() + "</TABLE>");
            }
            #endregion
            #region 处[center][/center]标记
            r = new Regex(@"(\[center\])([ \S\t]*?)(\[\/center\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<CENTER>" + m.Groups[2].ToString() + "</CENTER>");
            }
            #endregion
            #region 处[IMG][/IMG]标记
            r = new Regex(@"(\[IMG\])(http|https|ftp):\/\/([ \S\t]*?)(\[\/IMG\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<br><a onfocus=this.blur() href=" + m.Groups[2].ToString() + "://" + m.Groups[3].ToString() + " target=_blank><IMG SRC=" + m.Groups[2].ToString() + "://" + m.Groups[3].ToString() + " border=0 alt=按此在新窗口浏览图片 onload=javascript:if(screen.width-333<this.width)this.width=screen.width-333></a>");
            }
            #endregion
            #region 处[em]标记
            r = new Regex(@"(\[em([\S\t]*?)\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<img src=pic/em" + m.Groups[2].ToString() + ".gif border=0 align=middle>");
            }
            #endregion
            #region 处[flash=x][/flash]标记
            //处[mp=x][/mp]标记
            r = new Regex(@"(\[flash=)(\d*?),(\d*?)\]\[([\S\t]*?)\](\[\/flash\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<a href=" + m.Groups[4].ToString() + " TARGET=_blank><IMG SRC=pic/swf.gif border=0 alt=点击开新窗口欣赏该FLASH动画!> [全屏欣赏]</a><br><br><OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><PARAM NAME=movie VALUE=" + m.Groups[4].ToString() + "><PARAM NAME=quality VALUE=high><param name=menu value=false><embed src=" + m.Groups[4].ToString() + " quality=high menu=false pluginspage=http://www.macromedia.com/go/getflashplayer type=application/x-shockwave-flash width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + ">" + m.Groups[4].ToString() + "</embed></OBJECT>");
            }
            #endregion
            #region 处[dir=x][/dir]标记
            //处[dir=x][/dir]标记
            r = new Regex(@"(\[dir=)(\d*?),(\d*?)\]\[([\S\t]*?)\](\[\/dir\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<object classid=clsid:166B1BCA-3F9C-11CF-8075-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/director/sw.cab#version=7,0,2,0 width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><param name=src value=" + m.Groups[4].ToString() + "><embed src=" + m.Groups[4].ToString() + " pluginspage=http://www.macromedia.com/shockwave/download/ width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "></embed></object>");
            }
            #endregion
            #region 处[rm=x][/rm]标记
            //处[rm=x][/rm]标记
            r = new Regex(@"(\[rm=)(\d*?),(\d*?)\]\[([\S\t]*?)\](\[\/rm\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<OBJECT classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=OBJECT id=RAOCX width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><PARAM NAME=SRC VALUE=" + m.Groups[4].ToString() + "><PARAM NAME=CONSOLE VALUE=Clip1><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=true></OBJECT><br><OBJECT classid=CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=" + m.Groups[2].ToString() + "><PARAM NAME=SRC VALUE=" + m.Groups[4].ToString() + "><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=Clip1></OBJECT>");
            }
            #endregion
            #region 处[mp=x][/mp]标记
            //处[mp=x][/mp]标记
            r = new Regex(@"(\[mp=)(\d*?),(\d*?)\]\[([\S\t]*?)\](\[\/mp\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<object align=middle classid=CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + " ><param name=ShowStatusBar value=-1><param name=Filename value=" + m.Groups[4].ToString() + "><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=" + m.Groups[4].ToString() + "  width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "></embed></object>");
            }
            #endregion
            #region 处[qt=x][/qt]标记
            //处[qt=x][/qt]标记
            r = new Regex(@"(\[qt=)(\d*?),(\d*?)\]\[([\S\t]*?)\](\[\/qt\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<embed src=" + m.Groups[4].ToString() + " width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + " autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/>");
            }
            #endregion
            #region 处[QUOTE][/QUOTE]标记
            r = new Regex(@"(\[QUOTE\])\[([ \S\t]*?)\](\[\/QUOTE\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<table cellpadding=0 cellspacing=0 border=1 WIDTH=94% bordercolor=#000000 bgcolor=#F2F8FF align=center  style=FONT-SIZE: 9pt><tr><td  ><table width=100% cellpadding=5 cellspacing=1 border=0><TR><TD >" + m.Groups[2].ToString() + "</table></table><br>");
            }
            #endregion
            #region 处[move][/move]标记
            r = new Regex(@"(\[move\])\[([ \S\t]*?)\](\[\/move\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<MARQUEE scrollamount=3>" + m.Groups[2].ToString() + "</MARQUEE>");
            }
            #endregion
            #region 处[FLY][/FLY]标记
            r = new Regex(@"(\[FLY\])\[([ \S\t]*?)\](\[\/FLY\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<MARQUEE width=80% behavior=alternate scrollamount=3>" + m.Groups[2].ToString() + "</MARQUEE>");
            }
            #endregion
            #region 处[image][/image]标记
            //处[image][/image]标记
            r = new Regex(@"(\[image\])\[([ \S\t]*?)\](\[\/image\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<img src=\"" + m.Groups[2].ToString() + "\" border=0 align=middle onload=\"javascript:if(this.width>screen.width-333)this.width=screen.width-333\"><br>");
            }
            #endregion
            #region 处[upload][/upload]标记
            //处[upload][/upload]标记
            r = new Regex(@"(\[upload\])([ \S\t]*?)(\[\/upload\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<img src=\"" + m.Groups[2].ToString() + "\" border=0 align=middle onload=\"javascript:if(this.width>screen.width-333)this.width=screen.width-333\"><br>");
            }
            #endregion
            #region 处[upload][/upload]标记
            //处[upload][/upload]标记
            r = new Regex(@"(\[uploadzip\])([ \S\t]*?)(\[\/uploadzip\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<a href=\"" + m.Groups[2].ToString() + "\" border=0 align=middle><img src=\"/images/Fileico/rar.gif\" border=\"0\"></a><br>");
            }
            #endregion

            #region 处[UPLOADpdf][/UPLOADpdf]标记
            //处[UPLOADpdf][/UPLOADpdf]标记
            r = new Regex(@"(\[UPLOADpdf\])([ \S\t]*?)(\[\/UPLOADpdf\])", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(),
                 "<a href=\"" + m.Groups[2].ToString() + "\" border=0 align=middle><img src=\"/images/Fileico/rar.gif\" border=\"0\"></a><br>");
            }
            #endregion



            return sDetail;
        }
        #endregion

        #region = Replicate =
        /// <summary>
        /// 以指定的次数重复字符表达式

        /// </summary>
        /// <param name="character">由字符数据组成的字母数字表达式。</param>
        /// <param name="number">是正整数。如果为负，则返回空字符串。</param>
        /// <returns>返回具有指定长度的、重复字符组成的字符串。</returns>
        public static string Replicate(string character, int number)
        {
            if (number < 1)
            {
                return string.Empty;
            }

            StringBuilder str = new StringBuilder(character, number);

            return str.ToString();
        }
        #endregion

        #region = Html =

        #region = Filter HTML =
        /// <summary>
        /// Filters the script.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public static string FilterScript(string content)
        {
            if (string.IsNullOrEmpty(content) == true)
            {
                return content;
            }
            string regexstr = @"(?i)<script([^>])*>(\w|\W)*?</script([^>])*>";//@"<script.*</script>";
            content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<script([^>])*?>", string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "</script>", string.Empty, RegexOptions.IgnoreCase);

            regexstr = @" href *= *[\s\S]*?script *:.+?""";
            //content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);

            regexstr = @" on\w+?="".+?"" ";
            content = Regex.Replace(content, regexstr, " ", RegexOptions.IgnoreCase);

            return content;
        }

        public static string FilterIFrame(string content)
        {
            if (string.IsNullOrEmpty(content) == true)
            {
                return content;
            }
            string regexstr = @"(?i)<iframe([^>])*>(\w|\W)*</iframe([^>])*>";//@"<script.*</script>";
            content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<iframe([^>])*>", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(content, "</iframe>", string.Empty, RegexOptions.IgnoreCase);
        }

        public static string FilterFrameset(string content)
        {
            if (string.IsNullOrEmpty(content) == true)
            {
                return content;
            }
            string regexstr = @"(?i)<iframe([^>])*>(\w|\W)*</frameset([^>])*>";//@"<script.*</script>";
            content = Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<frameset([^>])*>", string.Empty, RegexOptions.IgnoreCase);
            return Regex.Replace(content, "</frameset>", string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion

        /// <summary>
        /// Removes the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string RemoveHtml(string html)
        {
            html = Regex.Replace(html, "<br>", " ");
            html = Regex.Replace(html, @"<[\s|\S]+?>", string.Empty);
            html = Regex.Replace(html, "&nbsp;", " ");
            html = Regex.Replace(html, " {2,}", " ");
            html = Regex.Replace(html, @"\s", "");
            return html;
        }

        public static string RemoveHtmlTag(string content, string[] tags)
        {
            string regexstr1, regexstr2;
            foreach (string tag in tags)
            {
                if (string.IsNullOrEmpty(tag) == false)
                {
                    regexstr1 = string.Format(@"<{0}([^>])*>", tag);
                    regexstr2 = string.Format(@"</{0}([^>])*>", tag);
                    content = Regex.Replace(content, regexstr1, string.Empty, RegexOptions.IgnoreCase);
                    content = Regex.Replace(content, regexstr2, string.Empty, RegexOptions.IgnoreCase);
                }
            }
            return content;

        }

        public static string RemoveHtmlTag(string content, string tag)
        {
            string returnStr;
            string regexstr1 = string.Format(@"<{0}([^>])*>", tag);
            string regexstr2 = string.Format(@"</{0}([^>])*>", tag);
            returnStr = Regex.Replace(content, regexstr1, string.Empty, RegexOptions.IgnoreCase);
            returnStr = Regex.Replace(returnStr, regexstr2, string.Empty, RegexOptions.IgnoreCase);
            return returnStr;

        }

        public static string ReplaceSpace(string content)
        {
            string findstr = "(?<fore>(?:(?:[^< ])*(?:<(?:!--(?:(?:[^-])*(?:(?=-->)|-))*--|(?:[^>])+)>)?)*)[ ](?<back>(?:(?:[^< ])*(?:<(?:!--(?:(?:[^-])*(?:(?=-->)|-))*--|(?:[^>])+)>)?)*)";
            //"(?<fore>(?:[^< ]*(?:<[^>]+>)?)*)[ ](?<back>(?:[^< ]*(?:<[^>]+>)?)*)";
            string replacestr = "${fore}&nbsp;${back}";
            return Regex.Replace(content, findstr, replacestr, RegexOptions.IgnoreCase);

        }

        public static string[] CatchHtmlBlock(string content, string tag)
        {
            string findstr = string.Format(@"(?i)<{0}([^>])*>(\w|\W)*</{1}([^>])*>", tag, tag);
            System.Text.RegularExpressions.MatchCollection matchs = System.Text.RegularExpressions.Regex.Matches(content, findstr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            string[] strArray = new string[matchs.Count];
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = matchs[i].Value;
            }
            return strArray;
        }

        #endregion

        #region = DecodingBigEndianUnicode =
        /// <summary>
        /// Decodings the big endian unicode.
        /// </summary>
        ///  /// <remarks>
        /// 帮助解决网页和JS文件中的中文编码问题的小工具 
        /// http://thinhunan.cnblogs.com/archive/2005/12/27/305590.html
        /// </remarks>
        /// <param name="encodedString">The encoded string.</param>
        /// <returns></returns>
        public static string DecodingBigEndianUnicode(string encodedString)
        {
            Regex regUnicode = new Regex(@"\\u(?<1>[a-zA-Z0-9]{2})(?<2>[a-zA-Z0-9]{2})");
            MatchCollection mc = regUnicode.Matches(encodedString);
            string s = string.Empty;
            foreach (Match m in mc)
            {
                byte b1 = byte.Parse(m.Groups[1].Value, System.Globalization.NumberStyles.HexNumber);
                byte b2 = byte.Parse(m.Groups[2].Value, System.Globalization.NumberStyles.HexNumber);
                s += Encoding.BigEndianUnicode.GetString(new byte[] { b1, b2 });
            }
            return s;
        }
        #endregion

        #region = EncodingBigEndianUnicode =
        /// <summary>
        /// Encodings the big endian unicode.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <remarks>
        /// 帮助解决网页和JS文件中的中文编码问题的小工具 
        /// http://thinhunan.cnblogs.com/archive/2005/12/27/305590.html
        /// </remarks>
        /// <returns></returns>
        public static string EncodingBigEndianUnicode(string text)
        {
            string s = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                string s1 = text.Substring(i, 1);
                byte[] bs = Encoding.BigEndianUnicode.GetBytes(s1);
                s1 = @"\u";
                foreach (byte b in bs)
                {
                    string s2 = b.ToString("x");
                    if (s2.Length == 1)
                        s2 = "0" + s2;
                    s1 += s2;
                }
                s += s1;
            }
            return s;
        }
        #endregion

        #region = ArrayToString =
        /// <summary>
        /// Arrays to string.
        /// </summary>
        /// <param name="s_array">The s_array.</param>
        /// <param name="SeparateChar">The separate char.</param>
        /// <returns></returns>
        public static string ArrayToString(string[] s_array, string SeparateChar)
        {
            StringBuilder str = new StringBuilder();

            foreach (string s in s_array)
            {
                str.Append(s);
                str.Append(SeparateChar);
            }

            return Regex.Replace(str.ToString(), SeparateChar + "$", string.Empty);
        }

        public static string ArrayToString(string[] s_array)
        {
            return ArrayToString(s_array, ",");
        }
        #endregion

        #region 获取数字
        public static int GetInt(object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            int result = 0;
            int.TryParse(obj.ToString(), out result);
            return result;
        }
        #endregion



        #region 将特定字符串转成HashTable
        /// <summary>
        /// 将特定字符串转成HashTable，分隔符分别为'&'和'|'
        /// </summary>
        /// <param name="Content">待转字符</param>
        /// <returns>HashTable</returns>
        public static System.Collections.Hashtable ConvertToHashTable(string Content)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            string[] Strs = Content.Split('&');
            foreach (string str in Strs)
            {
                string[] KeyAndValue = str.Split('|');
                if (KeyAndValue.Length >= 2)
                {
                    HT.Add(KeyAndValue[0], KeyAndValue[1]);
                }
            }
            return HT;
        }
        /// <summary>
        /// 将特定字符串转成HashTable
        /// </summary>
        /// <param name="Content">待转字符</param>
        /// <returns>HashTable</returns>
        public static System.Collections.Hashtable ConvertToHashTable(string Content, char Sp1, char Sp2)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            string[] Strs = Content.Split(Sp1);
            foreach (string str in Strs)
            {
                string[] KeyAndValue = str.Split(Sp2);
                if (KeyAndValue.Length >= 2)
                {
                    HT.Add(KeyAndValue[0], KeyAndValue[1]);
                }
            }
            return HT;
        }
        #endregion

        #region Base64编码与解码

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = string.Empty;
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(string code_type, string code)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        #endregion

        #region 字符编码转换utf-8转gb2312
        /// <summary>
        /// 字符编码转换utf-8转gb2312
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string ConversionEncoding(string Source)
        {

            Encoding big = Encoding.GetEncoding("GB2312");
            Encoding utf = Encoding.UTF8;
            byte[] srcarray = big.GetBytes(Source);
            byte[] utfarray = Encoding.Convert(big, utf, srcarray);
            string utfresult = utf.GetString(utfarray);
            return utfresult;
        }

        public static string ConvertBig5toGB2312(string source)
        {
            Encoding big = Encoding.GetEncoding("Big5");
            Encoding gb = Encoding.GetEncoding("GB2312");
            byte[] sourcearray = big.GetBytes(source);
            byte[] gbarray = Encoding.Convert(big, gb, sourcearray);

            return gb.GetString(gbarray);
        }


        #endregion
    }
}
