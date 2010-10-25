
using System;
using System.Collections;
namespace Portal.Facilities
{

    /// <summary>
    /// RewriterRule���󼯺�
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
        /// ���һ��ʵ��������
        /// </summary>
        /// <param name="r">һ��RewriterRuleʵ��.</param>
        public void Add(RewriterRule r)
        {
            this.InnerList.Add(r);
        }

        /// <summary>
        /// ����Indexѡȡʵ��
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
