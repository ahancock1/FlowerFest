// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   IMailService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMailService
    {
        bool Send(string from, string to, string body, string subject = "");
    }
}
