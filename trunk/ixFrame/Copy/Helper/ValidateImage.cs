using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Drawing;
namespace Portal.Facilities
{
    public enum ValidateSaveMode
    {
        Session = 1,
        Cookie = 2
    }
    public class ValidateImage
    {
        #region Constructor
        /// <summary>
        /// Create Validate
        /// </summary>
        public ValidateImage()
        {
            Character charText = new Character();
            this.CreateCheckCodeImage(charText.GenerateCheckCode(5), "CheckCode");
        }

        /// <summary>
        /// Create input value to image
        /// </summary>
        /// <param name="checkCode">input value</param>
        public ValidateImage(string checkCode, string cookiesname)
        {
            this.CreateCheckCodeImage(checkCode, cookiesname);
        }

        /// <summary>
        ///  Create input value to image
        /// </summary>
        /// <param name="checkCode">input value</param>
        /// <param name="fettle">fettle value(1:no style)</param>
        public ValidateImage(string checkCode, int fettle, string cookiesname)
        {
            this.CreateCheckCodeImage(checkCode, fettle, cookiesname);
        }

        public ValidateImage(string checkCode, int fettle, bool OpenSave, string cookiesname)
        {
            this.CreateCheckCodeImage(checkCode, fettle, OpenSave, cookiesname);
        }

        public ValidateImage(string checkCode, int fettle, bool OpenSave, double PicLengthRate, string cookiesname)
        {
            this.CreateCheckCodeImage(checkCode, fettle, OpenSave, cookiesname);
        }

        public ValidateImage(string checkCode, int fettle, bool OpenSave, int SaveMode, string cookiesname)
        {
            this.CreateCheckCodeImage(checkCode, fettle, OpenSave, SaveMode, cookiesname);
        }


        public ValidateImage(string checkCode, int fettle, bool OpenSave, int SaveMode, double PicLengthRate, string cookiesname)
        {
            this.CreateCheckCodeImage(checkCode, fettle, OpenSave, SaveMode, PicLengthRate, cookiesname);
        }
        #endregion

        #region Create Check Code Image
        private void CreateCheckCodeImage(string checkCode, string cookiesname)
        {
            this.CreateCheckCodeImage(checkCode, 0, cookiesname);
        }

        private void CreateCheckCodeImage(string checkCode, int fettle, string cookiesname)
        {
            CreateCheckCodeImage(checkCode, fettle, true, cookiesname);
        }

        private void CreateCheckCodeImage(string checkCode, int fettle, bool OpenSave, string cookiesname)
        {
            CreateCheckCodeImage(checkCode, fettle, OpenSave, (int)ValidateSaveMode.Cookie, cookiesname);
        }


        private void CreateCheckCodeImage(string checkCode, int fettle, bool OpenSave, int SaveMode, string cookiesname)
        {
            CreateCheckCodeImage(checkCode, fettle, OpenSave, SaveMode, 12.5, cookiesname);
        }

        private void CreateCheckCodeImage(string checkCode, int fettle, bool OpenSave, int SaveMode, double PicLengthRate, string cookiesname)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            //验证码保存方式

            CodeSaveMode(checkCode, OpenSave, SaveMode, cookiesname);

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * PicLengthRate)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器

                Random random = new Random();

                //清空图片背景色

                g.Clear(Color.White);

                switch (fettle)
                {
                    case 0:
                        plotDefault(checkCode, image, g, random);
                        break;
                    case 1:
                        plotMethod1(checkCode, image, g, random);
                        break;
                    case 2:
                        plotMethod2(checkCode, image, g, random);
                        break;
                    case 3:
                        plotMethod3(checkCode, image, g, random);
                        break;
                    case 4:
                        plotMethod4(checkCode, image, g, random);
                        break;
                    default:
                        break;
                }

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Gif";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion

        #region 绘图方式，默认

        private static void plotDefault(string checkCode, System.Drawing.Bitmap image, Graphics g, Random random)
        {
            //画图片的背景噪音线

            for (int i = 0; i < 25; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);

            g.DrawString(checkCode, font, brush, 2, 2);

            //画图片的前景噪音点

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }

            //画图片的边框线

            g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
        }

        private static void plotMethod1(string checkCode, System.Drawing.Bitmap image, Graphics g, Random random)
        {
            //画图片的背景噪音线

            for (int i = 0; i < 15; i++)
            {
                int x1 = random.Next(image.Width);
                int x2 = random.Next(image.Width);
                int y1 = random.Next(image.Height);
                int y2 = random.Next(image.Height);

                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            //Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular));
            Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Black, 1.2f, true);

            g.DrawString(checkCode, font, brush, 2, 2);

            //画图片的前景噪音点

            for (int i = 0; i < 0; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }
        }

        private static void plotMethod2(string checkCode, System.Drawing.Bitmap image, Graphics g, Random random)
        {
            #region
            ////画图片的背景噪音线

            //for (int i = 0; i < BackgroundNoiseAmount; i++)
            //{
            //    int x1 = random.Next(image.Width);
            //    int x2 = random.Next(image.Width);
            //    int y1 = random.Next(image.Height);
            //    int y2 = random.Next(image.Height);

            //    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            //}


            //Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));

            //System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);

            //g.DrawString(checkCode, font, brush, 2, 2);

            ////画图片的前景噪音点

            //for (int i = 0; i < ForegroundNoiseAmount; i++)
            //{
            //    int x = random.Next(image.Width);
            //    int y = random.Next(image.Height);

            //    image.SetPixel(x, y, Color.FromArgb(random.Next()));
            //}

            ////画图片的边框线

            //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            #endregion
            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //定义字体            
            string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            //随机输出噪点
            for (int i = 0; i < 50; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //输出不同字体和颜色的验证码字符

            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = random.Next(7);
                int findex = random.Next(5);

                Font f = new System.Drawing.Font(font[findex], 10, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), f, b, 3 + (i * 12), ii);
            }
            //画一个边框

            g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);


        }

        private static void plotMethod3(string checkCode, System.Drawing.Bitmap image, Graphics g, Random random)
        {
            //清空图片背景色

            g.Clear(Color.White);

            //定义颜色
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            int[] fintSize = { 10, 11, 12, 13 };
            //定义字体            
            string[] Arrfont = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            ////随机输出噪点
            //for (int i = 0; i < 50; i++)
            //{
            //    int x = random.Next(image.Width);
            //    int y = random.Next(image.Height);
            //    g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            //}

            string[] arrBlack = { " ", "  ", "   " };


            int cindex = random.Next(7);
            int findex = random.Next(5);
            int fzindex = random.Next(4);
            int Blackindex = random.Next(3);
            int placeindex = random.Next(3);

            string TempCode = checkCode;

            switch (placeindex)
            {
                case 0:
                    break;
                case 1:
                    TempCode += arrBlack[Blackindex];
                    break;
                case 2:
                    TempCode = arrBlack[Blackindex] + TempCode;
                    break;
                default:
                    break;
            }
            checkCode = TempCode;

            Font font = new System.Drawing.Font(Arrfont[findex], fintSize[fzindex], (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Black, 1.2f, true);

            g.DrawString(checkCode, font, brush, 2, 2);


            //画图片的前景噪音点

            for (int i = 0; i < 50; i++)
            {
                int x = random.Next(image.Width);
                int y = random.Next(image.Height);

                image.SetPixel(x, y, Color.FromArgb(random.Next()));
            }


            ////画图片的边框线

            //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

            //画一个边框

            //g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);


        }

        private static void plotMethod4(string checkCode, System.Drawing.Bitmap image, Graphics g, Random random)
        {

            Font font = new System.Drawing.Font("Arial", 10, (System.Drawing.FontStyle.Regular));
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Black, 1.2f, true);

            g.DrawString(checkCode, font, brush, 2, 2);

        }

        #endregion

        #region 验证码保存方式

        private void CodeSaveMode(string checkCode, bool OpenSave, int SaveMode, string cookiesname)
        {
            if (OpenSave)
            {
                if (ValidateSaveMode.Cookie == (ValidateSaveMode)SaveMode)
                {
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(cookiesname, checkCode));
                    //HttpContext.Current.Response.Cookies["CheckCode"].Domain = ".cjcp.com.cn";
                    //HttpContext.Current.Response.Cookies["CheckCode"].Expires = DateTime.Now.AddMinutes(30);

                }
                else if (ValidateSaveMode.Session == (ValidateSaveMode)SaveMode)
                {
                    HttpContext.Current.Session[cookiesname] = checkCode;
                }
            }
        }
        #endregion

        #region 验证验证吗

        public static bool CheckCode(string Code)
        {
            if (string.IsNullOrEmpty(Code.Trim()))
            {
                return false;
            }
            string strCode = string.Empty;
            HttpCookie CodeCookie = HttpContext.Current.Response.Cookies["CheckCode"];
            if (CodeCookie.Value != null)
            {
                strCode = CodeCookie.Value.Trim();
            }
            if (Code.Trim() != strCode.Trim())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
