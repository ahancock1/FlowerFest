// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   MailService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Interfaces;
    using System.Net.Mail;
    using System.Net;

    public class MailService : IMailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }

        public bool Send(string from, string to, string body, string subject = "")
        {
            var username = _config["smtp:username"];
            var password = _config["smtp:password"];
            var server = _config["smtp:server"];
            var port = Convert.ToInt32(_config["smtp:port"]);
            var ssl = Convert.ToBoolean(_config["smtp:ssl"]);

            using (var message = new MailMessage
            {
                From = new MailAddress(from)
            })
            {
                message.To.Add(new MailAddress(to));
                message.Subject = subject;

                message.Body = body;

                try
                {
                    using (var client = new SmtpClient(server, port)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(username, password),
                        EnableSsl = ssl
                    })
                    {
                        client.Send(message);
                    }                
                }
                catch (Exception e)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
