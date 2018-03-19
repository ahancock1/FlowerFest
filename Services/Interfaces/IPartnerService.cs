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
        Task<bool> Create(Partner partner);
        Task<bool> Update(Partner partner);
        Task<bool> Delete(Guid id);
        Task<Partner> Get(Guid id);
    }
}