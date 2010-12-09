using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Text;
using System.Security.Principal;

namespace Wcf01
{
    public class Global : System.Web.HttpApplication
    {
        const string accessDeniedStatus = "Access Denied";

        const string accessDeniedHtml = "<html><body>401 Access Denied</body></html>";

        const string realmFormatString = "Basic realm=\"{0}\"";

        const string authServerHeader = "WWW-Authenticate";

        const string authClientHeader = "Authorization";

        const string basicAuth = "Basic";

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

            if (Request.Headers[authClientHeader] == null)
            {

                Response.StatusCode = 401;

                Response.StatusDescription = accessDeniedStatus;

                Response.Write(accessDeniedHtml);

                Response.AddHeader(authServerHeader,
                    string.Format(realmFormatString,
                    Request.Url.GetLeftPart(UriPartial.Authority)));

            }

            else
            {

                string credential = ASCIIEncoding.ASCII.GetString(
                    Convert.FromBase64String(GetBase64CredentialsFromHeader(Request.Headers[authClientHeader])));

                string[] usernameandpassword = credential.Split(':');

                bool aut = Authenticate(usernameandpassword[0], usernameandpassword[1]);

                IIdentity id = new SvcIdentity(usernameandpassword[0], aut);

                Context.User = new SvcPrinciple(id);

            }
        }

        bool Authenticate(string username, string password)
        {

            //your code logic here to authenticate user

            if (username == "hack") return false;

            else

                return true;

        }

        protected string GetBase64CredentialsFromHeader(string credsHeaderValue)
        {
            string credsHeader = credsHeaderValue;

            string creds = null;

            int credsPosition = credsHeader.IndexOf(basicAuth, StringComparison.OrdinalIgnoreCase);

            if (credsPosition != -1)
            {

                credsPosition += basicAuth.Length + 1;

                creds = credsHeader.Substring(credsPosition, credsHeader.Length - credsPosition);

            }

            return (creds);

        }


        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}