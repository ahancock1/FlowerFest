// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SupportRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System.IO;
    using Interfaces;
    using Models;

    public class PartnerRepository 
        : BaseRepository<PartnerModel>, IPartnerRepository
    {
        private readonly string _path;

        public PartnerRepository(string path)
            : base(path)
        {
            _path = path;

            Seed();
        }

        private void Seed()
        {
            if (Directory.GetFiles(_path).Length > 0) return;

            Create(new PartnerModel
            {
                Name = "Mind Dorset",
                Image = "~/img/support/minddorset.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Mind Dorset",
                Image = "~/img/support/minddorset.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Captains Club",
                Image = "~/img/support/captainsclub.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Cultral Trust",
                Image = "~/img/support/cultraltrust.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "English Heritage",
                Image = "~/img/support/englishheritage.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Filer Knapper",
                Image = "~/img/support/filerknapper.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Floral Direct",
                Image = "~/img/support/floradirect.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Music Arts Festival",
                Image = "~/img/support/musicartsfestival.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Opi FLoor",
                Image = "~/img/support/opiflor.png",
                Link = ""
            });
            
            Create(new PartnerModel
            {
                Name = "Saxon Square",
                Image = "~/img/support/saxonsquare.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Signs of Distinction",
                Image = "~/img/support/signsofdistinction.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Soho",
                Image = "~/img/support/soho.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Thomas Tripp",
                Image = "~/img/support/thomastripp.png",
                Link = ""
            });

            Create(new PartnerModel
            {
                Name = "Urban",
                Image = "~/img/support/urban.png",
                Link = ""
            });
        }
    }
}