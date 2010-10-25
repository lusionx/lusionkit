using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Facilities
{
    public class UploadImage
    {
        string path = "";
        private static Random rnd = new Random(); //获取一个随机数
        System.Drawing.Image.GetThumbnailImageAbort callb = null;
        System.Drawing.Image image, newimage;
        public UploadImage()
        {

            path = ConfigurationManager.AppSettings["LoadPath"];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="FileName">文件名</param>
        /// <returns></returns>
        public bool UploadFile(FileUpload file, string FileName, string OldName, out string fileext) //实现玩家文件上传功能的主函数
        {
            fileext = "";
            // string FileName = GetUniquelyString(); //获取一个不重复的文件名
            if (file.PostedFile.ContentLength <= 0)
            {
                return false;
            }
            string postFileName;
            string ImageContentType = file.PostedFile.ContentType.ToLower();//文件类型
            string FileType = "";
            if (ImageContentType.IndexOf("jpeg") > -1) FileType = "jpg";
            if (ImageContentType.IndexOf("gif") > -1) FileType = "gif";
            fileext = FileType;


            if (ImageContentType.IndexOf("jpeg") > -1 || ImageContentType.IndexOf("gif") > -1)
            {

                //try
                //{
                //Stream stream = file.PostedFile.InputStream;
                //System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                string newpath = HttpContext.Current.Server.MapPath("~/" + path + FileName + "." + FileType);

                //if (image.Width != 48 && image.Height != 48)
                //{
                //    string oldpath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + path + FileName + "1." + FileType;

                //    file.PostedFile.SaveAs(oldpath); //存储指定的文件到指定的目录
                //    ////为上传的图片建立引用
                //    //image = System.Drawing.Image.FromFile(oldpath);
                //    ////生成缩略图
                //    //newimage = image.GetThumbnailImage(48, 48, callb, new System.IntPtr());
                //    ////把缩略图保存到指定的虚拟路径
                //    //newimage.Save(HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + path + FileName + "." + FileType);
                //    ////释放image对象占用的资源
                //    //image.Dispose();
                //    ////释放newimage对象的资源
                //    //newimage.Dispose();
                //    //长和宽尺寸 
                //    //CommonUtility.ShowMessage(this.Page, "图片不符合规格");


                //    Thumbnail.MakeThumbnail(oldpath, newpath, 48, 48, Thumbnail.ThumbnailMode.HW);
                //}
                //else
                //{
                file.PostedFile.SaveAs(newpath); //存储指定的文件到指定的目录
                //}
                //stream.Close();
                //}
                //catch
                //{

                //    return false;
                //}
                try
                {
                    if (File.Exists(path + OldName))
                    {
                        File.Delete(path + OldName);
                    }
                }
                catch
                {

                    return false;
                }

            }
            return true;
        }

        #region 获取一个不重复的文件名
        /// <summary>
        /// 获取一个不重复的文件名
        /// </summary>
        /// <returns></returns>
        public static string GetUniquelyString() //获取一个不重复的文件名
        {
            const int RANDOM_MAX_VALUE = 1000;
            string strTemp, strYear, strMonth, strDay, strHour, strMinute, strSecond, strMillisecond;

            DateTime dt = DateTime.Now;
            int rndNumber = rnd.Next(RANDOM_MAX_VALUE);
            strYear = dt.Year.ToString();
            strMonth = (dt.Month > 9) ? dt.Month.ToString() : "0" + dt.Month.ToString();
            strDay = (dt.Day > 9) ? dt.Day.ToString() : "0" + dt.Day.ToString();
            strHour = (dt.Hour > 9) ? dt.Hour.ToString() : "0" + dt.Hour.ToString();
            strMinute = (dt.Minute > 9) ? dt.Minute.ToString() : "0" + dt.Minute.ToString();
            strSecond = (dt.Second > 9) ? dt.Second.ToString() : "0" + dt.Second.ToString();
            strMillisecond = dt.Millisecond.ToString();

            strTemp = strYear + strMonth + strDay + "_" + strHour + strMinute + strSecond + "_" + strMillisecond + "_" + rndNumber.ToString();

            return strTemp;
        }
        #endregion
        public class Thumbnail
        {
            ///<summary>
            ///生成缩略图
            ///</summary>
            ///<param name="originalImagePath">源图路径（物理路径）</param>
            ///<param name="thumbnailPath">缩略图路径（物理路径）</param>
            ///<param name="width">缩略图宽度</param>
            ///<param name="height">缩略图高度</param>
            ///<param name="mode">生成缩略图的方式</param>    
            public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ThumbnailMode mode)
            {
                System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

                int towidth = width;
                int toheight = height;

                int x = 0;
                int y = 0;
                int ow = originalImage.Width;
                int oh = originalImage.Height;

                switch (mode)
                {
                    case ThumbnailMode.HW://指定高宽缩放（可能变形）                
                        break;
                    case ThumbnailMode.W://指定宽，高按比例                    
                        toheight = originalImage.Height * width / originalImage.Width;
                        break;
                    case ThumbnailMode.H://指定高，宽按比例
                        towidth = originalImage.Width * height / originalImage.Height;
                        break;
                    case ThumbnailMode.Cut://指定高宽裁减（不变形）                
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        break;
                    default:
                        break;
                }

                //新建一个bmp图片
                System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

                //新建一个画板
                Graphics g = System.Drawing.Graphics.FromImage(bitmap);

                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                g.Clear(Color.Transparent);

                //在指定位置并且按指定大小绘制原图片的指定部分
                g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                    new Rectangle(x, y, ow, oh),
                    GraphicsUnit.Pixel);

                try
                {
                    //以jpg格式保存缩略图
                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }
            }

            /// <summary>
            /// 缩略图格式
            /// </summary>
            public enum ThumbnailMode
            {
                /// <summary>
                /// 指定高宽缩放（可能变形）。 
                /// </summary>
                HW,
                /// <summary>
                /// 指定宽，高按比例。 
                /// </summary>
                W,
                /// <summary>
                /// 指定高，宽按比例。 
                /// </summary>
                H,
                /// <summary>
                /// 指定高宽裁减（不变形）。 
                /// </summary>
                Cut
            }
        }
    }
}
