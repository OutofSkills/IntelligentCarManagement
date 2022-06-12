using ClientUI.Utils;
using Models.Data_Transfer_Objects;

namespace ClientUI.Services.Emails
{
    public interface IEmailsService
    {
        Task<RequestResponse> SendAsync(EmailDTO dto);
    }
}
