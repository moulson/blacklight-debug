using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using FHR.Domain.Services;
using FHR.Domain.Services.Interfaces;
using Ninject.Modules;
using RestSharp;

namespace FHR.Web.Ninject_Bindings
{
    public class NinjectSupportingLibraries : NinjectModule
    {

        private readonly Configuration _configuration;

        public NinjectSupportingLibraries()
        {
            _configuration = new Configuration();
        }


        public override void Load()
        {
            Bind<IRestClient>().ToConstructor(x => new RestClient(_configuration.FoodRatingApiUrl));
        }
    }
}