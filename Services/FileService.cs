// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   FileService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Http;
    using Extensions;

    public class FileService : IFileService
    {
        private readonly string _path;

        public FileService(string path)
        {
            _path = path;

            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
        
        public async Task<string> Save(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return null;
            }

            var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var filepath = Path.Combine(_path, filename);

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }

        public bool Validate(IFormFile file, params string[] extensions)
        {
            if (file == null || file.Length == 0) return false;

            return extensions
                .Any(
                    type =>
                        Path.GetExtension(file.FileName)
                            .Contains(type, StringComparison.OrdinalIgnoreCase));
        }
    }
}