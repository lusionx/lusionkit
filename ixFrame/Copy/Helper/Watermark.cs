using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Portal.Facilities;

namespace Portal.Facilities
{
	public static class Watermark
	{
		//待审核水印图片
		private const String WaitCheckWaterMark = @"\Images\e-jjj.com.std.png";
		//大图水印标记
		private const String EJJJRemark = @"\Images\e-jjj.com.ss.png";
		//小图水印标记
		private const String EJJJSmallRemark = @"\Images\e-jjj.com.ss.png";

		#region 加水印
		public static void AddWatermark( string fileName, string imageRuntimePath, Bitmap image, int type )
		{
			using ( Graphics g = Graphics.FromImage ( image ) )
			{
				switch ( ( PosterImageType ) type )
				{
					case PosterImageType.Origin:
						AddWatermarkImage ( fileName, imageRuntimePath, image, WatermarkType.Standard, g );
						break;
					case PosterImageType.Clip_100_140_M2:
						AddWatermarkImage ( fileName, imageRuntimePath, image, WatermarkType.Small, g );
						break;
				}
			}
		}
		#endregion


		#region 添加水印
		/// <summary>
		/// 添加水印
		/// </summary>
		/// <param name="type">添加水印类型 WatermarkType</param>
		/// <param name="g">Graphics 实例</param>
		/// <param name="x">位置x</param>
		/// <param name="y">位置y</param>
		public static void AddWatermarkImage( string fileName, string imageRuntimePath, Bitmap image, WatermarkType type, Graphics g )
		{
			string ApplicationPhyPath = HttpContext.Current.Request.ServerVariables ["APPL_PHYSICAL_PATH"].Substring ( HttpContext.Current.Request.ServerVariables ["APPL_PHYSICAL_PATH"].Length - 1, 1 ) != "\\" ? HttpContext.Current.Request.ServerVariables ["APPL_PHYSICAL_PATH"] : HttpContext.Current.Request.ServerVariables ["APPL_PHYSICAL_PATH"].Substring ( 0, HttpContext.Current.Request.ServerVariables ["APPL_PHYSICAL_PATH"].Length - 1 );

			string ImageFile = string.Empty;
			string ImageEJJJFile = string.Empty;
			float s = 0F;

			switch ( type )
			{
				case WatermarkType.Standard:	//? x ?
					ImageFile = ApplicationPhyPath + WaitCheckWaterMark;
					ImageEJJJFile = ApplicationPhyPath + EJJJRemark;
					s = 0.5F;
					break;
				case WatermarkType.Small:		//72 x 9
					ImageEJJJFile = ApplicationPhyPath + EJJJSmallRemark;
					s = 0.2F;
					break;
				case WatermarkType.Smaller:		//41 x 9
				case WatermarkType.ExtraSmall:	//? x ?
					break;
			}
			if ( type != WatermarkType.Small )
			{
				using ( Bitmap theIconBmp = new Bitmap ( System.Drawing.Image.FromFile ( ImageFile ) ) )
				{
					int x = ( image.Width - theIconBmp.Width ) / 2;
					int y = ( image.Height - theIconBmp.Height ) / 2;

					float [] [] ptsArray = { new float [] { 1, 0, 0, 0, 0 }, new float [] { 0, 1, 0, 0, 0 }, new float [] { 0, 0, 1, 0, 0 }, new float [] { 0, 0, 0, s, 0 }, new float [] { 0, 0, 0, 0, 1 } };
					ColorMatrix clrMatrix = new ColorMatrix ( ptsArray );
					ImageAttributes imgAttributes = new ImageAttributes ();
					imgAttributes.SetColorMatrix ( clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );

					//处理透明标记,暂时不用
					//theIconBmp.MakeTransparent(Color.FromArgb(255, 255, 0, 0));
					g.DrawImage ( theIconBmp, new Rectangle ( x, y, theIconBmp.Width, theIconBmp.Height ), 0, 0, theIconBmp.Width, theIconBmp.Height, GraphicsUnit.Pixel, imgAttributes );

				}
			}

			using ( Bitmap theDownIconBmp = new Bitmap ( System.Drawing.Image.FromFile ( ImageEJJJFile ) ) )
			{
				int x = image.Width - theDownIconBmp.Width;
				int y = image.Height - theDownIconBmp.Height;

				float [] [] ptsArray = { new float [] { 1, 0, 0, 0, 0 }, new float [] { 0, 1, 0, 0, 0 }, new float [] { 0, 0, 1, 0, 0 }, new float [] { 0, 0, 0, s, 0 }, new float [] { 0, 0, 0, 0, 1 } };
				ColorMatrix clrMatrix = new ColorMatrix ( ptsArray );
				ImageAttributes imgAttributes = new ImageAttributes ();
				imgAttributes.SetColorMatrix ( clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );

				//处理透明标记,暂时不用
				//theIconBmp.MakeTransparent(Color.FromArgb(255, 255, 0, 0));
				g.DrawImage ( theDownIconBmp, new Rectangle ( x, y, theDownIconBmp.Width, theDownIconBmp.Height ), 0, 0, theDownIconBmp.Width, theDownIconBmp.Height, GraphicsUnit.Pixel, imgAttributes );
			}

			#region 输出图片
			HttpContext.Current.Response.Clear ();
			HttpContext.Current.Response.ContentType = "image/jpeg";
			EncoderParameters myp = new EncoderParameters ( 1 );
			myp.Param [0] = new EncoderParameter ( Encoder.Quality, 90L );
			ImageCodecInfo [] encoders = ImageCodecInfo.GetImageEncoders ();
			image.Save ( HttpContext.Current.Response.OutputStream, encoders [1], myp );

			#endregion

			if ( !Directory.Exists ( imageRuntimePath ) )
				Directory.CreateDirectory ( imageRuntimePath );
			if ( type != WatermarkType.Small )
			{
				image.Save ( imageRuntimePath + fileName + ".jpg", ImageFormat.Jpeg );
			}
			else
			{
				image.Save ( imageRuntimePath + fileName + "_s.jpg", ImageFormat.Jpeg );
			}

			image.Dispose ();

			HttpContext.Current.Response.Clear ();

		}

		#endregion

	}
}
