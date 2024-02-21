using NetBox.Admin.Core.Common.Dtos;

namespace NetBox.Admin.Core.Common.Interfaces;

public interface IEmailService
{
    Task SendEmailByQueue(EmailModel email);

    Task<string> GetEmailTemplate(string emailTemplateName);
}
