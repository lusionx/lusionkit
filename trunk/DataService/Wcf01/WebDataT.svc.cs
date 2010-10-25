using System;
using System.Collections.Generic;
using System.Data.Services;
//using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Reflection;
using System.Collections;
using System.Security.Authentication;
//using Wcf01.BLL;

namespace Wcf01
{
    public class WebDataT : DataService<KjptDB>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(IDataServiceConfiguration config)
        {
            config.UseVerboseErrors = false;
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            var type = typeof(KjptDB);
            //foreach (var a in type.GetProperties().Where(e => e.PropertyType.Name == "Table`1"))
            foreach (var a in FindProperty(type, "Table`1"))
            {
                config.SetEntitySetAccessRule(a.Name, EntitySetRights.All);
            }

            foreach (var a in FindProperty(type, "IQueryable`1"))
            {
                config.SetEntitySetAccessRule(a.Name, EntitySetRights.AllRead);
            }
            //config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }

        protected override KjptDB CreateDataSource()
        {
            return base.CreateDataSource();
        }

        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            if (Authenticate)
            {
                base.OnStartProcessingRequest(args);
            }
            else
            {
                throw new AuthenticationException("Authentication Failed");
            }
        }

        protected override void HandleException(HandleExceptionArgs args)
        {
            if (args.Exception is AuthenticationException)
            {
                var e = args.Exception;
                args.Exception = new DataServiceException(400, e.GetType().Name, e.Message, "en-US", e);
            }
        }

        protected bool Authenticate
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }


        public static IEnumerable<MemberInfo> FindProperty(Type classtype, string PropertyTypeName)
        {
            foreach (var a in classtype.GetProperties())
            {
                if (a.PropertyType.Name == PropertyTypeName)
                {
                    yield return a;
                }
            }
        }
    }
}
