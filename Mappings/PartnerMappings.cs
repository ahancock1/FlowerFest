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
    using ViewModels.Home;

    public class PartnerMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<PartnerViewModel, Partner>();
            config.CreateMap<Partner, PartnerViewModel>();
        }
    }
}