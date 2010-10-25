using System;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Portal.Facilities;

namespace Portal.Facilities.Helper
{
	public class ImageHelper
	{
		#region public static Bitmap Assert( Bitmap image )
		/// <summary>
		/// avoid Exception:image has an indexed pixel format or its format is undefined.
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static Bitmap Assert( Bitmap image )
		{
			Graphics graphics = null;
			try
			{
				graphics = Graphics.FromImage ( image );
			}
			catch
			{
				Bitmap indexedImage = image.Clone () as Bitmap;
				image.Dispose ();
				if ( indexedImage != null )
				{
					image = GetThumbnailImage ( indexedImage, indexedImage.Width, indexedImage.Height );
					indexedImage.Dispose ();
				}
				else
				{
					//�����ϲ��ᷢ��
					image = new Bitmap ( image.Width, image.Height );
				}
			}
			finally
			{
				if ( graphics != null )
					graphics.Dispose ();
			}
			return image;
		}

		#endregion

		#region ThumbnailImage

		/// <summary>
		/// ��ȡʡ��ͼ�����Bitmap.GetThumbnailImage��
		/// </summary>
		/// <param name="sourceImage"></param>
		/// <param name="targetSize"></param>
		/// <returns></returns>
		public static Bitmap GetThumbnailImage( Bitmap sourceImage, Size targetSize )
		{
			return GetThumbnailImage ( sourceImage, targetSize.Width, targetSize.Height );
		}

		/// <summary>
		///  ��ȡʡ��ͼ�����Bitmap.GetThumbnailImage��
		/// </summary>
		/// <param name="sourceImage"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static Bitmap GetThumbnailImage( Bitmap sourceImage, int width, int height )
		{
			return GetThumbnailImage ( sourceImage, width, height, ImageClipType.ScaleToFit );
		}

		public static Bitmap GetThumbnailImage( Bitmap sourceImage, int width, int height, ImageClipType clipType )
		{
			Rectangle srcRectangle;
			Rectangle destRectangle;
			switch ( clipType )
			{
				case ImageClipType.Notset:
					srcRectangle = destRectangle = new Rectangle ( 0, 0, sourceImage.Width, sourceImage.Height );
					break;
				case ImageClipType.ScaleToFit:
					ScaleToFit ( sourceImage.Size, width, height, out srcRectangle, out destRectangle );
					break;
				case ImageClipType.FixWidthTrimHeight:
					FixWidthTrimHeight ( sourceImage.Size, width, height, out srcRectangle, out destRectangle );
					break;
				case ImageClipType.FixWidth:
					FixWidth ( sourceImage.Size, width, height, out srcRectangle, out destRectangle );
					break;
				case ImageClipType.FixWidthOrFixHeight:
					FixWidthOrFixHeight ( sourceImage.Size, width, height, out srcRectangle, out destRectangle );
					break;
				case ImageClipType.FixWidthAndFixHeight:
					FixWidthAndFixHeight ( sourceImage.Size, width, height, out srcRectangle, out destRectangle );
					break;
				default:
					throw new ArgumentOutOfRangeException ( "clipType" );
			}
			//TODO:����
			if ( sourceImage.Size != srcRectangle.Size )
			{
				Bitmap srcBitmap = new Bitmap ( srcRectangle.Width, srcRectangle.Height );
				using ( Graphics g = Graphics.FromImage ( srcBitmap ) )
				{
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.CompositingQuality = CompositingQuality.HighQuality;
					//g.InterpolationMode = InterpolationMode.High;
					//g.SmoothingMode = SmoothingMode.HighQuality;
					//g.Clear ( Color.White );
					g.DrawImage( sourceImage, 0, 0, srcRectangle.Width, srcRectangle.Height );
				}
				Bitmap destBitmap = new Bitmap ( destRectangle.Width, destRectangle.Height );
				using ( Graphics g = Graphics.FromImage ( destBitmap ) )
				{
					//g.InterpolationMode = InterpolationMode.High;
					//g.SmoothingMode = SmoothingMode.HighQuality;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.CompositingQuality = CompositingQuality.HighQuality;

					//g.Clear ( Color.White );
					g.DrawImage ( srcBitmap, destRectangle, new Rectangle ( srcRectangle.X, srcRectangle.Y, destRectangle.Width, destRectangle.Height ), GraphicsUnit.Pixel );
				}
				srcBitmap.Dispose ();
				return destBitmap;
			}
			else
			{
				Bitmap destBitmap = new Bitmap ( destRectangle.Width, destRectangle.Height );
				using ( Graphics g = Graphics.FromImage ( destBitmap ) )
				{
					//g.CompositingQuality = CompositingQuality.AssumeLinear;
					//g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					//g.SmoothingMode = SmoothingMode.HighQuality;

					g.InterpolationMode = InterpolationMode.HighQualityBicubic;
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.PixelOffsetMode = PixelOffsetMode.HighQuality;
					g.CompositingQuality = CompositingQuality.HighQuality;
					
					//g.Clear ( Color.White );
					g.DrawImage ( sourceImage, new Rectangle ( 0, 0, destRectangle.Width, destRectangle.Height ), srcRectangle, GraphicsUnit.Pixel );
				}
				return destBitmap;
			}
		}

		#region private static void ScaleToFit( Size srcImageSize, int destWidth, int destHeight, out Rectangle srcRectangle, out Rectangle destRectangle )
		/// <summary>
		/// �ȱ�����
		/// </summary>
		/// <param name="srcImageSize"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <param name="srcRectangle"></param>
		/// <param name="destRectangle"></param>
		private static void ScaleToFit( Size srcImageSize, int destWidth, int destHeight, out Rectangle srcRectangle, out Rectangle destRectangle )
		{
			if ( srcImageSize.Width > destWidth ||
				 srcImageSize.Height > destHeight )
			{
				int sourceWidth = srcImageSize.Width;
				int sourceHeight = srcImageSize.Height;
				int destX = 0;
				int destY = 0;
				float nPercent = 0;
				float nPercentW = ( ( float ) destWidth / ( float ) sourceWidth );
				float nPercentH = ( ( float ) destHeight / ( float ) sourceHeight );
				if ( nPercentH < nPercentW )
				{
					nPercent = nPercentH;
					destX = System.Convert.ToInt16 ( ( destWidth - ( sourceWidth * nPercent ) ) / 2 );
				}
				else
				{
					nPercent = nPercentW;
					destY = System.Convert.ToInt16 ( ( destHeight - ( sourceHeight * nPercent ) ) / 2 );
				}
				destWidth = ( int ) ( sourceWidth * nPercent );
				destHeight = ( int ) ( sourceHeight * nPercent );
			}
			srcRectangle = new Rectangle ( 0, 0, srcImageSize.Width, srcImageSize.Height );
			destRectangle = new Rectangle ( 0, 0, destWidth <= 0 ? 1 : destWidth, destHeight <= 0 ? 1 : destHeight );
		}

		#endregion

		#region �̶���ظ�
		/// <summary>
		/// �̶���ظ�
		/// ���ԡ�����ҳ����ͼ��������150��������200��Ϊ��
		/// ��>150����>200���������150���ߵȱ����������ź�����߳���200����߽�ȡ0-200��λ��
		/// ��>150����<200���������150���ߵȱ�����������
		/// ��<150����>200��Ӧ��û�д���ͼƬ
		/// ��<150����<200��Ӧ��û�д���ͼƬ
		/// </summary>
		/// <param name="srcImageSize"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <param name="srcRectangle"></param>
		/// <param name="destRectangle"></param>
		/// <returns></returns>
		private static void FixWidthTrimHeight( Size srcImageSize, int destWidth, int destHeight, out Rectangle srcRectangle, out Rectangle destRectangle )
		{
			int srcX = 0, srcY = 0;
			int srcWidth = srcImageSize.Width;
			int srcHeight = srcImageSize.Height;
			int width = srcWidth;
			int height = srcHeight;
			if ( srcWidth <= destWidth && srcHeight <= destHeight )
			{
				//Ӱ�˸���ͼ��������
				srcRectangle = new Rectangle ( srcX, srcY, srcWidth, srcHeight );
				destRectangle = new Rectangle ( 0, 0, width, height );
			}
			else
			{
				//if ( srcWidth > destWidth )
				//{
				width = destWidth;
				height = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
				if ( height > destHeight )
				{
					//srcY = ( height - destHeight ) / 2;
					srcWidth = destWidth;
					srcHeight = height;
					height = destHeight;
				}
				//}
				//else
				//{
				//    width = destWidth;
				//    height = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
				//    //srcWidth = width;
				//    //srcHeight = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
				//}
				srcRectangle = new Rectangle ( srcX, srcY, srcWidth, srcHeight );
				destRectangle = new Rectangle ( 0, 0, width, height );
			}
		}

		#endregion

		#region ����ȱ����ţ������Ƹ�
		/// <summary>
		/// ����ȱ����ţ������Ƹ�
		/// </summary>
		/// <param name="srcImageSize"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <param name="srcRectangle"></param>
		/// <param name="destRectangle"></param>
		/// <returns></returns>
		private static void FixWidth( Size srcImageSize, int destWidth, int destHeight, out Rectangle srcRectangle, out Rectangle destRectangle )
		{
			int srcX = 0, srcY = 0;
			int srcWidth = srcImageSize.Width;
			int srcHeight = srcImageSize.Height;
			int width = srcWidth;
			int height = srcHeight;
			if ( srcWidth > destWidth )
			{
				width = destWidth;
				height = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
			}
			srcRectangle = new Rectangle ( srcX, srcY, srcWidth, srcHeight );
			destRectangle = new Rectangle ( 0, 0, width, height );
		}

		#endregion

		#region �̶�����߹̶���
		/// <summary>
		/// �̶�����߹̶���
		/// ��>=100����>=140����>=�ߣ��������100���ߵȱ�����������
		/// ��>=100����>=140����<=�ߣ����ߴ����140����ȱ�����������
		/// ��>=100����<=140���������100���ߵȱ�����������
		/// ��<=100����>=140�����ߴ����140����ȱ�����������
		/// ��<=100����<=140��Ӧ��û�д���ͼƬ
		/// </summary>
		/// <param name="srcImageSize"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <param name="srcRectangle"></param>
		/// <param name="destRectangle"></param>
		/// <returns></returns>
		private static void FixWidthOrFixHeight( Size srcImageSize, int destWidth, int destHeight, out Rectangle srcRectangle, out Rectangle destRectangle )
		{
			int srcX = 0, srcY = 0;
			int srcWidth = srcImageSize.Width;
			int srcHeight = srcImageSize.Height;
			int width = srcWidth;
			int height = srcHeight;
			if ( srcWidth >= destWidth && srcHeight >= destHeight )
			{
				//��>100����>140����/��>100/140���������100���ߵȱ�����������
				//��>100����>140����/��<100/140�����ߴ����140����ȱ�����������
				float srcPercent = ( ( float ) srcWidth / ( float ) srcHeight );
				float destPercent = ( ( float ) destWidth / ( float ) destHeight );
				if ( srcPercent >= destPercent )
				{
					width = destWidth;
					height = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
				}
				else
				{
					height = destHeight;
					width = Convert.ToInt32 ( ( float ) srcWidth / ( float ) srcHeight * destHeight );
				}
			}
			else if ( srcWidth >= destWidth && srcHeight <= destHeight )
			{
				width = destWidth;
				height = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
			}
			else if ( srcWidth <= destWidth && srcHeight >= destHeight )
			{
				height = destHeight;
				width = Convert.ToInt32 ( ( float ) srcWidth / ( float ) srcHeight * destHeight );
			}
			srcRectangle = new Rectangle ( srcX, srcY, srcWidth, srcHeight );
			destRectangle = new Rectangle ( 0, 0, width, height );
		}

		#endregion

		#region �̶���͸ߣ����С����Ŵ󣬿��ȡ�м�������
		/// <summary>
		/// �̶���͸ߣ����С����Ŵ󣬿��ȡ�м�������
		/// </summary>
		/// <param name="srcImageSize"></param>
		/// <param name="destWidth"></param>
		/// <param name="destHeight"></param>
		/// <param name="srcRectangle"></param>
		/// <param name="destRectangle"></param>
		/// <returns></returns>
		private static void FixWidthAndFixHeight( Size srcImageSize, int destWidth, int destHeight, out Rectangle srcRectangle, out Rectangle destRectangle )
		{
			int srcX = 0, srcY = 0;
			int destX = 0, destY = 0;
			int srcWidth = srcImageSize.Width;
			int srcHeight = srcImageSize.Height;
			int width = srcWidth;
			int height = srcHeight;
			float srcPercent = ( ( float ) srcWidth / ( float ) srcHeight );
			float destPercent = ( ( float ) destWidth / ( float ) destHeight );
			//���Դ�����Ŀ�Ŀ�Դ�ߴ���Ŀ�ĸ�
			if ( srcWidth > destWidth && srcHeight > destHeight )
			{
				//if ( srcWidth > srcHeight )
				if ( srcPercent > destPercent )
				{
					height = destHeight;
					width = Convert.ToInt32 ( ( float ) srcWidth / ( float ) srcHeight * destHeight );
					if ( width > destWidth )
					{
						srcX = ( width - destWidth ) / 2;
						srcWidth = width;
						srcHeight = destHeight;
						width = destWidth;
					}
				}
				else
				{
					width = destWidth;
					height = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
					if ( height > destHeight )
					{
						//srcY = ( height - destHeight ) / 2;
						srcWidth = destWidth;
						srcHeight = height;
						height = destHeight;
					}
				}
			}
			//���Դ�����Ŀ�Ŀ�Դ��С��Ŀ�ĸ�
			else if ( srcWidth > destWidth && srcHeight < destHeight )
			{
				width = destWidth;
				height = destHeight;
				//srcX = ( Convert.ToInt32 ( ( float ) srcWidth / ( float ) srcHeight * destHeight ) - srcWidth ) / 2;
				int _srcWidth = Convert.ToInt32 ( ( float ) srcWidth / ( float ) srcHeight * destHeight );
				srcX = ( _srcWidth - width ) / 2;
				srcWidth = _srcWidth;
				srcHeight = destHeight;
			}
			//���Դ��С��Ŀ�Ŀ�Դ�ߴ���Ŀ�ĸ�
			else if ( srcWidth < destWidth && srcHeight > destHeight )
			{
				width = destWidth;
				height = destHeight;
				//srcY = ( Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth ) - srcHeight ) / 2;
				int _srcHeight = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
				//srcY = ( _srcHeight - height ) / 2;
				srcWidth = destWidth;
				srcHeight = _srcHeight;
			}
			//���Դ��С��Ŀ�Ŀ�Դ��С��Ŀ�ĸ�
			else
			{
				//if ( srcWidth > srcHeight )
				if ( srcPercent > destPercent )
				{
					height = destHeight;
					width = Convert.ToInt32 ( ( float ) srcWidth / ( float ) srcHeight * destHeight );
					if ( width > destWidth )
					{
						srcX = ( width - destWidth ) / 2;
						srcWidth = width;
						srcHeight = destHeight;
						width = destWidth;
					}
				}
				else
				{
					width = destWidth;
					height = Convert.ToInt32 ( ( float ) srcHeight / ( float ) srcWidth * destWidth );
					if ( height > destHeight )
					{
						//srcY = ( height - destHeight ) / 2;
						srcWidth = destWidth;
						srcHeight = height;
						height = destHeight;
					}
				}
			}
			srcRectangle = new Rectangle ( srcX, srcY, srcWidth, srcHeight );
			destRectangle = new Rectangle ( destX, destY, width, height );

		}

		#endregion

		#endregion

		#region public static Bitmap AddWatermark( Bitmap srcBitmap, string watermarkPath )



		public static Bitmap AddWatermark( Bitmap srcBitmap, string watermarkPath )
		{
			return AddWatermark ( srcBitmap, watermarkPath, WatermarkPositionType.TopLeft );
		}

		public static Bitmap AddWatermark( Bitmap srcBitmap, string watermarkPath, WatermarkPositionType positionType )
		{
			using ( Bitmap watermarkBitmap = new Bitmap ( System.Drawing.Image.FromFile ( watermarkPath ) ) )
			{
				int posX = 0, posY = 0;
				int offsetX = 5, offsetY = 5;
				int srcWidth = srcBitmap.Width;
				int srcHeight = srcBitmap.Height;
				int watermarkWidth = watermarkBitmap.Width;
				int watermarkHeight = watermarkBitmap.Height;
				switch ( positionType )
				{
					case WatermarkPositionType.TopLeft:
						posX = offsetX;
						posY = offsetY;
						break;
					case WatermarkPositionType.TopCenter:
						posX = srcWidth / 2 - watermarkWidth / 2;
						posY = offsetY;
						break;
					case WatermarkPositionType.TopRight:
						posX = srcWidth - watermarkWidth - offsetX;
						posY = offsetY;
						break;
					case WatermarkPositionType.Center:
						posX = srcWidth / 2 - watermarkWidth / 2;
						posY = srcHeight / 2 - watermarkHeight / 2;
						break;
					case WatermarkPositionType.BottomLeft:
						posX = offsetX;
						posY = srcHeight - watermarkHeight - offsetY;
						break;
					case WatermarkPositionType.BottomCenter:
						posX = srcWidth / 2 - watermarkWidth / 2;
						posY = srcHeight - watermarkHeight - offsetY;
						break;
					case WatermarkPositionType.BottomRight:
						posX = srcWidth - watermarkWidth - offsetX;
						posY = srcHeight - watermarkHeight - offsetY;
						break;
					default:
						throw new ArgumentOutOfRangeException ( "positionType" );
				}
				return AddWatermark ( srcBitmap, posX, posY, watermarkBitmap, 0.2f );
			}
		}

		public static Bitmap AddWatermark( Bitmap srcBitmap, int posX, int posY, Bitmap watermarkBitmap, float transparency )
		{
			Bitmap destBitmap = new Bitmap ( srcBitmap.Width, srcBitmap.Height );
			using ( Graphics g = Graphics.FromImage ( destBitmap ) )
			{
				//��ԭͼ
				g.InterpolationMode = InterpolationMode.High;
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.DrawImage ( srcBitmap, 0, 0, srcBitmap.Width, srcBitmap.Height );
				//��ˮӡͼ
				float [] [] ptsArray = {
				                     	new float[] {1, 0, 0, 0, 0}, new float[] {0, 1, 0, 0, 0}, new float[] {0, 0, 1, 0, 0},
				                     	new float[] {0, 0, 0, transparency, 0}, new float[] {0, 0, 0, 0, 1}
				                     };
				ColorMatrix clrMatrix = new ColorMatrix ( ptsArray );
				ImageAttributes imgAttributes = new ImageAttributes ();
				imgAttributes.SetColorMatrix ( clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );
				g.DrawImage ( watermarkBitmap, new Rectangle ( posX, posY, watermarkBitmap.Width, watermarkBitmap.Height ), 0, 0,
							 watermarkBitmap.Width, watermarkBitmap.Height, GraphicsUnit.Pixel, imgAttributes );
			}
			return destBitmap;
		}

		//public static Bitmap AddWatermark( Bitmap srcBitmap, int posX, int posY, string watermarkPath, float transparency )
		//{
		//    Bitmap destBitmap = new Bitmap ( srcBitmap.Width, srcBitmap.Height );
		//    using ( Graphics g = Graphics.FromImage ( destBitmap ) )
		//    {
		//        //��ԭͼ
		//        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
		//        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
		//        g.DrawImage ( srcBitmap, 0, 0, srcBitmap.Width, srcBitmap.Height );
		//        //��ˮӡͼ
		//        using ( Bitmap watermarkBitmap = new Bitmap ( System.Drawing.Image.FromFile ( watermarkPath ) ) )
		//        {
		//            float [] [] ptsArray = { new float [] { 1, 0, 0, 0, 0 }, new float [] { 0, 1, 0, 0, 0 }, new float [] { 0, 0, 1, 0, 0 }, new float [] { 0, 0, 0, transparency, 0 }, new float [] { 0, 0, 0, 0, 1 } };
		//            ColorMatrix clrMatrix = new ColorMatrix ( ptsArray );
		//            ImageAttributes imgAttributes = new ImageAttributes ();
		//            imgAttributes.SetColorMatrix ( clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap );
		//            g.DrawImage ( watermarkBitmap, new Rectangle ( posX, posY, watermarkBitmap.Width, watermarkBitmap.Height ), 0, 0, watermarkBitmap.Width, watermarkBitmap.Height, GraphicsUnit.Pixel, imgAttributes );
		//        }
		//    }
		//    return destBitmap;
		//} 

		#endregion

        public static bool IsAllowedExtension(HttpPostedFile postFile)
        {
            string fileclass = string.Empty;
            //byte buffer;
            //buffer = postFile.InputStream.ReadByte();
            fileclass = SafeConvert.ToString(postFile.InputStream.ReadByte());
            //buffer = postFile.InputStream.ReadByte();
            fileclass += SafeConvert.ToString(postFile.InputStream.ReadByte());
            postFile.InputStream.Position = 0;
            if (fileclass == "255216" || fileclass == "7173" || fileclass == "6677" || fileclass == "13780") //˵��255216��jpg;7173��gif;6677��BMP,13780��PNG;7790��exe,8297��rar
            {
                return true;
            }
            else
            {
                return false;
            }
        }  
	}

    //public unsafe class FastBitmap
    //{

    //    public struct PixelData
    //    {
    //        public byte blue;
    //        public byte green;
    //        public byte red;
    //    }

    //    Bitmap Subject;
    //    int SubjectWidth;
    //    BitmapData bitmapData = null;
    //    Byte* pBase = null;

    //    public FastBitmap( Bitmap SubjectBitmap )
    //    {
    //        this.Subject = SubjectBitmap;
    //        try
    //        {
    //            LockBitmap ();
    //        }
    //        catch ( Exception ex )
    //        {
    //            throw ex;
    //        }

    //    }

    //    public void Release()
    //    {
    //        try
    //        {
    //            UnlockBitmap ();
    //        }
    //        catch ( Exception ex )
    //        {
    //            throw ex;
    //        }
    //    }

    //    public Bitmap Bitmap
    //    {
    //        get
    //        {
    //            return Subject;
    //        }
    //    }

    //    public void SetPixel( int X, int Y, Color Colour )
    //    {
    //        try
    //        {
    //            PixelData* p = PixelAt ( X, Y );
    //            p->red = Colour.R;
    //            p->green = Colour.G;
    //            p->blue = Colour.B;
    //        }
    //        catch ( AccessViolationException ave )
    //        {
    //            throw ( ave );
    //        }
    //        catch ( Exception ex )
    //        {
    //            throw ex;
    //        }
    //    }

    //    public Color GetPixel( int X, int Y )
    //    {
    //        try
    //        {
    //            PixelData* p = PixelAt ( X, Y );
    //            return Color.FromArgb ( ( int ) p->red, ( int ) p->green, ( int ) p->blue );
    //        }
    //        catch ( AccessViolationException ave )
    //        {
    //            throw ( ave );
    //        }
    //        catch ( Exception ex )
    //        {
    //            throw ex;
    //        }
    //    }

    //    private void LockBitmap()
    //    {
    //        GraphicsUnit unit = GraphicsUnit.Pixel;
    //        RectangleF boundsF = Subject.GetBounds ( ref unit );
    //        Rectangle bounds = new Rectangle ( ( int ) boundsF.X,
    //            ( int ) boundsF.Y,
    //            ( int ) boundsF.Width,
    //            ( int ) boundsF.Height );

    //        SubjectWidth = ( int ) boundsF.Width * sizeof ( PixelData );
    //        if ( SubjectWidth % 4 != 0 )
    //        {
    //            SubjectWidth = 4 * ( SubjectWidth / 4 + 1 );
    //        }

    //        bitmapData = Subject.LockBits ( bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb );
    //        pBase = ( Byte* ) bitmapData.Scan0.ToPointer ();
    //    }

    //    private PixelData* PixelAt( int x, int y )
    //    {
    //        return ( PixelData* ) ( pBase + y * SubjectWidth + x * sizeof ( PixelData ) );
    //    }

    //    private void UnlockBitmap()
    //    {
    //        Subject.UnlockBits ( bitmapData );
    //        bitmapData = null;
    //        pBase = null;
    //    }
    //}

}
