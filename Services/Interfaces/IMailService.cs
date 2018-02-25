// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   IMailService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IMailService
    {
        Task<bool> Send(string from, string to, string body, string subject = "");
    }
}
