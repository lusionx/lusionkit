using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
namespace Portal.Facilities
{
    public class UserCookies
    {
        /// <summary>
        /// 返回主站用户ID
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static int GetEJJJUserID()
        {
            //return 5;
            string idCard = "";
            if (HttpContext.Current.Request.Cookies["Coop"] != null)
            {
                idCard = HttpContext.Current.Request.Cookies["Coop"]["CoopUCardID"];
            }
            int user_id = 0;
            if (idCard.Length != 20)
                return 0;
            try
            {
                user_id = Convert.ToInt32(idCard.Substring(8, 8));
            }
            catch 
            {
               
            }
       
            return user_id;



        }
        /// <summary>
        /// 返回主站用户类别
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static int GetEJJJUserType()
        {
          
            string idCard = "";
            if (HttpContext.Current.Request.Cookies["Coop"] != null)
            {
                idCard = HttpContext.Current.Request.Cookies["Coop"]["CoopUCardID"];
            }
            if (idCard.Length != 20)
                return 0;

            string card = idCard.Substring(idCard.Length - 4);
            
            if (card.Substring(0, 2) == "10")//企业
            {
                return 1;
            }
            else if (card.Substring(0, 2) == "01")//个人
            {

                return 0;
            }
            
            return 0;
           
        }
    }
}
