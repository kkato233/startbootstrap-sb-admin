using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace startbootstrap_sb_admin.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
