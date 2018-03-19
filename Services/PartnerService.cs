// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   PartnerService.cs can not be copied and/or distributed without the express
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

    public class PartnerService : IPartnerService
    {
        private readonly IMapper _mapper;
        private readonly IPartnerRepository _repository;

        public PartnerService(IPartnerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<Partner>> GetPartners()
        {
            return Task.FromResult(
                _mapper.Map<IEnumerable<Partner>>(
                    _repository.All()));
        }

        public Task<Partner> Get(Guid id)
        {
            ThrowIfNull(id);

            return Task.FromResult(
                _mapper.Map<Partner>(
                    _repository.Get(p => p.Id.Equals(id))));
        }

        public Task<bool> Create(Partner partner)
        {
            ThrowIfNull(partner);

            return Task.FromResult(
                _repository
                    .Create(
                        _mapper.Map<PartnerModel>(partner)));
        }

        public Task<bool> Update(Partner partner)
        {
            ThrowIfNull(partner);

            return Task.FromResult(
                _repository
                    .Update(
                        _mapper.Map<PartnerModel>(partner)));
        }

        public Task<bool> Delete(Guid id)
        {
            ThrowIfNull(id);

            var model = _repository.Get(p => p.Id.Equals(id));

            if (model != null)
            {
                return Task.FromResult(
                    _repository.Delete(model));
            }

            return Task.FromResult(false);
        }

        private void ThrowIfNull<T>(T item)
        {
            if (item == null)
            {
                throw new ArgumentException($"{typeof(T)} can not be null.");
            }
        }
    }
}