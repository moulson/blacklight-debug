using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.Http;
using FHR.Domain.Models;
using FHR.Domain.Services.Interfaces;
using log4net;
using RestSharp;

namespace FHR.Domain.Services
{
    public class LocalAuthorityService : ILocalAuthorityService
    {
        private readonly IRestClient _restClient;
        private readonly IConfiguration _configuration;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public LocalAuthorityService(IRestClient restClient, IConfiguration configuration)
        {
            _restClient = restClient;
            _configuration = configuration;
        }
        public IEnumerable<LocalAuthority> GetAuthorities()
        {
            var request = new RestRequest(_configuration.FoodRatingApiRequestAuthorities, Method.GET);
            request.AddHeader("x-api-version", _configuration.FoodRatingApiVersion); // API version to use.
            request.RootElement = "authorities"; // Tell RestSharp which Json element to start parsing at.

            var response = _restClient.Execute<List<LocalAuthority>>(request);
            if (!response.IsSuccessful)
            {
                Logger.Error($"A failed response was received from {_configuration.FoodRatingApiRequestAuthorities} with the status code of {response.StatusCode.ToString()} inside GetAuthorities()");
                throw new ObjectNotFoundException();
            }

            return response.Data;
        }

        public IEnumerable<Establishment> GetEstablishmentsByAuthority(int localAuthorityId)
        {
            var establishments = new List<Establishment>();

            try
            {
                var currentPage = 1;
                var requestUrl = _configuration.FoodRatingApiRequestEstablishments;
                IRestResponse<EstablishmentPagedRequest> response;

                var request = new RestRequest(requestUrl, Method.GET);
                request.AddHeader("x-api-version", _configuration.FoodRatingApiVersion); // API version to use.
                request.AddQueryParameter("LocalAuthorityId", localAuthorityId.ToString());
                request.AddQueryParameter("pageSize", _configuration.FoodRatingApiPageSize.ToString());

                do // Make multiple requests until we have received all the pages. This is just to ensure if we set a page size too low that we still receive all ratings.
                {
                    currentPage++;
                    request.AddOrUpdateParameter("pageNumber", currentPage, ParameterType.QueryString);
                    response = _restClient.Execute<EstablishmentPagedRequest>(request);
                    if (!response.IsSuccessful)
                    {
                        Logger.Error(
                            $"A failed response was received from {requestUrl} with the status code of {response.StatusCode.ToString()} inside GetEstablishmentsByAuthority()");
                        throw new ObjectNotFoundException();
                    }

                    establishments.AddRange(response.Data
                        .Establishments); // Add the pages set of establishments to the overall collection.
                } while (response.IsSuccessful && currentPage < response.Data.Meta.TotalPages);
            }
            catch (ObjectNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.Error("An error occured while obtaining establishments", e);
                throw;
            }

            return establishments;
        }

    }
}
