// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   PartnerMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using AutoMapper;
    using DTO;
    using Models;

    public class PartnerMappings : Profile
    {
        public PartnerMappings()
        {
            CreateMap<PartnerModel, Partner>();
            CreateMap<Partner, PartnerModel>();
        }
    }
}