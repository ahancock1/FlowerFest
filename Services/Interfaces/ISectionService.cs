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
        Task<Section> CreateSection(Section section);
        Task<Section> UpdateSection(Section section);
        Task<bool> DeleteSection(Guid id);
    }
}