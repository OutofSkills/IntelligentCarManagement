using Models.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Utils
{
    public interface IEmailTemplate
    {
        string GenerateTemplate(EmailDTO email);
    }
}
