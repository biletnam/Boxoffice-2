using Autofac;
using Autofac.Integration.WebApi;
using Clearsoft.BoxOffice.Common.Logging;
using Clearsoft.BoxOffice.Web.Api.App_Start;
using Clearsoft.BoxOffice.Web.Common;
using System.Web.Http;
using System;
using System.Net.Http;
using Clearsoft.BoxOffice.Web.Api.Security;
using Clearsoft.BoxOffice.Web.Common.Security;

namespace Clearsoft.BoxOffice.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Autofac initialisation
            var config = GlobalConfiguration.Configuration;

            

            IContainer container = new AutofacConfigurator().Configure();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterHandlers();

        }

        private void RegisterHandlers()
        {
            var logManager = WebContainerManager.Get<ILogManager>();
            var userSession = WebContainerManager.Get<IUserSession>();

            GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthenticationMessageHandler(logManager, WebContainerManager.Get<IBasicSecurityService>()));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new EventDataSecurityMessageHandler(logManager, userSession));
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if(exception != null)
            {
                var log = WebContainerManager.Get<ILogManager>().GetLog(typeof(WebApiApplication));

                log.Error("Unhandled exception.", exception);
            }
        }
    }
}
