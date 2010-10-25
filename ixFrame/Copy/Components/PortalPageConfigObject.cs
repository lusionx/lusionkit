using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Facilities
{
    public sealed class PortalPageConfigObject
    {
        #region Fields

        private string identityName = string.Empty;
        private string name = string.Empty;
        private string className = string.Empty;
        private bool outputCache = false;
        private int duration = 3;
        private string[] varyByParams = null;
        private bool isXml = false;
        private bool isAsyn = false;

        #endregion

        #region Properties

        public bool IsXml
        {
            get { return isXml; }
            set { isXml = value; }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string ClassName
        {
            get
            {
                return className;
            }
            set
            {
                className = value;
            }
        }

        public bool OutputCache
        {
            get
            {
                return outputCache;
            }
            set
            {
                outputCache = value;
            }
        }

        public int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        public string[] VaryByParams
        {
            get
            {
                return varyByParams;
            }
            set
            {
                varyByParams = value;
            }
        }


        public bool IsAsyn
        {
            get { return isAsyn; }
            set { isAsyn = value; }
        }


        public string IdentityName
        {
            get { return identityName; }
            set { identityName = value; }
        }

        #endregion

        #region Public Methods

        public PortalPageConfigObject(string name, string className, string outputCacheString, string durationString, string varyByParamString, string isAsynString, string isXmlString)
        {
            this.name = name;
            this.className = className;
            this.outputCache = Convert.ToBoolean(outputCacheString);
            this.duration = Convert.ToInt32(durationString);
            if (string.Compare(varyByParamString, "none", true) != 0)
            {
                varyByParams = varyByParamString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
            this.isAsyn = Convert.ToBoolean(isAsynString);
            this.isXml = Convert.ToBoolean(isXmlString);
            this.identityName = name.Split('/')[0];
        }

        #endregion

    }

}
