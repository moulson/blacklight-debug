using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHR.Domain.Services.Interfaces
{
    public interface IConfiguration
    {
        string FoodRatingApiUrl { get; }
        string FoodRatingApiVersion { get; }
        string FoodRatingApiRequestAuthorities { get; }
        string FoodRatingApiRequestEstablishments { get; }
        int FoodRatingApiPageSize { get; }
    }
}
