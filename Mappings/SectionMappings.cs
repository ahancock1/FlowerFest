// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SectionMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using AutoMapper;
    using DTO;
    using Models;
    using ViewModels.Home;
    using ViewModels.Shared;

    public class SectionMappings : ContentMapping
    {
        public override void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<SectionModel, Section>();
            config.CreateMap<Section, SectionModel>();

            config.CreateMap<Section, SectionViewModel>();
            config.CreateMap<SectionViewModel, Section>();

            config.CreateMap<Section, HeaderSectionViewModel>();
            config.CreateMap<HeaderSectionViewModel, Section>();
        }
    }
}