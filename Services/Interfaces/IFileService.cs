// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IFileService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        void Save(IFormFile file);
    }

    public class FileService : IFileService
    {
        private readonly string _path;

        public FileService(string path)
        {
            _path = path;
        }

        public void Save(IFormFile file)
        {

        }
    }
}