using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.Services.EmailService
{
    public interface IEmailService
    {
        Task SendMail();
    }
}
