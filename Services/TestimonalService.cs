// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   TestimonalService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using DTO;
    using Interfaces;
    using Models;
    using Repository.Interfaces;

    public class TestimonalService : ITestimonalService
    {
        private readonly IMapper _mapper;
        private readonly ITestimonalRepository _repository;

        public TestimonalService(ITestimonalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<Testimonial>> GetTestimonals()
        {
            return Task.FromResult(
                _mapper.Map<IEnumerable<Testimonial>>(
                    _repository
                        .All()));
        }

        public Task<bool> Create(Testimonial testimonal)
        {
            if (testimonal == null)
            {
                throw new ArgumentException("Testimonal can not be null.");
            }

            return Task.FromResult(
                _repository
                    .Create(
                        _mapper.Map<TestimonalModel>(testimonal)));
        }
    }
}