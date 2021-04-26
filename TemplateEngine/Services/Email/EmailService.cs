using System;
using System.Collections.Generic;
using TemplateEngine.Domain.Consts.Email;
using TemplateEngine.Domain.Email;
using TemplateEngine.Services.Email.TemplateModels;
using TemplateEngine.Services.Template;

namespace TemplateEngine.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly ITemplateService _templateService;

        public EmailService(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public void SendDefaultEmail(List<Recipient> recipients)
        {
            var template = _templateService.GetTemplateByAlias(TemplateAlias.Default); // grabbing template from db first

            if (string.IsNullOrEmpty(template)) // if empty or null, throw exception
            {
                throw new Exception();
            }

            foreach (var recipient in recipients) // loop though recipients and build message
            {
                var msg = new TemplatedMessage
                {
                    From = "Someone Random",
                    To = recipient.Email,
                    Subject = "Default email...",
                    Template = template,
                    TemplateModel = new DefaultModel
                    {
                        Name = recipient.Name,
                        Url = "https://google.com"
                    }
                };

                var sendResult = ExecuteMessage(msg); // always true for demo

                if (!sendResult)
                {
                    // add to log with recipient info
                }
            }
        }

        //to extend, just add a new public method (like this one), along with a new templateModel and templateAlias.
        public void SendCompanyEmail(List<Recipient> recipients)
        {
            var template = _templateService.GetTemplateByAlias(TemplateAlias.Company);

            if (string.IsNullOrEmpty(template))
            {
                throw new Exception();
            }

            foreach (var recipient in recipients)
            {
                var msg = new TemplatedMessage
                {
                    From = "Daniel James",
                    To = recipient.Email,
                    Subject = "About your company...",
                    Template = template,
                    TemplateModel = new CompanyModel
                    {
                        Name = recipient.Name,
                        Company = recipient.Company
                    }
                };

                var sendResult = ExecuteMessage(msg); // always true for demo

                if (!sendResult)
                {
                    // add to log with recipient info
                }
            }
        }

        private bool ExecuteMessage(TemplatedMessage msg)
        {
            msg.Body = _templateService.TokenizeMessage(msg.Template, msg.TemplateModel);

            return SendEmail(msg);
        }

        private bool SendEmail(TemplatedMessage msg)
        {
            //convert templated msg to mime smtp using mail client such as mailkit, etc and send.

            return true;
        }
    }
}
