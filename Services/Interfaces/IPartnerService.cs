// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IPartnerService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;

    public interface IPartnerService
    {
        Task<IEnumerable<Partner>> GetPartners();
        Task<bool> CreatePartner(Partner partner);
        Task<bool> UpdatePartner(Partner partner);
        Task<bool> DeletePartner(Guid id);
        Task<Partner> GetPartner(Guid id);
    }
}