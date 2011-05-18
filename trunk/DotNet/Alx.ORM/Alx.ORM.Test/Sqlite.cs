using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alx.ORM.Core;

namespace Alx.ORM.Test
{
    public class TestSqlite
    {
        public void Test()
        {
            var str = "Data Source=D:\\WebApp\\gae\\myApp\\client\\info.sqlite3;Version=3;";
            ObjectContext db = new ObjectContext(str);
            //foreach (var u in db.Query<User>(a => a.Name == "lxing"))
            //{
            //    //Console.WriteLine(u.Name);
            //}
            foreach (var u in db.Query<Ts_user>())
            {
                //Console.WriteLine(u.Name);
            }
            object[] pars = { "%x%" };
            foreach (var u in db.Query<Ts_user>("name like ? ", pars))
            {
                Console.WriteLine(u.Name);
            }
            //Console.Read();

            var u1 = new Ts_user
            {
                Name = "lxing",
                Age = 100,
                Id = 101,
                Password = "wwwww22"
            };
            db.Update(u1);
        }
    }
}
