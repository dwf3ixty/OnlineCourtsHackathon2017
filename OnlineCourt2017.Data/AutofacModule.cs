using Autofac;
using Autofac.Extras.AttributeMetadata;
using OnlineCourt2017.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourt2017.Data
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<AppDbContext>().As<AppDbContext>().InstancePerRequest();
            builder.RegisterType<SecurityService>().As<SecurityService>().InstancePerRequest();
            builder.RegisterType<CaseService>().As<CaseService>().InstancePerRequest();

            builder.RegisterModule<AttributedMetadataModule>();
        }
    }
}
