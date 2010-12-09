using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Reflection;

namespace Tool
{
    public static class TExend
    {
        public static bool In<TSource>(this TSource t, params TSource[] c) where TSource : IEquatable<TSource>
        {
            foreach (var x in c)
            {
                if (x.Equals(t))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool In<TSource>(this TSource t, IEnumerable<TSource> c) where TSource : IEquatable<TSource>
        {
            foreach (var x in c)
            {
                if (x.Equals(t))
                {
                    return true;
                }
            }
            return false;
        }
        public static string FormatEach<TSource>(this IEnumerable<TSource> ie, string format) where TSource : new()
        {            
            List<PropertyInfo> names = typeof(TSource).GetProperties().ToList();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ArrayList ar = new ArrayList();
            foreach (var x in ie)
            {
                ar.Clear();
                foreach (var a in names)
                {
                    ar.Add(a.GetValue(x, null));
                }
                sb.AppendFormat(format, ar.ToArray());
            }
            return sb.ToString();

        }
        public static string FormatEach(this IEnumerable ie, string format)
        {
            bool i = true;
            List<PropertyInfo> names = new List<PropertyInfo>();
            foreach (var x in ie)
            {
                if (i)
                {
                    foreach (var a in x.GetType().GetProperties())
                    {
                        names.Add(a);
                    }
                    break;
                }
                else
                {
                    return string.Empty;
                }
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            ArrayList ar = new ArrayList();
            foreach (var x in ie)
            {
                ar.Clear();
                foreach (var a in names)
                {
                    ar.Add(a.GetValue(x, null));
                }
                sb.AppendFormat(format, ar.ToArray());
            }
            return sb.ToString();

        }        
    }

    public static class Exendx
    {
        public static Color FromString(this Color col, string str)
        {
            str = str.TrimStart('#');
            return Color.FromArgb(Convert.ToInt32(str.Substring(0, 2), 16),
                Convert.ToInt32(str.Substring(2, 2), 16),
                Convert.ToInt32(str.Substring(4, 2), 16));
        }

        public static IEnumerable<FileInfo> GetAllFiles(this DirectoryInfo root, string searchPattern)
        {
            foreach (DirectoryInfo dir in root.GetDirectories())
            {
                foreach (FileInfo file in dir.GetAllFiles(searchPattern))
                {
                    yield return file;
                }
            }
            foreach (FileInfo file in root.GetFiles(searchPattern, SearchOption.TopDirectoryOnly))
            {
                yield return file;
            }
        }

        public static IEnumerable<FileInfo> GetAllFiles(this DirectoryInfo root)
        {
            foreach (DirectoryInfo dir in root.GetDirectories())
            {
                foreach (FileInfo file in GetAllFiles(dir))
                {
                    yield return file;
                }
            }
            foreach (FileInfo file in root.GetFiles())
            {
                yield return file;
            }
        }
    }
    public static class DateTimeExtend
    {
        public static DateTime MinSQLDateTime(this DateTime dt)
        {
            return new DateTime(1753, 1, 1, 0, 0, 0);
        }
        public static DateTime MaxSQLDateTime(this DateTime dt)
        {
            return DateTime.MaxValue;
        }
        public static DateTime MinSQLSamllDateTime(this DateTime dt)
        {
            return new DateTime(1900, 1, 1, 0, 0, 0);
        }

        public static DateTime MaxSQLSamllDateTime(this DateTime dt)
        {
            return new DateTime(2076, 6, 6, 23, 59, 0);
        }

    }


    public static class StringExtend
    {
        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source.ToArray());
        }

        public static DateTime ToDateTime(this string str)
        {
            return SafeConvert.ToDateTime(str);
        }

        public static int ToInt32(this string str)
        {
            return SafeConvert.ToInt32(str);
        }

        public static byte ToByte(this string str)
        {
            return SafeConvert.ToByte(str);
        }

        public static char Char(this string str)
        {
            if (str != null && str.Length == 1)
            {
                return str.ToCharArray()[0];
            }
            else
            {
                throw new IndexOutOfRangeException("长度不为1");
            }
        }

        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static string FormatWith(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        public static string FormatWith(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        public static string FormatWith(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }

    public static class EnumExtend
    {
        public static Tenum ToEnum<Tenum>(this int i)
        {
            return (Tenum)Enum.ToObject(typeof(Tenum), i);
        }

        public static Tenum ToEnum<Tenum>(this string i)
        {
            return (Tenum)Enum.Parse(typeof(Tenum), i, true);
        }
    }


}
