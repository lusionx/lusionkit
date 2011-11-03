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
            TestDelete();
            //TestSelect();

        }

        public void TestDelete()
        {
            string cnstr = @"Data Source=guolei;User Id=HR_PPORTAL;Password=1qaz2wsx;";
            var db = new ObjectContext(cnstr);
            db.Delete(new HEALTH_REMIND_ASYNC { ID = new Guid("63ce7844-5d73-401e-b1e4-bee8b31dc9b0") });
        }

        public void TestSelect()
        {
            string cnstr = @"Data Source=guolei;User Id=HR_PPORTAL;Password=1qaz2wsx;";
            var db = new ObjectContext(cnstr);
            var a = db.Query<INF_PERSON>("", null, 0, 1);

        }

        public void TestInsert()
        {
            string cnstr = @"Data Source=guolei;User Id=HR_PPORTAL;Password=1qaz2wsx;";
            var db = new ObjectContext(cnstr);
            var obj = new SYS_USER()
            {
                CREATEDATE = DateTime.Now,
                EMAIL = "aaaa"
            };

            db.Insert(obj);
        }

        public void Test1()
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Tabel(Name = "SYS_USER")]
    public class SYS_USER : TableBase
    {
        public string Txt { get; set; }

        private System.Guid _person_key;
        /// <summary>
        /// 注册用户的用户编号（主键）
        /// </summary>
        [Column(Name = "PERSON_KEY", DbType = DbType.Binary, Nullable = false, IsPrimary = true)]
        public System.Guid PERSON_KEY { get { return _person_key; } set { _person_key = value; } }

        private System.String _name;
        /// <summary>
        /// 注册用户的用户名
        /// </summary>
        [Column(Name = "NAME", DbType = DbType.String, Nullable = false)]
        public System.String NAME { get { return _name; } set { _name = value; } }

        private System.String _password;
        /// <summary>
        /// 注册用户的密码
        /// </summary>
        [Column(Name = "PASSWORD", DbType = DbType.String, Nullable = false)]
        public System.String PASSWORD { get { return _password; } set { _password = value; } }

        private System.String _tel;
        /// <summary>
        /// 用户的电话号码（必填项）
        /// </summary>
        [Column(Name = "TEL", DbType = DbType.String, Nullable = false)]
        public System.String TEL { get { return _tel; } set { _tel = value; } }

        private System.String _email;
        /// <summary>
        /// 用户的邮箱
        /// </summary>
        [Column(Name = "EMAIL", DbType = DbType.String, Nullable = true)]
        public System.String EMAIL { get { return _email; } set { _email = value; } }

        private System.DateTime? _createdate;
        /// <summary>
        /// 用户注册日期
        /// </summary>
        [Column(Name = "CREATEDATE", DbType = DbType.DateTime, Nullable = true)]
        public System.DateTime? CREATEDATE { get { return _createdate; } set { _createdate = value; } }

        private System.String _hr_code;
        /// <summary>
        /// 健康档案编号
        /// </summary>
        [Column(Name = "HR_CODE", DbType = DbType.String, Nullable = true)]
        public System.String HR_CODE { get { return _hr_code; } set { _hr_code = value; } }

        private System.String _id_no;
        /// <summary>
        /// 身份证号
        /// </summary>
        [Column(Name = "ID_NO", DbType = DbType.String, Nullable = true)]
        public System.String ID_NO { get { return _id_no; } set { _id_no = value; } }

        private System.String _region;
        /// <summary>
        /// 所在县行政区划编码
        /// </summary>
        [Column(Name = "REGION", DbType = DbType.String, Nullable = false)]
        public System.String REGION { get { return _region; } set { _region = value; } }

        private System.String _real_name;
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Column(Name = "REAL_NAME", DbType = DbType.String, Nullable = true)]
        public System.String REAL_NAME { get { return _real_name; } set { _real_name = value; } }

    }


    /// <summary>
    /// 个人健康档案基本信息copy 其中的本系统中没有字典表, 引用的各种code 同公服, 算是一种约定
    /// </summary>
    [Tabel(Name = "INF_PERSON")]
    public class INF_PERSON : TableBase
    {
        private System.Guid _person_key;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "PERSON_KEY", DbType = DbType.Binary, Nullable = false, IsPrimary = true)]
        public System.Guid PERSON_KEY { get { return _person_key; } set { _person_key = value; } }

        private System.String _hr_code;
        /// <summary>
        /// 健康档案编号      村/街道行政区划编码(12) + 顺序号(6)
        /// </summary>
        [Column(Name = "HR_CODE", DbType = DbType.String, Nullable = false)]
        public System.String HR_CODE { get { return _hr_code; } set { _hr_code = value; } }

        private System.String _hr_id;
        /// <summary>
        /// 健康档案标识符  建档机构组织机构主体码(8).日期(8).顺序号(5)
        /// </summary>
        [Column(Name = "HR_ID", DbType = DbType.String, Nullable = false)]
        public System.String HR_ID { get { return _hr_id; } set { _hr_id = value; } }

        private System.String _name;
        /// <summary>
        /// 姓名
        /// </summary>
        [Column(Name = "NAME", DbType = DbType.String, Nullable = false)]
        public System.String NAME { get { return _name; } set { _name = value; } }

        private System.String _gender;
        /// <summary>
        /// 性别  DIC_GENDER
        /// </summary>
        [Column(Name = "GENDER", DbType = DbType.String, Nullable = false)]
        public System.String GENDER { get { return _gender; } set { _gender = value; } }

        private System.DateTime _birthday;
        /// <summary>
        /// 出生时间, 可能不真实
        /// </summary>
        [Column(Name = "BIRTHDAY", DbType = DbType.DateTime, Nullable = false)]
        public System.DateTime BIRTHDAY { get { return _birthday; } set { _birthday = value; } }

        private System.String _id_no;
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Column(Name = "ID_NO", DbType = DbType.String, Nullable = true)]
        public System.String ID_NO { get { return _id_no; } set { _id_no = value; } }

        private System.String _work_unit;
        /// <summary>
        /// 工作单位,   下岗待业或无工作经历者须具体注明
        /// </summary>
        [Column(Name = "WORK_UNIT", DbType = DbType.String, Nullable = true)]
        public System.String WORK_UNIT { get { return _work_unit; } set { _work_unit = value; } }

        private System.String _tel;
        /// <summary>
        /// 本人电话
        /// </summary>
        [Column(Name = "TEL", DbType = DbType.String, Nullable = true)]
        public System.String TEL { get { return _tel; } set { _tel = value; } }

        private System.String _contact_name;
        /// <summary>
        /// 重要联系人姓名
        /// </summary>
        [Column(Name = "CONTACT_NAME", DbType = DbType.String, Nullable = true)]
        public System.String CONTACT_NAME { get { return _contact_name; } set { _contact_name = value; } }

        private System.String _contact_tel;
        /// <summary>
        /// 联系人电话
        /// </summary>
        [Column(Name = "CONTACT_TEL", DbType = DbType.String, Nullable = true)]
        public System.String CONTACT_TEL { get { return _contact_tel; } set { _contact_tel = value; } }

        private System.String _rsopr_code;
        /// <summary>
        /// 常住地址户籍标志  DIC_RSOPR
        /// </summary>
        [Column(Name = "RSOPR_CODE", DbType = DbType.String, Nullable = true)]
        public System.String RSOPR_CODE { get { return _rsopr_code; } set { _rsopr_code = value; } }

        private System.String _nation_code;
        /// <summary>
        /// 民族代码  DIC_NATION
        /// </summary>
        [Column(Name = "NATION_CODE", DbType = DbType.String, Nullable = true)]
        public System.String NATION_CODE { get { return _nation_code; } set { _nation_code = value; } }

        private System.String _abo_code;
        /// <summary>
        /// ABO 血型代码  DIC_BLOOD_ABO
        /// </summary>
        [Column(Name = "ABO_CODE", DbType = DbType.String, Nullable = true)]
        public System.String ABO_CODE { get { return _abo_code; } set { _abo_code = value; } }

        private System.String _rh_code;
        /// <summary>
        /// RH 阴性代码    DIC_BLOOD_RH
        /// </summary>
        [Column(Name = "RH_CODE", DbType = DbType.String, Nullable = true)]
        public System.String RH_CODE { get { return _rh_code; } set { _rh_code = value; } }

        private System.String _education_code;
        /// <summary>
        /// 文化程度 DIC_EDUCATION
        /// </summary>
        [Column(Name = "EDUCATION_CODE", DbType = DbType.String, Nullable = true)]
        public System.String EDUCATION_CODE { get { return _education_code; } set { _education_code = value; } }

        private System.String _occupation_code;
        /// <summary>
        /// 职业  DIC_OCCUPATION
        /// </summary>
        [Column(Name = "OCCUPATION_CODE", DbType = DbType.String, Nullable = true)]
        public System.String OCCUPATION_CODE { get { return _occupation_code; } set { _occupation_code = value; } }

        private System.String _marriage_code;
        /// <summary>
        /// 婚姻状态  DIC_MARRIAGE
        /// </summary>
        [Column(Name = "MARRIAGE_CODE", DbType = DbType.String, Nullable = true)]
        public System.String MARRIAGE_CODE { get { return _marriage_code; } set { _marriage_code = value; } }

        private System.String _area_code;
        /// <summary>
        /// 所处行政区划编码, 精确到村/街道(12)   DIC_AREA
        /// </summary>
        [Column(Name = "AREA_CODE", DbType = DbType.String, Nullable = true)]
        public System.String AREA_CODE { get { return _area_code; } set { _area_code = value; } }

        private System.String _area_groupcode;
        /// <summary>
        /// 行政区划组编码(2), 创建时界面填写, 不足两位前补零
        /// </summary>
        [Column(Name = "AREA_GROUPCODE", DbType = DbType.String, Nullable = true)]
        public System.String AREA_GROUPCODE { get { return _area_groupcode; } set { _area_groupcode = value; } }

        private System.String _address;
        /// <summary>
        /// 住址
        /// </summary>
        [Column(Name = "ADDRESS", DbType = DbType.String, Nullable = true)]
        public System.String ADDRESS { get { return _address; } set { _address = value; } }

        private System.String _origin_area_code;
        /// <summary>
        /// 籍贯     县或县级以上行政区划编码  DIC_AREA
        /// </summary>
        [Column(Name = "ORIGIN_AREA_CODE", DbType = DbType.String, Nullable = true)]
        public System.String ORIGIN_AREA_CODE { get { return _origin_area_code; } set { _origin_area_code = value; } }

        private System.String _birth_area_code;
        /// <summary>
        /// 出生地 县或县级以上行政区划编码  DIC_AREA
        /// </summary>
        [Column(Name = "BIRTH_AREA_CODE", DbType = DbType.String, Nullable = true)]
        public System.String BIRTH_AREA_CODE { get { return _birth_area_code; } set { _birth_area_code = value; } }

        private System.String _registered_area_code;
        /// <summary>
        /// 户籍地 县或县级以上行政区划编码  DIC_AREA
        /// </summary>
        [Column(Name = "REGISTERED_AREA_CODE", DbType = DbType.String, Nullable = true)]
        public System.String REGISTERED_AREA_CODE { get { return _registered_area_code; } set { _registered_area_code = value; } }

        private System.String _is_public;
        /// <summary>
        /// 公开与否 0 不公开 1 公开
        /// </summary>
        [Column(Name = "IS_PUBLIC", DbType = DbType.String, Nullable = true)]
        public System.String IS_PUBLIC { get { return _is_public; } set { _is_public = value; } }

        private System.String _status;
        /// <summary>
        /// 状态  0 录入 1 启用 2 作废
        /// </summary>
        [Column(Name = "STATUS", DbType = DbType.String, Nullable = true)]
        public System.String STATUS { get { return _status; } set { _status = value; } }

        private System.DateTime _date_create;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Name = "DATE_CREATE", DbType = DbType.DateTime, Nullable = false)]
        public System.DateTime DATE_CREATE { get { return _date_create; } set { _date_create = value; } }

        private System.DateTime _date_update;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Column(Name = "DATE_UPDATE", DbType = DbType.DateTime, Nullable = false)]
        public System.DateTime DATE_UPDATE { get { return _date_update; } set { _date_update = value; } }

        private System.String _remark;
        /// <summary>
        /// 备注
        /// </summary>
        [Column(Name = "REMARK", DbType = DbType.String, Nullable = true)]
        public System.String REMARK { get { return _remark; } set { _remark = value; } }

        private System.String _name_py;
        /// <summary>
        /// 姓名拼音码
        /// </summary>
        [Column(Name = "NAME_PY", DbType = DbType.String, Nullable = true)]
        public System.String NAME_PY { get { return _name_py; } set { _name_py = value; } }

        private System.String _is_perfect;
        /// <summary>
        /// 是否完善 0 否 1 是
        /// </summary>
        [Column(Name = "IS_PERFECT", DbType = DbType.String, Nullable = true)]
        public System.String IS_PERFECT { get { return _is_perfect; } set { _is_perfect = value; } }

        private System.String _is_lock;
        /// <summary>
        /// 是否锁定 0 否 1 是
        /// </summary>
        [Column(Name = "IS_LOCK", DbType = DbType.String, Nullable = true)]
        public System.String IS_LOCK { get { return _is_lock; } set { _is_lock = value; } }

        private System.String _reg_area_groupcode;
        /// <summary>
        /// 户籍地组编码
        /// </summary>
        [Column(Name = "REG_AREA_GROUPCODE", DbType = DbType.String, Nullable = true)]
        public System.String REG_AREA_GROUPCODE { get { return _reg_area_groupcode; } set { _reg_area_groupcode = value; } }

        private System.String _reg_address;
        /// <summary>
        /// 户籍地址
        /// </summary>
        [Column(Name = "REG_ADDRESS", DbType = DbType.String, Nullable = true)]
        public System.String REG_ADDRESS { get { return _reg_address; } set { _reg_address = value; } }

        private System.String _is_dead;
        /// <summary>
        /// 是否死亡 0 否 1 是
        /// </summary>
        [Column(Name = "IS_DEAD", DbType = DbType.String, Nullable = true)]
        public System.String IS_DEAD { get { return _is_dead; } set { _is_dead = value; } }

        private System.DateTime? _death_date;
        /// <summary>
        /// 死亡时间
        /// </summary>
        [Column(Name = "DEATH_DATE", DbType = DbType.DateTime, Nullable = true)]
        public System.DateTime? DEATH_DATE { get { return _death_date; } set { _death_date = value; } }

        private System.Guid _responsible_mperson;
        /// <summary>
        /// 责任医生 INF_MPERSON.MPERSON_KEY
        /// </summary>
        [Column(Name = "RESPONSIBLE_MPERSON", DbType = DbType.Binary, Nullable = true)]
        public System.Guid RESPONSIBLE_MPERSON { get { return _responsible_mperson; } set { _responsible_mperson = value; } }

        private System.String _private_code;
        /// <summary>
        /// 自编码
        /// </summary>
        [Column(Name = "PRIVATE_CODE", DbType = DbType.String, Nullable = true)]
        public System.String PRIVATE_CODE { get { return _private_code; } set { _private_code = value; } }

        private System.String _father_name;
        /// <summary>
        /// 父亲名字
        /// </summary>
        [Column(Name = "FATHER_NAME", DbType = DbType.String, Nullable = true)]
        public System.String FATHER_NAME { get { return _father_name; } set { _father_name = value; } }

        private System.String _mother_name;
        /// <summary>
        /// 母亲名字
        /// </summary>
        [Column(Name = "MOTHER_NAME", DbType = DbType.String, Nullable = true)]
        public System.String MOTHER_NAME { get { return _mother_name; } set { _mother_name = value; } }

        private System.String _spouse_name;
        /// <summary>
        /// 配偶名字
        /// </summary>
        [Column(Name = "SPOUSE_NAME", DbType = DbType.String, Nullable = true)]
        public System.String SPOUSE_NAME { get { return _spouse_name; } set { _spouse_name = value; } }

    }


    /// <summary>
    /// 从公服同步的 个人健康提醒
    /// </summary>
    [Tabel(Name = "HEALTH_REMIND_ASYNC")]
    public partial class HEALTH_REMIND_ASYNC : TableBase
    {
        private System.Guid _id;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "ID", DbType = DbType.Binary, Nullable = false, IsPrimary = true)]
        public System.Guid ID { get { return _id; } set { _id = value; } }

        private System.Guid _person_key;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "PERSON_KEY", DbType = DbType.Binary, Nullable = false)]
        public System.Guid PERSON_KEY { get { return _person_key; } set { _person_key = value; } }

        private System.String _doctor_name;
        /// <summary>
        /// 随访医生
        /// </summary>
        [Column(Name = "DOCTOR_NAME", DbType = DbType.String, Nullable = false)]
        public System.String DOCTOR_NAME { get { return _doctor_name; } set { _doctor_name = value; } }

        private System.DateTime _date_update;
        /// <summary>
        /// 
        /// </summary>
        [Column(Name = "DATE_UPDATE", DbType = DbType.DateTime, Nullable = false)]
        public System.DateTime DATE_UPDATE { get { return _date_update; } set { _date_update = value; } }

        private System.DateTime _deadline;
        /// <summary>
        /// 截止时间
        /// </summary>
        [Column(Name = "DEADLINE", DbType = DbType.DateTime, Nullable = false)]
        public System.DateTime DEADLINE { get { return _deadline; } set { _deadline = value; } }

        private System.Guid _exam_key;
        /// <summary>
        /// 对应公服的随访exam_key
        /// </summary>
        [Column(Name = "EXAM_KEY", DbType = DbType.Binary, Nullable = false)]
        public System.Guid EXAM_KEY { get { return _exam_key; } set { _exam_key = value; } }

        private System.String _type;
        /// <summary>
        /// (精神病:1)(高血压:2)(糖尿病:3)(混合:4)
        /// </summary>
        [Column(Name = "TYPE", DbType = DbType.String, Nullable = false)]
        public System.String TYPE { get { return _type; } set { _type = value; } }

    }

}
