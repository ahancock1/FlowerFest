// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   FileService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DTO;
    using Extensions;
    using Interfaces;
    using Microsoft.AspNetCore.Http;

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

        private Dictionary<string, string> _mimeTypes => new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.ms-word"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"}
        };

        public async Task<string> Save(IFormFile file)
        {
            if (!Validate(file))
            {
                throw new NotSupportedException("File format is not supported.");
            }

            var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var filepath = Path.Combine(_path, filename);

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }

        public Task<bool> Delete(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException("File id can not be null.");
            }

            var path = Directory.GetFiles(_path)
                .FirstOrDefault(file =>
                    file.Contains(id.ToString()));

            if (path == null)
            {
                return Task.FromResult(false);
            }

            File.Delete(path);

            return Task.FromResult(true);
        }

        public Task<FileDetail> GetFile(Guid id)
        {
            foreach (var path in Directory.GetFiles(_path))
            {
                if (path.Contains(id.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return Task.FromResult(new FileDetail
                    {
                        Id = Path.GetFileNameWithoutExtension(path)
                            .ToLowerInvariant(),
                        Name = Path.GetFileName(path)
                            .ToLowerInvariant(),
                        Path = path
                    });
                }
            }

            return null;
        }

        public Task<IEnumerable<FileDetail>> GetFiles()
        {
            var files = Directory.GetFiles(_path)
                .Select(path => new FileDetail
                {
                    Id = Path.GetFileNameWithoutExtension(path)
                        .ToLowerInvariant(),
                    Name = Path.GetFileName(path)
                        .ToLowerInvariant(),
                    Path = path
                })
                .ToList();

            return Task.FromResult((IEnumerable<FileDetail>) files);
        }

        public async Task<byte[]> Read(FileDetail file)
        {
            if (file == null)
            {
                throw new ArgumentException("File can not be null.");
            }

            var path = Path.Combine(_path, file.Name);

            using (var memory = new MemoryStream())
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                return memory.GetBuffer();
            }
        }

        public Task<string> GetContentType(FileDetail file)
        {
            if (file != null)
            {
                return Task.FromResult(
                    _mimeTypes[Path.GetExtension(file.Name)]);
            }

            return null;
        }

        private bool Validate(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            var extension = Path.GetExtension(file.FileName);

            if (_mimeTypes.ContainsKey(extension.ToLowerInvariant()))
            {
                return true;
            }

            return false;
        }
    }
}