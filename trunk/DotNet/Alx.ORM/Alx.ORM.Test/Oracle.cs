using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alx.ORM.Core;
using System.Data;
using System.Data.Common;

namespace Alx.ORM.Test
{
    public class TestOracle : ITest
    {
        string cnstr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.119.120.103)(PORT=1522)))(CONNECT_DATA=(SERVICE_NAME=oraeleven)));User Id=hr_platform;Password=HR_PF;";

        public void Test()
        {
            TestSelect();
        }

        public void TestSelect()
        {
            var db = new ObjectContext(cnstr);
            var a = db.Query<SYS_USER>();
            a = db.Query<SYS_USER>("name = ? ", new object[] { "lxing" });
        }

        public void TestInsert()
        {
            var db = new ObjectContext(cnstr);
            var obj = new SYS_USER();
            obj.AreaCode = "123456";
            obj.ID = Guid.NewGuid();
            obj.Name = "lxing";
            obj.Password = "qwqwq";
            db.Insert(obj);
        }

        public void Test1()
        {
            var Provider = new Oracle.DataAccess.Client.OracleClientFactory();
            var Connection = Provider.CreateConnection();
            Connection.ConnectionString = cnstr;
            var cmd = Provider.CreateCommand();
            cmd.CommandText = "insert into test (id) values (:a) ";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = Connection;
            var par = Provider.CreateParameter();
            par.Direction = ParameterDirection.Input;
            par.ParameterName = ":a";
            par.DbType = DbType.Binary;
            par.Value = Guid.NewGuid().ToByteArray();
            cmd.Parameters.Add(par);
            Connection.Open();
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
    }

    [Tabel(Name = "SYS_USER")]
    public class SYS_USER : TableBase
    {
        private System.Guid _id;
        [Column(Name = "ID", DbType = DbType.Binary, Nullable = false, IsPrimary = true)]
        public System.Guid ID { get { return _id; } set { _id = value; } }

        private System.String _name;
        [Column(Name = "NAME", DbType = DbType.String, Nullable = true, IsPrimary = false)]
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private System.String _password;
        [Column(Name = "PASSWORD", DbType = DbType.String, Nullable = true, IsPrimary = false)]
        public System.String Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private System.String _remark;
        [Column(Name = "REMARK", DbType = DbType.String, Nullable = true, IsPrimary = false)]
        public System.String Reamrk
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
            }
        }

        private System.String _area_code;
        [Column(Name = "AREA_CODE", DbType = DbType.String, Nullable = true, IsPrimary = false)]
        public System.String AreaCode
        {
            get
            {
                return _area_code;
            }
            set
            {
                _area_code = value;
            }
        }

    }
}
