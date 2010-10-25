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
    /// ͼƬ
    /// </summary>
    public enum PosterImageType
    {
        Origin = 1,
        /// <summary>
        /// ������500��������500
        /// </summary>
        Clip_500_500 = 31,
        /// <summary>
        /// ����100 x 140�ߴ�Ԥ��ͼ 
        /// </summary>
        Clip_100_140_M3 = 33,
        /// <summary>
        /// ӰƬ����ҳ
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
    /// ���ˮӡ��ʽ
    /// </summary>
    public enum WatermarkType
    {
        /// <summary>
        /// Сˮӡ
        /// </summary>
        Small,

        /// <summary>
        /// ��Сˮӡ
        /// </summary>
        Smaller,

        /// <summary>
        /// ��Сˮӡ
        /// </summary>
        ExtraSmall,

        /// <summary>
        /// ��׼ˮӡ
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
        Aries = 1,//������
        Taurus = 2,//��ţ��
        Gemini = 3,//˫����
        Cancer = 4,//��з��
        Leo = 5,//ʨ����
        Virgo = 6,//��Ů��
        Libra = 7,//�����
        Scorpio = 8,//��Ы��
        Sagittarius = 9,//������
        Capricorn = 10,//Ħ����
        Aquarius = 11,//ˮƿ��
        Pisces = 12,//˫����
    }

    public struct ConstellationShow
    {
        public const string Aries = "������";
        public const string Taurus = "��ţ��";
        public const string Gemini = "˫����";
        public const string Cancer = "��з��";
        public const string Leo = "ʨ����";
        public const string Virgo = "��Ů��";
        public const string Libra = "�����";
        public const string Scorpio = "��Ы��";
        public const string Sagittarius = "������";
        public const string Capricorn = "Ħ����";
        public const string Aquarius = "ˮƿ��";
        public const string Pisces = "˫����";

    }
    public enum SiteType
    {
        NotSet = -1,
        EJJJ = 0,

    }

}
