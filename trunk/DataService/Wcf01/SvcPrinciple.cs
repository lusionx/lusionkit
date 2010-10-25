using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Wcf01
{
    public class SvcPrinciple : IPrincipal
    {
        private IIdentity _id;

        public SvcPrinciple(IIdentity id)
        {
            _id = id;
        }

        public IIdentity Identity
        {
            get { return _id; }
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
