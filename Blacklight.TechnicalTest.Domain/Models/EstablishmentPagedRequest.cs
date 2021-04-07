using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHR.Domain.Models
{
    public class EstablishmentPagedRequest
    {
        public List<Establishment> Establishments { get; set; }
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public int TotalPages { get; set; }
    }
}
