using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.AccessControl;
using System.Text;
using System.Web;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Configuration;

namespace Portal.Facilities.Helper
{

    public class FileUploadHelper
    {
        public const int size = 1024 * 64;
        //ͼƬ����վ��
        public static readonly string ImageServiceDomain = ConfigurationSettings.AppSettings["ImageServiceDomain"];
        //ˮӡͼƬ·��
        public static readonly string CopyrightImage = ConfigurationSettings.AppSettings["CopyrightImage"];
        //WebService��֤��
        public static readonly string ValidateCode = ConfigurationSettings.AppSettings["ValidateCode"];
        //��֤�ϴ��ļ���չ��
        public static string fileExt = ConfigurationSettings.AppSettings["fileExt"];
        //��֤ͼƬ�ϴ���չ��
        public static string imgExt = ConfigurationSettings.AppSettings["imgExt"];
        //ÿ���ϴ��ֽ���
        public static readonly int ByteNum = int.Parse(ConfigurationSettings.AppSettings["ByteNum"]);

        public static string UploadImg(HttpPostedFile file, string SavePath, string FileName, ref string Err)
        {
            Err = "";
            string ext = Path.GetExtension(file.FileName);
            if (!validateImgExt(ext))
            {
                Err = "��֧�ִ˸�ʽ�ϴ�!";
            }
            if (FileName == "")
            {
                Err = "δ�����ļ���";
            }
            if (SavePath == "")
            {
                //SavePath =CreatePath("upfile/", "YYYY-MM-DD");
                Err = "δ���ñ���·��";
            }
            if (Err != "")
            {
                return "";
            }
            //FileName = FileName + ext;
            System.IO.Stream stream = file.InputStream;
            byte[] buffer = new byte[size];
            int index = 0, count = size;

            if (stream.Length < size)
            {
                count = (int)stream.Length;
                buffer = new byte[stream.Length];
            }

            Copy.UploadFile.UploadFileSoapClient upservice = new Copy.UploadFile.UploadFileSoapClient();
            int iPosition = 0;
            int n = 0;
            while ((count = stream.Read(buffer, 0, count)) != 0)
            {
                upservice.SaveFileStream(buffer, iPosition + index, count, n, SavePath, FileName, ref Err, ValidateCode);
                if (Err != "")
                {
                    //Js.ResponseAlert(Err);
                    return "";
                }
                index += count;
                n += 1;
                if (count != size)
                {
                    break;
                }
            }
            stream.Close();
            stream.Dispose();
            return ImageServiceDomain + SavePath + FileName;
        }

        public static string UploadFile(HttpPostedFile file, string SavePath, string FileName, ref string Err)
        {
            string ext = Path.GetExtension(file.FileName);
            if (!validateFileExt(ext))
            {
                Err = "��֧�ִ˸�ʽ�ϴ�!";
            }
            System.IO.Stream stream = file.InputStream;
            if (FileName == "")
            {
                FileName = CreateFileName() + Path.GetExtension(file.FileName);
            }
            if (SavePath == "")
            {
                SavePath = CreatePath("upfile/", "YYYY-MM-DD");
            }
            byte[] buffer = new byte[size];
            int index = 0, count = size;

            if (stream.Length < size)
            {
                count = (int)stream.Length;
                buffer = new byte[stream.Length];
            }

            Copy.UploadFile.UploadFileSoapClient upservice = new Copy.UploadFile.UploadFileSoapClient();
            int iPosition = 0;
            int n = 0;
            while ((count = stream.Read(buffer, 0, count)) != 0)
            {
                upservice.SaveFileStream(buffer, iPosition + index, count, n, SavePath, FileName, ref Err, ValidateCode);
                if (Err != "")
                {
                    return "";
                }
                index += count;
                n += 1;
                if (count != size)
                {
                    break;
                }
            }
            stream.Close();
            stream.Dispose();
            return ImageServiceDomain + SavePath + FileName;
        }

        public static bool validateFileExt(string Ext)
        {

            bool Ret = false;
            if (fileExt.IndexOf(Ext.ToLower()) > 0)
            {
                Ret = true;
            }
            return Ret;
        }

        public static bool validateImgExt(string Ext)
        {

            bool Ret = false;

            if (imgExt.IndexOf(Ext.ToLower()) > 0)
            {
                Ret = true;
            }
            return Ret;
        }

        public static string CreatePath(string FolderPath, string PathMode)
        {
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            string strDay = DateTime.Now.Day.ToString();
            string path = FolderPath;
            switch (PathMode)
            {
                case "YYYY-MM-DD":
                    path += strYear + "/" + strMonth + "/" + strDay + "/";
                    break;
                case "YYYYMM-DD":
                    path += strYear + "-" + strMonth + "/" + strDay + "/";
                    break;
                case "YYYYMM":
                    path += strYear + "-" + strMonth + "/";
                    break;
                case "YYYYMMDD":
                    path += strYear + "-" + strMonth + "-" + strDay + "/";
                    break;
                default:
                    break;
            }
            return path;
        }


        public static string CreateFileName()
        {
            const int max = 9999;
            string strTemp, strYear, strMonth, strDay, strHour, strMinute, strSecond, strMillisecond;

            DateTime dt = DateTime.Now;

            strYear = dt.Year.ToString();
            strMonth = (dt.Month > 9) ? dt.Month.ToString() : "0" + dt.Month;
            strDay = (dt.Day > 9) ? dt.Day.ToString() : "0" + dt.Day;
            strHour = (dt.Hour > 9) ? dt.Hour.ToString() : "0" + dt.Hour;
            strMinute = (dt.Minute > 9) ? dt.Minute.ToString() : "0" + dt.Minute;
            strSecond = (dt.Second > 9) ? dt.Second.ToString() : "0" + dt.Second;
            strMillisecond = dt.Millisecond.ToString();

            strTemp = strYear + strMonth + strDay + strHour + strMinute + strSecond + strMillisecond + GetRandom(max);
            return strTemp;
        }


        private static string GetRandom(int max)
        {
            Random random = new Random();
            return (random.Next(max).ToString());
        }
    }
}
