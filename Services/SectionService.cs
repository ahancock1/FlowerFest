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

        public Task<Section> CreateSection(Section section)
        {
            if (section == null)
            {
                throw new ArgumentException("Section can not be null.");
            }

            ValidateIndex(section.Index);

            var model = _mapper.Map<SectionModel>(section);

            if (_repository.Create(model))
            {
                return Task.FromResult(section);
            }

            return null;
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

        public Task<Section> UpdateSection(Section section)
        {
            if (section == null)
            {
                throw new ArgumentException("Section can not be null.");
            }

            ValidateIndex(section.Index);

            var model = _mapper.Map<SectionModel>(section);

            if (_repository.Update(model))
            {
                return Task.FromResult(section);
            }

            return null;
        }

        public Task<bool> DeleteSection(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("Section id can not be null.");
            }

            var model = _repository.Get(section => section.Id.Equals(id));
            if (model != null)
            {
                return Task.FromResult(
                    _repository.Delete(model));
            }

            return Task.FromResult(false);
        }
    }
}