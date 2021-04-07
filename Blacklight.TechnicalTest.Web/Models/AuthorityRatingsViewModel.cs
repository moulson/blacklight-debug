using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FHR.Web.Models
{
    public class AuthorityRatingsViewModel
    {
        public bool AuthoritiesLoadedSuccessfuly { get; set; }
        public IEnumerable<LocalAuthorityViewModel> Authorities { get; set; }


        public IEnumerable<SelectListItem> AuthoritiesSelectList
        {
            get
            {
                var selectList = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Disabled = true,
                        Selected = true,
                        Text = "Select Your Local Authority"
                    }
                };
                selectList.AddRange(Authorities.Select(x => new SelectListItem() { Value = x.LocalAuthorityId.ToString(), Text = x.Name }));
                return selectList;
            }
        }
    }
}