using System.Collections.Generic;
using FHR.Domain.Models;

namespace FHR.Domain.Services.Interfaces
{
    public interface ILocalAuthorityService
    {
        IEnumerable<LocalAuthority> GetAuthorities();
        IEnumerable<Establishment> GetEstablishmentsByAuthority(int localAuthorityId);
    }
}
