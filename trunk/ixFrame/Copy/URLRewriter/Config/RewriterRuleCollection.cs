
using System;
using System.Collections;
namespace Portal.Facilities
{

    /// <summary>
    /// RewriterRule对象集合
    /// </summary>
    [Serializable()]
    public sealed class RewriterRuleCollection : CollectionBase
    {
        private string name = string.Empty;

        public RewriterRuleCollection()
        {
        }

        public RewriterRuleCollection(string name)
        {
            this.name = name;
        }

        public bool IsMatch(string url)
        {
            if (name.Length == 0)
                return true;
            if (url.StartsWith(name))
                return true;
            return false;
        }
        /// <summary>
        /// 添加一个实例到集合
        /// </summary>
        /// <param name="r">一个RewriterRule实例.</param>
        public void Add(RewriterRule r)
        {
            this.InnerList.Add(r);
        }

        /// <summary>
        /// 根据Index选取实例
        /// </summary>
        public RewriterRule this[int index]
        {
            get
            {
                return (RewriterRule)this.InnerList[index];
            }
            set
            {
                this.InnerList[index] = value;
            }
        }
    }
}
