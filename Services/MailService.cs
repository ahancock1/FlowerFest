// -----------------------------------------------------------------------
//   Copyright (C) 2017 Adam Hancock
//    
//   MailService.cs can not be copied and/or distributed without the express
//   permission of Adam Hancock
// -----------------------------------------------------------------------

namespace FlowerFest.Services
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Interfaces;
    using System.Net.Mail;
    using System.Net;
    using System.Threading.Tasks;

    public class MailService : IMailService
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _server;
        private readonly int _port;
        private readonly bool _ssl;

        public MailService(IConfiguration config)
        {
            _username = config["smtp:username"];
            _password = config["smtp:password"];
            _server = config["smtp:server"];
            _port = Convert.ToInt32(config["smtp:port"]);
            _ssl = Convert.ToBoolean(config["smtp:ssl"]);
        }

        public Task<bool> Send(string from, string to, string body, string subject = "")
        {
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
                    using (var client = new SmtpClient(_server, _port)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(_username, _password),
                        EnableSsl = _ssl
                    })
                    {
                        client.Send(message);
                    }

                    return Task.FromResult(true);
                }
                catch (Exception e)
                {
                    throw new Exception("Error sending email", e);
                }
            }
        }
    }
}
