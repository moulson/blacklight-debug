using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Autofac.Util;

namespace FHR.Web.Ninject_Bindings
{
    public class NinjectAutoMapper : NinjectModule
    {
        public override void Load()
        {
            var profiles = new List<Profile>();
            var profileAssemblies = ConfigurationManager.AppSettings["AutoMapper.ProfileAssemblies"];
            if (string.IsNullOrWhiteSpace(profileAssemblies)) throw new Exception("could not find Profile Assembles");
            profileAssemblies.Split(',')
                .ToList()
                .ForEach(x => profiles.AddRange(Assembly.Load(x)
                    .GetLoadableTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t))
                    .Select(t => (Profile)Activator.CreateInstance(t))
                    .ToList()));


            var config = new MapperConfiguration(x => profiles.ForEach(x.AddProfile));
            Bind<MapperConfiguration>().ToConstant(config);
            Bind<IMapper>().ToConstant(config.CreateMapper());
        }
    }
}