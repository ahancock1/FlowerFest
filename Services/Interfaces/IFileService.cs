// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IFileService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DTO;
    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        Task<string> Save(IFormFile file);
        Task<bool> Delete(Guid id);
        Task<FileDetail> GetFile(Guid id);
        Task<IEnumerable<FileDetail>> GetFiles();
        Task<byte[]> Read(FileDetail file);
        Task<string> GetContentType(FileDetail file);
    }
}