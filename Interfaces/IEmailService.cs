using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;

namespace VSApi.Interfaces
{
    public interface IEmailService
    {
        MimeMessage PrepareMessage(string alertStr, string price, string oldPrice, string change);
        void SendMessage(MimeMessage message);
    }
}
