using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TemplateEngine.Domain.Email;
using TemplateEngine.Services.Email;

namespace TemplateEngine
{
    public class App
    {
        private readonly IEmailService _emailService;

        public App(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Run()
        {
            var recipients = new List<Recipient>
            {
                new Recipient
                {
                    Name = "John",
                    Email = "john@example.com",
                    Company = "Example Company"
                }
            };

            _emailService.SendDefaultEmail(recipients);

            // sending to another template that was added later...
            _emailService.SendCompanyEmail(recipients);

            Console.WriteLine("Hello!");
        }
    }
}
