using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alx.ORM.Core;
using System.Data;

namespace Alx.ORM.Test
{

    [Tabel(Name = "ts_user")]
    public class Ts_user : TableBase
    {
        private System.Int32 _id;
        [Column(Name = "id", DbType = DbType.Int32, Nullable = false, IsPrimary = true)]
        public System.Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private System.String _name;
        [Column(Name = "name", DbType = DbType.String, Nullable = true, IsPrimary = false)]
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

        private System.Int32 _age;
        [Column(Name = "age", DbType = DbType.Int32, Nullable = true, IsPrimary = false)]
        public System.Int32 Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }

        private System.String _password;
        [Column(Name = "password", DbType = DbType.String, Nullable = true, IsPrimary = false)]
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

    }

    [Tabel(Name = "ct_carton")]
    public class Ct_carton : TableBase
    {
        private string _name;
        [Column(Name = "name", DbType = DbType.Int32, Nullable = false, IsPrimary = true)]
        public string Name
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

        private string _name_en;
        [Column(Name = "name_en", DbType = DbType.Int32, Nullable = false, IsPrimary = false)]
        public string Name_en
        {
            get
            {
                return _name_en;
            }
            set
            {
                _name_en = value;
            }
        }

        private int _week;
        [Column(Name = "week", DbType = DbType.Int32, Nullable = false, IsPrimary = false)]
        public int Week
        {
            get
            {
                return _week;
            }
            set
            {
                _week = value;
            }
        }

        private string _remark;
        [Column(Name = "remark", DbType = DbType.Int32, Nullable = false, IsPrimary = false)]
        public string Remark
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

        private string _labels;
        [Column(Name = "labels", DbType = DbType.Int32, Nullable = false, IsPrimary = false)]
        public string Labels
        {
            get
            {
                return _labels;
            }
            set
            {
                _labels = value;
            }
        }

        private int _episode;
        [Column(Name = "episode", DbType = DbType.Int32, Nullable = false, IsPrimary = false)]
        public int Episode
        {
            get
            {
                return _episode;
            }
            set
            {
                _episode = value;
            }
        }

        private string _start;
        [Column(Name = "start", DbType = DbType.Int32, Nullable = false, IsPrimary = false)]
        public string Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        private string _last;
        [Column(Name = "last", DbType = DbType.Int32, Nullable = true, IsPrimary = false)]
        public string Last
        {
            get
            {
                return _last;
            }
            set
            {
                _last = value;
            }
        }

    }

}