// -----------------------------------------------------------------------
//   Copyright (C) 2018 Adam Hancock
//    
//   IFileService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        Task<string> Save(IFormFile file);
        bool Validate(IFormFile file, params string[] extensions);
    }
}