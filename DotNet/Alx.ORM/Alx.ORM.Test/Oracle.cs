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
        public void Test()
        {
            //TestInsert();
            //TestDelete();
            //TestSelect();
            TestUpdate();

        }

        public void TestUpdate()
        {
            string cnstr = @"Data Source=dev;User Id=HR_PPORTAL;Password=1qaz2wsx;";
            var db = new ObjectContext(cnstr);
        }

        public void TestDelete()
        {
            string cnstr = @"Data Source=dev;User Id=HR_PPORTAL;Password=1qaz2wsx;";

        }

        public void TestSelect()
        {
            string cnstr = @"Data Source=dev;User Id=HR_PPORTAL;Password=1qaz2wsx;";


        }

        public void TestInsert()
        {
            string cnstr = @"Data Source=dev;User Id=HR_PPORTAL;Password=1qaz2wsx;";

        }

        public void Test1()
        {

        }
    }

    [Tabel(Name = "TMP_TEST")]
    public partial class TMP_TEST : TableBase
    {
        private System.Guid _person_key;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "PERSON_KEY", DbType = DbType.Binary, Nullable = false, IsPrimary = true)]
        public System.Guid PERSON_KEY { get { return _person_key; } set { if (_person_key != value) { _person_key = value; ChangeColumn.Add("PERSON_KEY"); } } }

        private System.String _name;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "NAME", DbType = DbType.String, Nullable = false)]
        public System.String NAME { get { return _name; } set { if (_name != value) { _name = value; ChangeColumn.Add("NAME"); } } }

        private System.DateTime _createdate;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "CREATEDATE", DbType = DbType.DateTime, Nullable = false)]
        public System.DateTime CREATEDATE { get { return _createdate; } set { if (_createdate != value) { _createdate = value; ChangeColumn.Add("CREATEDATE"); } } }

        private System.String _visible;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "VISIBLE", DbType = DbType.String, Nullable = false)]
        public System.String VISIBLE { get { return _visible; } set { if (_visible != value) { _visible = value; ChangeColumn.Add("VISIBLE"); } } }

        private System.Decimal _age;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "AGE", DbType = DbType.Decimal, Nullable = false, DefaultValue = DD.fu1)]
        public System.Decimal AGE { get { return _age; } set { if (_age != value) { _age = value; ChangeColumn.Add("AGE"); } } }
    }




    public class DD : IDefultVal
    {
        public const object fu1 = null;

        public Func<object> ss()
        {
            Func<object> fn = null;
            fn = () => 1;
            return fn;
        }

        public object GetVal()
        {

            return -1;
        }
    }

}