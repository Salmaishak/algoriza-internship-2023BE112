using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Vezeeta.Services.Interfaces
{
    public interface IEmailService
    {

        public  Task<HttpStatusCode> SendEmail(string recipientEmail, string password, string doctorName);


    }


}
