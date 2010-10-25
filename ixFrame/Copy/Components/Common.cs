using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Globalization;
using System.Web;
using Portal.Facilities.Helper;
using Portal.Facilities;

namespace Portal.Facilities
{

    /// <summary>
    /// ���÷�����
    /// </summary>
    public class CommonUtility
    {
        #region ȡ��Ӧ�ó���URL,������β��"/"
        public static string ApplicationUrl
        {
            get
            {
                Uri uri = HttpContext.Current.Request.Url;
                string applicationPath = ApplicationPath;
                string url = "http://" + uri.Host;
                if (uri.Port != 80)
                {
                    url += ":" + uri.Port.ToString();
                }
                return url + applicationPath;

            }
        }
        #endregion

        #region ȡ������Ŀ¼
        static public string ApplicationPath
        {
            get
            {
                string applicationPath = HttpContext.Current.Request.ApplicationPath;
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                else
                {
                    return applicationPath;
                }
            }

        }
        #endregion

        #region ȡ�õ�ǰ����ʱ��
        /// <summary>
        /// ȡ�õ�ǰ����ʱ��
        /// ������ʾ��ǰ��¼��Ա�ı���ʱ��
        /// </summary>
        /// <param name="utc">ʱ��</param>
        /// <returns></returns>
        public static DateTime GetLocalTime(double utc)
        {
            return DateTime.Now.AddHours(utc - 8);
        }
        #endregion

        #region ȡ��ָ���ı���ʱ��
        /// <summary>
        /// ȡ��ָ���ı���ʱ��
        /// ������ʾָ���ı���ʱ��
        /// </summary>
        /// <param name="utc">ʱ��</param>
        /// <param name="theDateTime">ָ��������ʱ��</param>
        /// <returns></returns>
        public static DateTime GetLocalTime(double utc, DateTime theDateTime)
        {
            return theDateTime.AddHours(utc - 8);
        }
        #endregion

        #region ȡ��UTC����
        /// <summary>
        /// ȡ��UTC����
        /// </summary>
        /// <param name="utc">ʱ��</param>
        /// <returns></returns>
        public static string GetUTCDescription(double utc)
        {
            if (utc == -12)
                return ("UTC-12");
            if (utc == -11)
                return ("UTC-11");
            if (utc == -10)
                return ("UTC-10");
            if (utc == -9.5)
                return ("UTC-9:30");
            if (utc == -9)
                return ("UTC-9");
            if (utc == -8)
                return ("UTC-8");
            if (utc == -7)
                return ("UTC-7");
            if (utc == -6)
                return ("UTC-6");
            if (utc == -5)
                return ("UTC-5");
            if (utc == -4)
                return ("UTC-4");
            if (utc == -3.5)
                return ("UTC-3:30");
            if (utc == -3)
                return ("UTC-3");
            if (utc == -2)
                return ("UTC-2");
            if (utc == -1)
                return ("UTC-1");
            if (utc == 0)
                return ("UTC");
            if (utc == 1)
                return ("UTC+1");
            if (utc == 2)
                return ("UTC+2");
            if (utc == 3)
                return ("UTC+3");
            if (utc == 3.5)
                return ("UTC+3:30");
            if (utc == 4)
                return ("UTC-4");
            if (utc == 4.5)
                return ("UTC+4:30");
            if (utc == 5)
                return ("UTC+5");
            if (utc == 5.5)
                return ("UTC+5:30");
            if (utc == 5.75)
                return ("UTC+5:45");
            if (utc == 6)
                return ("UTC+6");
            if (utc == 6.5)
                return ("UTC+6:30");
            if (utc == 7)
                return ("UTC+7");
            if (utc == 8)
                return ("UTC+8");
            if (utc == 9)
                return ("UTC+9");
            if (utc == 9.5)
                return ("UTC+9:30");
            if (utc == 10)
                return ("UTC+10");
            if (utc == 10.5)
                return ("UTC+10:30");
            if (utc == 11)
                return ("UTC+11");
            if (utc == 11.5)
                return ("UTC+11:3");
            if (utc == 12)
                return ("UTC+12");
            if (utc == 12.75)
                return ("UTC+12:45");
            if (utc == 13)
                return ("UTC+13");
            if (utc == 14)
                return ("UTC+14");

            return string.Empty;
        }
        #endregion

        #region �����������ʽ
        /// <summary>
        /// �����������ʽ
        /// </summary>
        /// <param name="obj">�����</param>
        public static void SetInputBehavior(System.Web.UI.WebControls.WebControl obj)
        {
            obj.CssClass = "box";
            obj.Attributes.Add("onfocus", "this.className='line';");
            obj.Attributes.Add("onblur", "this.className='box';");
        }
        #endregion

        #region ���ַ����еķ�б�ˣ�\��ת��Ϊб�ܣ�/��
        /// <summary>
        /// ���ַ����еķ�б�ˣ�\��ת��Ϊб�ܣ�/��
        /// </summary>
        /// <param name="src">��ת�����ַ���</param>
        /// <returns>ת������ַ���</returns>
        public static string BackSlashToSlash(string src)
        {
            return src.Replace('\\', '/');
        }
        #endregion

        #region ���ɵ�����ҳ

        /// <summary>
        /// ����ҳ����
        /// </summary>
        /// <param name="RecordCount">�ܼ�¼��</param>
        /// <param name="PageIndex">ҳ����</param>
        /// <param name="pageSize">ҳ�ߴ�</param>
        /// <returns></returns>
        public string BuildPageNavigation(int RecordCount, int PageIndex, int pageSize)
        {

            int startAt = pageSize * (PageIndex - 1) + 1;
            int endAt = RecordCount < startAt + pageSize - 1 ? RecordCount : startAt + pageSize - 1;

            int prevPage = GetPrevPageIndex(RecordCount, startAt, pageSize);
            int nextPage = GetNextPageIndex(RecordCount, startAt, pageSize);


            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("��{0} -{1}�����,", startAt, endAt);
            sb.AppendFormat("��{0}������;", RecordCount);

            if (prevPage != 0)
                sb.AppendFormat("<a href=\"?" + QueryStringCons.PageIndex + "={0} \">��һҳ</a>&nbsp;", prevPage);
            else
                sb.Append("<span class=\"gray\">��һҳ</span>&nbsp;");
            if (nextPage != 0)
                sb.AppendFormat("<a href=\"?" + QueryStringCons.PageIndex + "={0} \">��һҳ</a>", nextPage);
            else
                sb.Append("<span class=\"gray\">��һҳ</span>");

            return sb.ToString();

        }


        #endregion

        #region ��ȡ��¼��ҳ;
        /// <summary>
        /// ��ȡ��һҳ����ʼ��¼��;
        /// </summary>
        /// <param name="RecordCount">�ܼ�¼��</param>
        /// <param name="StartAt">��ҳ����ʼ��¼</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <returns>��һҳ����ʼ��¼��(���û����һҳ����0)</returns>
        public int GetNextPage(int RecordCount, int StartAt, int PageSize)
        {
            int nextStartAt = StartAt + PageSize;

            return nextStartAt >= RecordCount ? 0 : nextStartAt;
        }
        /// <summary>
        /// �õ���һҳ��ҳ����
        /// </summary>
        /// <param name="RecordCount">�ܼ�¼��</param>
        /// <param name="StartAt">��ҳ����ʼ��¼</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <returns>�õ���һҳ��ҳ���������û�з���0</returns>
        public int GetNextPageIndex(int RecordCount, int StartAt, int PageSize)
        {

            if (StartAt + PageSize - 1 >= RecordCount)
                return 0;
            else
            {
                int prevPageIndex = (StartAt - 1) / PageSize;
                return prevPageIndex + 2;
            }

        }
        /// <summary>
        /// �õ���һҳ��ҳ����
        /// </summary>
        /// <param name="RecordCount">�ܼ�¼��</param>
        /// <param name="StartAt">��ҳ����ʼ��¼</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <returns>�õ���һҳ��ҳ���������û�з���0</returns>
        public int GetPrevPageIndex(int RecordCount, int StartAt, int PageSize)
        {
            if (StartAt == 1)
                return 0;
            else
            {
                int prevPageIndex = (StartAt - 1) / PageSize;
                return prevPageIndex;
            }
        }
        /// <summary>
        /// ��ȡ��һҳ����ʼ��¼��;
        /// </summary>
        /// <param name="RecordCount">�ܼ�¼��</param>
        /// <param name="StartAt">��ҳ����ʼ��¼</param>
        /// <param name="PageSize">ҳ��С</param>
        /// <returns>��һҳ��ʼ��¼��(���û����һҳ����0)</returns>
        public int GetPrevPage(int RecordCount, int StartAt, int PageSize)
        {
            int preStartAt = StartAt - PageSize;
            return preStartAt <= 0 ? 0 : preStartAt;
        }


        #endregion

        #region �������շ�������
        public static string GetStar(DateTime birthday)
        {
            DateTime start, end;
            DateTime birth = new DateTime(birthday.Year, birthday.Month, birthday.Day);

            string temp = "δ֪";

            //������ 03/21 - 04/19 
            start = new DateTime(birthday.Year, 3, 21);
            end = new DateTime(birthday.Year, 4, 19);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "������";

            //��ţ�� 04/20 - 05/20 
            start = new DateTime(birthday.Year, 4, 20);
            end = new DateTime(birthday.Year, 5, 20);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "��ţ��";

            //˫���� 05/21 - 06/21 
            start = new DateTime(birthday.Year, 5, 21);
            end = new DateTime(birthday.Year, 6, 21);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "˫����";

            //��з�� 06/22 - 07/22 
            start = new DateTime(birthday.Year, 6, 22);
            end = new DateTime(birthday.Year, 7, 22);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "��з��";

            //ʨ���� 07/23 - 08/22 
            start = new DateTime(birthday.Year, 7, 23);
            end = new DateTime(birthday.Year, 8, 22);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "ʨ����";

            //��Ů�� 08/23 - 09/22 
            start = new DateTime(birthday.Year, 8, 23);
            end = new DateTime(birthday.Year, 9, 22);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "��Ů��";

            //����� 09/23 - 10/23 
            start = new DateTime(birthday.Year, 9, 23);
            end = new DateTime(birthday.Year, 10, 23);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "�����";

            //��Ы�� 10/24 - 11/22 
            start = new DateTime(birthday.Year, 10, 24);
            end = new DateTime(birthday.Year, 11, 22);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "��Ы��";

            //������ 11/23 - 12/21 
            start = new DateTime(birthday.Year, 11, 22);
            end = new DateTime(birthday.Year, 12, 21);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "������";

            //ˮƿ�� 01/20 - 02/18 
            start = new DateTime(birthday.Year, 1, 20);
            end = new DateTime(birthday.Year, 2, 18);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "ˮƿ��";

            //˫���� 02/19 - 03/20 
            start = new DateTime(birthday.Year, 2, 19);
            end = new DateTime(birthday.Year, 3, 20);
            if (start.CompareTo(birth) <= 0 && end.CompareTo(birth) >= 0)
                return temp = "˫����";

            //ħ���� 12/22 - 01/19 
            //start = new DateTime(birthday.Year, 12, 22);
            //end = new DateTime(birthday.Year, 1, 19);
            //if (start.CompareTo(birth) <= 0 || end.CompareTo(birth) <= 0)
            //     return temp = "Ħ����";
            temp = "Ħ����";

            return temp;
        }
        #endregion

        #region ��������������ʼ��������ֵ(=�·�*100+����)
        public static int[] GetDateFromConstellation(Constellation c)
        {
            int[] date = null;
            switch (c)
            {
                case Constellation.Aries:
                    date = new int[] { 321, 419 };
                    break;
                case Constellation.Taurus:
                    date = new int[] { 420, 520 };
                    break;
                case Constellation.Gemini:
                    date = new int[] { 521, 621, };
                    break;
                case Constellation.Cancer:
                    date = new int[] { 622, 722, };
                    break;
                case Constellation.Leo:
                    date = new int[] { 723, 822, };
                    break;
                case Constellation.Virgo:
                    date = new int[] { 823, 922, };
                    break;
                case Constellation.Libra:
                    date = new int[] { 923, 1023, };
                    break;
                case Constellation.Scorpio:
                    date = new int[] { 1024, 1121, };
                    break;
                case Constellation.Sagittarius:
                    date = new int[] { 1122, 1221, };
                    break;
                case Constellation.Capricorn:
                    date = new int[] { 1222, 119, };
                    break;
                case Constellation.Aquarius:
                    date = new int[] { 120, 218, };
                    break;
                case Constellation.Pisces:
                    date = new int[] { 219, 320, };
                    break;
            }
            return date;
        }
        #endregion

        #region ����ö����ŷ�������
        public static string GetConstellation(Constellation c)
        {
            string s = string.Empty;
            switch (c)
            {
                case Constellation.Aries:
                    s = "������";
                    break;
                case Constellation.Taurus:
                    s = "��ţ��";
                    break;
                case Constellation.Gemini:
                    s = "˫����";
                    break;
                case Constellation.Cancer:
                    s = "��з��";
                    break;
                case Constellation.Leo:
                    s = "ʨ����";
                    break;
                case Constellation.Virgo:
                    s = "��Ů��";
                    break;
                case Constellation.Libra:
                    s = "�����";
                    break;
                case Constellation.Scorpio:
                    s = "��Ы��";
                    break;
                case Constellation.Sagittarius:
                    s = "������";
                    break;
                case Constellation.Capricorn:
                    s = "Ħ����";
                    break;
                case Constellation.Aquarius:
                    s = "ˮƿ��";
                    break;
                case Constellation.Pisces:
                    s = "˫����";
                    break;
            }
            return s;
        }
        #endregion

        #region ���ݵ�ǰʱ�䷵����һ����ʼʱ��
        public static DateTime[] GetLastWeekDateTime()
        {
            DateTime time = DateTime.Now.Date;
            DateTime begin = time.AddDays(-7);
            return new DateTime[] { begin, time };
        }

        #endregion

        #region ���ݵ�ǰʱ�䷵����һ����ʼ��������ֵ(=�·�*100+����)
        public static int[] GetLastWeekDate()
        {
            DateTime time = DateTime.Now;
            DateTime begin = time.AddDays(-7);
            DateTime end = time.AddDays(-1);
            return new int[] { begin.Year * 10000 + begin.Month * 100 + begin.Day, end.Year * 10000 + end.Month * 100 + end.Day };
        }
        #endregion




        #region ���ص�����������ֵ(=�·�*100+����)
        public static int GetDate()
        {
            return GetDate(DateTime.Now);
        }
        public static int GetDate(DateTime dataTime)
        {
            return dataTime.Year * 10000 + dataTime.Month * 100 + dataTime.Day;
        }
        #endregion

        #region ��������ʱ������ʱ��,���صĸ�ʽ#��#����#��#ʱ#��ǰ/��
        /// <summary>
        /// ��������ʱ������ʱ��,���صĸ�ʽ#��#����#��#ʱ#��ǰ/��
        /// </summary>
        /// <param name="time">����ʱ��</param>
        /// <param name="isShort">True���ض̸�ʽ,False���س���ʽ</param>
        /// <returns></returns>
        public static string GetRelative(DateTime time, bool isShort)
        {
            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - time.Ticks);
            string str = GetRelativeTime(span, isShort);
            if (str.Length == 0)
            {
                str = "һ������";
            }
            else
            {
                if (span.Ticks < 0)
                    str += "��";
                else
                    str += "ǰ";
            }
            return str;
        }
        public static string GetRelative(DateTime time)
        {
            return GetRelative(time, false);
        }

        public static string GetRelativeTime(TimeSpan span, bool isShort)
        {
            StringBuilder str = new StringBuilder();
            int temp = 0;

            if (span.Days != 0)
            {
                temp = span.Days;
                if (!isShort)
                {
                    if (temp / 365 != 0)
                    {
                        str.Append(string.Format("{0}��", Math.Abs(temp / 365)));
                        temp %= 365;
                    }

                    if (temp / 31 != 0)
                    {
                        str.Append(string.Format("{0}����", Math.Abs(temp / 31)));
                        temp %= 31;
                    }
                }
                if (temp != 0)
                {
                    str.Append(string.Format("{0}��", Math.Abs(temp)));
                    if (isShort)
                    {
                        return str.ToString();
                    }
                }
            }
            if (span.Hours != 0)
            {
                str.Append(string.Format("{0}Сʱ", Math.Abs(span.Hours)));
                if (isShort)
                {
                    return str.ToString();
                }

            }
            if (span.Minutes != 0)
            {
                str.Append(string.Format("{0}����", Math.Abs(span.Minutes)));
                if (isShort)
                {

                    return str.ToString();
                }
            }
            return str.ToString();
        }
        #endregion

        #region ȡ�ñ�����һ���е�����
        /// <summary>
        /// ȡ�ñ�����һ���е�����
        /// </summary>
        /// <returns></returns>
        public static int GetThisWeekNo()
        {
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            return myCal.GetWeekOfYear(DateTime.Now, myCWR, DayOfWeek.Sunday);
        }
        #endregion

        #region ȡ�ñ��ܵĵ�һ��
        /// <summary>
        /// ȡ�ñ��ܵĵ�һ��
        /// </summary>
        /// <returns></returns>
        public static DateTime GetWeekFirstDay()
        {
            DateTime dt = DateTime.Now;
            while (dt.DayOfWeek != DayOfWeek.Sunday)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }
        #endregion

        #region ��Ӣ���·�ת��Ϊ�����·�

        /// <summary>
        /// ��Ӣ���·�ת��Ϊ�����·�
        /// </summary>
        /// <param name="strMonth"></param>
        /// <returns></returns>
        public static string MonthStringToIntString(string strMonth)
        {
            string IntDay = "0";
            strMonth = strMonth.ToUpper();
            switch (strMonth)
            {
                case "JANUARY":
                    IntDay = "1";
                    break;
                case "FEBRUARY":
                    IntDay = "2";
                    break;
                case "MARCH":
                    IntDay = "3";
                    break;
                case "APRIL":
                    IntDay = "4";
                    break;
                case "MAY":
                    IntDay = "5";
                    break;
                case "JUNE":
                    IntDay = "6";
                    break;
                case "JULY":
                    IntDay = "7";
                    break;
                case "AUGUST":
                    IntDay = "8";
                    break;
                case "SEPTEMBER":
                    IntDay = "9";
                    break;
                case "OCTOBER":
                    IntDay = "10";
                    break;
                case "NOVEMBER":
                    IntDay = "11";
                    break;
                case "DECEMBER":
                    IntDay = "12";
                    break;
            }

            return IntDay;
        }

        #endregion

        #region ����yyyy-mm-dd��ʽ����������
        public static DateProperty GetYearMonthDay(string strdate)
        {
            DateProperty dateProperty = new DateProperty();
            dateProperty.Year = 0;
            dateProperty.Month = 0;
            dateProperty.Day = 0;
            string[] arrdate = strdate.Split(new char[] { '-' }, StringSplitOptions.None);
            if (arrdate.Length > 0)
            {
                if (arrdate[0].Length > 0)
                {
                    dateProperty.Year = SafeConvert.ToInt32(arrdate[0]);
                    if (arrdate.Length > 1)
                    {
                        if (arrdate[1].Length > 0)
                            dateProperty.Month = SafeConvert.ToInt32(arrdate[1]);
                        if (arrdate.Length > 2)
                        {
                            if (arrdate[2].Length > 0)
                                dateProperty.Day = SafeConvert.ToInt32(arrdate[2]);
                        }
                    }
                }
            }
            return dateProperty;
        }
        #endregion


        #region ���������շ��� ��-��-�ո�ʽ�ַ���
        public static string GetDateString(int year, int month, int day)
        {
            string returnDate = string.Empty;
            if (year > 0)
            {
                returnDate += year.ToString();
                if (month > 0)
                {
                    returnDate += "-" + month.ToString();
                    if (day > 0)
                        returnDate += "-" + day.ToString();
                }
            }
            return returnDate;
        }
        #endregion

        #region ��������ʱ��ļ��
        /// <summary>
        /// ��������ʱ��ļ����������
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static TimeSpan DateDiff(DateTime time1, DateTime time2)
        {
            TimeSpan ts1 = new TimeSpan(time1.Ticks);
            TimeSpan ts2 = new TimeSpan(time2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        public static TimeSpan DateDiff(DateTime DateTime1)
        {
            return DateDiff(DateTime1, DateTime.Now);
        }

        #endregion

        public static DateTime GetFullDateTime(string strdate)
        {
            string[] arrdate = strdate.Split(new char[] { '-' }, StringSplitOptions.None);
            for (int i = 0; i < arrdate.Length; i++)
            {
                try
                {
                    Convert.ToInt32(arrdate[i]);
                }
                catch
                {
                    return Convert.ToDateTime(null);
                }
            }
            if (arrdate.Length == 1)
            {
                strdate += "-01-01";
                return Convert.ToDateTime(strdate);
            }
            else if (arrdate.Length == 2)
            {
                strdate += "-01";
                return Convert.ToDateTime(strdate);
            }
            else if (arrdate.Length == 3)
            {
                return Convert.ToDateTime(strdate);
            }
            else
            {
                return Convert.ToDateTime(null);
            }
        }

        /// <summary>
        /// �ж������Ƿ�������
        /// </summary>
        /// <param name="o">Ŀǰ֧�����ͣ�DataSet��DataTable��String</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object o)
        {
            try
            {
                if (o == null)
                    return true;
                if (o is DataSet)
                {
                    DataSet dataSet = o as DataSet;
                    if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0 && dataSet.Tables[0] != null && dataSet.Tables[0].Rows != null && dataSet.Tables[0].Rows.Count > 0)
                    {
                        return false;
                    }
                    return true;
                }
                if (o is DataTable)
                {
                    DataTable dataTable = o as DataTable;
                    if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                    {
                        return false;
                    }
                    return true;
                }
                /*
                if ( o is ICollection )
                {
                    if ( ( o as ICollection ).Count > 0 )
                        return false;
                    return true;
                }
                if ( o is IList )
                {
                    if ( ( o as IList ).Count > 0 )
                        return false;
                    return true;
                }
                */
                if (o is string)
                {
                    return string.IsNullOrEmpty(o as string);
                }
            }
            catch
            {
                return true;
            }
            //�����δ֪���ͷ���false
            return false;
        }

        
    }
}
