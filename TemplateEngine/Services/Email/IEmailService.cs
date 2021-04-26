using System.Collections.Generic;
using TemplateEngine.Domain.Email;

namespace TemplateEngine.Services.Email
{
    public interface IEmailService
    {
        void SendDefaultEmail(List<Recipient> recipients);
        void SendCompanyEmail(List<Recipient> recipients);
    }
}
