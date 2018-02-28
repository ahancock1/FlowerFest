// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   FileMappings.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using Areas.Dashboard.ViewModels.Files;
    using AutoMapper;
    using DTO;

    public class FileMappings : IMapperConfiguration
    {
        public void Configure(IMapperConfigurationExpression config)
        {
            config.CreateMap<FileDetail, FileViewModel>();
            config.CreateMap<FileViewModel, FileDetail>();
        }
    }
}