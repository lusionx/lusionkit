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
        string cnstr = "Data Source=sc1;User Id=hr_sc_spt;Password=1qaz2wsx;";

        public void Test()
        {
            //TestInsert();
            TestSelect();

        }

        public void TestSelect()
        {
            var db = new ObjectContext(cnstr);
            var a = db.Query<ALXTEST>();
            var dd = 12321m;
            dd = 0;
        }

        public void TestInsert()
        {
            var db = new ObjectContext(cnstr);
            var obj = new ALXTEST();
            obj.Code = "1234";
            obj.Count = 12321;
            obj.DATE_CREATE = DateTime.Now;
            obj.Id = Guid.NewGuid();
            obj.Name = "类型";
            obj.Cc2 = 123.33M;
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

    [Tabel(Name = "ALXTEST")]
    public class ALXTEST : TableBase
    {
        private const decimal _defult = 0;

        private System.Guid _id;
        [Column(Name = "ID", DbType = DbType.Binary, Nullable = false, IsPrimary = true)]
        public System.Guid Id { get { return _id; } set { _id = value; } }

        private System.String _name;
        [Column(Name = "NAME", DbType = DbType.String, Nullable = true, IsPrimary = false)]
        public System.String Name { get { return _name; } set { _name = value; } }

        private System.String _code;
        [Column(Name = "CODE", DbType = DbType.String, Nullable = false, IsPrimary = false)]
        public System.String Code { get { return _code; } set { _code = value; } }

        private System.DateTime _date_create;
        [Column(Name = "DATE_CREATE", DbType = DbType.DateTime, Nullable = false, IsPrimary = false)]
        public System.DateTime DATE_CREATE { get { return _date_create; } set { _date_create = value; } }

        private System.Decimal _count;
        [Column(Name = "COUNT", DbType = DbType.Decimal, Nullable = false, IsPrimary = false, DefaultValue = 0)]
        public System.Decimal Count { get { return _count; } set { _count = value; } }

        private System.Decimal _cc2;
        [Column(Name = "CC2", DbType = DbType.Decimal, Nullable = false, IsPrimary = false, DefaultValue = 0)]
        public System.Decimal Cc2 { get { return _cc2; } set { _cc2 = value; } }

    }
}
