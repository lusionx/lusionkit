using System;
using System.Text.RegularExpressions;

namespace Portal.Facilities
{
    /// <summary>
    /// URL��д������
    /// </summary>
    [Serializable()]
    public class RewriterRule
    {
        // ˽������
        private string lookFor = string.Empty;
        private Regex lookForRegex = null;
        private string sendTo = string.Empty;
        private string subDomainLookFor = string.Empty;
        private bool redirect = false;

        #region ��������
        /// <summary>
        /// �����˵�URLģʽ
        /// </summary>
        public string LookFor
        {
            get
            {
                return lookFor;
            }
            set
            {
                lookFor = value;
            }
        }

        /// <summary>
        /// Ҫת����URL
        /// </summary>
        public string SendTo
        {
            get
            {
                return sendTo;
            }
            set
            {
                sendTo = value;
            }
        }


        public string SubDomainLookFor
        {
            get { return subDomainLookFor; }
            set { subDomainLookFor = value; }
        }


        public bool Redirect
        {
            get { return redirect; }
            set { redirect = value; }
        }


        public Regex LookForRegex
        {
            get { return lookForRegex; }
            set { lookForRegex = value; }
        }

        #endregion

        public RewriterRule(string lookFor, Regex lookForRegex, string sendTo, bool redirect)
        {
            this.lookFor = lookFor;
            this.lookForRegex = lookForRegex;
            this.sendTo = sendTo;
            this.redirect = redirect;
        }


        public RewriterRule(string lookFor, Regex lookForRegex, string sendTo, string subDomainLookFor, bool redirect)
        {
            this.lookFor = lookFor;
            this.lookForRegex = lookForRegex;
            this.sendTo = sendTo;
            this.subDomainLookFor = subDomainLookFor;
            this.redirect = redirect;
        }
    }
}
