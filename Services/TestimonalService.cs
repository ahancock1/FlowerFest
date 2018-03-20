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
    using Helpers;
    using Interfaces;
    using Models;
    using Repository.Interfaces;

    public class TestimonalService : ITestimonalService
    {
        private readonly IMapper _mapper;
        private readonly ITestimonalRepository _repository;

        public TestimonalService(ITestimonalRepository repository, 
            IMapper mapper)
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

        public Task<bool> CreateTestimonial(Testimonial testimonial)
        {
            Gaurd.ThrowIfNull(testimonial);

            return Task.FromResult(
                _repository
                    .Create(
                        _mapper.Map<TestimonialModel>(testimonial)));
        }


        public Task<bool> UpdateTestimonial(Testimonial section)
        {
            Gaurd.ThrowIfNull(section);

            return Task.FromResult(
                _repository
                    .Update(
                        _mapper.Map<TestimonialModel>(section)));
        }

        public Task<bool> DeleteTestimonial(Guid id)
        {
            Gaurd.ThrowIfNull(id);

            var model = _repository.Get(t => t.Id.Equals(id));
            if (model != null)
            {
                return Task.FromResult(
                    _repository.Delete(model));
            }

            return Task.FromResult(false);
        }

        public Task<Testimonial> GetTestimonial(Guid id)
        {
            Gaurd.ThrowIfNull(id);

            return Task.FromResult(
                _mapper.Map<Testimonial>(
                    _repository.Get(s => s.Id.Equals(id))));
        }
    }
}