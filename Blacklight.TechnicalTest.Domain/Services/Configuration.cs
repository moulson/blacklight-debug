using System;
using System.Configuration;
using FHR.Domain.Services.Interfaces;

namespace FHR.Domain.Services
{
    public class Configuration : IConfiguration
    {
        public string FoodRatingApiUrl => ConfigurationManager.AppSettings["FoodRatingApi.Url"];
        public string FoodRatingApiVersion => ConfigurationManager.AppSettings["FoodRatingApi.Version"];
        public string FoodRatingApiRequestAuthorities => ConfigurationManager.AppSettings["FoodRatingApi.Request.Authorities"];
        public string FoodRatingApiRequestEstablishments => ConfigurationManager.AppSettings["FoodRatingApi.Request.Establishments"];
        public int FoodRatingApiPageSize => Convert.ToInt32(ConfigurationManager.AppSettings["FoodRatingApi.Request.PageSize"]);
    }
}
