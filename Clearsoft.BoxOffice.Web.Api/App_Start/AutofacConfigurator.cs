using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Clearsoft.BoxOffice.Common;
using log4net.Config;
using Clearsoft.BoxOffice.Common.Logging;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Clearsoft.BoxOffice.Data.SqlServer.Mapping;
using NHibernate;
using Clearsoft.BoxOffice.Web.Common;
using Clearsoft.BoxOffice.Web.Common.Security;
using Clearsoft.BoxOffice.Data.Entities;
using Clearsoft.BoxOffice.Data.SqlServer.QueryProcessors;
using Clearsoft.BoxOffice.Web.Api.MaintenanceProcessing;
using AutoMapper;
using Clearsoft.BoxOffice.Web.Api.Security;
using NHibernate.Context;
using Clearsoft.BoxOffice.Web.Api.InquiryProcessing;
using Clearsoft.BoxOffice.Data.QueryProcessors;

namespace Clearsoft.BoxOffice.Web.Api.App_Start
{
    public class AutofacConfigurator
    {
        public IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            AddBindings(builder);
            return builder.Build();
        }

        private void AddBindings(ContainerBuilder builder)
        {
            ConfigureLog4net(builder);
            ConfigureUserSession(builder);
            ConfigureNHibernate(builder);
            ConfigureAutoMapper(builder);

            builder.RegisterType<DateTimeAdapter>().As<IDateTime>().InstancePerLifetimeScope();
            builder.RegisterType<ActionTransactionHelper>().As<IActionTransactionHelper>().InstancePerLifetimeScope();
            builder.RegisterType<JObjectUpdateablePropertyDetector>().As<IUpdateablePropertyDetector>().SingleInstance();

            //maintenance processors
            builder.RegisterType<AddEventMaintenanceProcessor>().As<IAddEventMaintenanceProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<PublishEventWorkflowProcessor>().As<IPublishEventWorkflowProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<UnpublishEventWorkflowProcessor>().As<IUnpublishEventWorkflowProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<EventByIdInquiryProcessor>().As<IEventByIdInquiryProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<EventPerformancesMaintenanceProcessor>().As<IEventPerformancesMaintenanceProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateEventMaintenanceProcessor>().As<IUpdateEventMaintenanceProcessor>().InstancePerLifetimeScope();

            //security
            builder.RegisterType<BasicSecurityService>().As<IBasicSecurityService>().SingleInstance();

            //query processors
            builder.RegisterType<AddEventQueryProcessor>().As<IAddEventQueryProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<EventByIdQueryProcessor>().As<IEventByIdQueryProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateEventStatusQueryProcessor>().As<IUpdateEventStatusQueryProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<UpdateEventQuesryProcessor>().As<IUpdateEventQueryProcessor>().InstancePerLifetimeScope();
        }

        private void ConfigureLog4net(ContainerBuilder builder)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();

            builder.RegisterType<LogManagerAdapter>().As<ILogManager>().SingleInstance();
        }

        private void ConfigureNHibernate(ContainerBuilder builder)
        {
            var sessionFactory = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("Clearsoft.BoxofficeDb"))).CurrentSessionContext("web").Mappings(m => m.FluentMappings.AddFromAssemblyOf<EventMap>()).BuildSessionFactory();
            
            builder.RegisterInstance(sessionFactory).As<ISessionFactory>().SingleInstance();
            //builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerRequest();
            
            builder.Register(c => CreateSession(c.Resolve<ISessionFactory>())).As<ISession>();
        }

        private ISession CreateSession(ISessionFactory sessionfactory)
        {
            if (!CurrentSessionContext.HasBind(sessionfactory))
            {
                var session = sessionfactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }
            return sessionfactory.GetCurrentSession();
        }

        private void ConfigureUserSession(ContainerBuilder builder)
        {
            var userSession = new UserSession();
            builder.RegisterInstance(userSession).As<IUserSession>().SingleInstance();
        }

        private void ConfigureAutoMapper(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfiles(Assembly.GetExecutingAssembly()));
                return config;
            }).SingleInstance().AutoActivate().AsSelf();

            builder.Register(tmpContext =>
            {
                var ctx = tmpContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();

                return config.CreateMapper();
            }).As<IMapper>();
        }
    }
}