using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using Portal.Facilities.Helper;

namespace Portal.Facilities
{
    /// <summary>
    /// 定义了URL重写的配置类文件结构以及获取配置
    /// </summary>
    [Serializable()]
    [XmlRoot("RewriterConfig")]
    public sealed class RewriterConfiguration
    {
        #region 常量定义

        string applicationPath = HttpContext.Current != null ? HttpContext.Current.Request.ApplicationPath : string.Empty;
        private static RewriterConfiguration instance = null;
        //private static object lockObject = new object ();
        /// <summary>
        /// 配置文件文件名
        /// </summary>
        public const string URL_CONFIGURATIONS_FILE = "Config/RewriteURLs.config";
        /// <summary>
        /// 用户URL的模版
        /// </summary>
        public const string USER_URL_TEMPLATE_STRING = "~/my/{0}";
        /// <summary>
        /// 用户URL的正则
        /// </summary>
        public const string USER_URL_REGEX_STRING = "~/my/([0-9a-zA-Z]+)";
        public static Regex USER_URL_REGEX = null;
        /// <summary>
        /// 验证用户URL的正则的正则
        /// </summary>
        private const string IDENTIFY_USER_URL_REGEX_STRING = "~/my/[\\\\\\(\\)\\[\\]a-z0-9\\-\\+_\\{\\}\\|\\,]+";
        public static Regex IDENTIFY_USER_URL_REGEX = new Regex(IDENTIFY_USER_URL_REGEX_STRING, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region 私有属性
        private RewriterRuleCollection subDomainRules;

        private RewriterRuleCollection allRules;
        #endregion

        #region 公共属性
        public RewriterRuleCollection AllRules
        {
            get { return allRules; }
            set { allRules = value; }
        }
        /// <summary>
        /// 二级域名重写规则
        /// </summary>
        public RewriterRuleCollection SubDomainRules
        {
            get
            {
                return subDomainRules;
            }
            set
            {
                subDomainRules = value;
            }
        }
        #endregion

        private RewriterConfiguration(XmlDocument doc)
        {

            allRules = new RewriterRuleCollection();
            XmlNode root = doc.SelectSingleNode("RewriterConfig/Rules");
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    string lookFor = n.SelectSingleNode("LookFor").InnerText;
                    string sendTo = n.SelectSingleNode("SendTo").InnerText;
                    bool redirect = false;
                    XmlAttribute redirectAttribute = n.Attributes["Redirect"];
                    if (redirectAttribute != null)
                    {
                        redirect = SafeConvert.ToBoolean(redirectAttribute.Value);
                    }
                    Regex lookForRegex = new Regex("^" + RewriterUtils.ResolveUrl(applicationPath, lookFor) + "$", RegexOptions.IgnoreCase);
                    RewriterRule rewriterRule = new RewriterRule(lookFor, lookForRegex, sendTo, redirect);

                    allRules.Add(rewriterRule);
                    if (IDENTIFY_USER_URL_REGEX.IsMatch(lookFor))
                    {
                        string subDomainLookFor = "~" + IDENTIFY_USER_URL_REGEX.Replace(lookFor, "");
                        rewriterRule.SubDomainLookFor = subDomainLookFor;
                        rewriterRule = new RewriterRule(lookFor, lookForRegex, sendTo, subDomainLookFor, redirect);
                        subDomainRules.Add(rewriterRule);
                    }
                }
            }
        }

        #region 取得配置实例

        public RewriterConfiguration()
        {
            USER_URL_REGEX = new Regex(RewriterUtils.ResolveUrl(applicationPath, RewriterConfiguration.USER_URL_REGEX_STRING),
                       RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 从配置文件中返回一个RewriterConfiguration实例
        /// </summary>
        public static RewriterConfiguration GetConfig()
        {

            if (instance == null)
            {
                string path = null;
                HttpContext context = HttpContext.Current;
                if (context != null)
                    path = context.Server.MapPath("~/" + URL_CONFIGURATIONS_FILE);
                else
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, URL_CONFIGURATIONS_FILE);
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                instance = new RewriterConfiguration(doc);
            }
            return instance;
        }

        #endregion
    }
}
