using System;
using System.Data;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using FHR.Domain.Models;
using FHR.Domain.Services.Interfaces;
using FHR.Web.Models;
using log4net;

namespace FHR.Web.Controllers
{
    public class RatingController : Controller
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMapper _mapper;
        private readonly ILocalAuthorityService _localAuthorityService;

        public RatingController(IMapper mapper, ILocalAuthorityService localAuthorityService)
        {
            _mapper = mapper;
            _localAuthorityService = localAuthorityService;
        }

        public ActionResult Index()
        {
            var authRatingViewModel = new AuthorityRatingsViewModel();
            try
            {
                var authorities = _localAuthorityService.GetAuthorities(); // Get all local authorities.
                _mapper.Map(authorities, authRatingViewModel);
            }
            catch (Exception e)
            {
                authRatingViewModel.AuthoritiesLoadedSuccessfuly = false;
                Logger.Error("An error occured while loading Rating Index", e);
            }

            return View(authRatingViewModel);
        }

        public JsonResult GetRatingPercentages(int id)
        {
            var establishmentRatingsSummary =
                new EstablishmentRatingsSummary(_localAuthorityService.GetEstablishmentsByAuthority(id));

            return Json(establishmentRatingsSummary.GetRatingPercentages(), JsonRequestBehavior.AllowGet);
        }
    }
}