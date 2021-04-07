using FHR.Domain.Services;
using FHR.Domain.Services.Interfaces;
using Ninject.Modules;
using RestSharp;

namespace FHR.Web.Ninject_Bindings
{
    public class NinjectServices : NinjectModule
    {
        public override void Load()
        {
            Bind<ILocalAuthorityService>().To<LocalAuthorityService>();
            Bind<IConfiguration>().To<Configuration>();
        }
    }
}