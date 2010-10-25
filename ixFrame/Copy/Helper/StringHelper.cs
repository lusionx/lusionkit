using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Portal.Facilities
{
    /// <summary>
    /// �ַ���������
    /// </summary>
    public class StringHelper
    {
		private static Randoms random = new Randoms ();

		#region ������Ӣ�ĵ��ַ�����ȡ
		public static string GetChineseSubString( string inputString, int count )
		{
			return GetChineseSubString ( inputString, count, ".." );
		}
		public static string GetChineseSubString( string inputString, int count, string addition )
		{
			int limit = count * 2;
			if ( limit >= GetChineseStringLength ( inputString ) )
			{
				return inputString;
			}
			else
			{
				limit -= addition.Length;
				StringBuilder sb = new StringBuilder ();
				char [] chars = inputString.ToCharArray ();
				for ( int i = 0; i < chars.Length; i++ )
				{
					char c = chars [i];
					sb.Append ( c );
					if ( c > 127 )
					{
						limit -= 2;
						if ( limit < 0 )
						{
							sb.Length--;
							break;
						}
					}
					else
					{
						limit--;
					}
					if ( limit == 0 )
					{
						break;
					}
				}
				return sb.ToString () + addition;
			}
		}

		public static int GetChineseStringLength( string inputString )
		{
			int length = 0;
			if ( !string.IsNullOrEmpty ( inputString ) )
			{
				char [] chars = inputString.ToCharArray ();
				for ( int i = 0; i < chars.Length; i++ )
				{
					if ( chars [i] > 127 )
					{
						length += 2;
					}
					else
					{
						length++;
					}
				}
			}
			return length;
		} 
		
		#endregion
		
        #region ȡ��ǰnλ���ȵ��ִ�
        /// <summary>
        /// ȡ��ǰnλ���ȵ��ִ�
        /// </summary>
        /// <param name="theString">��Ҫ������ַ���</param>
        /// <param name="count">��Ҫ���ַ�������</param>
        /// <returns></returns>
        public static string GetSubString(string theString, int count)
        {
            if ( count >= theString.Length )
                return theString;
            else
                return theString.Substring( 0, count );
        }

        /// <summary>
        /// ȡ��ǰnλ���ȵ��ִ��Ӻ�׺�ַ���
        /// </summary>
        /// <param name="theString">��Ҫ������ַ���</param>
        /// <param name="count">��Ҫ���ַ�������</param>
        /// <param name="addition">��׺�ַ���</param>
        /// <returns></returns>
        public static string GetSubString(string theString, int count, string addition)
        {
            string a = GetSubString( theString, count );

            if ( a.Length < theString.Length )
                return a + addition;
            else
                return a;
        }
        #endregion

        #region ����URL����ȡ��Ӣ����
        /// <summary>
        /// ����URL����ȡ��Ӣ����
        /// </summary>
        /// <param name="queryString">URL����</param>
        /// <returns></returns>
        public static string GetEnglishNameFromUrl(string queryString)
        {
			return queryString.Replace(",", ".").Replace("_", " ").Replace("��", ":").Replace("��", "/").Replace("��", "+");
        }
        #endregion

        #region ����Ӣ����ȡ������URL���ַ���
        /// <summary>
        /// ����Ӣ����ȡ������URL���ַ���
        /// </summary>
        /// <param name="englishName">Ӣ����</param>
        /// <returns></returns>
        public static string GetUrlFromEnglishName(string englishName)
        {
			return englishName.Replace(".", ",").Replace(" ", "_").Replace(":", "��").Replace("/", "��").Replace("+", "��");
        }
        #endregion

        #region �����ʽ��
        /// <summary>
        /// �����ʽ��
        /// </summary>
        /// <param name="context">����</param>
        /// <param name="paragraphClass">������ʽClass</param>
        /// <param name="paragraphStyle">������ʽ</param>
        /// <returns></returns>
        public static string ParagraphFormat(string context, string paragraphClass, string paragraphStyle)
        {
            //ȡ����ʽת��
            //return context.Replace( Environment.NewLine, "<br/>");

            //��ʼ��StringBuilder
            StringBuilder sb = new StringBuilder( 512 );

            //��ʼ������ģʽ
            Regex regex = new Regex( @"(.*\r?\n)" );
            //ȡ��ƥ�伯��
            MatchCollection mc = regex.Matches( context + "\n" );

            foreach ( Match m in mc )
            {
                //��ȡƥ����
                string s = m.Groups[0].Value.Trim();

                //��ƥ���Ϊ�������
                if ( s != string.Empty )
                {
                    //��ƥ����Ǳ�����ȡ�ö��䣬����ȡ�ñ���
                    if ( !Regex.Match( s, @"^/h\d.*$" ).Success )
                    {
                        sb.Append( "<p" );
                        //��ȡ������ʽClass
                        if ( paragraphClass.Trim() != string.Empty )
                            sb.Append( " class=\"" + paragraphClass + "\"" );
                        //��ȡ������ʽ
                        if ( paragraphStyle.Trim() != string.Empty )
                            sb.Append( " style=\"" + paragraphStyle + "\"" );
                        sb.Append( ">" );
                        //��ȡ��������
                        sb.Append( m.Groups[0].Value.Trim() );
                        sb.Append( "</p>\n" );
                    }
                    else
                    {
                        //��ȡ������
                        string h = Regex.Replace( s, @"^/(h\d).*$", "$1" );
                        //��ȡ��������
                        string t = Regex.Replace( s, @"^/h\d(.*)$", "$1" );
                        sb.Append( "<" + h + ">" + t + "</" + h + ">\n" );
                    }
                }
            }

            //���ظ�ʽ������
            return sb.ToString();
        }
        #endregion

        #region ȡ�����͵�Base64�ַ���
        /// <summary>
        /// ȡ�����͵�Base64�ַ���
        /// </summary>
        /// <param name="o">���Ͷ���</param>
        /// <returns></returns>
        public static string GetBase64StringFromString(object o)
        {
            byte[] b = Encoding.UTF8.GetBytes( o.ToString() );
            return Convert.ToBase64String( b );
        }
        #endregion

        #region ȡ��Base64�ַ���������
        /// <summary>
        /// ȡ��Base64�ַ���������
        /// </summary>
        /// <param name="s">�ַ���</param>
        /// <returns></returns>
        public static string GetStringFromBase64String(string s)
        {
            byte[] b = Convert.FromBase64String( s );
            return Encoding.UTF8.GetString( b );
        }
        #endregion

        #region ������ʾ�ؼ���
        /// <summary>
        /// ���ظ���(��ɫ)��ʾ�ؼ��ֵ������ı�
        /// </summary>
        /// <param name="keyword">�ؼ����ı�</param>
        /// <param name="content">�����ı�</param>
        /// <returns>��������ı�</returns>
        public static string ShowHighlightKeyword(string keyword, string content)
        {
            if ( keyword == string.Empty || content == string.Empty )
                return string.Empty;

            string charset = @"`~!@#$%^&*()_+-=[]\|}{;:'/.,<>?������������������";

            foreach ( char c in charset )
            {
                keyword = keyword.Replace( c.ToString(), " " );
            }

            string a = keyword;

            for ( int i = 0 ; i < a.Length ; i++ )
            {
                try
                {
                    string c = a.Substring( i, 1 );
                    int asc = (int)Convert.ToChar( c );
                    if ( asc > 255 )
                    {
                        int n = a.IndexOf( c );
                        a = a.Insert( n, " " );
                        a = a.Insert( n + 2, " " );
                        i += 2;
                    }
                }
                catch
                {
                    continue;
                }
            }

            foreach ( string b in a.Split( (char)32 ) )
            {
                switch ( b.Trim().ToLower() )
                {
                    case "":
                        break;
                    default:
                        Regex r2 = new Regex( "(" + b + ")", RegexOptions.IgnoreCase );
                        content = r2.Replace( content, "[(]$1[)]" );
                        break;
                }
            }

            content = content.Replace( "[(]", "<span class=\"red\">" );
            content = content.Replace( "[)]", "</span>" );

            return content;
        }
        #endregion

        #region ��ȡ��ȫ��������

        /// <summary>
        /// ��ȡ��ȫ����������Ĭ��ֵΪ0
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int SafeInt(string text)
        {
            return SafeInt( text, 0 );
        }

        /// <summary>
        /// ��ȡ��ȫ��������
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int SafeInt(string text, int defaultValue)
        {
            if ( !string.IsNullOrEmpty( text ) )
            {
                try
                {
                    return Int32.Parse( text );
                }
                catch ( Exception )
                {
                }

            }
            return defaultValue;
        }

        #endregion

        #region �ַ����Ƚ�
        /// <summary>
        /// �ַ����Ƚ�
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static bool StringCompare(string strA, string strB)
        {
            if ( string.Compare( strA, strB, true ) == 0 )
                return true;
            return false;
        }

        #endregion

        #region ��ȡ��ȫ��HTML�����ı�
        /// <summary>
        /// ��ȡ��ȫ��HTML�����ı�
        /// �˷��������˵�������ɲ���ȫ���ص�HTML���
        /// By Marila
        /// </summary>
        /// <param name="content">HTML�����ı�</param>
        /// <returns></returns>
        public static string GetSafeHtmlText(string content)
        {
            if ( string.IsNullOrEmpty( content ) )
                return string.Empty;

            string s = content;

            //����<iframe>
            s = Regex.Replace( s, @"<iframe[\s\S]*?>([\s\S]*?<\/iframe[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<iframe", string.Empty, RegexOptions.IgnoreCase );

            //����<script>
            s = Regex.Replace( s, @"<script[\s\S]*?>([\s\S]*?<\/script[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<script", string.Empty, RegexOptions.IgnoreCase );

            //����<input>
            s = Regex.Replace( s, @"<input[\s\S]*?>([\s\S]*?<\/input[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<input", string.Empty, RegexOptions.IgnoreCase );

            //����<textarea>
            s = Regex.Replace( s, @"<textarea[\s\S]*?>([\s\S]*?<\/textarea[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<textarea", string.Empty, RegexOptions.IgnoreCase );

            //����<select>
            s = Regex.Replace( s, @"<select[\s\S]*?>([\s\S]*?<\/select[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<select", string.Empty, RegexOptions.IgnoreCase );

            //����<option>
            s = Regex.Replace( s, @"<option[\s\S]*?>([\s\S]*?<\/option[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<option", string.Empty, RegexOptions.IgnoreCase );

            //����<form>
            s = Regex.Replace( s, @"<form[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<\/form[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );

            //����<body>
            s = Regex.Replace( s, @"<body[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<\/body[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );

            //����<html>
            s = Regex.Replace( s, @"<html[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<\/html[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );

            //����<meta>
            s = Regex.Replace( s, @"<meta[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<meta", string.Empty, RegexOptions.IgnoreCase );

            //����<link>
            s = Regex.Replace( s, @"<link[\s\S]*?>", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<link", string.Empty, RegexOptions.IgnoreCase );

            //����<frameset>
            s = Regex.Replace( s, @"<frameset[\s\S]*?>([\s\S]*?<\/frameset[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<frameset", string.Empty, RegexOptions.IgnoreCase );

            //����<frame>
            s = Regex.Replace( s, @"<frame[\s\S]*?>([\s\S]*?<\/frame[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<frame", string.Empty, RegexOptions.IgnoreCase );

            //����<noframe>
            s = Regex.Replace( s, @"<noframe[\s\S]*?>([\s\S]*?<\/noframe[\s\S]*?>)?", string.Empty, RegexOptions.IgnoreCase );
            s = Regex.Replace( s, @"<noframe", string.Empty, RegexOptions.IgnoreCase );

            //����onload
            s = Regex.Replace( s, @"onload=(\"")?\s*[^\""\n]*(\"")?", string.Empty, RegexOptions.IgnoreCase );
            //����onmouseover
            s = Regex.Replace( s, @"onmouseover=(\"")?\s*[^\""\n]*(\"")?", string.Empty, RegexOptions.IgnoreCase );
            return s;
        }
        #endregion

        public static string GetDisplayText( string inputText )
        {
        	if ( string.IsNullOrEmpty( inputText ) )
        		return string.Empty;
        	return inputText.Replace( Environment.NewLine, "<br/>" );
        }

    	/// <summary>
        /// ��ȡ���ϸ�ʽ��HTML
        /// </summary>
        /// <param name="inputHtml"></param>
        /// <returns></returns>
        public static string GetCleanHTMLStringFromHtml(string inputHtml)
        {
			return GetCleanHTMLStringFromHtml ( inputHtml, AllowedHtmlTagType.Default );
        }

		public static string GetCleanHTMLStringFromHtml( string inputHtml, AllowedHtmlTagType allowedHtmlTagType )
		{
			Dictionary<string, string> allowedTags = AllowedHtmlTags.Current.GetAllowedHtmlTags ( allowedHtmlTagType );
			//Dictionary<string, string> allowedTags = new Dictionary<string, string>( );
			if ( allowedTags == null || allowedTags.Count == 0 )
			{
				try
				{
					allowedTags = AllowedHtmlTags.Current.GetAllowedHtmlTags ( allowedHtmlTagType );
					if ( allowedTags == null || allowedTags.Count == 0 )
					{
						Logger.Current.Log ( allowedHtmlTagType.ToString (), inputHtml );
					}
				}
				catch ( Exception e )
				{
					ExceptionService.Current.Handle ( e );
				}
			}
			return GetCleanHTMLStringFromHtml( inputHtml, allowedTags );
		}
        /// <summary>
        /// ��ȡ���ϸ�ʽ��HTML
        /// </summary>
        /// <param name="inputHtml"></param>
        /// <param name="allowedTags">����ı�Ǽ���</param>
        /// <returns></returns>
		public static string GetCleanHTMLStringFromHtml( string inputHtml, Dictionary<string, string> allowedTags )
        {
            if ( string.IsNullOrEmpty( inputHtml ) )
                return string.Empty;
            string output = GetSafeHtmlText( inputHtml );
            output = output.Replace( "&nbsp;", "��" );
            output = HtmlHelper.MakeupTag( Cubb( output, true ), allowedTags );
            output = output.Replace( "��", "&nbsp;" );
            return output;
        }

        /// <summary>
        /// ͳ������������HTML��ǣ�
        /// </summary>
        /// <param name="inputHtml"></param>
        /// <returns></returns>
        public static int GetWordCountFromHtml(string inputHtml)
        {
            if ( string.IsNullOrEmpty( inputHtml ) )
                return 0;
            string text = GetCleanText( inputHtml );
            return text.Replace( "&nbsp;", " " ).Replace( "��", " " ).Trim().Length;
        }

        /// <summary>
        /// ȥ������ַ���
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string GetCleanText(string inputString)
        {
            if ( string.IsNullOrEmpty( inputString ) )
                return string.Empty;
            //Todo:����
            string text = Regex.Replace( inputString, "<script((.|\n)*?)</script>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline );
            text = Regex.Replace( text, "onload=(\")?\\s*[^\"\n]*(\")?", "", RegexOptions.IgnoreCase );
            text = Regex.Replace( text, @"onload=(\"")?\s*[^\""\n]*(\"")?", "", RegexOptions.IgnoreCase );
            text = Regex.Replace( text, "onload=\\\"if[^\"]*\\\"", "", RegexOptions.IgnoreCase );
            text = Regex.Replace( text, "\"javascript:", "", RegexOptions.IgnoreCase | RegexOptions.Multiline );
            string pattern = @"\<[^>]*>";
            Regex regex = new Regex( pattern, RegexOptions.IgnoreCase );
            text = regex.Replace( text, "", -1 );
        	pattern = @"\[[^[]*\]";
			regex = new Regex ( pattern, RegexOptions.IgnoreCase );
			text = regex.Replace ( text, "", -1 );
            return text;
        }

        /// <summary>
        /// ���Ա�ǵõ��ַ�����������
        /// </summary>
        /// <param name="formattedBody"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetFormattedSubString(string formattedBody, int length)
        {
            if ( string.IsNullOrEmpty( formattedBody ) )
            {
                return "";
            }
            else if ( formattedBody.Length < length )
            {
                return formattedBody;
            }
            else
            {
                //todo:���ж��ַ������д���
                int count = 0;
                int match = 0;
                StringBuilder body = new StringBuilder();
                while ( count < formattedBody.Length )
                {
                    char c = formattedBody[count];
                    if ( c == '<' )
                        match += 1;
                    else if ( c == '>' )
                        match -= 1;
                    else if ( match == 0 )
                        body.Append( c );
                    if ( match <= 0 && body.Length >= length )
                        break;
                    count++;
                }
                return body.ToString() + "...";
                //return formattedBody.Substring ( 0, length ) + "..";
            }
        }

        /// <summary>
        /// �õ�������ǵ����ַ�����������
        /// </summary>
        /// <param name="formattedBody"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetFormattedString(string formattedBody, int length)
        {
            if ( string.IsNullOrEmpty( formattedBody ) )
            {
                return "...";
            }
            else if ( formattedBody.Length < length )
            {
                return formattedBody;
            }
            else
            {
                int totalCount = 0;
                int substring = 0;
                int match = 0;
                StringBuilder body = new StringBuilder();
                while ( totalCount < formattedBody.Length )
                {
                    char c = formattedBody[totalCount];
                    if ( c == '<' )
                        match += 1;
                    else if ( c == '>' )
                        match -= 1;
                    else if ( match == 0 )
                        substring++;
                    body.Append( c );
                    if ( match <= 0 && substring >= length )
                        break;
                    totalCount++;
                }
                return body.ToString() + "...";
                //return formattedBody.Substring ( 0, length ) + "..";
            }
        }

		/// <summary>
		/// �õ��滻����ַ���
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string GetReplaceCaptionText(string name)
		{
			return name.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "&quot;");
		}
        ///// <summary>
        ///// ��������
        ///// </summary>
        ///// <param name="html"></param>
        ///// <returns></returns>
        //public static string GetReduceImageHtml( string html )
        //{
        //    string pattern = "<img.*src=\"([^\"]*)\"[^/]*/>";
        //    Regex regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "<img src=\"$1\" border=\"0\" onload=\"javascript:if(typeof BlogMaxImageSize != 'undefined') {if(this.width > BlogMaxImageSize) this.width= BlogMaxImageSize;} else {if(this.width > screen.width / 2 ) this.width=screen.width / 2;}\" alt=\"\"/>", -1 );
        //    return html;
        //}

        ///// <summary>
        ///// �õ�ժҪ�ַ�������Img��ǣ�ȥ��������ǣ�������
        ///// </summary>
        ///// <param name="html"></param>
        ///// <returns></returns>
        //public static string GetSummaryHtml( string html )
        //{
        //    string pattern = @"\<img";
        //    Regex regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "��", -1 );

        //    pattern = @"</p>";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "/r/n", -1 );

        //    pattern = @"<br\s*/?>";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "/r/n", -1 );

        //    pattern = @"(��[^>]*)>";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "$1��", -1 );

        //    pattern = @"\<[^>]*>";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "", -1 );

        //    pattern = @"��";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "<img", -1 );

        //    pattern = @"��";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "/>", -1 );

        //    pattern = "<img.*src=\"([^\"]*)\"[^/]*[/]?>";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "<img src=\"$1\" alt=\"\"/>", -1 );

        //    //pattern = "<img.*src=\"([^\"]*)\"[^/]*/>";
        //    //regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    //html = regex.Replace ( html, "<img src=\"$1\" border=\"0\" onload=\"javascript:if(typeof BlogMaxImageSize != 'undefined') {if(this.width > BlogMaxImageSize) this.width= BlogMaxImageSize;} else {if(this.width > screen.width / 2 ) this.width=screen.width / 2;}\" alt=\"\"/>", -1 );

        //    pattern = @"\r\n";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "<br/>", -1 );

        //    pattern = @"\n";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "<br/>", -1 );

        //    pattern = @"\r";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "<br/>", -1 );

        //    pattern = @"//>";
        //    regex = new Regex ( pattern, RegexOptions.IgnoreCase );
        //    html = regex.Replace ( html, "/>", -1 );
        //    return html;
        //}

        /// <summary>
        /// �����Ѿ��̶�ͼƬ��ȵ�HTML
        /// </summary>
        /// <param name="html">HTML</param>
        /// <param name="maxImageWidth">�����</param>
        /// <returns></returns>
        public static string GetFixupImageWidthHtml(string html, int maxImageWidth)
        {
			//�ÿͻ��˽ű�ʵ��
			//string pattern = "<img";
			//Regex regex = new Regex( pattern, RegexOptions.IgnoreCase );
			//html = regex.Replace( html, "<img onload=\"javascript:if(this.width > " + maxImageWidth + ") this.width=" + maxImageWidth + ";\"", -1 );
            return html;
        }

        /// <summary>
        /// ��ȡһ��HTML����
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="length">��Ҫ��ȡ�ĳ���</param>
		/// <param name="allowedHtmlTagType">�����HTML�������</param>
        /// <returns></returns>
		public static string GetHtmlSubstring( string htmlString, int length, AllowedHtmlTagType allowedHtmlTagType )
        {
            if ( string.IsNullOrEmpty( htmlString ) )
                return string.Empty;
			Dictionary<string, string> allowedTags = AllowedHtmlTags.Current.GetAllowedHtmlTags ( allowedHtmlTagType );
			try
            {
                string html = htmlString.Replace( "&nbsp;", "��" );
				string substringHtml = HtmlHelper.Substring ( Cubb ( html, true ), length, allowedTags );
				if ( substringHtml.Length < htmlString.Length )
					html = substringHtml + "...";
				else
					html = substringHtml;
                html = html.Replace( "��", "&nbsp;" );
                return html;
            }
            catch ( Exception e )
            {
                ExceptionService.Current.Handle( e );
                return htmlString;
            }
        }

		public static string GetHtmlSubstring( string htmlString, int length, bool ignoreTag )
		{
			if ( string.IsNullOrEmpty ( htmlString ) )
				return string.Empty;
			try
			{
				string html = htmlString.Replace ( "&nbsp;", "��" );
				//todo:
				/*
			& ���� & &amp; 
			������ ' &apos; 
			˫���� " &quot; 
			���� > &gt; 
			С�� < &lt; 
			*/
				if ( ignoreTag )
				{
					html = GetCleanText ( html );
					html = GetSubString ( html, length, "..." );
				}
				else
				{
					//html = HtmlHelper.Clean ( html, false, true );
					//html = GetFormattedString( html, length );
					//html = HtmlHelper.MakeupTag( html );
					string substringHtml = HtmlHelper.Substring ( Cubb ( html, true ), length );
					if ( substringHtml.Length < htmlString.Length )
						html = substringHtml + "...";
					else
						html = substringHtml;
				}
				html = html.Replace ( "��", "&nbsp;" );
				return html;
			}
			catch ( Exception e )
			{
				ExceptionService.Current.Handle ( e );
				return htmlString;
			}
		}

        /// <summary>
        /// ����û���������ݽ�������
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string CleanInputString(string inputString)
        {
			if ( string.IsNullOrEmpty( inputString ) )
				return string.Empty;
            StringBuilder retVal = new StringBuilder();
                inputString = inputString.Trim();
                for ( int i = 0 ; i < inputString.Length ; i++ )
                {
                    switch ( inputString[i] )
                    {
                        case '"':
                            retVal.Append( "&quot;" );
                            break;
                        case '<':
                            retVal.Append( "&lt;" );
                            break;
                        case '>':
                            retVal.Append( "&gt;" );
                            break;
						case (char)14:
							retVal.Append(' ');
							break;
                        default:
                            retVal.Append( inputString[i] );
                            break;
                    }
                }
                retVal.Replace( "'", " " );
            return retVal.ToString();
        }

        /// <summary>
        /// ���ظ�ʽ������ַ���
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetParagraph(string content, int wordCount)
        {
            return ParagraphFormat( GetHtmlSubstring( content, wordCount, false ), "para", String.Empty );
        }

        /// <summary>
        /// �滻BR
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CheckedString(string str)
        {
            str = Regex.Replace( str, "<b/>", "<b>", RegexOptions.IgnoreCase );
            int startPosition;
            int endPosition;
            for ( int i = 0 ; i < str.Length ; i++ )
            {
                startPosition = str.IndexOf( "<b>" );
                endPosition = str.IndexOf( "</b>" );
                if ( endPosition < startPosition && endPosition != -1 )
                {
                    str = str.Substring( 0, endPosition ) + str.Substring( endPosition + 4, str.Length - endPosition - 4 );
                    i = endPosition;
                    continue;
                }
                if ( startPosition != -1 && endPosition > startPosition )
                {
                    str = str.Substring( 0, startPosition ) + str.Substring( endPosition + 4, str.Length - endPosition - 4 );
                    i = startPosition;
                }
                else
                {
                    break;
                }
            }
            str = Regex.Replace( str, "<b>", "", RegexOptions.IgnoreCase );
            str = Regex.Replace( str, "</b>", "", RegexOptions.IgnoreCase );
            str = Regex.Replace( str, "<br>", "", RegexOptions.IgnoreCase );
            return str;
        }

        /// <summary>
        /// ʵ�����
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String Encode(String text)
        {
            if ( string.IsNullOrEmpty( text ) )
                return string.Empty;
			//string inputString = text.Replace( "&amp;", "&" ).Replace( "&", "&amp;" ).Replace( "&amp;nbsp;", "&nbsp;" ).Replace( "&amp;gt;", "&gt;" ).Replace( "&amp;lt;", "&lt;" ).Replace( "&amp;quot;", "&quot;" );
            StringBuilder sb = new StringBuilder();
			char [] chars = text.ToCharArray ();
            for ( int i = 0 ; i < chars.Length ; i++ )
            {
                if ( chars[i] > 127 )
                {
                    sb.Append( "&#x" );
                    sb.Append( ( (short)chars[i] ).ToString( "X" ) );
                    sb.Append( ";" );
                }
                else
                {
                    sb.Append( chars[i] );
                }
            }
            return sb.ToString();
        }

    	
		public static string BlockSpamEncode( string text )
		{
			if ( string.IsNullOrEmpty( text ) )
			{
				return string.Empty;
			}
			StringBuilder builder = new StringBuilder ();
			/*
			for ( int i = 0; i < text.Length; i++ )
			{
				if ( random.Next ( 2 ) == 1 )
				{
					builder.AppendFormat ( "&#{0};", Convert.ToInt32 ( text [i] ) );
				}
				else
				{
					builder.Append ( text [i] );
				}
			}
			 * */
			for ( int i = 0; i < text.Length; i++ )
			{
				char c = text[ i ];
				builder.Append( ScrambleStyle.Current.GetString ( c ) );
			}
			return builder.ToString ();
		}


        #region BBDecode

        #region UBBת��

        #region ������ת����HTML���벢��BR�滻����
        /// <summary>
        /// HTMLת��
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Chtml(string content)
        {
            string a = content;

            if ( a != string.Empty )
            {
                a = HttpUtility.HtmlEncode( a );
                a = a.Replace( "\n", "<br />" );

            }

            return a;
        }
        #endregion

        #region ����ת��
        /// <summary>
        /// ����ת��
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Cemotion(string content)
        {
            string a = content;

            if ( a != string.Empty )
            {
                Regex r = new Regex( @"\[em(\d{1,3})\]" );

                //Emotion
                a = r.Replace( a, "<img src=\"http://www.e-jjj.com/i/emotion/em$1.gif\" border=\"0\" alt=\"����ͼƬ\" align=\"middle\" />" );

                //Common
                a = a.Replace( "8-)", "<img src=\"http://www.e-jjj.com/i/emotion/em5.gif\" border=\"0\" alt=\"����ͼƬ\" align=\"middle\" />" );
                a = a.Replace( ":-)", "<img src=\"http://www.e-jjj.com/i/emotion/em63.gif\" border=\"0\" alt=\"����ͼƬ\" align=\"middle\" />" );
                a = a.Replace( ":)", "<img src=\"http://www.e-jjj.com/i/emotion/em63.gif\" border=\"0\" alt=\"����ͼƬ\" align=\"middle\" />" );
                a = a.Replace( "����", "<img src=\"http://www.e-jjj.com/i/emotion/em63.gif\" border=\"0\" alt=\"����ͼƬ\" align=\"middle\" />" );
                a = a.Replace( ":'(", "<img src=\"http://www.e-jjj.com/i/emotion/em35.gif\" border=\"0\" alt=\"����ͼƬ\" align=\"middle\" />" );
            }

            return a;
        }
        #endregion
        /// <summary>
        /// UBBת��
        /// </summary>
        /// <param name="content"></param>
        /// <param name="isconvertemotion">ָ���Ƿ�ת������</param>
        /// <returns></returns>
        public static string Cubb(string content, bool isconvertemotion)
        {
            string a = content;

            if ( a != string.Empty )
            {
                Regex r;

                //����JS
                r = new Regex( @"javascript", RegexOptions.IgnoreCase );
                a = r.Replace( a, "javascript" );
                r = new Regex( @"jscript:", RegexOptions.IgnoreCase );
                a = r.Replace( a, "jscript:" );
                r = new Regex( @"js:", RegexOptions.IgnoreCase );
                a = r.Replace( a, "js:" );
                r = new Regex( @"value", RegexOptions.IgnoreCase );
                a = r.Replace( a, "value" );
                r = new Regex( @"about:", RegexOptions.IgnoreCase );
                a = r.Replace( a, "about:" );
                r = new Regex( @"file:", RegexOptions.IgnoreCase );
                a = r.Replace( a, "file:" );
                r = new Regex( @"document.cookie", RegexOptions.IgnoreCase );
                a = r.Replace( a, "document.cookie" );
                r = new Regex( @"vbscript:", RegexOptions.IgnoreCase );
                a = r.Replace( a, "vbscript:" );
                r = new Regex( @"vbs:", RegexOptions.IgnoreCase );
                a = r.Replace( a, "vbs:" );
                r = new Regex( @"(on(mouse|exit|error|click|key))", RegexOptions.IgnoreCase );
                a = r.Replace( a, "on$2" );
                r = new Regex( @"script", RegexOptions.IgnoreCase );
                a = r.Replace( a, "script" );

                //[b][/b]ת��
                r = new Regex( @"(\[b\])(.+?)(\[\/b\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<b>$2</b>" );

                //[i][/i]ת��
                r = new Regex( @"\[i\](.+?)\[/i\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<i>$1</i>" );

                //[u][/u]ת��
                r = new Regex( @"\[u\](.+?)\[/u\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<u>$1</u>" );

                //[s][/s]ת��
                r = new Regex( @"\[s\](.+?)\[/s\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<s>$1</s>" );

                //[center][/center]ת��
                r = new Regex( @"\[center\](.+?)\[/center\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<div align=\"center\">$1</div>" );

                //[color=][/color]ת��
                r = new Regex( @"\[color=(.+?)\](.+?)\[\/color\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font color=\"$1\">$2</font>" );

                //[size=][/size]ת��
                r = new Regex( @"(\[size=1\])(.+?)(\[\/size\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font size=\"1\">$2</font>" );

                r = new Regex( @"(\[size=2\])(.+?)(\[\/size\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font size=\"2\">$2</font>" );

                r = new Regex( @"(\[size=3\])(.+?)(\[\/size\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font size=\"3\">$2</font>" );

                r = new Regex( @"(\[size=4\])(.+?)(\[\/size\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font size=\"4\">$2</font>" );

                r = new Regex( @"(\[size=5\])(.+?)(\[\/size\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font size=\"5\">$2</font>" );

                r = new Regex( @"(\[size=6\])(.+?)(\[\/size\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font size=\"6\">$2</font>" );

                //�Զ�URL����
                /*
                r = new Regex ( @"((?<prefix>\]|=)?(?<theurl>(http|https|ftp)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*))" );
                MatchCollection mc = r.Matches ( a );

                int i = 0;

                foreach ( Match m in mc )
                {
                    if ( m.Success && m.Groups ["prefix"].ToString ().ToLower () != "]" && m.Groups ["prefix"].ToString ().ToLower () != "=" )
                    {
                        a = a.Insert ( m.Index + i, "[url]" );
                        a = a.Insert ( m.Index + m.Length + i + 5, "[/url]" );
                        i += 11;
                    }
                }
                */

                //[url][/url]ת��
                r = new Regex( @"\[url\](?<theurl>.+?)\[\/url\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<a href=\"${theurl}\" class=\"fc1 ul\" target=\"_blank\">${theurl}</a>" );

                //[url=][/url]ת��
                r = new Regex( @"\[url=(?<theurl>.+?)\](?<theurl2>.+?)\[\/url\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<a href=\"${theurl}\" class=\"fc1 ul\" target=\"_blank\">${theurl2}</a>" );

                //[email][/email]ת��
                r = new Regex( @"\[email\](.?)\[\/email\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<a href=\"mailto:$1\" class=\"fc1 ul\">$1</a>" );

                //[email=][/email]ת��
                r = new Regex( @"\[email=(.+?)\](.+?)\[\/email\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<a href=\"mailto:$1\" class=\"fc1 ul\">$2</a>" );

                //[img][/img]ת��
                r = new Regex( @"(\[img\])(.+?)(\[\/img\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<img src=\"$2\" border=\"0\" onload=\"javascript:if(typeof MaxImageSize != 'undefined') {if(this.width > MaxImageSize) this.width= MaxImageSize;} else {if(this.width > screen.width / 2 ) this.width=screen.width / 2;}\">" );

                //[img=���][/img]ת��
                r = new Regex( @"(\[img=(\d+)\])(.+?)(\[\/img\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<img src=\"$3\" border=\"0\" width=\"$2\">" );

                //[limg][/limg]ת��
                r = new Regex( @"(\[limg\])(.+?)(\[\/limg\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<img src=\"$2\" border=\"0\" align=\"left\" onload=\"javascript:if(typeof MaxImageSize != 'undefined') {if(this.width > MaxImageSize) this.width= MaxImageSize;} else {if(this.width > screen.width / 2 ) this.width=screen.width / 2;}\">" );

                //[limg=���][/limg]ת��
                r = new Regex( @"(\[limg=(\d+)\])(.+?)(\[\/limg\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<img src=\"$3\" border=\"0\" width=\"$2\" align=\"left\">" );

                //[rimg][/rimg]ת��
                r = new Regex( @"(\[rimg\])(.+?)(\[\/rimg\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<img src=\"$2\" border=\"0\" align=\"right\" onload=\"javascript:if(typeof MaxImageSize != 'undefined') {if(this.width > MaxImageSize) this.width= MaxImageSize;} else {if(this.width > screen.width / 2 ) this.width=screen.width / 2;}\">" );

                //[rimg=���][/rimg]ת��
                r = new Regex( @"(\[rimg=(\d+)\])(.+?)(\[\/rimg\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<img src=\"$3\" border=\"0\" width=\"$2\" align=\"right\">" );

                //[code][/code]ת��
                r = new Regex( @"\[code\](.+?)\[\/code\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<table width=\"80%\" border=\"0\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"#FFFFFF\" style=\"border:1px solid #000000;margin:10px;\"><tr><td><font face=\"Courier new\" color=\"black\">$1</font></td></tr></table>" );

                //[face=][/face]ת��
                r = new Regex( @"(\[face=(.+?)\])(.+?)(\[\/face\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<font face=\"$2\">$3</font>" );

                //[fly][/fly]ת��
                r = new Regex( @"\[fly\](.+?)\[\/fly\]", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<marquee width=\"100%\" behavior=\"alternate\" scrollamount=\"3\">$1</marquee>" );

                //[move][/move]ת��
                r = new Regex( @"(\[move\])(.+?)(\[\/move\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<marquee width=\"100%\" scrollamount=\"3\">$2</marquee>" );

                //[swf=num,num][/swf]ת��
                r = new Regex( @"(\[swf=(\d+),(\d+)\])(.+?)(\[\/swf\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=4,0,2,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=$2 height=$3><PARAM NAME=movie VALUE=\"$4\"><PARAM NAME=quality VALUE=high><embed src=\"$4\" quality=high pluginspage='http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width=$2 height=$3>$4</embed></OBJECT><br />" );

                //[dir=num,num][/dir]ת��
                r = new Regex( @"(\[dir=(\d+),(\d+)\])(.+?)(\[\/dir\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<OBJECT classid=clsid:166B1BCA-3F9C-11CF-8075-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/director/sw.cab width=$2 height=$3><param name=src value=\"$4\"><embed src=\"$4\" pluginspage=http://www.macromedia.com/shockwave/download/ width=$2 height=$3></embed></OBJECT><br />" );

                //[rm=num,num][/rm]ת��
                r = new Regex( @"(\[rm=(\d+),(\d+)\])(.+?)(\[\/rm\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<object classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=object id=RAOCX width=$2 height=$3><PARAM NAME=SRC value=\"$4\"><PARAM NAME=CONSOLE VALUE=2004280084352954><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=true></object><br><object classid=clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=$2><PARAM NAME=SRC value=\"$4\"><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=2004280084352954></object><br />" );

                //[rm=num,num:y][/rm]ת��
                r = new Regex( @"(\[rm=(\d+),(\d+)\:y\])(.+?)(\[\/rm\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<object classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=object id=RAOCX width=$2 height=$3><PARAM NAME=SRC value=\"$4\"><PARAM NAME=CONSOLE VALUE=2004280084352954><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=true></object><br><object classid=clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=$2><PARAM NAME=SRC value=\"$4\"><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=2004280084352954></object><br />" );

                //[rm=num,num:n][/rm]ת��
                r = new Regex( @"(\[rm=(\d+),(\d+)\:n\])(.+?)(\[\/rm\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<object classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=object id=RAOCX width=$2 height=$3><PARAM NAME=SRC value=\"$4\"><PARAM NAME=CONSOLE VALUE=2004280084352954><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=false></object><br><object classid=clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=$2><PARAM NAME=SRC value=\"$4\"><PARAM NAME=AUTOSTART VALUE=0><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=2004280084352954></object><br />" );

                //[mp=num,num][/mp]ת��
                r = new Regex( @"(\[mp=(\d+),(\d+)\])(.+?)(\[\/mp\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<object align=middle classid=clsid:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=$2 height=$3><param name=ShowStatusBar value=-1><param name=Filename value=\"$4\"><param name=AutoStart value=1><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=\"$4\" width=$2 height=$3></embed></object><br />" );

                //[mp=num,num:y][/mp]ת��
                r = new Regex( @"(\[mp=(\d+),(\d+)\:y\])(.+?)(\[\/mp\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<object align=middle classid=clsid:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=$2 height=$3><param name=ShowStatusBar value=-1><param name=Filename value=\"$4\"><param name=AutoStart value=1><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=\"$4\" width=$2 height=$3></embed></object><br />" );

                //[mp=num,num:n][/mp]ת��
                r = new Regex( @"(\[mp=(\d+),(\d+)\:n\])(.+?)(\[\/mp\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<object align=middle classid=clsid:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=$2 height=$3><param name=ShowStatusBar value=-1><param name=Filename value=\"$4\"><param name=AutoStart value=0><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=\"$4\" width=$2 height=$3></embed></object><br />" );

                //[qt=num,num][/qt]ת��
                r = new Regex( @"(\[qt=(\d+),(\d+)\])(.+?)(\[\/qt\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<embed src=\"$4\" width=$2 height=$3 autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/><br />" );

                //[qt=num,num:y][/qt]ת��
                r = new Regex( @"(\[qt=(\d+),(\d+)\:y\])(.+?)(\[\/qt\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<embed src=\"$4\" width=$2 height=$3 autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/><br />" );

                //[qt=num,num:n][/qt]ת��
                r = new Regex( @"(\[qt=(\d+),(\d+)\:n\])(.+?)(\[\/qt\])", RegexOptions.IgnoreCase );
                a = r.Replace( a, "<embed src=\"$4\" width=$2 height=$3 autoplay=false loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/><br />" );



                #region [movie][/movie]ת��

                r = new Regex( @"(\[movie\])(.+?)(\[\/movie\])", RegexOptions.IgnoreCase );
                MatchCollection Matches = r.Matches( a );

                foreach ( Match m in Matches )
                {
                    if ( m.Groups[0].Value.ToLower().StartsWith( "[movie]http://www.allmov.com/exec/movjs.aspx?" ) )
                    {
                        string oldString = m.Groups[0].Value;
                        string newString = r.Replace( oldString, "<script type=\"text/javascript\" src=\"$2\"></script>" );
                        a = a.Replace( oldString, newString );
                    }
                }

                #endregion

                if ( isconvertemotion )
                {
                    a = Cemotion( a );
                }
            }

            return a;
        }
        #endregion

        /*
		/// <summary>
		/// ת��UBB����
		/// </summary>
		/// <param name="encodedString"></param>
		/// <returns></returns>
		public static string UBBcodeToHtml( string encodedString )
		{
			/*
			string quoteStartHtml = "";
			string quoteEndHtml = "</div></BLOCKQUOTE>";
			string emptyquoteStartHtml = "<BLOCKQUOTE><div>";
			string emptyquoteEndHtml = "</div></BLOCKQUOTE>";


			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
			//color
			encodedString = Regex.Replace ( encodedString, @"\[color=(.+?)\](.+?)\[\/color\]", "<font color=\"$1\">$2</font>", options );
			// Bold, Italic, Underline
			//
			encodedString = Regex.Replace ( encodedString, @"\[b(?:\s*)\]((.|\n)*?)\[/b(?:\s*)\]", "<b>$1</b>", options );
			encodedString = Regex.Replace ( encodedString, @"\[i(?:\s*)\]((.|\n)*?)\[/i(?:\s*)\]", "<i>$1</i>", options );
			encodedString = Regex.Replace ( encodedString, @"\[u(?:\s*)\]((.|\n)*?)\[/u(?:\s*)\]", "<u>$1</u>", options );

			// Left, Right, Center
			encodedString = Regex.Replace ( encodedString, @"\[left(?:\s*)\]((.|\n)*?)\[/left(?:\s*)]", "<div style=\"text-align:left\">$1</div>", options );
			encodedString = Regex.Replace ( encodedString, @"\[center(?:\s*)\]((.|\n)*?)\[/center(?:\s*)]", "<div style=\"text-align:center\">$1</div>", options );
			encodedString = Regex.Replace ( encodedString, @"\[right(?:\s*)\]((.|\n)*?)\[/right(?:\s*)]", "<div style=\"text-align:right\">$1</div>", options );

			// Quote
			//
			//encodedString = Regex.Replace(encodedString, "\\[quote(?:\\s*)user=\"((.|\n)*?)\"\\]((.|\n)*?)\\[/quote(\\s*)\\]", quote, options);
			//encodedString = Regex.Replace(encodedString, "\\[quote(\\s*)\\]((.|\n)*?)\\[/quote(\\s*)\\]", emptyquote, options);
			encodedString = Regex.Replace ( encodedString, "\\[quote(?:\\s*)user=(?:\"|&quot;|&#34;)(.*?)(?:\"|&quot;|&#34;)\\]", quoteStartHtml, options );
			encodedString = Regex.Replace ( encodedString, "\\[/quote(\\s*)\\]", quoteEndHtml, options );
			encodedString = Regex.Replace ( encodedString, "\\[quote(\\s*)\\]", emptyquoteStartHtml, options );
			encodedString = Regex.Replace ( encodedString, "\\[/quote(\\s*)\\]", emptyquoteEndHtml, options );

			// Anchors
			//
			encodedString = Regex.Replace ( encodedString, @"\[url(?:\s*)\]www\.(.*?)\[/url(?:\s*)\]", "<a href=\"http://www.$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace ( encodedString, @"\[url(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace ( encodedString, @"\[url=(?:""|&quot;|&#34;)((.|\n)*?)(?:\s*)(?:""|&quot;|&#34;)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );
			encodedString = Regex.Replace ( encodedString, @"\[url=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );
			encodedString = Regex.Replace ( encodedString, @"\[link(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$1</a>", options );
			encodedString = Regex.Replace ( encodedString, @"\[link=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/link(?:\s*)\]", "<a href=\"$1\" target=\"_blank\" title=\"$1\">$3</a>", options );

			// Image
			//
			encodedString = Regex.Replace ( encodedString, @"\[img(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img src=\"$1\" border=\"0\" />", options );
			encodedString = Regex.Replace ( encodedString, @"\[img=((.|\n)*?)x((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/img(?:\s*)\]", "<img width=\"$1\" height=\"$3\" src=\"$5\" border=\"0\" />", options );
			encodedString = Regex.Replace ( encodedString, @"(\[img=(\d+)\])(.+?)(\[\/img\])", "<img src=\"$3\" border=\"0\" width=\"$2\">", options );

			// Color
			//
			encodedString = Regex.Replace ( encodedString, @"\[color=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/color(?:\s*)\]", "<span style=\"color=$1;\">$3</span>", options );

			// Horizontal Rule
			//
			encodedString = Regex.Replace ( encodedString, @"\[hr(?:\s*)\]", "<hr />", options );

			// Email
			//
			encodedString = Regex.Replace ( encodedString, @"\[email(?:\s*)\]((.|\n)*?)\[/email(?:\s*)\]", "<a href=\"mailto:$1\">$1</a>", options );

			// Font size
			//
			encodedString = Regex.Replace ( encodedString, @"\[size=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/size(?:\s*)\]", "<span style=\"font-size:$1\">$3</span>", options );
			encodedString = Regex.Replace ( encodedString, @"\[font=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/font(?:\s*)\]", "<span style=\"font-family:$1;\">$3</span>", options );
			encodedString = Regex.Replace ( encodedString, @"\[align=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/align(?:\s*)\]", "<div style=\"text-align:$1;\">$3</span>", options );
			encodedString = Regex.Replace ( encodedString, @"\[float=((.|\n)*?)(?:\s*)\]((.|\n)*?)\[/float(?:\s*)\]", "<div style=\"float:$1;\">$3</div>", options );

			string sListFormat = "<ol class=\"anf_list\" style=\"list-style:{0};\">$1</ol>";
			// Lists
			encodedString = Regex.Replace ( encodedString, @"\[\*(?:\s*)]\s*([^\[]*)", "<li>$1</li>", options );
			encodedString = Regex.Replace ( encodedString, @"\[list(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", "<ul class=\"anf_list\">$1</ul>", options );
			encodedString = Regex.Replace ( encodedString, @"\[list=1(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format ( sListFormat, "decimal" ), options );
			encodedString = Regex.Replace ( encodedString, @"\[list=i(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format ( sListFormat, "lower-roman" ), RegexOptions.Compiled );
			encodedString = Regex.Replace ( encodedString, @"\[list=I(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format ( sListFormat, "upper-roman" ), RegexOptions.Compiled );
			encodedString = Regex.Replace ( encodedString, @"\[list=a(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format ( sListFormat, "lower-alpha" ), RegexOptions.Compiled );
			encodedString = Regex.Replace ( encodedString, @"\[list=A(?:\s*)\]((.|\n)*?)\[/list(?:\s*)\]", string.Format ( sListFormat, "upper-alpha" ), RegexOptions.Compiled );

			return encodedString;
		}
			 * 			 * */

        #endregion
        #region ��̨��������
        //ӰƬ������Ϣ�����ṹ����
        public struct SynopsisStruct
        {
            public string Source;
            public string SourceLink;
            public string Content;
            public string EnterUser;
            public string EnterUserNickName;
            public string ApproveUser;
            public string ApproveTime;
            public string ApprovePower;
        }
        //ӰƬ������Ϣ�ṹ��������
        public static SynopsisStruct GetSynopsis(string content)
        {
            //������Ϣ������
            const string Source = "source";
            const string SourceLink = "sourcelink";
            const string Content = "content";
            string ParagraphOldRegex = @"(?:^<Synopsis><Source>)(?<source>[\w\W]*?)(?:</Source>" + Environment.NewLine + @"<Content>)(?<content>[\w\W]*?)(?:</Content>" + Environment.NewLine + @"</Synopsis>$)";
            string ParagraphRegex = @"(?:^<Synopsis><Source>)(?<source>[\w\W]*?)(?:</Source><SourceLink>)(?<sourcelink>[\w\W]*?)(?:</SourceLink><Content>)(?<content>[\w\W]*)(?:</Content></Synopsis>$)";
            //�ṹ��ʼ��
            SynopsisStruct synopsis = new SynopsisStruct();
            synopsis.Source = string.Empty;
            synopsis.SourceLink = string.Empty;
            synopsis.Content = string.Empty;
            //����ƥ����ڽṹ��ʧ�ܺ�ƥ��һ�ڽṹ����ʧ��ȡԭʼ����
            if ( content != null && content != string.Empty )
            {
                MatchCollection Contents = Regex.Matches( content, ParagraphRegex, RegexOptions.None );
                if ( Contents.Count > 0 )
                {
                    synopsis.Source = Contents[0].Groups[Source].Value.Trim();
                    synopsis.SourceLink = Contents[0].Groups[SourceLink].Value.Trim();
                    synopsis.Content = Contents[0].Groups[Content].Value;
                }
                else
                {
                    //����һ�ں�̨������
                    Contents = Regex.Matches( content, ParagraphOldRegex, RegexOptions.None );
                    if ( Contents.Count > 0 )
                    {
                        synopsis.Source = Contents[0].Groups[Source].Value.Trim();
                        synopsis.Content = Contents[0].Groups[Content].Value;
                    }
                    else
                    {
                        synopsis.Content = content;
                    }
                }
            }
            return synopsis;
        }
        //Ӱ�˴��ǽṹ��������
        public static SynopsisStruct[] GetBiography(string content)
        {
            //����������
            const string Source = "source";
            const string SourceLink = "sourcelink";
            const string Content = "content";
            const string EnterUser = "enteruser";
            const string EnterUserNickName = "enterusernickname";
            const string ApproveUser = "approveuser";
            const string ApproveTime = "approvetime";
            const string ApprovePower = "approvepower";
            
            string ParagraphRegex = @"(?:<Bio><Source>)(?<source>[\w\W]*?)(?:</Source><SourceLink>)(?<sourcelink>[\w\W]*?)(?:</SourceLink><Content>)(?<content>[\w\W]*?)(?:</Content></Bio>)";
            string ParagraphRegexV3 = @"(?:<Bio><Source>)(?<source>[\w\W]*?)(?:</Source><SourceLink>)(?<sourcelink>[\w\W]*?)(?:</SourceLink><Content>)(?<content>[\w\W]*?)(?:</Content>" +
                @"<EnterUser>)(?<enteruser>[\w\W]*?)(?:</EnterUser><EnterUserNickName>)(?<enterusernickname>[\w\W]*?)(?:</EnterUserNickName><ApproveUser>)(?<approveuser>[\w\W]*?)(?:</ApproveUser><ApproveTime>)(?<approvetime>[\w\W]*?)(?:</ApproveTime><ApprovePower>)(?<approvepower>[\w\W]*?)(?:</ApprovePower></Bio>)";
            
            bool isMatchSuccess = false;
            SynopsisStruct[] biography = null;
            
            if (!string.IsNullOrEmpty(content))
            {
                //����ƥ�����ڽṹ
                MatchCollection Contents = Regex.Matches(content, ParagraphRegexV3);
                int count = Contents.Count;
                if (count > 0)
                {
                    isMatchSuccess = true;
                    biography = new SynopsisStruct[count];
                    for (int i = 0; i < count; i++)
                    {
                        biography[i].Source = Contents[i].Groups[Source].Value.Trim();
                        biography[i].SourceLink = Contents[i].Groups[SourceLink].Value.Trim();
                        biography[i].Content = Contents[i].Groups[Content].Value;
                        biography[i].EnterUser = Contents[i].Groups[EnterUser].Value;
                        biography[i].EnterUserNickName = Contents[i].Groups[EnterUserNickName].Value;
                        biography[i].ApproveUser = Contents[i].Groups[ApproveUser].Value;
                        biography[i].ApproveTime = Contents[i].Groups[ApproveTime].Value;
                        biography[i].ApprovePower = Contents[i].Groups[ApprovePower].Value;
                    }
                }
                else
                {
                    //ƥ����ڽṹ
                    Contents = Regex.Matches(content, ParagraphRegex);
                    count = Contents.Count;
                    if (count > 0)
                    {
                        isMatchSuccess = true;
                        biography = new SynopsisStruct[count];
                        for (int i = 0; i < count; i++)
                        {
                            biography[i].Source = Contents[i].Groups[Source].Value.Trim();
                            biography[i].SourceLink = Contents[i].Groups[SourceLink].Value.Trim();
                            biography[i].Content = Contents[i].Groups[Content].Value;
                            biography[i].EnterUser = string.Empty;
                            biography[i].EnterUserNickName = string.Empty;
                            biography[i].ApproveUser = string.Empty;
                            biography[i].ApproveTime = string.Empty;
                            biography[i].ApprovePower = string.Empty;
                        }
                    }
                }
            }
            if ( !isMatchSuccess )
            {
                //ȡԭʼ����
                biography = new SynopsisStruct[1];
                biography[0].Source = string.Empty;
                biography[0].SourceLink = string.Empty;
                biography[0].Content = content == null ? string.Empty : content;
                biography[0].EnterUser = string.Empty;
                biography[0].EnterUserNickName = string.Empty;
                biography[0].ApproveUser = string.Empty;
                biography[0].ApproveTime = string.Empty;
                biography[0].ApprovePower = string.Empty;
            }
            return biography;
        }
        #endregion

        public static string GetSourceLink(SynopsisStruct synopsisStruct)
        {
            if ( !string.IsNullOrEmpty( synopsisStruct.Source ) )
            {
                if ( !string.IsNullOrEmpty( synopsisStruct.SourceLink ) )
                {
                    return "<p class=\"tr\"> ���� <a target=\"_blank\" href=\"" + synopsisStruct.SourceLink + "\">" + synopsisStruct.Source + "</a></p>";
                }
                else
                {
                    return "<p class=\"tr\"> ���� " + synopsisStruct.Source + "</p>";
                }
            }
            return string.Empty;
        }

        #region ��ʽ��ʱ��
        /// <summary>
        /// ��ʽ��ʱ��
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatDateTime( DateTime dateTime )
        {
			return dateTime.ToString ( "yyyy-MM-dd HH:mm" );
        }
        #endregion

		public static string TrimRight( string inputString, string removeString )
		{
			if ( inputString.EndsWith ( removeString, StringComparison.CurrentCultureIgnoreCase ) )
				return inputString.TrimEnd ( removeString.ToCharArray () );
			return inputString;
		}
		
		/// <summary>
		/// MD5 encodes the passed string
		/// </summary>
		/// <param name="input">The string to encode.</param>
		/// <returns>An encoded string.</returns>
		public static string MD5String( string input )
		{
			// Create a new instance of the MD5CryptoServiceProvider object.
			MD5 md5Hasher = MD5.Create ();

			// Convert the input string to a byte array and compute the hash.
			byte [] data = md5Hasher.ComputeHash ( Encoding.Default.GetBytes ( input ) );

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder ();

			// Loop through each byte of the hashed data 
			// and format each one as a hexadecimal string.
			for ( int i = 0; i < data.Length; i++ )
			{
				sBuilder.Append ( data [i].ToString ( "x2" ) );
			}

			// Return the hexadecimal string.
			return sBuilder.ToString ();
		}


		public static IList CombinationArrayList( string delimitedString, string delimiter, System.Type baseType )
		{
			ArrayList list = new ArrayList ();
			string [] inputStrings = delimitedString.Split ( delimiter.ToCharArray () );
			foreach ( string inputString in inputStrings )
			{
				if ( baseType.Equals ( typeof ( string ) ) )
					list.Add ( inputString );
				if ( baseType.Equals ( typeof ( int ) ) )
					list.Add ( Convert.ToInt32 ( inputString ) );
			}
			if ( baseType.Equals ( typeof ( int ) ) )
				return ( int [] ) list.ToArray ( typeof ( int ) );
			if ( baseType.Equals ( typeof ( string ) ) )
				return ( string [] ) list.ToArray ( typeof ( string ) );
			return list;
		}
		
		public static string ToDelimitedString( ICollection collection, string delimiter )
		{
			StringBuilder delimitedString = new StringBuilder ();
			if ( collection is Hashtable )
			{
				foreach ( object o in ( ( Hashtable ) collection ).Keys )
				{
					delimitedString.Append ( o.ToString () + delimiter );
				}
			}
			if ( collection is ArrayList )
			{
				foreach ( object o in ( ArrayList ) collection )
				{
					delimitedString.Append ( o.ToString () + delimiter );
				}
			}
			if ( collection is String [] )
			{
				foreach ( string s in ( String [] ) collection )
				{
					delimitedString.Append ( s + delimiter );
				}
			}
			if ( collection is int [] )
			{
				foreach ( int i in ( int [] ) collection )
				{
					delimitedString.Append ( i.ToString () + delimiter );
				}
			}
			if ( collection is List<int> )
			{
				foreach ( int i in collection )
				{
					delimitedString.Append ( i.ToString () + delimiter );
				}
			}
			if ( collection is List<string> )
			{
				foreach ( string i in collection )
				{
					delimitedString.Append ( i.ToString () + delimiter );
				}
			}
			if ( collection is NameValueCollection )
			{
				NameValueCollection o = collection as NameValueCollection;
				for ( int i = 0, count = collection.Count; i < count; i++ )
				{
					delimitedString.Append( o.GetKey( i ) + "=" + o[ i ] + delimiter );
				}
			}
			return delimitedString.ToString ().TrimEnd ( Convert.ToChar ( delimiter ) );
		}

		#region ��WBR�����ַ���

		public static string FormatStringWithWBR(string str)
		{
			return FormatStringWithWBR(str, 10);
		}

		public static string FormatStringWithWBR(string str, int wordBradkSize)
		{
			StringBuilder sb = new StringBuilder();
			int len = str.Length;
			int start = 0;
			int rowStart = 0;
			char nextChar;

			for (int i = 1; i < len; i++)
			{
				nextChar = str[i];

				if (nextChar == ' ' || nextChar == '\t' || nextChar == '\r' || nextChar == '\n' || nextChar == '\0')
				{
					rowStart = i;
				}
				else
				{
					if ((i - rowStart) == wordBradkSize)
					{
						sb.Append(str.Substring(start, i - start));
						sb.Append("<wbr/>");
						start = rowStart = i;
					}
				}
			}

			sb.Append(str.Substring(start));

			return sb.ToString();
		}

		#endregion

		#region �˵��ı��е�Html��ʶ
		/// <summary>
		/// �˵��ı��е�Html��ʶ
		/// </summary>
		/// <param name="HtmlCode">Դ�ı�</param>
		/// <returns></returns>
		public static string NoHtml(string HtmlCode)
		{
			if (string.IsNullOrEmpty(HtmlCode))
			{
				return string.Empty;
			}
			return Regex.Replace(HtmlCode, "<[^<^>]*?>", "");
		}
		#endregion


		/// <summary>
		/// �����µ�href���Զ��滻Ϊ��
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
		public static string GetNoArchorBody(string html)
		{
			Regex reg = new Regex("<a.+?>|</a>", RegexOptions.IgnoreCase);

			return reg.Replace(html, string.Empty);
		}
	}
}
