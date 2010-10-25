using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Facilities
{
    public enum FormMethod
    {
        GET,
        POST
    }

    /// <summary>
    /// 图片
    /// </summary>
    public enum PosterImageType
    {
        Origin = 1,
        /// <summary>
        /// 宽限制500，高限制500
        /// </summary>
        Clip_500_500 = 31,
        /// <summary>
        /// 保存100 x 140尺寸预览图 
        /// </summary>
        Clip_100_140_M3 = 33,
        /// <summary>
        /// 影片概览页
        /// </summary>
        W75H75 = 4,   // 75 * 75
        Clip_100_140_M2
    }

    public enum WatermarkPositionType
    {
        TopLeft,
        TopCenter,
        TopRight,
        Center,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    /// <summary>
    /// 添加水印方式
    /// </summary>
    public enum WatermarkType
    {
        /// <summary>
        /// 小水印
        /// </summary>
        Small,

        /// <summary>
        /// 较小水印
        /// </summary>
        Smaller,

        /// <summary>
        /// 极小水印
        /// </summary>
        ExtraSmall,

        /// <summary>
        /// 标准水印
        /// </summary>
        Standard,
    }

    public enum ImageClipType
    {
        Notset = -1,
        ScaleToFit = 0,
        FixWidthTrimHeight = 1,
        FixWidth = 2,
        FixWidthOrFixHeight = 3,
        FixWidthAndFixHeight = 4,
    }


    public enum Constellation
    {
        Aries = 1,//白羊座
        Taurus = 2,//金牛座
        Gemini = 3,//双子座
        Cancer = 4,//巨蟹座
        Leo = 5,//狮子座
        Virgo = 6,//处女座
        Libra = 7,//天秤座
        Scorpio = 8,//天蝎座
        Sagittarius = 9,//射手座
        Capricorn = 10,//摩羯座
        Aquarius = 11,//水瓶座
        Pisces = 12,//双鱼座
    }

    public struct ConstellationShow
    {
        public const string Aries = "白羊座";
        public const string Taurus = "金牛座";
        public const string Gemini = "双子座";
        public const string Cancer = "巨蟹座";
        public const string Leo = "狮子座";
        public const string Virgo = "处女座";
        public const string Libra = "天秤座";
        public const string Scorpio = "天蝎座";
        public const string Sagittarius = "射手座";
        public const string Capricorn = "摩羯座";
        public const string Aquarius = "水瓶座";
        public const string Pisces = "双鱼座";

    }
    public enum SiteType
    {
        NotSet = -1,
        EJJJ = 0,

    }

}
