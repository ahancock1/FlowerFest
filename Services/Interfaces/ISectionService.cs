// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   ISectionService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;

    public interface ISectionService
    {
        Task<IEnumerable<Section>> GetSections();
        Task<bool> CreateSection(Section section);
        Task<bool> UpdateSection(Section section);
        Task<bool> DeleteSection(Guid id);
        Task<Section> GetSection(Guid id);
    }
}