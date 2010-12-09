using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alxf.Frame.ModelBase
{
    /// <summary>
    /// BM的属性验证,此处是基类,尽量不要直接用此类进行修饰,如果不满足要求,可以增加子类
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateAttribute : Attribute
    {
        /// <summary>
        /// 当值为空字符串的时候,的默认值
        /// </summary>
        public virtual string Default { get; set; }

        /// <summary>
        /// 正则验证表达式,按照js的语法写
        /// </summary>
        public virtual string Function { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public virtual bool IsRequired { get; set; }

        /// <summary>
        /// 验证不通过时,显示的提示信息
        /// </summary>
        public virtual string ErrorMessage { get; set; }
    }

    public class ValidateStringAttribute : ValidateAttribute
    {
        public int MaxLength { get; set; }

        public int MinLength { get; set; }

        public override string Function
        {
            get
            {
                var func = string.Format(@"function(val){{return val.length>={0} && val.length<={1}}}", MinLength, MaxLength);
                return func;
            }
            set
            {
                base.Function = value;
            }
        }
    }
}
