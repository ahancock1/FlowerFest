// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   SectionRepository.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Repository
{
    using System.IO;
    using Interfaces;
    using Models;

    public class SectionRepository : BaseRepository<SectionModel>, ISectionRepository
    {
        private readonly string _path;

        public SectionRepository(string path) 
            : base(path)
        {
            _path = path;

            Seed();
        }

        private void Seed()
        {
            if (Directory.GetFiles(_path).Length > 0) return;


        }
    }
}