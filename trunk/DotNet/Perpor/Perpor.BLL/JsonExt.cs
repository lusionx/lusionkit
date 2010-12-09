using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Perpor.BLL
{
    public static class JsonExt
    {
        public static string ToJson(this object item)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(item);
        }
        public static string ToJsJson2(this object item)
        {

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);

                StringBuilder sb = new StringBuilder();

                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));

                return sb.ToString();

            }

        }

        /// <summary>
        /// Json反序列化,用于接收客户端Json后生成对应的对象
        /// </summary>
        public static T FromJsonTo<T>(this string jsonString)
        {

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            T jsonObject = (T)ser.ReadObject(ms);

            ms.Close();

            return jsonObject;

        }

    }
}
