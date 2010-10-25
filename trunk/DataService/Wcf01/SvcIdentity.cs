using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Wcf01
{
    public class SvcIdentity : IIdentity
    {
        private bool _isAuthenticated = false;

        private string _name;

        public SvcIdentity(string name, bool isAuthenticated)
        {
            _isAuthenticated = isAuthenticated;

            _name = name;
        }

        public string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
