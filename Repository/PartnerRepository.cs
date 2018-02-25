// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SupportRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using Interfaces;
    using Models;

    public class PartnerRepository : BaseRepository<PartnerModel>, IPartnerRepository
    {
        public PartnerRepository(string path)
            : base(path)
        {
        }
    }
}