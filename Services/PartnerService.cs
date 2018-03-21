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
    using Helpers;
    using Interfaces;
    using Models;
    using Repository.Interfaces;

    public class PartnerService : IPartnerService
    {
        private readonly IMapper _mapper;
        private readonly IPartnerRepository _repository;

        public PartnerService(IPartnerRepository repository,
            IMapper mapper)
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

        public Task<Partner> GetPartnerById(Guid id)
        {
            Gaurd.ThrowIfNull(id);

            return Task.FromResult(
                _mapper.Map<Partner>(
                    _repository.Get(p => p.Id.Equals(id))));
        }

        public Task<bool> CreatePartner(Partner partner)
        {
            Gaurd.ThrowIfNull(partner);

            return Task.FromResult(
                _repository
                    .Create(
                        _mapper.Map<PartnerModel>(partner)));
        }

        public Task<bool> UpdatePartner(Partner partner)
        {
            Gaurd.ThrowIfNull(partner);

            return Task.FromResult(
                _repository
                    .Update(
                        _mapper.Map<PartnerModel>(partner)));
        }

        public Task<bool> DeletePartner(Guid id)
        {
            Gaurd.ThrowIfNull(id);

            var model = _repository.Get(p => p.Id.Equals(id));

            if (model != null)
            {
                return Task.FromResult(
                    _repository.Delete(model));
            }

            return Task.FromResult(false);
        }
    }
}