// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IAutoMapperConfiguration.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using AutoMapper;

    public interface IMapperConfiguration
    {
        void Configure(IMapperConfigurationExpression config);
    }
}