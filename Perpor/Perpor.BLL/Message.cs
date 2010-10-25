using System;
using System.Collections.Generic;
using System.Collections;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Web.Security;
using System.Net;
using System.Web;
using System.Linq;

namespace Perpor.BLL
{
    internal class SysConfig
    {
        /// <summary>
        /// ��ȡ��������, ���ز���ָ����[appSettings]�ڵ������ֵ
        /// </summary>
        /// <param name="key">��������</param>
        /// <returns>���õ�ֵ</returns>
        public static string GetConfig(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }

    public class Message
    {
        public static bool IsUserCredentials
        {
            get
            {
                string tmp = SysConfig.GetConfig("SmtpClientIsUserCredentials");
                return "true".Equals(tmp.ToLower());
            }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public static bool IsOnlyForTest
        {
            get
            {
                string tmp = SysConfig.GetConfig("SmtpClientIsOnlyForTest");
                return "true".Equals(tmp.ToLower());
            }
        }

        public static string TestAccount
        {
            get
            {
                return SysConfig.GetConfig("SmtpClientTestAccount");
            }
        }

        /// <summary>
        /// smtp�ʺ�
        /// </summary>
        public static string Account
        {
            get { return SysConfig.GetConfig("SmtpClientCredentialAccount"); }
        }

        /// <summary>
        /// �����֤
        /// </summary>
        public static NetworkCredential Credential
        {
            get
            {
                string password = SysConfig.GetConfig("SmtpClientCredentialPassword");
                return new NetworkCredential(Account, password);
            }
        }


        /// <summary>
        /// smtp������ַ
        /// </summary>
        public static string Host
        {
            get
            {
                return SysConfig.GetConfig("SmtpClientHost");
            }
        }



        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="receivers">�����ʼ����ʺ�</param>
        /// <param name="title">�ʼ�����</param>
        /// <param name="template">�ʼ�ģ��</param>
        /// <param name="contents">����</param>
        public static void SendMail(string receivers, string title, string templateFile, Hashtable contents)
        {
            if (templateFile.Contains("~"))
            {
                templateFile = HttpContext.Current.Server.MapPath(templateFile);
            }
            if (System.IO.File.Exists(templateFile))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(templateFile, Encoding.Default);
                System.Text.StringBuilder sb = new System.Text.StringBuilder(sr.ReadToEnd());
                sr.Close();
                foreach (DictionaryEntry content in contents)
                {
                    sb.Replace(content.Key.ToString(), content.Value.ToString());
                }

            }
            else
                throw new Exception(string.Format("�޷��ҵ�����Ϊ{0}���ʼ�ģ��", templateFile));
        }

        public static SmtpClient Smtp
        {
            get
            {
                SmtpClient smtp = new SmtpClient(Host);
                if (IsUserCredentials)
                {
                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = Credential;
                }
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                return smtp;
            }
        }

        public static bool SendMail(string receiver, string title, string content)
        {
            List<string> rcs = new List<string>();
            rcs.Add(receiver);
            return SendMail(rcs, title, content);
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="receivers">�ռ���</param>
        /// <param name="title">����/����</param>
        /// <param name="content">�ż�����</param>
        public static bool SendMail(List<string> receivers, string title, string content)
        {
            var smtp = Smtp;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(Account);
            msg.Subject = title;
            msg.Body = content;
            msg.BodyEncoding = Encoding.UTF8;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;            
            string to = TestAccount;            
            if (IsOnlyForTest)
            {
                for (int i = 0; i < receivers.Count; i++)
                {
                    msg.To.Add(new MailAddress(to));
                }
            }
            else
            {
                foreach (var x in receivers)
                {
                    msg.To.Add(new MailAddress(x));
                }
            }
            try
            {
                smtp.Send(msg);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
            finally
            {
                msg.Dispose();
            }
        }
    }
}
