// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   ContentMapping.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Mappings
{
    using System;
    using AutoMapper;

    public abstract class ContentMapping : IMapperConfiguration
    {
        public abstract void Configure(IMapperConfigurationExpression config);

        protected string CompileContent(string content)
        {
            throw new NotImplementedException();
        }
    }
}