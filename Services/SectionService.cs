// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SectionService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using DTO;
    using Helpers;
    using Interfaces;
    using Models;
    using Repository.Interfaces;

    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _repository;
        private readonly IMapper _mapper;

        public SectionService(ISectionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<Section>> GetSections()
        {
            return Task.FromResult(
                _mapper.Map<IEnumerable<Section>>(
                    _repository.All(
                        section =>
                            section.IsPublished,
                        section =>
                            section.Index)));
        }

        public Task<bool> CreateSection(Section section)
        {
            Gaurd.ThrowIfNull(section);

            ValidateIndex(section.Index);

            return Task.FromResult(
                _repository.Create(
                    _mapper.Map<SectionModel>(section)));
        }

        private void ValidateIndex(int index)
        {
            var sections = _repository.All(
                section =>
                    section.IsPublished,
                section =>
                    section.Index).ToList();

            if (sections.All(section => section.Index != index)) return;

            for (var i = index; i < sections.Count; i++)
            {
                var section = sections[i];
                section.Index++;

                _repository.Update(section);
            }
        }

        public Task<bool> UpdateSection(Section section)
        {
            Gaurd.ThrowIfNull(section);

            ValidateIndex(section.Index);

            return Task.FromResult(
                _repository
                    .Update(
                        _mapper.Map<SectionModel>(section)));
        }

        public Task<bool> DeleteSection(Guid id)
        {
            Gaurd.ThrowIfNull(id);

            var model = _repository.Get(section => section.Id.Equals(id));
            if (model != null)
            {
                return Task.FromResult(
                    _repository.Delete(model));
            }

            return Task.FromResult(false);
        }

        public Task<Section> GetSection(Guid id)
        {
            Gaurd.ThrowIfNull(id);

            return Task.FromResult(
                _mapper.Map<Section>(
                    _repository.Get(s => s.Id.Equals(id))));
        }
    }
}